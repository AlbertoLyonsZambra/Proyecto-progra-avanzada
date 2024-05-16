using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sonido : MonoBehaviour
{
    private Sprite On;
    [SerializeField] private Sprite Off;
    [SerializeField] private Button boton;
    private bool isOn = true;
    void Start()
    {
        On = boton.image.sprite;
        Gestor_audio.Instance.audioSourceSFX.clip = Gestor_audio.Instance.musicaJuego;
    }
 
    public void cambioSilenciadoSonido()
    {
        if(isOn)
        {
            boton.image.sprite = Off;
            Gestor_audio.Instance.cambioSilenciadoSonido(isOn);
            isOn = false;
        }
        else
        {
            boton.image.sprite = On;
            Gestor_audio.Instance.cambioSilenciadoSonido(isOn);
            isOn = true;
        }
    }
}
