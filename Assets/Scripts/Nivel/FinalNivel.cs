using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class FinalNivel : GenericSingleton<FinalNivel>
{
    [SerializeField] private GameObject pantallaFinal;
    [SerializeField] private GameObject destino;
    [SerializeField] private GameObject planeta;
    [SerializeField] private AudioClip clipVictoria;
    [SerializeField] private AudioClip clipTransicion;
    [SerializeField] private GameObject planetaNivel0;
    [SerializeField] private GameObject planetaNivel1;
    [SerializeField] private GameObject planetaNivel2;
    [SerializeField] private GameObject planetaNivel3;
    [HideInInspector] public bool victoria = false;
    [SerializeField] private GameObject panel1; 
    
    private Vector3 plataforma;
    private Transform nave;
    private void OnEnable()
    {

        GameObject planetaCargar = planetaNivel0;
        int nivel = PlayerPrefs.GetInt("nivelActual");
        if (nivel == 1)
        {
            planetaCargar = planetaNivel1;
        }
        if (nivel == 2)
        {
            planetaCargar = planetaNivel2;
        }
        if (nivel == 3)
        {
            planetaCargar = planetaNivel3;
        }
        Vector3 posicion = planeta.transform.position;
        Instantiate(planetaCargar, posicion,Quaternion.identity, destino.transform);
        Destroy(planeta);
        Transform padre = GameObject.Find("Jugador").transform.Find("Naves Jugador");
        nave = BuscarNave(padre, "default");
    }
    private Transform BuscarNave(Transform padre, string nombre)
    {
        foreach (Transform hijo in padre)
        {
            if (hijo.name == nombre)
                return hijo;

            Transform resultado = BuscarNave(hijo, nombre);
            if (resultado != null)
                return resultado;
        }
        return null;
    }
    public void Victoria()
    {
        nave.gameObject.GetComponent<CapsuleCollider>().enabled = false;
        GestorTaller.Instance.guardarMats();
        int nivelActual = PlayerPrefs.GetInt("nivelActual");
        victoria = true;
        Tutorial.Instance.aparecioAsteroide = true;
        MovimientoCarriles.Instance.InclinacionJugador(1, 1);
        if (nivelActual < 4)
        {
            PlayerPrefs.SetInt("nivelActual", nivelActual + 1);
        }
        //nivelActual = PlayerPrefs.GetInt("nivelActual");
        GameObject.Find("Sistema carriles").transform.Find("Instanciador_objetos").gameObject.SetActive(false);
        GameObject.Find("Animaciones").gameObject.SetActive(false);
        JugadorNivel.Instance.enabled = false;
        MovimientoCarriles.Instance.enabled = false;
        InstanciadorObjetos.Instance.enabled = false;
        MatrizCarriles.Instance.enabled = false;
        Gestor_audio.Instance.EjecutarAudio(Gestor_audio.Instance.audioSourceSFX, clipTransicion);
        StartCoroutine(MostrarPantallaFinal(3));
        //StartCoroutine(ExecuteOnEndAfterDelay(2f));
        
    }
    IEnumerator MostrarPantallaFinal(int segundos)
    {
        yield return new WaitForSeconds(segundos);
        
        Gestor_audio.Instance.EjecutarAudio(Gestor_audio.Instance.audioSourceMusica, clipVictoria);
        Gestor_audio.Instance.audioSourceMusica.loop = false;
        pantallaFinal.SetActive(true);
        panel1.SetActive(true);
        StartCoroutine(ExecuteOnEndAfterDelay(3f));
    }
    void Update()
    {
        if (victoria) 
        {
            plataforma = transform.Find("Destino").position;
            nave.position = Vector3.Lerp(nave.position, plataforma, 0.1f);
        }
    }

    private IEnumerator ExecuteOnEndAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        panel1.SetActive(true);
        StartCoroutine(ExecuteOnEndAfterDelay1(3f));

    }

    private IEnumerator ExecuteOnEndAfterDelay1(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        
        StartCoroutine(CargarFrenesiScene());

    }

    private IEnumerator CargarFrenesiScene()
    {
        yield return new WaitForSeconds(2); // Espera 2 segundos
        SceneManager.LoadScene("Frenesi"); // Carga la escena "Frenesi"
        Time.timeScale = 0;
    }
}
