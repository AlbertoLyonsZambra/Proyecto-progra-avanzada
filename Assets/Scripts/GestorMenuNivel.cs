using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestorMenuNivel : MonoBehaviour
{
    [SerializeField] private AudioClip[] musicasNivel;
    [SerializeField] private Material[] skyboxNiveles;
    [SerializeField] private GameObject[] planetasInicio;
    [SerializeField] private GameObject planetaInicial;
    private GameObject planetaNuevo;
    void Start()
    {
        AudioClip musicaPaPoner = musicasNivel[0];
        Material skyBoxPaPoner = skyboxNiveles[0];
        int nivel = MenuPrincipal.Instance.nivelActual;

        bool puedeAparecer = true;
        Vector3 posicion = planetaInicial.transform.position;
        Vector3 escala = planetaInicial.transform.localScale;
        GameObject planeta = planetaInicial;

        if (nivel == 0) { planetaInicial.SetActive(false); puedeAparecer = false; }
        if (nivel == 1)
        {
            musicaPaPoner = musicasNivel[1];
            skyBoxPaPoner = skyboxNiveles[1];
            planeta = planetasInicio[0];
        }
        else if (nivel == 2)
        {
            musicaPaPoner = musicasNivel[2];
            skyBoxPaPoner = skyboxNiveles[2];
            planeta = planetasInicio[1];
        }
        else if (nivel == 3)
        {
            musicaPaPoner = musicasNivel[3];
            skyBoxPaPoner = skyboxNiveles[3];
            planeta = planetasInicio[2];
        }
        else if (nivel >= 4)
        {
            musicaPaPoner = musicasNivel[4];
            skyBoxPaPoner = skyboxNiveles[4];
            planeta = planetasInicio[3];
        }
        if (puedeAparecer)
        {
            planetaNuevo = Instantiate(planeta, posicion, Quaternion.identity);
            planeta.transform.localScale = escala;
            planetaNuevo.tag = "Obs_Asteroide";
        }
        RenderSettings.skybox = skyBoxPaPoner;
        planetaInicial.SetActive(false);
        Gestor_audio.Instance.EjecutarAudio(Gestor_audio.Instance.audioSourceMusica, musicaPaPoner);
    }
    private void Update()
    {
        if (MenuPrincipal.Instance.jugando)
        {
            Vector3 velocidadPlataforma = Vector3.back * 6 * Time.deltaTime;
            planetaNuevo.transform.Translate(velocidadPlataforma);
        }
    }
}
