using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProgresoNivel : GenericSingleton<ProgresoNivel> {

    [Header("UI references:")]
    [SerializeField] private Image uiFillImage;
    [SerializeField] private GameObject porcentajeTexto;
    [HideInInspector] public TextMeshProUGUI porcentaje;
    private float total;
    private float tiempoInicial;
    

    private void Start()
    {
        // Use TextMeshProUGUI for UI text components
        porcentaje = porcentajeTexto.GetComponent<TextMeshProUGUI>();
        tiempoInicial = Time.time;
        // Calculate total time
        total = MenuPrincipal.Instance.duracionNivel + 232;
    }

    private void Update()
    {
        float tiempoTranscurrido = Time.time - tiempoInicial;

        if (tiempoTranscurrido <= total) {
            // Calculate the fill amount (between 0 and 1)
            uiFillImage.fillAmount = tiempoTranscurrido / total;
            
            // Calculate the percentage (between 0 and 100)
            float porcentajeTranscurrido = (tiempoTranscurrido / total) * 100;
            
            // Update the text with rounded percentage
            porcentaje.text = Mathf.RoundToInt(porcentajeTranscurrido).ToString() + "%";
        }
    }
}
