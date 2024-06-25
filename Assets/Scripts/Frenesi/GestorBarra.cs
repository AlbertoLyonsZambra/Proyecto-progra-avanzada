using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
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

    public int duration;
    private int remainingDuration;
    public string nextSceneName = "juego"; // Nombre de la siguiente escena

    // Start is called before the first frame update
    void Start()
    {
        duration = 10;
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
        felicidadesTexto.SetActive(true);
        prontoTexto.SetActive(true);
        OnEnd();
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
}

