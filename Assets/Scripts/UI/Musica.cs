using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Musica : MonoBehaviour
{
    [SerializeField] private Sprite On;
    [SerializeField] private Sprite Off;
    [SerializeField] private Button boton;
    void Start()
    {
        if(PlayerPrefs.GetInt("isMusicaMute") == 1){boton.image.sprite = Off;}
        else{boton.image.sprite = On;}
    }

    public void cambioSilenciadoMusica()
    {
        if(PlayerPrefs.GetInt("isMusicaMute") == 1)
        {
            boton.image.sprite = On;
            Gestor_audio.Instance.cambioSilenciadoMusica(false); 
        }
        else
        {
            boton.image.sprite = Off;
            Gestor_audio.Instance.cambioSilenciadoMusica(true); 
        }
    }
    
}
