using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.SceneManagement;

public class MenuPrincipal : GenericSingleton<MenuPrincipal>
{
    [SerializeField] private GameObject GestorBateria;  
    [SerializeField] private GameObject finalNivel;    
    [SerializeField] private GameObject pantallaInicial;
    [SerializeField] private GameObject pantallaTaller;
    [SerializeField] private GameObject sistemaCarriles;
    [SerializeField] private GameObject laseres;
    [SerializeField] public float aceleracionObstaculos;
    [HideInInspector] public int nivelActual;
    [HideInInspector] public bool victoria = false;
    public bool jugando = false;
    public bool enTaller = false;
    void Start()
    {
        Application.targetFrameRate = 60;
        nivelActual = 3;
    }
    void Update()
    {

    }
    public void Jugar() 
    {
        if(JugadorNivel.Instance.escogioNave)
        {
            GestorBateria.SetActive(true);
            float duracionNivel = 2.0f + nivelActual * 30f; // +30s de duracion por nivel
            GestorAnimaciones.Instance.TallerAJuego();
            pantallaTaller.SetActive(false);
            Gestor_audio.Instance.EjecutarAudio(Gestor_audio.Instance.audioSourceMusica, Gestor_audio.Instance.musicaJuego);
            sistemaCarriles.SetActive(true);
            laseres.SetActive(true);
            GestorTaller.Instance.ultimaNaveJugador.gameObject.SetActive(true);
            StartCoroutine(EsperarAEventoCoroutine(Time.time, duracionNivel, "finalNivel"));
        }else{print("No se ha seleccionado ninguna nave");}
        
    }
    public void Taller()
    {
        GestorAnimaciones.Instance.InicioATaller();
        Gestor_audio.Instance.EjecutarAudio(Gestor_audio.Instance.audioSourceMusica, Gestor_audio.Instance.musicaTienda);
        pantallaInicial.SetActive(false);
        Invoke("PantallaTaller", 2);
    }
    public void Victoria()
    {
        victoria = true;
        PlayerPrefs.SetInt("nivelActual", nivelActual + 1);
        nivelActual = PlayerPrefs.GetInt("nivelActual");
        // Hacer mas logica aqui despues :]
        print(" ganaste el nivel congrats ");
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
    private void PantallaTaller(){
        pantallaTaller.SetActive(true);
        StartCoroutine(SeleccionarNave());
    }
    IEnumerator SeleccionarNave()
    {
        yield return new WaitForSeconds(3);
        pantallaTaller.transform.Find("Seleccionar nave").gameObject.SetActive(true);
    }
}