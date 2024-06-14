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
    [SerializeField] private GameObject FlechaAsteroide;
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
    [HideInInspector] public bool aparecioAsteroide = false;
    
    void OnEnable()
    {
        if(PlayerPrefs.GetInt("pasoTutorial") == 0)
        {
            asteroideDisparar = gameObject.transform.Find("Dispare asteroide color").gameObject;
            textoDisparar = gameObject.transform.Find("Presiona disparar").gameObject;
            iconoTouch = gameObject.transform.Find("Icono touch");
            posicionVertical = vertical.transform.localPosition;
            posicionHorizontal = horizontal.transform.localPosition;
            posicionInicial = Vector3.zero;
            indicadorBateria.SetActive(false);
            StartCoroutine(EsperarEvento(2));
        }

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
        Destroy(GameObject.Find("Flecha Roja(Clone)").gameObject);
        aparecioAsteroide = false;
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
        if (yaAbajo && yaArriba && yaDerecha && yaDerecha)
        {
            yield break;
        }
        cambiarCentrar(false);
        iconoTouch.gameObject.SetActive(true);
        cambiarHorizontal("izquierda");
        yield return new WaitForSeconds(segundos);
        iconoTouch.gameObject.SetActive(false);
        cambiarCentrar(true);
        yield return new WaitForSeconds(segundos);
        StartCoroutine(MoverIconoArriba(segundos));
        
    }
    IEnumerator MoverIconoArriba(float segundos)
    {
        if (yaAbajo && yaArriba && yaDerecha && yaDerecha)
        {
            yield break;
        }
        cambiarCentrar(false);
        iconoTouch.gameObject.SetActive(true);
        cambiarVertical("arriba");
        yield return new WaitForSeconds(segundos);
        iconoTouch.gameObject.SetActive(false);
        cambiarCentrar(true);
        yield return new WaitForSeconds(segundos);
        StartCoroutine(MoverIconoDerecha(segundos));
    }
    IEnumerator MoverIconoDerecha(float segundos)
    {
        if (yaAbajo && yaArriba && yaDerecha && yaDerecha)
        {
            yield break;
        }
        cambiarCentrar(false);
        iconoTouch.gameObject.SetActive(true);
        cambiarHorizontal("derecha");
        yield return new WaitForSeconds(segundos);
        iconoTouch.gameObject.SetActive(false);
        cambiarCentrar(true);
        yield return new WaitForSeconds(segundos);
        StartCoroutine(MoverIconoAbajo(segundos));
    }
    IEnumerator MoverIconoAbajo(float segundos)
    {
        if (yaAbajo && yaArriba && yaDerecha && yaDerecha)
        {
            yield break;
        }
        cambiarCentrar(false);
        iconoTouch.gameObject.SetActive(true);
        cambiarVertical("abajo");
        yield return new WaitForSeconds(segundos);
        iconoTouch.gameObject.SetActive(false);
        cambiarCentrar(true);
        yield return new WaitForSeconds(segundos);
        StartCoroutine(MoverIconoIzquierda(segundos));
    }
    IEnumerator EsperarEvento(float segundos)
    {
        // Empezar con tutorial de deslizar
        if (segundos == 2)
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
                Vector2 posicionJugador = MatrizCarriles.Instance.getPosicion(MovimientoCarriles.Instance.filaActual, MovimientoCarriles.Instance.columnaActual);
                GameObject poolLaser = GameObject.Find("[SimpleObjectPool] - LaserRojo OP");
                poolLaser.SetActive(false);
                poolLaser.SetActive(true);
                Vector3 posicionAsteroide = new Vector3(posicionJugador.x, posicionJugador.y, -150);
                Vector3 posicionFlecha = new Vector3(posicionJugador.x, posicionJugador.y+2, -150);
                Time.timeScale = 0.5f;
                Instantiate(AsteroideParaDisparar, posicionAsteroide, Quaternion.identity);
                Instantiate(FlechaAsteroide, posicionFlecha, Quaternion.Euler(0,0,-90));
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
                if(textoDisparar != null){textoDisparar.SetActive(true);}
                
            }
            else if (disparoLaser >= 3)
            {
                StartCoroutine(EsperarEvento(0.5f));
                //IniciarJuego();
            }
        }
    }
}
