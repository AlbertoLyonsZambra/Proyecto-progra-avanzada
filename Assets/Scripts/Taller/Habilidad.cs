using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Habilidad : MonoBehaviour
{
    
    [Header("Habilidad:")]
    [SerializeField] private string nombre;
    [SerializeField] private string desc;
    [SerializeField] private int[] precio;
    public bool comprado = false;
    public bool comprable = false;

    [Header("Enlaces:")]
    [SerializeField] private GameObject[] enlaceSiguienteGO;
    [SerializeField] private GameObject[] enlaceAnteriorGO;
    [HideInInspector] public Enlaces[] enlaceSiguiente;
    [HideInInspector] public Enlaces[] enlaceAnterior;
    [HideInInspector] public Habilidad[] habilidadSiguiente;
    [HideInInspector] public Habilidad[] habilidadAnterior;
    void Start()
    {
        IniciliazacionListas();
        if(tag == "primeraHabilidad"){comprable = true;}
        ActualizarInteractuabilidad();
    }
    
    private void SeleccionarHabilidad()
    {
        Color colorPanelDetalle;
        ColorUtility.TryParseHtmlString("#000000", out colorPanelDetalle);
        colorPanelDetalle.a = 0.8f;
        GestorHabilidades.Instance.DesaparecerPanelDetalle(false);
        GestorHabilidades.Instance.nombre.text = nombre;
        GestorHabilidades.Instance.desc.text = desc;
        Image panelDetalle = GestorHabilidades.Instance.nombre.gameObject.transform.parent.gameObject.GetComponent<Image>();
        panelDetalle.color = colorPanelDetalle;
        if(precio.Length == 1){GestorHabilidades.Instance.precio.text = precio[0].ToString() + " V" ;}
        else if(precio.Length == 2){GestorHabilidades.Instance.precio.text = precio[0].ToString() + " V,  " + precio[1].ToString() + " N";}
        else if(precio.Length == 3){GestorHabilidades.Instance.precio.text = precio[0].ToString() + " V,  " + precio[1].ToString() + " N,  " + precio[2].ToString() + " R";}
        else {print("precio ingresado a la habilidad " + nombre + "  no valido");}
        GestorHabilidades.Instance.habilidadSeleccionada = this;
    }

    public void ActualizarInteractuabilidad()
    {
        Button boton = gameObject.GetComponent<Button>();
        if(!comprable && !comprado)
        {
            Interactuable(false);
        }
        if(comprable)
        {
            Interactuable(true);
        }

        if(enlaceSiguiente != null)
        {
            for (int i = 0; i < enlaceSiguiente.Length; i++)
            {
                enlaceSiguiente[i].habilidadSalidaBTN.gameObject.GetComponent<Habilidad>().ActualizarInteractuabilidad();
                ActualizarEnlaces();
            }
        }
    }
    public void Interactuable(bool interactuable)
    {
        Button boton = gameObject.GetComponent<Button>();
        ColorBlock colorBlock = boton.colors;
        if (!interactuable)
        {
            colorBlock.normalColor = Color.gray; 
            colorBlock.highlightedColor = Color.black;

            
            Color highlightedColor = colorBlock.highlightedColor;
            highlightedColor.a = 0.2f; 
            colorBlock.highlightedColor = highlightedColor;
            colorBlock.highlightedColor = Color.black;

            colorBlock.disabledColor = Color.gray;
            boton.colors = colorBlock;
        }
        else
        {
            colorBlock.normalColor = Color.white; 
            colorBlock.highlightedColor = Color.white; 
            colorBlock.disabledColor = Color.white;
            boton.colors = colorBlock;
        }
    }
    public void Comprado()
    {
        transform.Find("ticket").gameObject.SetActive(true);
        comprado = true;
    }
    public bool SePuedeComprar()
    {
        for (int i = 0; i < enlaceAnterior.Length; i++)
        {
            if(!habilidadAnterior[i].comprado){return false;}
        }
        return true;
    }

    public Habilidad[] ObtenerSiguientes()
    {
        Habilidad[] habilidadSiguiente = new Habilidad[enlaceSiguiente.Length];
        for (int i = 0; i < enlaceSiguiente.Length; i++)
        {
            habilidadSiguiente[i] = enlaceSiguiente[i].habilidadSalidaBTN.gameObject.GetComponent<Habilidad>();
        }
        return habilidadSiguiente;
    }

    public Habilidad[] ObtenerAnteriores()
    {
        Habilidad[] habilidadAnterior = new Habilidad[enlaceAnterior.Length];
        for (int i = 0; i < enlaceAnterior.Length; i++)
        {
            habilidadAnterior[i] = enlaceAnterior[i].habilidadEntradaBTN.gameObject.GetComponent<Habilidad>();
        }
        return habilidadAnterior;
    }
    private void ActualizarEnlaces()
    {

        for (int i = 0; i < enlaceSiguiente.Length; i++)
        {
            Habilidad habilidadEntrada = enlaceSiguiente[i].habilidadEntradaBTN.gameObject.GetComponent<Habilidad>();
            Habilidad habilidadSalida = enlaceSiguiente[i].habilidadSalidaBTN.gameObject.GetComponent<Habilidad>();
            
            if(habilidadEntrada.comprable && !habilidadSalida.comprado)
            {
                enlaceSiguienteGO[i].transform.Find("relleno").GetComponent<Image>().fillAmount = 0.5f;
            }
            else if(!habilidadEntrada.comprado && !habilidadSalida.comprado)
            {
                enlaceSiguienteGO[i].transform.Find("relleno").GetComponent<Image>().fillAmount = 0f;
            }
            else if(habilidadEntrada.comprado && habilidadSalida.comprado)
            {
                enlaceSiguienteGO[i].transform.Find("relleno").GetComponent<Image>().fillAmount = 1f;
            }
            else if(habilidadEntrada.comprado && habilidadSalida.comprable)
            {
                enlaceSiguienteGO[i].transform.Find("relleno").GetComponent<Image>().fillAmount = 1f;
                gameObject.GetComponent<Button>();
            }
        }
    }

    private void IniciliazacionListas()
    {
        
        enlaceSiguiente = new Enlaces[enlaceSiguienteGO.Length];
        for (int i = 0; i < enlaceSiguienteGO.Length; i++){enlaceSiguiente[i] = enlaceSiguienteGO[i].gameObject.GetComponent<Enlaces>();}
        enlaceAnterior = new Enlaces[enlaceAnteriorGO.Length];
        for (int i = 0; i < enlaceAnteriorGO.Length; i++){enlaceAnterior[i] = enlaceAnteriorGO[i].gameObject.GetComponent<Enlaces>();}
        habilidadSiguiente = new Habilidad[enlaceSiguiente.Length];
        habilidadAnterior = new Habilidad[enlaceAnterior.Length];
        habilidadSiguiente = ObtenerSiguientes();
        habilidadAnterior = ObtenerAnteriores();
    }

}
