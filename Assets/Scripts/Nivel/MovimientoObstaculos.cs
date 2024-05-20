using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoObstaculos : MonoBehaviour
{
    private float aceleracion; // aceleracion ajustable
    private float tiempoInicio;
    private Vector3 posicionInicial;
    private Vector3 posicionFinal;
    private float velocidadInicial = 1f;
    Vector3 nuevaPosicion;
    void Start()
    {
        nuevaPosicion = new Vector3(0, 0, 0);
    }
    void OnEnable()
    {
        if(tag=="finalNivel"){posicionFinal = transform.parent.Find("posFinal").position; print(posicionFinal);}
        tiempoInicio = Time.time;
        posicionInicial = transform.position;
        velocidadInicial = 1f;
    }
    void Update()
    {
        movLineal();
    }
    void movLineal()
    {
        float tiempoTranscurrido = Time.time - tiempoInicio;
        if(tag == "Obs_Asteroide")
        {
            aceleracion = MenuPrincipal.Instance.aceleracionObstaculos;
            float nuevaVelocidad = velocidadInicial + aceleracion * tiempoTranscurrido; // aceleracion
            float desplazamiento = (velocidadInicial + nuevaVelocidad) / 2 * tiempoTranscurrido; // calcula desplazamiento usando la velocidad promedio (v = (v0 + v1) / 2)
            velocidadInicial = nuevaVelocidad; // actualiza la velocidad inicial para el siguiente frame
            nuevaPosicion = posicionInicial + new Vector3(0, 0, -desplazamiento); // nueva posicion
            transform.position = nuevaPosicion; // actualiza la posicion anterior con la nueva posicion
            // rotacion aleatoria de los asteroides
            Vector3 rotacion = new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
            transform.Rotate(rotacion * Time.deltaTime, Space.Self);
        }
        if(tag == "finalNivel")
        {
            if(nuevaPosicion.z >= posicionFinal.z)
            {   //print(nuevaPosicion.z + " || " + posicionFinal.z);
                float nuevaVelocidad = velocidadInicial + tiempoTranscurrido; // aceleracion
                float desplazamiento = (velocidadInicial + nuevaVelocidad) / 2 * 0.001f; // calcula desplazamiento usando la velocidad promedio (v = (v0 + v1) / 2)
                velocidadInicial = nuevaVelocidad; // actualiza la velocidad inicial para el siguiente frame
                nuevaPosicion = posicionInicial + new Vector3(0, 0, -desplazamiento); // nueva posicion
                transform.position = nuevaPosicion; // actualiza la posicion anterior con la nueva posicion
            }
            else
            {print(" llegamo companero ");}
        }
        if(tag == "Planeta")
        {
            float nuevaVelocidad = velocidadInicial +  tiempoTranscurrido; // aceleracion
            float desplazamiento = (velocidadInicial + nuevaVelocidad) / 2 * 0.0001f; // calcula desplazamiento usando la velocidad promedio (v = (v0 + v1) / 2)
            velocidadInicial = nuevaVelocidad;
            nuevaPosicion = posicionInicial + new Vector3(0, 0, -desplazamiento); // nueva posicion
            // transform.position = nuevaPosicion;
            Vector3 rotacion = new Vector3(0, Random.Range(0, 360), 0);
            // transform.Rotate(rotacion * Time.deltaTime, Space.Self);
            transform.Rotate(Vector3.up * Time.deltaTime * 3f, Space.Self);
        }
        if(tag == "Consumible")
        {
            aceleracion = MenuPrincipal.Instance.aceleracionObstaculos;
            float nuevaVelocidad = velocidadInicial + aceleracion * tiempoTranscurrido;
            float desplazamiento = (velocidadInicial + nuevaVelocidad) / 2 * tiempoTranscurrido; 
            velocidadInicial = nuevaVelocidad; 
            nuevaPosicion = posicionInicial + new Vector3(0, 0, -desplazamiento);
            transform.position = nuevaPosicion; 
            Vector3 rotacion = new Vector3(0, Random.Range(0, 360), 0);
            transform.Rotate(rotacion * Time.deltaTime, Space.Self);
        }
    }
}
