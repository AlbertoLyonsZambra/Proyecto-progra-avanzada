using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GestorBarra : MonoBehaviour
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
    private GestorVida gestorVida;

    public int duration;
    private int remainingDuration;
    public string nextSceneName = "Juego"; // Nombre de la siguiente escena
    private int waveCount = 0; // Contador de oleadas
    public int oleadasPermitidas; // int para controlar las oleadas

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("nivelActual") <=3)
        {
            oleadasTexto.SetActive(false);
        }
        duration = 40;
        gestorVida = GestorVida.Instance;
        oleadasPermitidas = PlayerPrefs.GetInt("nivelActual");
        controlSpawn = DemoSpawnerControl.Instance;
        Begin(duration);
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
                DestroyAllEnemies(); // Destruir todos los enemigos y el spawner
                StartCoroutine(ExecuteOnEndAfterDelay2(2f));
                StartCoroutine(ExecuteOnEndAfterDelay3(5f)); // Ejecutar OnEnd después de 5 segundos
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
            if (PlayerPrefs.GetInt("nivelActual") >= 4)
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
        if (PlayerPrefs.GetInt("nivelActual") >= 4)
        {
            oleadasTexto.SetActive(true);
        }
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