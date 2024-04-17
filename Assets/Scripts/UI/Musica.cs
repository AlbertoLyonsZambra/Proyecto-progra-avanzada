using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Musica : MonoBehaviour
{
    private Sprite On;
    [SerializeField] private Sprite Off;
    [SerializeField] private Button boton;
    private bool isOn = true;
    [SerializeField] AudioSource audioSource;

    void Start()
    {
        On = boton.image.sprite;
    }

    public void cambioSilenciado()
    {
        if(isOn)
        {
            boton.image.sprite = Off;
            isOn = false;
            audioSource.mute = true;
        }
        else
        {
            boton.image.sprite = On;
            isOn = true;
            audioSource.mute = false;
        }
    }
}
