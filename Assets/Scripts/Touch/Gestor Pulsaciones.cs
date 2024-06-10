using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestorPulsaciones : GenericSingleton<GestorPulsaciones>
{
    private Touch pulsacion;
    private bool pulsando;
    private Vector2 inicioPulsacion;
    private Vector2 finPulsacion;
    [SerializeField] private float sensibilidadDeslizamiento = 200f;
    void Update()
    {
        fasesTouch();
        entradaPulsaciones();
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
    public void entradaPulsaciones()
    {
        Color color;
        if(MenuPrincipal.Instance.enTaller){color=Color.red;}
        else{color = Color.green;}
        Vector3 posicionPulsacion = pulsacion.position;
        if (Input.GetKey(KeyCode.Mouse0) || pulsando)
        {
            Ray rayo;
            if (!pulsando){rayo = Camera.main.ScreenPointToRay(Input.mousePosition);}
            else{rayo = Camera.main.ScreenPointToRay(posicionPulsacion);}
            float distanciaRayo = 100f;
            RaycastHit colision;
            Debug.DrawRay(rayo.origin, rayo.direction * distanciaRayo, color);

            if (Physics.Raycast(rayo, out colision, distanciaRayo))
            {
                if (colision.collider != null)
                {
                    GestorTaller.Instance.SeleccionNave(colision);
                    // print("Chocando con " + colision.collider.gameObject.transform.parent.name);
                }
            }
            // RaycastHit[] colisiones = Physics.RaycastAll(rayo, distanciaRayo);
            // if (colisiones.Length > 0)
            // {
            //     RaycastHit ultimoColision = colisiones[colisiones.Length - 1];
            //     if (ultimoColision.collider != null)
            //     {
            //         SeleccionNave(ultimoColision);
            //         // print("Chocando con " + ultimoColision.collider.gameObject.transform.parent.name);
            //     }
            // }else{print("No se ha seleccionado ninguna nave");}
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
                Tutorial.Instance.yaArriba = true;
                // Restablecer las posiciones despu�s de hacer el deslizamiento
                inicioPulsacion = Vector2.zero;
                finPulsacion = Vector2.zero;
                return;
            }
            else if (diferencia.y < -sensibilidadDeslizamiento)
            {
                //print("DESLIZAMIENTO ABAJO");
                MovimientoCarriles.Instance.MovimientoJugador("aba");
                Tutorial.Instance.yaAbajo = true;
                // Restablecer las posiciones despu�s de hacer el deslizamiento
                inicioPulsacion = Vector2.zero;
                finPulsacion = Vector2.zero;
                return;
            }

            if (diferencia.x > sensibilidadDeslizamiento)
            {
                //print("DESLIZAMIENTO DERECHA");
                MovimientoCarriles.Instance.MovimientoJugador("der");
                Tutorial.Instance.yaDerecha = true;
                // Restablecer las posiciones despu�s de hacer el deslizamiento
                inicioPulsacion = Vector2.zero;
                finPulsacion = Vector2.zero;
                return;
            }
            else if (diferencia.x < -sensibilidadDeslizamiento)
            {
                //print("DESLIZAMIENTO IZQUIERDA");
                MovimientoCarriles.Instance.MovimientoJugador("izq");
                Tutorial.Instance.yaIzquierda = true;
                // Restablecer las posiciones despu�s de procesar el deslizamiento
                inicioPulsacion = Vector2.zero;
                finPulsacion = Vector2.zero;
                return;
            }
        }
    }
}


