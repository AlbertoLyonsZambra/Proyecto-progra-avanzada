using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GestorVida : GenericSingleton<GestorVida>
{
    public int vida;
    public GameObject[] corazones;
    public string tallerScene = "juego";
    [SerializeField] private GameObject hasMuertoTexto;
    [SerializeField] private GameObject prontoTexto;
    public bool hasMuerto = false;
    // Start is called before the first frame update
    void Start()
    {
        vida = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (vida == 3)
        {
            corazones[0].SetActive(true);
            corazones[1].SetActive(true);
            corazones[2].SetActive(true);
        }
        else if (vida == 2) 
        {
            corazones[0].SetActive(true);
            corazones[1].SetActive(true);
            corazones[2].SetActive(false);
        }
        else if (vida == 1)
        {
            corazones[0].SetActive(true);
            corazones[1].SetActive(false);
            corazones[2].SetActive(false);
        }

        else if (vida == 0)
        {
            corazones[0].SetActive(false);
            corazones[1].SetActive(false);
            corazones[2].SetActive(false);
            hasMuertoTexto.SetActive(true);
            prontoTexto.SetActive(true);
            hasMuerto = true;
            Die();
        }
    }

    void Die()
    {
       
        // Pausar el juego
        Time.timeScale = 0;
        // Cambiar a la escena de Game Over después de un pequeño retraso para mostrar la pausa
        StartCoroutine(LoadGameOverScene());
    }

    IEnumerator LoadGameOverScene()
    {
        yield return new WaitForSecondsRealtime(3); // Puedes ajustar el tiempo de retraso según sea necesario
        Time.timeScale = 1; // Asegurarte de reanudar el tiempo antes de cambiar de escena
        SceneManager.LoadScene(tallerScene);
    }


}
