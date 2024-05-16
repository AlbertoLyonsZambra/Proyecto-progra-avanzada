using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.SceneManagement;

public class MenuPrincipal : GenericSingleton<MenuPrincipal>
{
    [SerializeField] private GameObject menuPrincipal;
    [SerializeField] private GameObject sistemaCarriles;
    [SerializeField] private GameObject laseres;
    [SerializeField] public int velocidadObstaculos;
    [HideInInspector] public bool jugando = false ;
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
        Gestor_audio.Instance.ejecutarMusica(Gestor_audio.Instance.musicaJuego);
        jugando = true ;
        menuPrincipal.SetActive(false);
        sistemaCarriles.SetActive(true);
        laseres.SetActive(true);
    }
}