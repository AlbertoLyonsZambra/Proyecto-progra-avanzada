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
    private bool laserPuedeMoverse;
    private float velocidadLaser = 10.5f;
    private float posicionDesacelerar;
    void Start()
    {
        nuevaPosicion = new Vector3(0, 0, 0);
    }
    void OnEnable()
    {
        if(tag=="finalNivel")
        {
            posicionFinal = transform.parent.Find("posFinal").position;
            posicionDesacelerar = transform.parent.Find("posDesaceleracion").position.z;
            print(posicionFinal);
        }
        tiempoInicio = Time.time;
        posicionInicial = transform.position;
        velocidadInicial = 1f;
        laserPuedeMoverse = true;
    }
    void OnDisable()    
    {
        laserPuedeMoverse = false;
        if(tag == "Laser")
        {
            this.transform.position = new Vector3(0,0,0);
        }
    }
    void Update()
    {
        movLineal();
    }
    void movLineal()
    {
        float tiempoTranscurrido = Time.time - tiempoInicio;
        if(tag == "Obs_Asteroide" || tag == "MatNormal" || tag == "MatRaro" || tag == "MatSuper" || tag == "MatTutorial" )
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
        if (tag == "FlechaTutorial")
        {
            aceleracion = MenuPrincipal.Instance.aceleracionObstaculos;
            float nuevaVelocidad = velocidadInicial + aceleracion * tiempoTranscurrido; // aceleracion
            float desplazamiento = (velocidadInicial + nuevaVelocidad) / 2 * tiempoTranscurrido; // calcula desplazamiento usando la velocidad promedio (v = (v0 + v1) / 2)
            velocidadInicial = nuevaVelocidad; // actualiza la velocidad inicial para el siguiente frame
            nuevaPosicion = posicionInicial + new Vector3(0, 0, -desplazamiento); // nueva posicion
            transform.position = nuevaPosicion; // actualiza la posicion anterior con la nueva posicion
        }
        if (tag == "finalNivel")
        {
            if(nuevaPosicion.z >= posicionFinal.z)
            {
                // Se demora 1 minuto desde que aparece hasta que llega al jugador
                Vector3 velocidadPlataforma;
                if (nuevaPosicion.z >= posicionDesacelerar)
                {
                    velocidadPlataforma = Vector3.back * velocidadInicial*12 * Time.deltaTime;
                }
                else
                {
                    velocidadPlataforma = Vector3.back * velocidadInicial*6 * Time.deltaTime;
                }
                transform.Translate(velocidadPlataforma);
                nuevaPosicion = transform.position;
                /*
                //print(nuevaPosicion.z + " || " + posicionFinal.z);
                float nuevaVelocidad = velocidadInicial + tiempoTranscurrido; // aceleracion
                float desplazamiento = (velocidadInicial + nuevaVelocidad) / 2 * 0.001f; // calcula desplazamiento usando la velocidad promedio (v = (v0 + v1) / 2)
                velocidadInicial = nuevaVelocidad; // actualiza la velocidad inicial para el siguiente frame
                nuevaPosicion = posicionInicial + new Vector3(0, 0, -desplazamiento); // nueva posicion
                transform.position = nuevaPosicion; // actualiza la posicion anterior con la nueva posicion
                */
            }
            else
            {
                FinalNivel.Instance.Victoria();
            }
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
        if(tag == "Laser")
        {
            if(laserPuedeMoverse)
            {
                Vector3 movimiento = new Vector3(0,0,velocidadLaser) * Time.deltaTime;
                transform.Translate(movimiento);
            }
            
        }
    }
}