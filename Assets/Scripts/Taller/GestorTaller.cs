using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestorTaller : GenericSingleton<GestorTaller>
{
    [SerializeField] public GameObject navesTaller;
    [SerializeField] public GameObject navesJugador;
    [SerializeField] private Material materialBloqueado;
    [HideInInspector] public List<GameObject> nave;
    [HideInInspector] public Transform ultimaNaveJugador;
    [HideInInspector] public Transform ultimaNaveTaller;
    private bool[] elevados;
    
    void Start()
    {
        nave = new List<GameObject>();
        foreach (Transform hijo in navesTaller.transform){nave.Add(hijo.gameObject);}
        elevados = new bool[nave.Count];
        for (int i = 0; i < elevados.Length; i++){elevados[i] = false;}
        ActualizarNavesTaller();
    }

    void Update()
    {
    }

    public void ActualizarNavesTaller()
    {
        if (MenuPrincipal.Instance.nivelActual >= 0 && MenuPrincipal.Instance.nivelActual < nave.Count)
        {
            for(int i = 0; i <= MenuPrincipal.Instance.nivelActual; i++){nave[i].SetActive(true);}
            if (MenuPrincipal.Instance.nivelActual + 1 < nave.Count)
            {
                nave[MenuPrincipal.Instance.nivelActual + 1].transform.Find("default").GetComponent<MeshRenderer>().material = materialBloqueado;
                nave[MenuPrincipal.Instance.nivelActual + 1].SetActive(true);
            }
            else{print(" Tienes todas las naves ");}
        }
        else{print(" Falta validar esto por ahora (GestorTaller.cs) ");}
    }
    public void SeleccionNave(RaycastHit colision)
    {
        if(MenuPrincipal.Instance.enTaller)
        {
            ultimaNaveJugador = navesJugador.transform.Find(colision.collider.gameObject.transform.parent.name);
            ultimaNaveTaller = navesTaller.transform.Find(colision.collider.gameObject.transform.parent.name);
            NaveSeleccionada();
            JugadorNivel.Instance.escogioNave = true;
        }
    }
    public void NaveSeleccionada()
    {
        if(!elevados[int.Parse(ultimaNaveTaller.name)] && ultimaNaveTaller.Find("default").GetComponent<MeshRenderer>().material.color != materialBloqueado.color)
        {
            StartCoroutine(Elevar(ultimaNaveTaller, elevados[int.Parse(ultimaNaveTaller.name)]));
            for(int i = 0; i <= MenuPrincipal.Instance.nivelActual; i++)
            {
                if(nave[i].transform.name == ultimaNaveTaller.name){continue;}
                if(elevados[int.Parse(nave[i].transform.name)]){StartCoroutine(Elevar(nave[i].transform, elevados[int.Parse(ultimaNaveTaller.name)]));}
            }
        }
    }
    IEnumerator Elevar(Transform objeto, bool elevado)
    {
        Vector3 elevacion = Vector3.up * 0.15f;
        elevados[int.Parse(objeto.name)] = !elevado;
        if(elevado){elevacion *= -1f; nave[int.Parse(objeto.name)].transform.Find("Luz").gameObject.SetActive(false);}
        else{nave[int.Parse(objeto.name)].transform.Find("Luz").gameObject.SetActive(true);}
        float tiempoTranscurrido = 0f;
        while (tiempoTranscurrido < 0.5f)
        {
            objeto.Translate(elevacion * Time.deltaTime / 0.5f, Space.World);
            tiempoTranscurrido += Time.deltaTime;
            yield return null;
        }
    }
}
