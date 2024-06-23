using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestorMenuNivel : MonoBehaviour
{
    [SerializeField] private AudioClip musicaNivel0;
    [SerializeField] private AudioClip musicaNivel1;
    [SerializeField] private AudioClip musicaNivel2;
    [SerializeField] private AudioClip musicaNivel3;
    [SerializeField] private AudioClip musicaNivel4;
    [SerializeField] private Material skyboxNivel0;
    [SerializeField] private Material skyboxNivel1;
    [SerializeField] private Material skyboxNivel2;
    [SerializeField] private Material skyboxNivel3;
    [SerializeField] private Material skyboxNivel4;
    void Start()
    {
        AudioClip musicaPaPoner = musicaNivel0;
        Material skyBoxPaPoner = skyboxNivel0;
        int nivel = MenuPrincipal.Instance.nivelActual;
        if (nivel == 1)
        {
            musicaPaPoner = musicaNivel1;
            skyBoxPaPoner = skyboxNivel1;
        }
        else if (nivel == 2)
        {
            musicaPaPoner = musicaNivel2;
            skyBoxPaPoner = skyboxNivel2;
        }
        else if (nivel == 3)
        {
            musicaPaPoner = musicaNivel3;
            skyBoxPaPoner = skyboxNivel3;
        }
        else if (nivel >= 4)
        {
            musicaPaPoner = musicaNivel4;
            skyBoxPaPoner = skyboxNivel4;
        }
        Gestor_audio.Instance.EjecutarAudio(Gestor_audio.Instance.audioSourceMusica, musicaPaPoner);
        if (skyBoxPaPoner != null)
        {
            RenderSettings.skybox = skyBoxPaPoner;
        }
        else
        {
            Debug.LogWarning("No hay skybox seleccionada");
        }
    }
}
