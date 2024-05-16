using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestorPulsaciones : MonoBehaviour
{
    private Touch pulsacion;
    private bool pulsando;
    private Vector2 inicioPulsacion;
    private Vector2 finPulsacion;
    [SerializeField] private float sensibilidadDeslizamiento = 200f;
    void Update()
    {
        fasesTouch();
        entradaPulasciones();
        touchJugador();
    }
    void fasesTouch()
    {
        if (Input.touchCount > 0)
        {
            pulsacion = Input.GetTouch(0);
            switch (pulsacion.phase)
            {
                case TouchPhase.Began:
                    pulsando = true;
                    inicioPulsacion = pulsacion.position;
                    break;
                case TouchPhase.Moved:
                    pulsando = true;
                    break;
                case TouchPhase.Ended:
                    finPulsacion = pulsacion.position;
                    pulsando = false;
                    break;
                case TouchPhase.Stationary:
                    pulsando = true;
                    break;
                case TouchPhase.Canceled:
                    pulsando = false;
                    break;
            }
        }
    }
    public void entradaPulasciones()
    {
        Vector3 posicionPulsacion = pulsacion.position;
        if (Input.GetKey(KeyCode.Mouse0) || pulsando)
        {
            Ray rayo;
            
            if (!pulsando)
            {
                rayo = Camera.main.ScreenPointToRay(Input.mousePosition);
            }
            else
            {
                rayo = Camera.main.ScreenPointToRay(posicionPulsacion);
            }
            

            RaycastHit colision;
            float distanciaRayo = 100f;
            Debug.DrawRay(rayo.origin, rayo.direction * distanciaRayo, Color.green);

            if (Physics.Raycast(rayo, out colision, distanciaRayo))
            {
                if (colision.collider != null)
                {
                    //print("Chocando con" + colision.collider.gameObject.name);
                }
            }
        }
        
    }
    public void touchJugador()
    {
        if (!pulsando && (inicioPulsacion != Vector2.zero || finPulsacion != Vector2.zero))
        {
            Vector2 diferencia = finPulsacion - inicioPulsacion;

            if (diferencia.y > sensibilidadDeslizamiento)
            {
                //print("DESLIZAMIENTO ARRIBA");
                MovimientoCarriles.Instance.MovimientoJugador("arr");
                // Restablecer las posiciones después de hacer el deslizamiento
                inicioPulsacion = Vector2.zero;
                finPulsacion = Vector2.zero;
                return;
            }
            else if (diferencia.y < -sensibilidadDeslizamiento)
            {
                //print("DESLIZAMIENTO ABAJO");
                MovimientoCarriles.Instance.MovimientoJugador("aba");
                // Restablecer las posiciones después de hacer el deslizamiento
                inicioPulsacion = Vector2.zero;
                finPulsacion = Vector2.zero;
                return;
            }

            if (diferencia.x > sensibilidadDeslizamiento)
            {
                //print("DESLIZAMIENTO DERECHA");
                MovimientoCarriles.Instance.MovimientoJugador("der");
                // Restablecer las posiciones después de hacer el deslizamiento
                inicioPulsacion = Vector2.zero;
                finPulsacion = Vector2.zero;
                return;
            }
            else if (diferencia.x < -sensibilidadDeslizamiento)
            {
                //print("DESLIZAMIENTO IZQUIERDA");
                MovimientoCarriles.Instance.MovimientoJugador("izq");
                // Restablecer las posiciones después de procesar el deslizamiento
                inicioPulsacion = Vector2.zero;
                finPulsacion = Vector2.zero;
                return;
            }
        }
    }
}


