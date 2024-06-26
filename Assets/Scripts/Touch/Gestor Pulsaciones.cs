using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestorPulsaciones : GenericSingleton<GestorPulsaciones>
{
    private Touch pulsacion;
    private bool pulsando;
    private Vector2 inicioPulsacion;
    private Vector2 finPulsacion;
    private int cantidadTouch = 0;
    private float tiempoInactividad = 0f;
    private float tiempoReinicio = 0.2f;
    [SerializeField] private float sensibilidadDeslizamiento = 200f;
    [SerializeField] private float capacidadDistanciaToque = 50f;
    [SerializeField] private GameObject adelantar;

    void Update()
    {
        fasesTouch();
        entradaPulsaciones();
        touchJugador();
        if (cantidadTouch >= 50)
        {
            adelantar.SetActive(true);
        }
        if (!pulsando)
        {
            tiempoInactividad += Time.deltaTime;
            if (tiempoInactividad >= tiempoReinicio)
            {
                cantidadTouch = 0;
                tiempoInactividad = 0f;
            }
        }
        else
        {
            tiempoInactividad = 0f;
        }
    }
    void fasesTouch()
    {
        if (Input.touchCount > 0)
        {
            if (Input.touchCount >= 100)
            {
                adelantar.SetActive(true);
            }
            pulsacion = Input.GetTouch(0);
            switch (pulsacion.phase)
            {
                case TouchPhase.Began:
                    cantidadTouch++;
                    pulsando = true;
                    inicioPulsacion = pulsacion.position;
                    break;
                case TouchPhase.Moved:
                    pulsando = true;
                    break;
                case TouchPhase.Ended:
                    finPulsacion = pulsacion.position;
                    pulsando = false;
                    DetectarToque();
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
    void DetectarToque()
    {
        float distanciaPulsacion = Vector2.Distance(inicioPulsacion, finPulsacion);

        if (distanciaPulsacion <= capacidadDistanciaToque && MenuPrincipal.Instance.jugando)
        {
            JugadorNivel.Instance.disparoLaser();
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
                    if(colision.collider.gameObject.name == "Screen") {GestorAnimaciones.Instance.TallerATerminal(false);}
                    else if (colision.collider.gameObject.name == "default"){GestorTaller.Instance.SeleccionNave(colision);}
                }
            }
        }
    }
    public void touchJugador()
    {
        if (!pulsando && (inicioPulsacion != Vector2.zero || finPulsacion != Vector2.zero) && MenuPrincipal.Instance.jugando)
        {
            Vector2 diferencia = finPulsacion - inicioPulsacion;

            if (diferencia.y > sensibilidadDeslizamiento)
            {
                if (!Tutorial.Instance.aparecioAsteroide)
                {
                    MovimientoCarriles.Instance.MovimientoJugador("arr");
                }
                
                Tutorial.Instance.yaArriba = true;
                inicioPulsacion = Vector2.zero;
                finPulsacion = Vector2.zero;
                return;
            }
            else if (diferencia.y < -sensibilidadDeslizamiento)
            {
                if (!Tutorial.Instance.aparecioAsteroide)
                {
                    MovimientoCarriles.Instance.MovimientoJugador("aba");
                }
                Tutorial.Instance.yaAbajo = true;
                inicioPulsacion = Vector2.zero;
                finPulsacion = Vector2.zero;
                return;
            }

            if (diferencia.x > sensibilidadDeslizamiento)
            {
                if (!Tutorial.Instance.aparecioAsteroide)
                {
                    MovimientoCarriles.Instance.MovimientoJugador("der");
                }
                Tutorial.Instance.yaDerecha = true;
                inicioPulsacion = Vector2.zero;
                finPulsacion = Vector2.zero;
                return;
            }
            else if (diferencia.x < -sensibilidadDeslizamiento)
            {
                if (!Tutorial.Instance.aparecioAsteroide)
                {
                    MovimientoCarriles.Instance.MovimientoJugador("izq");
                }
                Tutorial.Instance.yaIzquierda = true;
                inicioPulsacion = Vector2.zero;
                finPulsacion = Vector2.zero;
                return;
            }
        }
    }
}


