using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MovimientoCarriles : GenericSingleton<MovimientoCarriles>
{
    [SerializeField] private float velocidad = 5f;
    private int columnaActual = 1;
    private int filaActual = 1;
    private Vector2[,] matriz;
    private float fuerzaGiro = 13f;
    protected override void Initialization()
    {
        matriz = MatrizCarriles.Instance.getMatriz();
    }
    void Update()
    {
       
        if (transform.gameObject.CompareTag("Player"))
        {
            MovimientoJugador("");
        }
        
    }
    public void MovimientoJugador(string sentido)
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || sentido == "izq")
        {
            if (columnaActual > 0)
            {
                transform.rotation = Quaternion.identity;
                transform.Rotate(0f, 0f, fuerzaGiro);
                columnaActual--;
            }
        }

        // Cambiar de carril hacia la derecha (por ejemplo)
        if (Input.GetKeyDown(KeyCode.RightArrow) || sentido == "der")
        {
            if (columnaActual < matriz.GetLength(0) - 1)
            {
                transform.rotation = Quaternion.identity;
                transform.Rotate(0f, 0f, -fuerzaGiro);
                columnaActual++;
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) || sentido == "arr")
        {
            if (filaActual > 0)
            {
                transform.rotation = Quaternion.identity;
                transform.Rotate(-fuerzaGiro, 0f, 0f);
                filaActual--;
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) || sentido == "aba")
        {
            if (filaActual < matriz.GetLength(0) - 1)
            {
                transform.rotation = Quaternion.identity;
                transform.Rotate(fuerzaGiro, 0f, 0f);
                filaActual++;
            }
        }
        // Interpolar hacia la posicion del carril actual
        
        Vector3 posicion = new Vector3(matriz[filaActual,columnaActual].x, matriz[filaActual, columnaActual].y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, posicion, velocidad * Time.deltaTime);
    
    }
}

