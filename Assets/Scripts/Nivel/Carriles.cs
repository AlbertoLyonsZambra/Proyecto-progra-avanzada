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

    private void Start()
    {
        matriz = new Vector3[filas.Length][];
        print(matriz[2][2]);
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
                columnaActual--;
            }
        }

        // Cambiar de carril hacia la derecha (por ejemplo)
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (columnaActual < columnas.Length - 1)
            {
                columnaActual++;
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (filaActual > 0)
            {
                filaActual--;
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (filaActual < columnas.Length - 1)
            {
                filaActual++;
            }
        }
        // Interpolar hacia la posición del carril actual
        Vector3 posicion = new Vector3(columnas[columnaActual].position.x, filas[filaActual].position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, posicion, velocidad * Time.deltaTime);
    }
}
