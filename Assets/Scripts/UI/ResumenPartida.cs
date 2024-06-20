using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResumenPartida : MonoBehaviour
{
    [SerializeField] private GameObject hasConseguido;
    private TextMeshProUGUI hasConseguidoTEXTO;
    [SerializeField] private GameObject materialVerdeObtenido;
    private TextMeshProUGUI materialVerdeObtenidoTEXTO;
    [SerializeField] private GameObject materialNaranjaObtenido;
    private TextMeshProUGUI materialNaranjaObtenidoTEXTO;
    [SerializeField] private GameObject materialRosaObtenido;
    private TextMeshProUGUI materialRosaObtenidoTEXTO;
    [SerializeField] private GameObject porcentajeLogrado;
    private TextMeshProUGUI porcentajeLogradoTEXTO;
    void OnEnable()
    {
        if(PlayerPrefs.GetInt("MatsV") > 0) 
        {
            materialVerdeObtenido.transform.parent.gameObject.SetActive(true);
            materialVerdeObtenidoTEXTO = materialVerdeObtenido.GetComponent<TextMeshProUGUI>();
            materialVerdeObtenidoTEXTO.text =  "+" + PlayerPrefs.GetInt("MatsV").ToString();
        }

        if(PlayerPrefs.GetInt("MatsN") > 0)
        {
            materialNaranjaObtenido.transform.parent.gameObject.SetActive(true);
            materialNaranjaObtenidoTEXTO = materialNaranjaObtenido.GetComponent<TextMeshProUGUI>();
            materialNaranjaObtenidoTEXTO.text =  "+" + PlayerPrefs.GetInt("MatsN").ToString();
        }

        if(PlayerPrefs.GetInt("MatsR") > 0)
        {
            materialRosaObtenido.transform.parent.gameObject.SetActive(true);
            materialRosaObtenidoTEXTO = materialRosaObtenido.GetComponent<TextMeshProUGUI>();
            materialRosaObtenidoTEXTO.text =  "+" + PlayerPrefs.GetInt("MatsR").ToString();
        }

        if(MenuPrincipal.Instance.nivelActual < 4)
        {
            porcentajeLogrado.SetActive(true);
            porcentajeLogradoTEXTO = porcentajeLogrado.GetComponent<TextMeshProUGUI>();
            porcentajeLogradoTEXTO.text = "Progresado: " + ProgresoNivel.Instance.porcentaje.text;
        }

        hasConseguidoTEXTO = hasConseguido.GetComponent<TextMeshProUGUI>();
        if(PlayerPrefs.GetInt("MatsV") > 0 || PlayerPrefs.GetInt("MatsN") > 0 || PlayerPrefs.GetInt("MatsR") > 0)
        {
            hasConseguidoTEXTO.text = "Has conseguido: ";
        }
        else
        {
            hasConseguidoTEXTO.text = "No conseguiste nada :( ";
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
