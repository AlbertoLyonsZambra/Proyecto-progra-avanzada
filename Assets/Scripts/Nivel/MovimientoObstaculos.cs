using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoObstaculos : MonoBehaviour
{
    [SerializeField] private float aceleracion; // aceleracion ajustable
    private float tiempoInicio;
    private Vector3 posicionInicial;
    private float velocidadInicial = 1f;
    void Start()
    {
        
    }
    void OnEnable()
    {
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
        aceleracion = MenuPrincipal.Instance.aceleracionObstaculos;
        float tiempoTranscurrido = Time.time - tiempoInicio; 
        float nuevaVelocidad = velocidadInicial + aceleracion * tiempoTranscurrido; // aceleracion
        float desplazamiento = (velocidadInicial + nuevaVelocidad) / 2 * tiempoTranscurrido; // calcula desplazamiento usando la velocidad promedio (v = (v0 + v1) / 2)
        velocidadInicial = nuevaVelocidad; // actualiza la velocidad inicial para el siguiente frame
        Vector3 nuevaPosicion = posicionInicial + new Vector3(0, 0, -desplazamiento); // nueva posicion
        transform.position = nuevaPosicion; // actualiza la posicion anterior con la nueva posicion
        if(tag =="Obs_Asteroide")
        {
            // rotacion aleatoria de los asteroides
            Vector3 rotacion = new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
            transform.Rotate(rotacion * Time.deltaTime, Space.Self);
        }
    }
}
