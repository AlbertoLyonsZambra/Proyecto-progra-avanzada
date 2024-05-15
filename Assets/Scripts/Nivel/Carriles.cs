using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Carriles : MonoBehaviour
{
    public Transform[] columnas;
    public Transform[] filas;
    public float velocidad = 5f;
    private int columnaActual = 1;
    private int filaActual = 1;
    public Vector3[][] matriz;
    private float fuerzaGiro = 13f;

    private void Start()
    {
        matriz = new Vector3[filas.Length][];
    }
    void Update()
    {
        if (transform.gameObject.CompareTag("Player"))
        {
            MovimientoJugador();
        }
        
    }
    void MovimientoJugador()
    {
        
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (columnaActual > 0)
            {
                transform.Rotate(0f, 0f, fuerzaGiro);
                columnaActual--;
            }
        }

        // Cambiar de carril hacia la derecha (por ejemplo)
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (columnaActual < columnas.Length - 1)
            {
                transform.Rotate(0f, 0f, -fuerzaGiro);
                columnaActual++;
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (filaActual > 0)
            {
                transform.Rotate(-fuerzaGiro, 0f, 0f);
                filaActual--;
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (filaActual < columnas.Length - 1)
            {
                transform.Rotate(fuerzaGiro, 0f, 0f);
                filaActual++;
            }
        }
        // Interpolar hacia la posici�n del carril actual
        Vector3 posicion = new Vector3(columnas[columnaActual].position.x, filas[filaActual].position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, posicion, velocidad * Time.deltaTime);
    
    }
}

