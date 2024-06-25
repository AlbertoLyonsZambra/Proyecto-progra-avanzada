using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class ProgresoNivel : GenericSingleton<ProgresoNivel> {

    [Header("UI references:")]
    [SerializeField] private Image uiFillImage;
    [SerializeField] private GameObject porcentajeTexto;
    [SerializeField] private GameObject normal;
    [SerializeField] private TextMeshProUGUI puntuacion;
    [SerializeField] private GameObject infinito;
    [HideInInspector] public TextMeshProUGUI porcentaje;
    private float total;
    private float tiempoInicial;
    

    private void Start()
    {
        if (MenuPrincipal.Instance.nivelActual <= 3)
        {
            porcentaje = porcentajeTexto.GetComponent<TextMeshProUGUI>();
            tiempoInicial = Time.time;
            total = MenuPrincipal.Instance.duracionNivel + 160;
        }
        else
        {
            normal.SetActive(false);
            infinito.SetActive(true);
            StartCoroutine(AumentarPuntuacionInfinito(0.7f));
        }
    }

    private void Update()
    {
        if (MenuPrincipal.Instance.nivelActual <= 3)
        {
            float tiempoTranscurrido = Time.time - tiempoInicial;

            if (tiempoTranscurrido <= total)
            {
                // Calculate the fill amount (between 0 and 1)
                uiFillImage.fillAmount = tiempoTranscurrido / total;

                // Calculate the percentage (between 0 and 100)
                float porcentajeTranscurrido = (tiempoTranscurrido / total) * 100;

                // Update the text with rounded percentage
                porcentaje.text = Mathf.RoundToInt(porcentajeTranscurrido).ToString() + "%";
            }
        }
    }
    IEnumerator AumentarPuntuacionInfinito(float tiempo)
    {
        if (!MuerteJugador.Instance.estaMuerto)
        {
            yield return new WaitForSeconds(tiempo);
            puntuacion.text = (int.Parse(puntuacion.text) + 1).ToString();
            StartCoroutine(AumentarPuntuacionInfinito(tiempo));
        }
        else
        {
            if (PlayerPrefs.GetInt("puntuacionInfinito") < int.Parse(puntuacion.text))
            {
                PlayerPrefs.SetInt("puntuacionInfinito", int.Parse(puntuacion.text));
            }
        }
    }
}
