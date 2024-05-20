using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class MovimientoCarriles : GenericSingleton<MovimientoCarriles>
{
    [SerializeField] private float velocidad = 5f;
    private int columnaActual = 1;
    private int filaActual = 1;
    private Vector2[,] matriz;
    private float fuerzaGiro = 13f;
    public void Start()
    {
        matriz = MatrizCarriles.Instance.matrizCarriles;
    }
    void Update()
    {
       
        if (transform.gameObject.CompareTag("Player"))
        {
            if (MenuPrincipal.Instance.jugando)
            {
                MovimientoJugador("");
            }
        }
        
    }
    public void MovimientoJugador(string sentido)
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || sentido == "izq")
        {
            if (columnaActual > 0)
            {
                Gestor_audio.Instance.EjecutarAudio(Gestor_audio.Instance.audioSourceSFX, Gestor_audio.Instance.naveSFX);
                columnaActual--;
                InclinacionJugador(filaActual, columnaActual);
            }
        }

        // Cambiar de carril hacia la derecha (por ejemplo)
        if (Input.GetKeyDown(KeyCode.RightArrow) || sentido == "der")
        {
            if (columnaActual < matriz.GetLength(0) - 1)
            {
                Gestor_audio.Instance.EjecutarAudio(Gestor_audio.Instance.audioSourceSFX, Gestor_audio.Instance.naveSFX);
                columnaActual++;
                InclinacionJugador(filaActual, columnaActual);
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) || sentido == "arr")
        {
            if (filaActual > 0)
            {
                Gestor_audio.Instance.EjecutarAudio(Gestor_audio.Instance.audioSourceSFX, Gestor_audio.Instance.naveSFX);
                filaActual--;
                InclinacionJugador(filaActual, columnaActual);
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) || sentido == "aba")
        {
            if (filaActual < matriz.GetLength(0) - 1)
            {
                Gestor_audio.Instance.EjecutarAudio(Gestor_audio.Instance.audioSourceSFX, Gestor_audio.Instance.naveSFX);
                filaActual++;
                InclinacionJugador(filaActual, columnaActual);
            }
        }
        // Interpolar hacia la posicion del carril actual
        
        Vector3 posicion = new Vector3(matriz[filaActual,columnaActual].x, matriz[filaActual, columnaActual].y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, posicion, velocidad * Time.deltaTime);
    
    }
    public void InclinacionJugador(int fila, int columna)
    {
        if(tag=="Player")
        {
            transform.rotation = Quaternion.identity;
            if(fila==0 && columna==0){transform.Rotate(-fuerzaGiro, 0f, fuerzaGiro);}
            if(fila==0 && columna==1){transform.Rotate(-fuerzaGiro, 0f, 0f);}
            if(fila==0 && columna==2){transform.Rotate(-fuerzaGiro, 0f, -fuerzaGiro);}
            if(fila==1 && columna==0){transform.Rotate(0f, 0f, fuerzaGiro);}
            if(fila==1 && columna==1){transform.Rotate(0f, 0f, 0f);}
            if(fila==1 && columna==2){transform.Rotate(0f, 0f, -fuerzaGiro);}
            if(fila==2 && columna==0){transform.Rotate(fuerzaGiro, 0f, fuerzaGiro);}
            if(fila==2 && columna==1){transform.Rotate(fuerzaGiro, 0f, 0f);}
            if(fila==2 && columna==2){transform.Rotate(fuerzaGiro, 0f, -fuerzaGiro);}
        }
    }
}

