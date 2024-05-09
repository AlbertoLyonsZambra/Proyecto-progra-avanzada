using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardadoJugador : MonoBehaviour
{
    private int puntuacion;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Guardar()
    {
        // Guardar informacion
        PlayerPrefs.SetString("NOSE", "Valor");

        PlayerPrefs.SetInt("Puntuacion", puntuacion);
    }
    public void Cargar()
    {
        //Obtener informacion
        string stringNOSE = PlayerPrefs.GetString("NOSE", "defaultValue");

        puntuacion = PlayerPrefs.GetInt("Puntuacion");
    }
}
