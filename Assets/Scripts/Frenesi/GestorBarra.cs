using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Asegúrate de importar este namespace
using UnityEngine.UI;
using TMPro;

public class GestorBarra : MonoBehaviour
{
    [SerializeField] private Image uiFill;
    [SerializeField] private TextMeshProUGUI uiText;
    [SerializeField] private GameObject felicidadesTexto;
    [SerializeField] private GameObject prontoTexto;
    [SerializeField] private GameObject panel1; 
    [SerializeField] private GameObject spawnerGameObject; // Referencia al GameObject del spawner
    private GestorVida gestorVida;

    public int duration;
    private int remainingDuration;
    public string nextSceneName = "juego"; // Nombre de la siguiente escena

    // Start is called before the first frame update
    void Start()
    {
        duration = 40;
        gestorVida = GestorVida.Instance;
        Begin(duration);
    }

    private void Begin(int second)
    {
        remainingDuration = second;
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
            felicidadesTexto.SetActive(true);
            prontoTexto.SetActive(true);
            DestroyAllEnemies(); // Destruir todos los enemigos y el spawner
            StartCoroutine(ExecuteOnEndAfterDelay1(2f));
            StartCoroutine(ExecuteOnEndAfterDelay(5f)); // Ejecutar OnEnd después de 2 segundos
        }
    }

    private IEnumerator ExecuteOnEndAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay); // Esperar durante el retraso en tiempo real
        OnEnd();
    }

    private IEnumerator ExecuteOnEndAfterDelay1(float delay)
    {
        yield return new WaitForSecondsRealtime(delay); // Esperar durante el retraso en tiempo real
        panel1.SetActive(true);
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
            //Destroy(spawnerGameObject);
            spawnerGameObject.SetActive(false);
        }
    }
}
