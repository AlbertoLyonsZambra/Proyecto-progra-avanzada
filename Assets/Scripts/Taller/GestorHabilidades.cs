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
    public int cuantasVecesPuedeChocar;
    public int bateriaLvl;
    public int cadenciaLvl;
    public int laserLvl;
    public bool tieneArmatoste;
    void Start()
    {
        cuantasVecesPuedeChocar = PlayerPrefs.GetInt("cuantasVecesPuedeChocar", 1); // empieza de 1, lo que es poco intuitivo, pero hace como que es 0, porque inicialmente puede chocar 0 veces para seguir vivo, pero si pongo cero no funciona bn y un webeo
        bateriaLvl = PlayerPrefs.GetInt("bateriaLvl", 1);
        cadenciaLvl = PlayerPrefs.GetInt("cadenciaLvl", 1);
        laserLvl = PlayerPrefs.GetInt("laserLvl", 1); 
        tieneArmatoste = PlayerPrefs.GetInt("tieneArmatoste", 0) == 1; 
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
        if(habilidadSeleccionada == null){return;}
        if(!habilidadSeleccionada.comprable){return;}
        ActualizarNivelesHabilidades();
        habilidadSeleccionada.Comprado();
        habilidadSeleccionada.comprable = false;
        for (int i = 0; i < habilidadSeleccionada.ObtenerSiguientes().Length; i++)
        {
            if(!habilidadSeleccionada.ObtenerSiguientes()[i].comprado){habilidadSeleccionada.ObtenerSiguientes()[i].comprable = habilidadSeleccionada.ObtenerSiguientes()[i].SePuedeComprar();}
            
        }
        habilidadSeleccionada.ActualizarInteractuabilidad();
      
    }

    private void ActualizarNivelesHabilidades()
    {
        string nombreH = habilidadSeleccionada.nombre.ToLower();
        if(nombreH.Contains("cadencia"))
        {
            PlayerPrefs.SetInt("cadenciaLvl", cadenciaLvl + 1);
            cadenciaLvl = PlayerPrefs.GetInt("cadenciaLvl");
        }
        else if(nombreH.Contains("potencia"))
        {
            PlayerPrefs.SetInt("bateriaLvl", bateriaLvl + 1);
            bateriaLvl = PlayerPrefs.GetInt("bateriaLvl");
        }
        else if(nombreH.Contains("arma"))
        {
            PlayerPrefs.SetInt("laserLvl", laserLvl + 1);
            laserLvl = PlayerPrefs.GetInt("laserLvl");

            if(nombreH.Contains("armatoste"))
            {
                PlayerPrefs.SetInt("tieneArmatoste", 1);
                tieneArmatoste = true;
            }
        }
        else if(nombreH.Contains("coraza"))
        {
            PlayerPrefs.SetInt("cuantasVecesPuedeChocar", cuantasVecesPuedeChocar + 1);
            cuantasVecesPuedeChocar = PlayerPrefs.GetInt("cuantasVecesPuedeChocar");
        }
    }

    public bool ChoqueAsteroide(int vecesQueHaChocado)
    {
        if (vecesQueHaChocado < cuantasVecesPuedeChocar){return false;}
        return true;
    }

}
