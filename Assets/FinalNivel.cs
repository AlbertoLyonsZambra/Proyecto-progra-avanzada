using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalNivel : GenericSingleton<FinalNivel>
{
    [SerializeField] private GameObject pantallaFinal;
    [HideInInspector] public bool victoria = false;
    private Vector3 plataforma;

    public void Victoria()
    {
        Vector3 diferencia = new Vector3(-1.89f, -1.22f, 3.56f);
        plataforma = transform.Find("Destino").position - diferencia;
        int nivelActual = PlayerPrefs.GetInt("nivelActual");
        victoria = true;
        if (nivelActual < 4)
        {
            PlayerPrefs.SetInt("nivelActual", nivelActual + 1);
        }
        nivelActual = PlayerPrefs.GetInt("nivelActual");
        GameObject.Find("Sistema carriles").transform.Find("Instanciador_objetos").gameObject.SetActive(false);
        GameObject.Find("Animaciones").gameObject.SetActive(false);
        JugadorNivel.Instance.enabled = false;
        MovimientoCarriles.Instance.enabled = false;
        InstanciadorObjetos.Instance.enabled = false;
        MatrizCarriles.Instance.enabled = false;
        StartCoroutine(MostrarPantallaFinal(1));
    }
    IEnumerator MostrarPantallaFinal(int segundos)
    {
        yield return new WaitForSeconds(segundos);
        pantallaFinal.SetActive(true);
        Time.timeScale = 0;
    }
    void Update()
    {
        if (victoria) 
        {
            MovimientoCarriles.Instance.MoverPlataformaFinal(plataforma);
        }
    }
}
