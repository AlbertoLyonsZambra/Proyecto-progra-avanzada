using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestorTaller : GenericSingleton<GestorTaller>
{
    [SerializeField] public GameObject naves;
    [SerializeField] public GameObject navesJugador;
    [SerializeField] private Material materialBloqueado;
    [HideInInspector] public List<GameObject> nave;
    
    void Start()
    {
        nave = new List<GameObject>();
        foreach (Transform hijo in naves.transform){nave.Add(hijo.gameObject);}
        ActualizarNavesTaller();
    }

    void Update()
    {
    }

    public void ActualizarNavesTaller()
    {
        if (JugadorNivel.Instance.nivelActual >= 0 && JugadorNivel.Instance.nivelActual < nave.Count)
        {
            for(int i = 0; i <= JugadorNivel.Instance.nivelActual; i++){nave[i].SetActive(true);}
            if (JugadorNivel.Instance.nivelActual + 1 < nave.Count)
            {
                nave[JugadorNivel.Instance.nivelActual + 1].transform.Find("default").GetComponent<MeshRenderer>().material = materialBloqueado;
                nave[JugadorNivel.Instance.nivelActual + 1].SetActive(true);
            }
            else{print(" Tienes todas las naves ");}
        }
        else{print(" Falta validar esto por ahora (GestorTaller.cs) ");}
    }
}
