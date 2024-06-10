using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : GenericSingleton<Tutorial>
{
    private Transform iconoTouch;
    [SerializeField] private float velocidad;
    [SerializeField] private RectTransform horizontal;
    [SerializeField] private RectTransform vertical;
    private Vector3 posicionZero = Vector3.zero;
    private Vector3 posicionVertical; 
    private Vector3 posicionHorizontal;
    private string sentidoHorizontal;
    private string sentidoVertical;
    private bool centrar;
    private void Awake()
    {
        iconoTouch = gameObject.transform.Find("Icono touch");
        posicionVertical = vertical.transform.localPosition;
        posicionHorizontal = horizontal.transform.localPosition;
    }
    private void Update()
    {
        MoverHorizontal(sentidoHorizontal);
        MoverVertical(sentidoVertical);
        Centrar();
    }
    public void cambiarHorizontal(string sentido)
    {
        sentidoHorizontal = sentido;
    }
    public void cambiarVertical(string sentido)
    {
        sentidoVertical = sentido;
    }
    void MoverHorizontal(string sentido)
    {
        if (sentido == "derecha")
        {
            centrar = false;
            iconoTouch.localPosition = Vector3.Lerp(iconoTouch.localPosition, posicionHorizontal*-1, velocidad * Time.deltaTime);
            return;
        }
        else if (sentido == "izquierda")
        {
            centrar = false;
            iconoTouch.localPosition = Vector3.Lerp(iconoTouch.localPosition, posicionHorizontal, velocidad * Time.deltaTime);
        }
    }
    void MoverVertical(string sentido)
    {
        if (sentido == "abajo")
        {
            centrar = false;
            iconoTouch.localPosition = Vector3.Lerp(iconoTouch.localPosition, posicionVertical*-1, velocidad * Time.deltaTime);
            return;
        }
        else if (sentido == "arriba")
        {
            centrar = false;
            iconoTouch.localPosition = Vector3.Lerp(iconoTouch.localPosition, posicionVertical, velocidad * Time.deltaTime);
        }
        
    }
    public void cambiarCentrar(bool siono)
    {
        centrar = siono;
    }
    void Centrar()
    {
        if (centrar)
        {
            iconoTouch.localPosition = Vector3.Lerp(iconoTouch.localPosition, posicionZero, velocidad * Time.deltaTime);
        }
    }
    public void PararTodo()
    {

    }
    public void IniciarJuego()
    {

    }
}
