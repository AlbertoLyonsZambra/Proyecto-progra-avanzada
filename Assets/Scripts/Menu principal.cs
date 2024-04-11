using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private AudioClip buttonSound;
    [SerializeField] private AudioSource audiosource;

    public void ChangeScene(string nameScene)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(nameScene);
        audiosource.clip = buttonSound;
        audiosource.Play();
    }
    public void QuitApp()
    {
        Application.Quit();
        Debug.Log("Se ha cerrado la aplicacion.");

    }
}
