using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestorAnimaciones : GenericSingleton<GestorAnimaciones>
{
    [SerializeField] public GameObject animacion1;
    [SerializeField] public GameObject animacion2;
    [SerializeField] public GameObject animacion3;
    [SerializeField] public GameObject animacion4;
    [SerializeField] public GameObject animacion5;
    [HideInInspector] public CPC_CameraPath anim1;
    [HideInInspector] public CPC_CameraPath anim2;
    [HideInInspector] public CPC_CameraPath anim3;
    [HideInInspector] public CPC_CameraPath anim4;
    [HideInInspector] public CPC_CameraPath anim5;
    public bool enTransicion = false;
    void Start()
    {
        anim1 = animacion1.GetComponent<CPC_CameraPath>();
        anim2 = animacion2.GetComponent<CPC_CameraPath>();
        anim3 = animacion3.GetComponent<CPC_CameraPath>();
        anim4 = animacion4.GetComponent<CPC_CameraPath>();
        anim5 = animacion5.GetComponent<CPC_CameraPath>();
    }

    void Update()
    {
    }
    public void TallerAJuego() 
    {
        MenuPrincipal.Instance.enTaller = false;
        Transicion(anim3, anim4, anim5, 2.0f, 10.0f);
        MenuPrincipal.Instance.jugando = true ;
    }
    public void InicioATaller()
    {
        Transicion(anim1, anim2, anim3, 2.0f, 10.0f);
        MenuPrincipal.Instance.enTaller = true;
    }
    // Transicion de una animacion (incio), a otra (destino), usando una animacion entre medio (transicion)
    public void Transicion(CPC_CameraPath inicio, CPC_CameraPath transicion, CPC_CameraPath destino, float tiempoTransicion, float tiempoDestino)
    {
        if(!enTransicion)
        {
        enTransicion = true;
        inicio.PausePath();
        StartCoroutine(MoverTarget(inicio.target, destino.target.transform.position, tiempoTransicion));
        transicion.PlayPath(tiempoTransicion);
        StartCoroutine(EsperarAnimacion(tiempoTransicion, destino, tiempoDestino));
        }
    }
    IEnumerator MoverTarget(Transform target, Vector3 posFinal, float duracion)
    {
        Vector3 posInicial = target.transform.position;
        float transcurrido = 0;

        while (transcurrido < duracion)
        {
            target.transform.position = Vector3.Lerp(posInicial, posFinal, transcurrido / duracion);
            transcurrido += Time.deltaTime;
            yield return null;
        }
        target.transform.position = posFinal;
    }
    IEnumerator EsperarAnimacion(float tiempo, CPC_CameraPath destino, float tiempoDestino)
    {
        yield return new WaitForSecondsRealtime(tiempo);
        destino.PlayPath(tiempoDestino);
        enTransicion = false;
    }
}