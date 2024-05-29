using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisionLaser : MonoBehaviour
{
    [SerializeField] private GameObject MatRecogible;
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Laser"))
        {
            other.transform.parent.gameObject.SetActive(false); //"Destruye" laser
            // gameObject.SetActive(false); //Desactiva la visibilidad del obstaculo
        }
        if (other.gameObject.CompareTag("Laser") && (tag == "MatNormal" || tag == "MatRaro" || tag == "MatSuper"))
        {
            Gestor_audio.Instance.EjecutarAudio(Gestor_audio.Instance.audioSourceSFX, Gestor_audio.Instance.asteroideRomperSFX);
            other.transform.parent.gameObject.SetActive(false);
            Instantiate(MatRecogible, transform.position, transform.rotation);
            gameObject.SetActive(false);
        }
    }
}
