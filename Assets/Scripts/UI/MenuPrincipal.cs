using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.SceneManagement;

public class MenuPrincipal : GenericSingleton<MenuPrincipal>
{
    [SerializeField] private GameObject gestorBateria;
    [SerializeField] private GameObject finalNivel;
    [SerializeField] public GameObject pantallaInicial;
    [SerializeField] public GameObject pantallaTaller;
    [SerializeField] private GameObject sistemaCarriles;
    [SerializeField] private GameObject laseres;
    [SerializeField] public GameObject pantallaTutorial;
    [SerializeField] public float aceleracionObstaculos;
    [HideInInspector] public int nivelActual;
    [SerializeField] private Ayuda ayudaScript;
    public bool jugando = false;
    public bool enTaller = false;
    public bool enTerminal = false;
    private int pasoTutorial = 0;
    void Start()
    {
        Application.targetFrameRate = 60;
        nivelActual = 4;//PlayerPrefs.GetInt("nivelActual");
        pasoTutorial = PlayerPrefs.GetInt("pasoTutorial");

        
        // Establece la pantalla de ayuda inicial como la pantalla inicial
        ayudaScript.CambiarPantallaActual("juego");
    }
    void Update()
    {
        
    }
    public void Jugar() 
    {
        if(JugadorNivel.Instance.escogioNave)
        {
            PlayerPrefs.SetInt("MatsV", 0);
            PlayerPrefs.SetInt("MatsN", 0);
            PlayerPrefs.SetInt("MatsR", 0);
            GestorAnimaciones.Instance.TallerAJuego();
            pantallaTaller.SetActive(false);
            Gestor_audio.Instance.EjecutarAudio(Gestor_audio.Instance.audioSourceMusica, Gestor_audio.Instance.musicaJuego);
            // Formula = 2 segundos de transicion del taller a juego, mas 5 minutos de base (300s), mas el nivel en el que est�
            float duracionNivel = 2.0f + 300 + nivelActual * 30f; // +30s de duracion por nivel
            sistemaCarriles.SetActive(true);
            laseres.SetActive(true);
            GestorTaller.Instance.ultimaNaveJugador.gameObject.SetActive(true);
            if (pasoTutorial == 1)
            {
                gestorBateria.SetActive(true); // Antes este gestor se activaba fuera del if
                print("EmpezoJuego");
                if(nivelActual <= 3) { StartCoroutine(EsperarAEventoCoroutine(Time.time, duracionNivel, "finalNivel")); }
                
                ayudaScript.CambiarPantallaActual("juego");
            }
            else
            {
                pantallaTutorial.SetActive(true);
                InstanciadorObjetos.Instance.enabled = false;
                ayudaScript.CambiarPantallaActual("juego");
            }
        }else{print("No se ha seleccionado ninguna nave");}

        //ayudaScript.CambiarPantallaActual("juego");
        
    }
    public void Taller()
    {
        GestorAnimaciones.Instance.InicioATaller();
        Gestor_audio.Instance.EjecutarAudio(Gestor_audio.Instance.audioSourceMusica, Gestor_audio.Instance.musicaTienda);
        pantallaInicial.SetActive(false);
        Invoke("PantallaTaller", 2);
        // Establece la pantalla de ayuda actual como la pantalla de taller
        ayudaScript.CambiarPantallaActual("taller");
    }
    public IEnumerator EsperarAEventoCoroutine(float tiempoInicio, float tiempoFinal, string evento)
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
        if(!JugadorNivel.Instance.escogioNave){
            pantallaTaller.transform.Find("Seleccionar nave").gameObject.SetActive(true);
        }
    }
}