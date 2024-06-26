using UnityEngine;
using UnityEngine.SceneManagement;
public class GestorEscenas : MonoBehaviour
{
    public GameObject finalNivel;
    public GameObject destino;
    public GameObject boton;
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
    public void AdelantarFinal()
    {
        finalNivel.SetActive(true);
        destino.transform.localPosition = new Vector3(-12.79897f, -7.84f, 567);
        boton.SetActive(false);

    }
}
