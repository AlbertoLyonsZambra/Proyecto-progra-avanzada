using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GestorHabilidades : GenericSingleton<GestorHabilidades>
{
    [Header("De la terminal")]
    [SerializeField] public TextMeshProUGUI nombre;
    [SerializeField] public TextMeshProUGUI desc;
    [SerializeField] public TextMeshProUGUI precio;
    private int[] montoAComprar = new int[] { -1, -1, -1 };
    [HideInInspector] public Habilidad habilidadSeleccionada;

    [Header("De la escena")]
    [SerializeField] Button[] AguanteChoque;
    private int cuantasVecesPuedeChocar;
    void Start()
    {
        cuantasVecesPuedeChocar = PlayerPrefs.GetInt("cuantasVecesPuedeChocar", 1); // empieza de 1, lo que es poco intuitivo, pero hace como que es 0, porque inicialmente puede chocar 0 veces para seguir vivo, pero si pongo cero no funciona bn y un webeo
    }

    

    // limpia todo el panel negro de la izq de la terminal
    public void DesaparecerPanelDetalle(bool desaparecer)
    {
        Color disabledColor;
        ColorUtility.TryParseHtmlString("#000000", out disabledColor);
        if(desaparecer){disabledColor.a = 0f;}
        Image imagenDetalle = nombre.gameObject.transform.parent.gameObject.GetComponent<Image>();
        imagenDetalle.color = disabledColor;
        
        nombre.text = "";
        desc.text = "";
        precio.text = "";
    }
   
    public void Comprar()
    {   
        habilidadSeleccionada.Comprado();
        habilidadSeleccionada.comprable = false;
        for (int i = 0; i < habilidadSeleccionada.ObtenerSiguientes().Length; i++)
        {
            habilidadSeleccionada.ObtenerSiguientes()[i].comprable = habilidadSeleccionada.ObtenerSiguientes()[i].SePuedeComprar();
        }
        habilidadSeleccionada.ActualizarInteractuabilidad();
        PlayerPrefs.SetInt("cuantasVecesPuedeChocar", cuantasVecesPuedeChocar + 1);
        cuantasVecesPuedeChocar = PlayerPrefs.GetInt("cuantasVecesPuedeChocar");
    }

    public bool ChoqueAsteroide(int vecesQueHaChocado)
    {
        if (vecesQueHaChocado < cuantasVecesPuedeChocar){return false;}
        return true;
    }

}
