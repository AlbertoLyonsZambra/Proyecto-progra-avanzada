using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GestorBarra : GenericSingleton<GestorBarra>
{
    [SerializeField] private Image uiFill;
    [SerializeField] private TextMeshProUGUI uiText;
    [SerializeField] private GameObject felicidadesTexto;
    [SerializeField] private GameObject oleadaSuperada;
    [SerializeField] private GameObject prontoTexto;
    [SerializeField] private GameObject oleadasTexto; // Referencia al texto que muestra las oleadas
    [SerializeField] private GameObject spawnerGameObject;
    [SerializeField] private GameObject panel1;
    [SerializeField] private DemoSpawnerControl controlSpawn;// Referencia al GameObject del spawner
    [SerializeField] private GameObject[] cofres;
    [SerializeField] private GameObject cofreMapa;
    [SerializeField] private GameObject[] matRecogibles;

    private GestorVida gestorVida;
    private GestorJuego gestorJuego;

    public bool victoria;
    
    public int duration;
    private int remainingDuration;
    public string nextSceneName = "Juego"; // Nombre de la siguiente escena
    private int waveCount; // Contador de oleadas
    public int oleadasPermitidas; // int para controlar las oleadas

    // Start is called before the first frame update
    void Start()
    {
        duration = 40;
        victoria = false;
        
        gestorVida = GestorVida.Instance;
        oleadasPermitidas = PlayerPrefs.GetInt("nivelActual");
        controlSpawn = DemoSpawnerControl.Instance;
        gestorJuego = GestorJuego.Instance;
        waveCount = 1;
        Begin(duration);
    }

    void Update()
    {
        if (PlayerPrefs.GetInt("nivelActual") == 4)
        {
            matRecogibles[0].SetActive(true);
            matRecogibles[1].SetActive(true);
            matRecogibles[2].SetActive(true);
            oleadasTexto.SetActive(true);
            PlayerPrefs.SetInt("jugandoFrenesi", 1);
        }
        else
        {
            matRecogibles[0].SetActive(false);
            matRecogibles[1].SetActive(false);
            matRecogibles[2].SetActive(false);
            oleadasTexto.SetActive(false);
            PlayerPrefs.SetInt("jugandoFrenesi", 0);
        }
        if (gestorVida.vida == 0)
        {
            matRecogibles[0].SetActive(false);
            matRecogibles[1].SetActive(false);
            matRecogibles[2].SetActive(false);
        }
        if (gestorJuego.oleadas)
        {
            if (waveCount <= 5)
            {
                cofreMapa = cofres[0];
                cofreMapa.SetActive(true);
            }
            else if (waveCount <= 10 && waveCount > 5)
            {
                cofreMapa.SetActive(false);
                cofreMapa = cofres[1];
                cofreMapa.SetActive(true);
            }
            else if (waveCount <= 15 && waveCount > 10)
            {
                cofreMapa.SetActive(false);
                cofreMapa = cofres[2];
                cofreMapa.SetActive(true);
            }
            else if (waveCount <= 20 && waveCount > 15)
            {
                cofreMapa.SetActive(false);
                cofreMapa = cofres[3];
                cofreMapa.SetActive(true);
            }
            else if (waveCount <= 25 && waveCount > 20)
            {
                cofreMapa.SetActive(false);
                cofreMapa = cofres[4];
                cofreMapa.SetActive(true);
            }
            else if (waveCount > 25)
            {
                cofreMapa.SetActive(false);
                cofreMapa = cofres[5];
                cofreMapa.SetActive(true);
            }
        }
    }

    private void Begin(int second)
    {
        remainingDuration = second;
        spawnerGameObject.SetActive(true);
        controlSpawn.enemyCount = 0;
        StartCoroutine(UpdateTimer());
    }

    private IEnumerator UpdateTimer()
    {
        while (remainingDuration >= 0)
        {
            uiText.text = $"{remainingDuration / 60:00} : {remainingDuration % 60:00}";
            uiFill.fillAmount = Mathf.InverseLerp(0, duration, remainingDuration);
            remainingDuration--;
            yield return new WaitForSeconds(1f);
        }

        if (!gestorVida.hasMuerto && gestorVida.vida > 0)
        {
            if (oleadasPermitidas == 4)
            {
                waveCount++;
          
                
                felicidadesTexto.SetActive(true);
                oleadaSuperada.SetActive(true);
                DestroyAllEnemies(); // Destruir todos los enemigos y el spawner
                StartCoroutine(ExecuteOnEndAfterDelay1(2f));
                StartCoroutine(ExecuteOnEndAfterDelay(5f)); // Ejecutar OnEnd después de 5 segundos
            }
            else
            {
                felicidadesTexto.SetActive(true);
                prontoTexto.SetActive(true);
                victoria = true;
                PlayerPrefs.SetInt("Victoria", victoria ? 1 : 0);
                DestroyAllEnemies(); // Destruir todos los enemigos y el spawner
                StartCoroutine(ExecuteOnEndAfterDelay2(2f));
                StartCoroutine(ExecuteOnEndAfterDelay3(5f)); // Ejecutar OnEnd después de 5 segundos
                if (oleadasPermitidas < 4)
                {
                    PlayerPrefs.SetInt("nivelActual", oleadasPermitidas + 1);
                }
            }
        }
    }

    private IEnumerator ExecuteOnEndAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay); // Esperar durante el retraso en tiempo real

        if (oleadasPermitidas == 4)
        {
            // Reiniciar el temporizador y comenzar una nueva oleada
            felicidadesTexto.SetActive(false);
            oleadaSuperada.SetActive(false);
            if (PlayerPrefs.GetInt("nivelActual") == 4)
            {
                oleadasTexto.GetComponentInChildren<TextMeshProUGUI>().text = $"Oleada: {waveCount}";
            }
            Begin(duration);
        }
        else
        {
            OnEnd();
        }
    }

    private IEnumerator ExecuteOnEndAfterDelay2(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        panel1.SetActive(true);
    }

    private IEnumerator ExecuteOnEndAfterDelay3(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        OnEnd();
    }

    private IEnumerator ExecuteOnEndAfterDelay1(float delay)
    {
        yield return new WaitForSecondsRealtime(delay); // Esperar durante el retraso en tiempo real
    }

    private void OnEnd()
    {
        print("End");
        Time.timeScale = 0; // Pausar el juego
        StartCoroutine(LoadNextSceneAfterDelay(3f)); // Cargar la nueva escena después de un retraso de 3 segundos
    }

    private IEnumerator LoadNextSceneAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay); // Esperar durante el retraso en tiempo real
        Time.timeScale = 1; // Reanudar el tiempo antes de cambiar de escena
        SceneManager.LoadScene(nextSceneName);
    }

    private void DestroyAllEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
        if (spawnerGameObject != null)
        {
            spawnerGameObject.SetActive(false); // Desactivar el spawner
        }
    }
}