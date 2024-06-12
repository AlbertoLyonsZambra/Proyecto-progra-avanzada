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

    public void TallerATerminal(bool alreves) 
    {
        if(!alreves && MenuPrincipal.Instance.enTaller) {
            MenuPrincipal.Instance.enTaller = false;
            Transicion(movsCamCPC[2], movsCamCPC[5], movsCamCPC[7], 1.0f, 30.0f, "terminal");
        }
        else if ((alreves && !MenuPrincipal.Instance.enTaller)) {
            MenuPrincipal.Instance.enTerminal = false;
            Transicion(movsCamCPC[7], movsCamCPC[6], movsCamCPC[2], 1.0f, 10.0f, "taller"); 
        }
    }

    public void TallerAJuego() 
    {
        MenuPrincipal.Instance.enTaller = false;
        Transicion(movsCamCPC[2], movsCamCPC[3], movsCamCPC[4], 2.0f, 10.0f, "juego");
    }

    public void InicioATaller()
    {
        Transicion(movsCamCPC[0], movsCamCPC[1], movsCamCPC[2], 1.6f, 10.0f, "taller");
        
    }

    // Transicion de una animacion (incio), a otra (destino), usando una animacion entre medio (transicion), la transicion es unidireccional, para la vuelta hay que hacer otra transicion
    public void Transicion(CPC_CameraPath inicio, CPC_CameraPath transicion, CPC_CameraPath destino, float tiempoTransicion, float tiempoDestino, string nombreDestino)
    {
        if(!enTransicion)
        {
            enTransicion = true;
            inicio.PausePath();
            StartCoroutine(MoverTarget(inicio.target, destino.target.transform.position, tiempoTransicion));
            transicion.PlayPath(tiempoTransicion);
            StartCoroutine(EsperarAnimacion(tiempoTransicion, destino, tiempoDestino, nombreDestino));
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

    IEnumerator EsperarAnimacion(float tiempo, CPC_CameraPath destino, float tiempoDestino, string nombreDestino)
    {
        yield return new WaitForSecondsRealtime(tiempo);
        destino.PlayPath(tiempoDestino);
        enTransicion = false;
        if(nombreDestino == "taller"){MenuPrincipal.Instance.enTaller=true; MenuPrincipal.Instance.pantallaTaller.SetActive(true); GestorTaller.Instance.NaveSeleccionada();}
        else if(nombreDestino == "terminal"){MenuPrincipal.Instance.enTerminal=true; MenuPrincipal.Instance.pantallaTaller.SetActive(false); GestorTaller.Instance.NaveSeleccionada();}
        else if(nombreDestino == "juego"){MenuPrincipal.Instance.jugando=true;}
    }
}