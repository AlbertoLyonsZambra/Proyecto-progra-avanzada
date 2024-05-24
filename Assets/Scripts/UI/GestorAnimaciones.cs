using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestorAnimaciones : GenericSingleton<GestorAnimaciones>
{
    [SerializeField] public List<GameObject> movsCamGO;
    [HideInInspector] public List<CPC_CameraPath> movsCamCPC;
    public bool enTransicion = false;
    void Start()
    {
        for(int i = 0; i < movsCamGO.Count; i++)
        {
            movsCamCPC.Add(movsCamGO[i].GetComponent<CPC_CameraPath>());
        }
    }

    void Update()
    {
    }
    public void TallerAJuego() 
    {
        MenuPrincipal.Instance.enTaller = false;
        Transicion(movsCamCPC[2], movsCamCPC[3], movsCamCPC[4], 2.0f, 10.0f);
        MenuPrincipal.Instance.jugando = true ;
    }
    public void InicioATaller()
    {
        Transicion(movsCamCPC[0], movsCamCPC[1], movsCamCPC[2], 2.0f, 10.0f);
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