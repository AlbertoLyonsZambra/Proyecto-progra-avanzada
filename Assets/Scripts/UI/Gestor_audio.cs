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
    public bool isMusicaMute;
    public bool isSFXMute;
   
    void Start()
    {
        audioSourceMusica.clip = musicaMenu;
        audioSourceMusica.Play();
    }
    public void cambioSilenciadoMusica(bool isOn)
    {
        if(isOn){audioSourceMusica.mute = true; isMusicaMute = true;}
        else{audioSourceMusica.mute = false; isMusicaMute = true;}
    }
    public void cambioSilenciadoSonido(bool isOn)
    {
        if(isOn){audioSourceSFX.mute = true; isSFXMute = false;}
        else{audioSourceSFX.mute = false; isMusicaMute = false;}
    }
    
    public void ejecutarSFX(AudioClip audioClip)
    {
        if(isSFXMute)
        {
        audioSourceSFX.loop = false;
        audioSourceSFX.clip = audioClip;
        audioSourceSFX.PlayOneShot(audioClip);
        }
    }
    public void ejecutarMusica(AudioClip audioClip)
    {
        if(isMusicaMute)
        {
        audioSourceMusica.loop = true;
        audioSourceMusica.clip = audioClip;
        audioSourceMusica.Play();
        }
    }
}
