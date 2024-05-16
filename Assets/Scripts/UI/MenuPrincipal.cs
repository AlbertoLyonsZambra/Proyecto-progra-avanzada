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
        jugando = true ;
        menuPrincipal.SetActive(false);
        sistemaCarriles.SetActive(true);
        laseres.SetActive(true);
        Gestor_audio.Instance.audioSourceMusica.mute = true;
        Gestor_audio.Instance.audioSourceSFX.mute = false;
    }
}