using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class MovimientoCarriles : GenericSingleton<MovimientoCarriles>
{
    [SerializeField] private float velocidad = 5f;
    [HideInInspector]public int columnaActual = 1;
    [HideInInspector] public int filaActual = 1;
    private Vector2[,] matriz;
    private float fuerzaGiro = 13f;
    private bool mover = true;
    private JugadorNivel jugador;
    public void Start()
    {
        matriz = MatrizCarriles.Instance.matrizCarriles;
        jugador = JugadorNivel.Instance;
        if(gameObject.transform.parent != null)
        {
            // if(gameObject.transform.parent.name == "0"){velocidad = 5f;}
            // else if(gameObject.transform.parent.name == "1"){velocidad = 9f;}
            // else if(gameObject.transform.parent.name == "2"){velocidad = 6f;}
            // else if(gameObject.transform.parent.name == "3"){velocidad = 6f;}
            // else if(gameObject.transform.parent.name == "4"){velocidad = 8f;}
        }
    }
    void Update()
    {
       
        if (transform.gameObject.CompareTag("Player"))
        {
            if (MenuPrincipal.Instance.jugando)
            {
                if(jugador.bateria == 0){ mover = false; }
                else{ mover = true;}
                MovimientoJugador("");
            }
        }
        
    }
    public void MovimientoJugador(string sentido)
    {
        if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) || sentido == "izq" ) && mover == true)
        {
            if (columnaActual > 0)
            {
                Gestor_audio.Instance.EjecutarAudio(Gestor_audio.Instance.audioSourceSFX, Gestor_audio.Instance.naveSFX);
                columnaActual--;
                InclinacionJugador(filaActual, columnaActual);
            }
        }

        // Cambiar de carril hacia la derecha (por ejemplo)
        if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) || sentido == "der") && mover == true)
        {
            if (columnaActual < matriz.GetLength(0) - 1)
            {
                Gestor_audio.Instance.EjecutarAudio(Gestor_audio.Instance.audioSourceSFX, Gestor_audio.Instance.naveSFX);
                columnaActual++;
                InclinacionJugador(filaActual, columnaActual);
            }
        }
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || sentido == "arr") && mover == true)
        {
            if (filaActual > 0)
            {
                Gestor_audio.Instance.EjecutarAudio(Gestor_audio.Instance.audioSourceSFX, Gestor_audio.Instance.naveSFX);
                filaActual--;
                InclinacionJugador(filaActual, columnaActual);
            }
        }
        if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) || sentido == "aba") && mover == true)
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

