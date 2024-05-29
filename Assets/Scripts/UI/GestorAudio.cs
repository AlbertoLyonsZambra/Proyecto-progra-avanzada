using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Text.RegularExpressions;

public class Gestor_audio : GenericSingleton<Gestor_audio>
{
    [SerializeField] public AudioSource audioSourceMusica;
    [SerializeField] public AudioSource audioSourceSFX;
    [SerializeField] public AudioClip laserSFX;
    [SerializeField] public AudioClip naveSFX;
    [SerializeField] public AudioClip bateriaSFX;
    [SerializeField] public AudioClip asteroideRomperSFX;
    [SerializeField] public AudioClip recogerMaterialSFX;
    [SerializeField] public AudioClip musicaMenu;
    [SerializeField] public AudioClip musicaTienda;
    [SerializeField] public AudioClip musicaJuego;
    
    void Start()
    {
        audioSourceMusica.mute = (PlayerPrefs.GetInt("isMusicaMute") == 1);
        audioSourceSFX.mute = (PlayerPrefs.GetInt("isSFXMute") == 1);
        EjecutarAudio(audioSourceMusica, musicaMenu);
        // if(audioSourceMusica.mute){audioSourceMusica.clip = musicaMenu; audioSourceMusica.Play();}
    }
    public void cambioSilenciadoMusica(bool isOn)
    {
        if(isOn){audioSourceMusica.mute = true; PlayerPrefs.SetInt("isMusicaMute", 1);}
        else{audioSourceMusica.mute = false; PlayerPrefs.SetInt("isMusicaMute", 0);}
    }
    public void silenciadoMuerte()
    {
        audioSourceMusica.mute = true;
    }
    public void cambioSilenciadoSonido(bool isOn)
    {
        if(isOn){audioSourceSFX.mute = true; PlayerPrefs.SetInt("isSFXMute", 1);}
        else{audioSourceSFX.mute = false; PlayerPrefs.SetInt("isSFXMute", 0);}
    }
    public void EjecutarAudio(AudioSource audiosource, AudioClip audioClip)
    {
        if(audiosource.mute){audiosource.clip = audioClip; audiosource.Play();}
        if(audiosource == audioSourceMusica)
        {
            if(!audioSourceMusica.mute)
            {
                audioSourceMusica.Stop();
                audioSourceMusica.loop = true;
                audioSourceMusica.clip = audioClip;
                audioSourceMusica.Play();
            }
        }
        else 
        {
            if(!audioSourceSFX.mute)
            {
                // audioSourceSFX.Stop();
                audioSourceSFX.loop = false;
                audioSourceSFX.clip = audioClip;
                audioSourceSFX.PlayOneShot(audioClip);
            }
        }
    }
}
