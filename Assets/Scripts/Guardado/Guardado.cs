using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guardado : GenericSingleton<Guardado>
{
    /*
    Si se agrega una nueva variable para guardar con los PlayerPrefs debe seguir este formato
    public tipoVariable nombreVariable;
    private const string variableKey = "variableKey";

    En GuardarPrefs():
    GuardarTipoVariable(variableKey, variable);

    En CargarPrefs():
    variable = CargarTipoVariable(variableKey, valorDefault);

    Hacer el metodo para guardar la variable
    public void SetVariable(tipoVariable valor)
    {
        tipoVariable = valor;
        GuardarTipoVariable(tipoVariableKey, valor);
    }

    Para OBTENER la variable solo usar Guardado.Instance.variable
    Para GUARDAR la variable solo usar Guardado.Instance.SetVariable(valor);
    */
    public int puntaje;
    public int ultimoNivel;
    public int naveActual;
    private const string puntajeKey = "puntaje";
    private const string ultimoNivelKey = "ultimoNivel";
    private const string naveActualKey = "naveActual";

    void Start()
    {
        CargarPrefs();
    }

    void OnApplicationQuit()
    {
        GuardarPrefs();
    }

    // Metodos para guardar y cargar
    public void GuardarPrefs()
    {
        GuardarInt(puntajeKey, puntaje);
        GuardarInt(ultimoNivelKey, ultimoNivel);
        GuardarInt(naveActualKey, naveActual);


        PlayerPrefs.Save();
    }
    public void CargarPrefs() 
    {
        puntaje = CargarInt(puntajeKey, 0);
        ultimoNivel = CargarInt(ultimoNivelKey, 1);
        naveActual = CargarInt(naveActualKey, 0);
    }
    // Complementa los dos metodos de arriba
    private void GuardarBool(string key, bool valor) { PlayerPrefs.SetInt(key, valor ? 1 : 0); }
    private bool CargarBool(string key, bool valorDefault) { return PlayerPrefs.GetInt(key, valorDefault ? 1 : 0) == 1; }
    private void GuardarInt(string key, int valor) { PlayerPrefs.SetInt(key, valor); }
    private int CargarInt(string key, int valorDefault) { return PlayerPrefs.GetInt(key, valorDefault); }
    private void GuardarString(string key, string valor) { PlayerPrefs.SetString(key, valor); }
    private string CargarString(string key, string valorDefault) { return PlayerPrefs.GetString(key, valorDefault); }

    // Usar estos para guardar, no usar PlayerPrefs.SetVariable()

    public void SetPuntaje(int punt)
    {
        puntaje = punt;
        GuardarInt(puntajeKey, punt);
    }

    public void SetUltimoNivel(int nivel)
    {
        ultimoNivel = nivel;
        GuardarInt(ultimoNivelKey, nivel);
    }

    public void SetNaveActual(int nave)
    {
        naveActual = nave;
        GuardarInt(naveActualKey, nave);
    }
}
