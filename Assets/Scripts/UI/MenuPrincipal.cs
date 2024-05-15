using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    //[SerializeField] private AudioClip buttonSound;
    //[SerializeField] private AudioSource audiosource;
    [SerializeField] private GameObject menuPrincipal;
    [SerializeField] private GameObject sistemaCarriles;
    [SerializeField] private GameObject laseres;

    public void ChangeScene(string nameScene)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(nameScene);
        //audiosource.clip = buttonSound;
        //audiosource.Play();
    }
    void Start()
    {
        Application.targetFrameRate = 70;
    }

    public void QuitApp()
    {
        Application.Quit();
        Debug.Log("Se ha cerrado la aplicacion.");

    }
    public void Jugar()
    {
        menuPrincipal.SetActive(false);
        sistemaCarriles.SetActive(true);
        laseres.SetActive(true);
    }
}
