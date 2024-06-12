using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : GenericSingleton<Tutorial>
{
    private Transform iconoTouch;
    [SerializeField] private float velocidad;
    [SerializeField] private RectTransform horizontal;
    [SerializeField] private RectTransform vertical;
    [SerializeField] private GameObject gestorBateria;
    [SerializeField] private GameObject indicadorBateria;
    [SerializeField] private GameObject AsteroideParaDisparar;
    private GameObject textoDisparar;
    private GameObject asteroideDisparar;
    private Vector3 posicionInicial;
    private Vector3 posicionVertical; 
    private Vector3 posicionHorizontal;
    private string sentidoHorizontal;
    private string sentidoVertical;
    private bool centrar;
    [HideInInspector] public bool yaIzquierda = false;
    [HideInInspector] public bool yaDerecha = false;
    [HideInInspector] public bool yaArriba = false;
    [HideInInspector] public bool yaAbajo = false;
    [HideInInspector] public int disparoLaser = 0;
    private bool turnoDisparo = false;
    private bool aparecioAsteroide = false;
    
    void OnEnable()
    {
        asteroideDisparar = gameObject.transform.Find("Dispare asteroide color").gameObject;
        textoDisparar = gameObject.transform.Find("Presiona disparar").gameObject;
        iconoTouch = gameObject.transform.Find("Icono touch");
        posicionVertical = vertical.transform.localPosition;
        posicionHorizontal = horizontal.transform.localPosition;
        posicionInicial = Vector3.zero;
        indicadorBateria.SetActive(false);
        StartCoroutine(EsperarEvento(3));
    }
    private void Update()
    {
        if (yaAbajo && yaArriba && yaDerecha && yaDerecha)
        {
            turnoDisparo = true;
        }
        MoverHorizontal(sentidoHorizontal);
        MoverVertical(sentidoVertical);
        if (centrar)
        {
            iconoTouch.localPosition = Vector3.zero;
        }
        DispararLaser();
    }
    public void cambiarHorizontal(string sentido)
    {
        sentidoHorizontal = sentido;
    }
    public void cambiarVertical(string sentido)
    {
        sentidoVertical = sentido;
    }
    void MoverHorizontal(string sentido)
    {
        if (sentido == "derecha" && !centrar)
        {
            iconoTouch.localPosition = Vector3.Lerp(iconoTouch.localPosition, posicionHorizontal*-1, velocidad * Time.deltaTime);
            return;
        }
        else if (sentido == "izquierda" && !centrar)
        {
            iconoTouch.localPosition = Vector3.Lerp(iconoTouch.localPosition, posicionHorizontal, velocidad * Time.deltaTime);
        }
    }
    void MoverVertical(string sentido)
    {
        if (sentido == "abajo" && !centrar)
        {
            iconoTouch.localPosition = Vector3.Lerp(iconoTouch.localPosition, posicionVertical*-1, velocidad * Time.deltaTime);
            return;
        }
        else if (sentido == "arriba" && !centrar)
        {
            iconoTouch.localPosition = Vector3.Lerp(iconoTouch.localPosition, posicionVertical, velocidad * Time.deltaTime);
        }
        
    }
    public void cambiarCentrar(bool siono)
    {
        centrar = siono;
        if (centrar) 
        {
            sentidoHorizontal = "";
            sentidoVertical = "";
        }
    }
    public void IniciarJuego()
    {
        MovimientoCarriles.Instance.enabled = true;
        Time.timeScale = 1;
        PlayerPrefs.SetInt("pasoTutorial", 1);
        gestorBateria.SetActive(true);
        //GestorBateria.Instance.enabled = true;
        InstanciadorObjetos.Instance.enabled = true;
        int nivelActual = PlayerPrefs.GetInt("nivelActual");
        indicadorBateria.SetActive(true);
        float duracionNivel = 2.0f + nivelActual * 30f; // +30s de duracion por nivel
        StartCoroutine(MenuPrincipal.Instance.EsperarAEventoCoroutine(Time.time, duracionNivel, "finalNivel"));
        gameObject.SetActive(false);
    }
    IEnumerator MoverIconoIzquierda(float segundos)
    {
        if (!yaIzquierda)
        {
            cambiarCentrar(false);
            iconoTouch.gameObject.SetActive(true);
            cambiarHorizontal("izquierda");
            yield return new WaitForSeconds(segundos);
            iconoTouch.gameObject.SetActive(false);
            cambiarCentrar(true);
            yield return new WaitForSeconds(segundos);
            StartCoroutine(MoverIconoIzquierda(segundos));
        }
        else 
        {
            iconoTouch.gameObject.SetActive(false);
            StartCoroutine(MoverIconoArriba(segundos));
            yield break;
        }
    }
    IEnumerator MoverIconoArriba(float segundos)
    {
        if (!yaArriba)
        {
            cambiarCentrar(false);
            iconoTouch.gameObject.SetActive(true);
            cambiarVertical("arriba");
            yield return new WaitForSeconds(segundos);
            iconoTouch.gameObject.SetActive(false);
            cambiarCentrar(true);
            yield return new WaitForSeconds(segundos);
            StartCoroutine(MoverIconoArriba(segundos));
        }
        else 
        {
            iconoTouch.gameObject.SetActive(false);
            StartCoroutine(MoverIconoDerecha(segundos));
            yield break;
        }
    }
    IEnumerator MoverIconoDerecha(float segundos)
    {
        if (!yaDerecha)
        {
            cambiarCentrar(false);
            iconoTouch.gameObject.SetActive(true);
            cambiarHorizontal("derecha");
            yield return new WaitForSeconds(segundos);
            iconoTouch.gameObject.SetActive(false);
            cambiarCentrar(true);
            yield return new WaitForSeconds(segundos);
            StartCoroutine(MoverIconoDerecha(segundos));
        }
        else
        {
            iconoTouch.gameObject.SetActive(false);
            StartCoroutine(MoverIconoAbajo(segundos));
            yield break;
        }
    }
    IEnumerator MoverIconoAbajo(float segundos)
    {
        if (!yaAbajo)
        {
            cambiarCentrar(false);
            iconoTouch.gameObject.SetActive(true);
            cambiarVertical("abajo");
            yield return new WaitForSeconds(segundos);
            iconoTouch.gameObject.SetActive(false);
            cambiarCentrar(true);
            yield return new WaitForSeconds(segundos);
            StartCoroutine(MoverIconoAbajo(segundos));
        }
        else
        {
            iconoTouch.gameObject.SetActive(false);
            yield break;
        }
    }
    IEnumerator EsperarEvento(float segundos)
    {
        // Empezar con tutorial de deslizar
        if (segundos == 3)
        {
            yield return new WaitForSeconds(segundos);
            iconoTouch.gameObject.SetActive(true);
            StartCoroutine(MoverIconoIzquierda(1));
        }
        // Tutorial para dispararle a asteroide
        if (segundos == 0.5f)
        {
            textoDisparar.SetActive(false);
            yield return new WaitForSeconds(segundos);
            asteroideDisparar.SetActive(true);
            if (!aparecioAsteroide)
            {
                MovimientoCarriles.Instance.enabled = false;
                GameObject poolLaser = GameObject.Find("[SimpleObjectPool] - LaserRojo OP");
                poolLaser.SetActive(false);
                poolLaser.SetActive(true);
                //GameObject.Find("[SimpleObjectPool] - LaserRojo OP").SetActive(true);
                Vector2 posicionJugador = MatrizCarriles.Instance.getPosicion(MovimientoCarriles.Instance.filaActual, MovimientoCarriles.Instance.columnaActual);
                Vector3 posicionAsteroide = new Vector3(posicionJugador.x, posicionJugador.y, -150);
                Time.timeScale = 0.5f;
                Instantiate(AsteroideParaDisparar, posicionAsteroide, Quaternion.identity);
                aparecioAsteroide = true;
            }
        }
       
    }
    void DispararLaser()
    {
        if (turnoDisparo)
        {
            if (disparoLaser == 0)
            {
                textoDisparar.SetActive(true);
            }
            else if (disparoLaser >= 3)
            {
                StartCoroutine(EsperarEvento(0.5f));
                //IniciarJuego();
            }
        }
    }
}
