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
    void Start()
    {
        AudioClip musicaPaPoner = musicaNivel0;
        int nivel = PlayerPrefs.GetInt("nivelActual");
        if (nivel == 1)
        {
            musicaPaPoner = musicaNivel1;
        }
        else if (nivel == 2)
        {
            musicaPaPoner = musicaNivel2;
        }
        else if (nivel == 3)
        {
            musicaPaPoner = musicaNivel3;
        }
        else if (nivel >= 4)
        {
            musicaPaPoner = musicaNivel4;
        }
        Gestor_audio.Instance.EjecutarAudio(Gestor_audio.Instance.audioSourceMusica, musicaPaPoner);
    }
}
