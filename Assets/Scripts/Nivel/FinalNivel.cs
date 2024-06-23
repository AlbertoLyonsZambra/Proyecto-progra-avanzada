using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;
using UnityEngine.UIElements;

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
    private Vector3 plataforma;
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
    }
    public void Victoria()
    {
        GestorTaller.Instance.guardarMats();
        Vector3 diferencia = new Vector3(-1.89f, -1.22f, 3.56f);
        plataforma = transform.Find("Destino").position - diferencia;
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
        StartCoroutine(MostrarPantallaFinal(2));
    }
    IEnumerator MostrarPantallaFinal(int segundos)
    {
        yield return new WaitForSeconds(segundos);
        
        Gestor_audio.Instance.EjecutarAudio(Gestor_audio.Instance.audioSourceMusica, clipVictoria);
        Gestor_audio.Instance.audioSourceMusica.loop = false;
        pantallaFinal.SetActive(true);
        Time.timeScale = 0;
    }
    void Update()
    {
        if (victoria) 
        {
            GestorAnimaciones.Instance.MoverPlataformaFinal();
        }
    }
}
