using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GestorEscenas : MonoBehaviour
{
    public void CambiarEscena(string escena)
    {
        SceneManager.LoadScene(escena);
        Time.timeScale = 1f;
        //Gestor_audio.Instance.cambioSilenciadoMusica(false);
        //Gestor_audio.Instance.cambioSilenciadoSonido(true);
    }
    public void ReiniciarTutorial()
    {
        PlayerPrefs.SetInt("pasoTutorial", 0);
    }
}
