using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialConsumible : MonoBehaviour
{
    private Vector3 posJugador;
    private Vector3 escalaInicial;
    private float velocidad = 10.0f;    
    void Start()
    {
        escalaInicial = transform.localScale;
        transform.rotation = Random.rotation;
    }

    void Update()
    {
        GirarYRecoger();
    }

    void GirarYRecoger()
    {
        if(tag == "Collider_DP" && MenuPrincipal.Instance.jugando)
        {
            transform.parent.gameObject.SetActive(true);
            Vector3 rotacion = new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
            transform.Rotate(rotacion * Time.deltaTime, Space.Self);
        }
        else
        {
            posJugador = JugadorNivel.Instance.transformJug.position;
            Vector3 rotacion = new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
            transform.Rotate(rotacion * Time.deltaTime, Space.Self);
            transform.position = Vector3.MoveTowards(transform.position, posJugador, velocidad * Time.deltaTime);
            transform.localScale = Vector3.Lerp(transform.localScale, escalaInicial * 0.5f, 5f * Time.deltaTime);
        }
    }
}
    