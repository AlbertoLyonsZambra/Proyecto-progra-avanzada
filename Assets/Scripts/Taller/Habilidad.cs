using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidad : MonoBehaviour
{
    [SerializeField] private string nombre;
    [SerializeField] private string desc;
    [SerializeField] private int[] precio;
    void Start()
    {
        
    }

    // Update is called once per frame
    public void SeleccionarHabilidad()
    {
        GestorHabilidades.Instance.DesaparecerPanelDetalle(false);
        GestorHabilidades.Instance.nombre.text = nombre;
        GestorHabilidades.Instance.desc.text = desc;
        if(precio.Length == 1){GestorHabilidades.Instance.precio.text = precio[0].ToString() + " V" ;}
        else if(precio.Length == 2){GestorHabilidades.Instance.precio.text = precio[0].ToString() + " V,  " + precio[1].ToString() + " N";}
        else if(precio.Length == 3){GestorHabilidades.Instance.precio.text = precio[0].ToString() + " V,  " + precio[1].ToString() + " N,  " + precio[2].ToString() + " R";}
        else {print("precio ingresado a la habilidad " + nombre + "  no valido");}
    }
}
