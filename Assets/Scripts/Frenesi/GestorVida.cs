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
    public string tallerScene = "Juego";
    [SerializeField] private GameObject hasMuertoTexto;
    [SerializeField] private GameObject prontoTexto;
    [SerializeField] private GameObject panel1;
    [SerializeField] private GameObject spawnerGameObject;
    [SerializeField] private GameObject particleSystemPrefab;
    [SerializeField] private GameObject reloj; 
    public bool hasMuerto;

    void Start()
    {
        vida = 3;
        hasMuerto = false;
    }

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
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            reloj.SetActive(false);

            // Instanciar el sistema de partículas en la posición del jugador con rotación -90 en X
            if (particleSystemPrefab != null && player != null)
            {
                Vector3 particlePosition = player.transform.position;
                Quaternion particleRotation = Quaternion.Euler(-90, 0, 0);
                Instantiate(particleSystemPrefab, particlePosition, particleRotation);
            }

            Destroy(player);
            DestroyAllEnemies();
            StartCoroutine(ExecuteOnEndAfterDelay1(1f));
            StartCoroutine(ExecuteOnEndAfterDelay(4f));
        }
    }

    private IEnumerator ExecuteOnEndAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        Die();
    }

    private IEnumerator ExecuteOnEndAfterDelay1(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        panel1.SetActive(true);
    }

    void Die()
    {
        Time.timeScale = 0;
        StartCoroutine(LoadGameOverScene());
        panel1.SetActive(true);
    }

    IEnumerator LoadGameOverScene()
    {
        yield return new WaitForSecondsRealtime(3);
        Time.timeScale = 1;
        SceneManager.LoadScene(tallerScene);
    }

    private void DestroyAllEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
        if (spawnerGameObject != null)
        {
            spawnerGameObject.SetActive(false);
        }
    }
}
