using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.SceneManagement;

public class MenuPrincipal : GenericSingleton<MenuPrincipal>
{
    [SerializeField] private GameObject pantallaInicial;
    [SerializeField] private GameObject pantallaTaller;
    [SerializeField] private GameObject sistemaCarriles;
    [SerializeField] private GameObject laseres;
    [SerializeField] public int velocidadObstaculos;
    [HideInInspector] public bool jugando = false;
    [HideInInspector] public bool enTaller = false;
    
    void Start()
    {
        Application.targetFrameRate = 60;
    }
    public void ChangeScene(string nameScene)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(nameScene);

    }
    public void Jugar()
    {
        if(!GestorAnimaciones.Instance.enTransicion)
        {
        GestorAnimaciones.Instance.TallerAJuego();
        pantallaTaller.SetActive(false);
        Gestor_audio.Instance.EjecutarAudio(Gestor_audio.Instance.audioSourceMusica, Gestor_audio.Instance.musicaJuego);
        jugando = true ;
        sistemaCarriles.SetActive(true);
        laseres.SetActive(true);
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
}