using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.SceneManagement;

public class MenuPrincipal : GenericSingleton<MenuPrincipal>
{
    [SerializeField] private GameObject finalNivel;    
    [SerializeField] private GameObject pantallaInicial;
    [SerializeField] private GameObject pantallaTaller;
    [SerializeField] private GameObject sistemaCarriles;
    [SerializeField] private GameObject laseres;
    [SerializeField] public float aceleracionObstaculos;
    [HideInInspector] public bool jugando = false;
    [HideInInspector] public bool enTaller = false;
    void Start()
    {
        Application.targetFrameRate = 60;
    }
    void Update()
    {

    }
    public void Jugar() // Cuando hayan mas niveles, obtener la variable que representa el nivel actual 
                        // y verificar en que nivel esta, para modificar cuanto deberia durar el nivel
                        // basado en cual nivel se encuentra el jugador
    {
        if(!GestorAnimaciones.Instance.enTransicion)
        {
        GestorAnimaciones.Instance.TallerAJuego();
        pantallaTaller.SetActive(false);
        Gestor_audio.Instance.EjecutarAudio(Gestor_audio.Instance.audioSourceMusica, Gestor_audio.Instance.musicaJuego);
        jugando = true ;
        sistemaCarriles.SetActive(true);
        laseres.SetActive(true);
        StartCoroutine(EsperarAEventoCoroutine(Time.time, 600f, "finalNivel"));
        }
    }
    public void Taller()
    {
        if(!GestorAnimaciones.Instance.enTransicion)
        {
        GestorAnimaciones.Instance.InicioATaller();
        Gestor_audio.Instance.EjecutarAudio(Gestor_audio.Instance.audioSourceMusica, Gestor_audio.Instance.musicaTienda);
        pantallaInicial.SetActive(false);
        pantallaTaller.SetActive(true);
        enTaller = true;
        }
    }

    IEnumerator EsperarAEventoCoroutine(float tiempoInicio, float tiempoFinal, string evento)
    {
        yield return new WaitForSeconds(tiempoFinal); // Espera (si usa WaitForSecondsRealtime puede funcionar distinto)
        float tiempoTranscurrido = Time.time - tiempoInicio; // Obtiene el tiempo transcurrido
        if(tiempoTranscurrido >= tiempoFinal) // Verifica si ya paso el tiempo necesitado
        {
            if(evento == "finalNivel"){finalNivel.SetActive(true);}
        }
        else
        {
            // yield return new WaitForSeconds(Random.Range(5f, 10f));
            // if(evento == "finalNivel"){finalNivel.SetActive(true);}
            print(" no paso na companero ");
        }
    }

}