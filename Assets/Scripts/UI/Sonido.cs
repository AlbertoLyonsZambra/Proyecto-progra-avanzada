using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sonido : MonoBehaviour
{
    [SerializeField] private Sprite On;
    [SerializeField] private Sprite Off;
    [SerializeField] private Button boton;
    void Start()
    {
        if(PlayerPrefs.GetInt("isSFXMute") == 1){boton.image.sprite = Off;}
        else{boton.image.sprite = On;}
    }
 
    public void cambioSilenciadoSonido()
    {
        if(PlayerPrefs.GetInt("isSFXMute") == 1)
        {
            boton.image.sprite = On;
            Gestor_audio.Instance.cambioSilenciadoSonido(false);
        }
        else
        {
            boton.image.sprite = Off;
            Gestor_audio.Instance.cambioSilenciadoSonido(true);
        }
    }
}
