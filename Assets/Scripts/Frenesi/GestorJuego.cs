using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GestorJuego : GenericSingleton<GestorJuego>
{
    [HideInInspector] public int nivelActual;
    [SerializeField] private Material skyboxNivel0;
    [SerializeField] private Material skyboxNivel1;
    [SerializeField] private Material skyboxNivel2;
    [SerializeField] private Material skyboxNivel3;
    [SerializeField] private Material skyboxNivel4;

    [SerializeField] private Material sueloNivel0;
    [SerializeField] private Material sueloNivel1;
    [SerializeField] private Material sueloNivel2;
    [SerializeField] private Material sueloNivel3;
    [SerializeField] private Material sueloNivel4;

    [SerializeField] private Material rocaNivel0;
    [SerializeField] private Material rocaNivel1;
    [SerializeField] private Material rocaNivel2;
    [SerializeField] private Material rocaNivel3;
    [SerializeField] private Material rocaNivel4;

    [SerializeField] private GameObject sueloPrefab;
    [SerializeField] private GameObject rocaPrefab;
    [SerializeField] private GameObject[] naves;
    [SerializeField] private GameObject naveMapa;
    public bool oleadas;

    [SerializeField] private GameObject panel;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        panel.SetActive(true);
        StartCoroutine(ExecuteOnEndAfterDelay(2f));
        // Skybox setup
        Material skyBoxPaPoner = skyboxNivel0;
        Material sueloPaPoner = sueloNivel0;
        Material rocaPaPoner = rocaNivel0;
        int nivel = PlayerPrefs.GetInt("nivelActual");
        oleadas = false;

        if (nivel == 0)
        {
            skyBoxPaPoner = skyboxNivel1;
            sueloPaPoner = sueloNivel1;
            rocaPaPoner = rocaNivel1;
            naveMapa = naves[0];
            naveMapa.SetActive(true);
        }
        else if (nivel == 1)
        {
            skyBoxPaPoner = skyboxNivel2;
            sueloPaPoner = sueloNivel2;
            rocaPaPoner = rocaNivel2;
            naveMapa = naves[1];
            naveMapa.SetActive(true);
        }
        else if (nivel == 2)
        {
            skyBoxPaPoner = skyboxNivel3;
            sueloPaPoner = sueloNivel3;
            rocaPaPoner = rocaNivel3;
            naveMapa = naves[2];
            naveMapa.SetActive(true);
        }
        else if (nivel == 3)
        {
            skyBoxPaPoner = skyboxNivel4;
            sueloPaPoner = sueloNivel4;
            rocaPaPoner = rocaNivel4;
            naveMapa = naves[3];
            naveMapa.SetActive(true);
        }

        else if (nivel > 3)
        {
            oleadas = true;
        }

        if (skyBoxPaPoner != null)
        {
            RenderSettings.skybox = skyBoxPaPoner;
        }
        else
        {
            Debug.LogWarning("No hay skybox seleccionada");
        }

        if (sueloPaPoner != null && sueloPrefab != null)
        {
            ChangeMaterialOfChildren(sueloPrefab, sueloPaPoner);
        }
        else
        {
            Debug.LogWarning("No hay material de suelo seleccionado o prefab del suelo no asignado");
        }

        if (rocaPaPoner != null && rocaPrefab != null)
        {
            ChangeMaterialOfChildren(rocaPrefab, rocaPaPoner);
        }
        else
        {
            Debug.LogWarning("No hay material de roca seleccionado o prefab de roca no asignado");
        }
    }

    void ChangeMaterialOfChildren(GameObject parentObject, Material newMaterial)
    {
        MeshRenderer[] meshRenderers = parentObject.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer meshRenderer in meshRenderers)
        {
            meshRenderer.material = newMaterial;
        }
    }

    private IEnumerator ExecuteOnEndAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        panel.SetActive(false);

    }
}
