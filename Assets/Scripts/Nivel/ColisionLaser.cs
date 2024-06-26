using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisionLaser : MonoBehaviour
{
    [SerializeField] private GameObject MatRecogible;
    private int cantLasers = 0;
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Dictionary<string, int> laserIncrementos = new Dictionary<string, int>{{ "LaserRojo", 1 }, { "LaserAzul", 3 }};
        if (laserIncrementos.TryGetValue(other.gameObject.name, out int incremento)){cantLasers += incremento;}
        if (other.gameObject.CompareTag("LaserMorado"))
        {
            if (tag == "Obs_Asteroide") { RomperMaterial(other, "Asteroide"); }
            if (tag == "MatNormal") { RomperMaterial(other, ""); }
            else if (tag == "MatRaro") { RomperMaterial(other, ""); }
            else if (tag == "MatSuper") { RomperMaterial(other, ""); }
        }
        else if (other.gameObject.CompareTag("Laser"))
        {
            other.transform.parent.gameObject.SetActive(false); // "Destruye" laser
            if (tag == "MatNormal" && cantLasers % 3 == 0) { RomperMaterial(other, ""); }
            else if (tag == "MatRaro" && cantLasers >= 5) { RomperMaterial(other, ""); }
            else if (tag == "MatSuper" && cantLasers % 9 == 0) { RomperMaterial(other, ""); }
            else if (tag == "MatTutorial" && cantLasers == 1) { RomperMaterial(other,""); }
           
        }
        
    }
    private void RomperMaterial(Collider other, string nombre)
    {
        cantLasers = 0;
        other.transform.parent.gameObject.SetActive(false);
        Gestor_audio.Instance.EjecutarAudio(Gestor_audio.Instance.audioSourceSFX, Gestor_audio.Instance.asteroideRomperSFX);
        gameObject.SetActive(false);
        if(!(nombre == "Asteoride"))
        {
            Instantiate(MatRecogible, transform.position, transform.rotation);
        }
        if (PlayerPrefs.GetInt("pasoTutorial") == 0) 
        {
            Tutorial.Instance.IniciarJuego();
        }
    }
    
}
