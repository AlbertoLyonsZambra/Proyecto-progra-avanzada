using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestorPulsaciones : MonoBehaviour
{
    private Touch pulsacion;
    private bool pulsando

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fasesTouch();
        entradaPulasciones();
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
                    break;
                case TouchPhase.Moved:
                    pulsando = true;
                    break;
                case TouchPhase.Ended:
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
            if (!pulsando)
            {
                Ray rayo = Camera.main.ScreenPointToRay(Input.mousePosition);
            }
            else
            {
                Ray rayo = Camera.main.ScreenPointToRay(posicionPulsacion);
            }
            

            RaycastHit colision;
            float distanciaRayo = 100f;
            Debug.DrawRay(rayo.origin, rayo.direction * distanciaRayo, Color.green);

            if (Physics.Raycast(rayo, out colision, distanciaRayo))
            {
                if (colision.collider != null)
                {
                    print("Chocando con" + colision.collider.gameObject.name);
                }
            }
        }
        
    }
}

