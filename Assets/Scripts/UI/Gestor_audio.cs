using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gestor_audio : GenericSingleton<Gestor_audio>
{
    [SerializeField] public AudioSource audioSourceMusica;
    [SerializeField] public AudioSource audioSourceSFX;
    [SerializeField] public AudioSource audioSourceJugador;
    [SerializeField] public AudioClip laserSFX;
    [SerializeField] public AudioClip naveSFX;
    [SerializeField] public AudioClip musicaMenu;
    [SerializeField] public AudioClip musicaJuego;
   
    void Start()
    {
        audioSourceMusica.clip = musicaMenu;
        audioSourceMusica.Play();
    }
    public void cambioSilenciadoMusica(bool isOn)
    {
        if(!isOn){audioSourceMusica.mute = true;}
        else{audioSourceMusica.mute = false;}
    }
    public void cambioSilenciadoSonido(bool isOn)
    {
        if(isOn){audioSourceSFX.mute = true;}
        else{audioSourceSFX.mute = false;}
    }
    
    public void ejecutarSFX(AudioClip audioClip)
    {
        audioSourceSFX.loop = false;
        audioSourceSFX.clip = audioClip;
        audioSourceSFX.Play();
        audioSourceSFX.PlayOneShot(audioClip);
    }
}
