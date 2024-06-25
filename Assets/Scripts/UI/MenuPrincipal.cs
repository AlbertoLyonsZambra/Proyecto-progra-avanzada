using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPrincipal : GenericSingleton<MenuPrincipal>
{
    [Header("Pantallas")]
    [SerializeField] public GameObject pantallaInicial;
    [SerializeField] public GameObject pantallaTaller;
    [SerializeField] public GameObject pantallaTutorial;
    [SerializeField] private Ayuda ayudaScript;
    [SerializeField] public GameObject barraProgreso;
    [SerializeField] private GameObject panel1; 
    [SerializeField] private Button botonFrenesi;

    [Header("Del jugador")]
    [SerializeField] private GameObject laseres;
    [SerializeField] private GameObject gestorBateria;

    [Header("De la escena")]
    [SerializeField] private GameObject finalNivel;
    [SerializeField] private GameObject sistemaCarriles;
    [SerializeField] public float aceleracionObstaculos;
    [HideInInspector] public int nivelActual;
    public bool jugando = false;
    public bool enTaller = false;
    public bool enTerminal = false;
    private int pasoTutorial = 0;
    // Estas dos de abajo son para medir el final del nivel
    [HideInInspector] public float duracionNivel;
    [HideInInspector] public float tiempoTranscurrido;

    void Start()
    {
        Application.targetFrameRate = 60;
        nivelActual = PlayerPrefs.GetInt("nivelActual");
        pasoTutorial = PlayerPrefs.GetInt("pasoTutorial");

        // Desactiva el botón de frenesí por defecto
        if (botonFrenesi != null)
        {
            botonFrenesi.gameObject.SetActive(false);
        }

        // Verifica si el nivel actual es 1 y activa el botón si es así
        ActualizarBotonFrenesi();

        
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
            if (nivelActual <=3)
            {
                Gestor_audio.Instance.EjecutarAudio(Gestor_audio.Instance.audioSourceMusica, Gestor_audio.Instance.musicaJuego);
            }
            else
            {
                Gestor_audio.Instance.EjecutarAudio(Gestor_audio.Instance.audioSourceMusica, Gestor_audio.Instance.musicaInfinito);
            }
            
            // Formula = 2 segundos de transicion del taller a juego, mas 20 segundos en demorarse en llegar la plataforma, mas el nivel en el que est�
            duracionNivel = 2.0f + 20 + (nivelActual + 1) * 30f; // +30s de duracion por nivel
            duracionNivel = 2;
            sistemaCarriles.SetActive(true);
            laseres.SetActive(true);
            GestorTaller.Instance.ultimaNaveJugador.gameObject.SetActive(true);
            if (pasoTutorial == 1)
            {
                barraProgreso.SetActive(true);
                gestorBateria.SetActive(true); // Antes este gestor se activaba fuera del if
                print("EmpezoJuego");
                if(nivelActual <= 3) 
                { 
                    StartCoroutine(EsperarAEventoCoroutine(Time.time, duracionNivel, "finalNivel"));
                    //barraProgreso.SetActive(true);
                }
                
                ayudaScript.CambiarPantallaActual("juego1");
            }
            else
            {
                pantallaTutorial.SetActive(true);
                InstanciadorObjetos.Instance.enabled = false;
                ayudaScript.CambiarPantallaActual("juego1");
            }
        }else{print("No se ha seleccionado ninguna nave");}

        //ayudaScript.CambiarPantallaActual("juego");
        
    }

    public void Frenesi()
    {
        int nivel = PlayerPrefs.GetInt("nivelActual");

        if (nivel == 1)
        {
            panel1.SetActive(true);
            StartCoroutine(CargarFrenesiScene());
        }
    }

    private IEnumerator CargarFrenesiScene()
    {
        yield return new WaitForSeconds(2); // Espera 2 segundos
        SceneManager.LoadScene("Frenesi"); // Carga la escena "Frenesi"
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
        tiempoTranscurrido = Time.time - tiempoInicio; // Obtiene el tiempo transcurrido
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

    private void PantallaTaller()
    {
        pantallaTaller.SetActive(true);
        StartCoroutine(SeleccionarNave());
    }

    IEnumerator SeleccionarNave()
    {
        yield return new WaitForSeconds(3);
        if(!JugadorNivel.Instance.escogioNave && nivelActual <= 1){
            pantallaTaller.transform.Find("Seleccionar nave").gameObject.SetActive(true);
        }
    }

    public void ActualizarBotonFrenesi()
    {
        int nivel = PlayerPrefs.GetInt("nivelActual");
        if (botonFrenesi != null)
        {
            botonFrenesi.gameObject.SetActive(nivel == 4);
        }
    }
}
