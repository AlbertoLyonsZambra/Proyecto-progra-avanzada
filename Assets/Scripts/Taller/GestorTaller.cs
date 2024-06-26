using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GestorTaller : GenericSingleton<GestorTaller>
{
    [Header("Taller")]
    [SerializeField] public GameObject navesTaller;
    [SerializeField] public GameObject navesJugador;
    [SerializeField] public GameObject seleccionarNave;
    [SerializeField] private Material materialBloqueado;
    [HideInInspector] public List<GameObject> nave;
    [HideInInspector] public Transform ultimaNaveJugador;
    [HideInInspector] public Transform ultimaNaveTaller;
    private bool[] elevados; 

    [Header("Terminal")]
    [SerializeField] private GameObject Materiales;
    private TextMeshProUGUI textoVerde; 
    private TextMeshProUGUI textoNaranja; 
    private TextMeshProUGUI textoRosa; 
    
    void Start() 
    {
        if (Materiales != null)
        {
            textoVerde = Materiales.transform.Find("Verde").Find("Cantidad").GetComponent<TextMeshProUGUI>();
            textoNaranja = Materiales.transform.Find("Naranja").Find("Cantidad").GetComponent<TextMeshProUGUI>();
            textoRosa = Materiales.transform.Find("Rosa").Find("Cantidad").GetComponent<TextMeshProUGUI>();
        }
        nave = new List<GameObject>();
        foreach (Transform hijo in navesTaller.transform){nave.Add(hijo.gameObject);}
        elevados = new bool[nave.Count];
        for (int i = 0; i < elevados.Length; i++){elevados[i] = false;}

        CheckVictory();
        ActualizarMatsTerminal();
        
    }

    void Update()
    {
    }

    void CheckVictory()
    {
        // Obtener el estado de victoria desde PlayerPrefs
        if (PlayerPrefs.GetInt("Victoria", 0) == 1)
        {
            // Si hay victoria, actualizar las naves del taller
            ActualizarNavesTaller(MenuPrincipal.Instance.nivelActual + 1);
        }
        else
        {
            // Si no hay victoria, mantener las naves en su estado actual (probablemente bloqueadas)
            
            ActualizarNavesTaller((MenuPrincipal.Instance.nivelActual)); // Mantener el �ltimo nivel desbloqueado
        }
    }

    public void ActualizarNavesTaller(int nivelDesbloqueado)
    {
        // Asegurarse de que el nivel desbloqueado est� dentro de los l�mites de la lista de naves
        if (nivelDesbloqueado >= 0 && nivelDesbloqueado < nave.Count)
        {
            // Activar las naves hasta el nivel desbloqueado
            for (int i = 0; i <= nivelDesbloqueado; i++)
            {
                nave[i].SetActive(true);
            }

            // Bloquear la siguiente nave si no se han desbloqueado todas
            if (nivelDesbloqueado + 1 < nave.Count)
            {
                nave[nivelDesbloqueado + 1].transform.Find("default").GetComponent<MeshRenderer>().material = materialBloqueado;
                nave[nivelDesbloqueado + 1].SetActive(true);
            }
            else
            {
                Debug.Log("Tienes todas las naves desbloqueadas.");
            }
        }
        else
        {
            Debug.LogError("El nivel desbloqueado est� fuera de los l�mites de la lista de naves.");
        }
    }

    public void SeleccionNave(RaycastHit colision)
    {
        if(MenuPrincipal.Instance.enTaller && !MenuPrincipal.Instance.enTerminal)
        {
            ultimaNaveTaller = navesTaller.transform.Find(colision.collider.gameObject.transform.parent.name);
            NaveSeleccionada();
            if(ultimaNaveTaller.Find("default").GetComponent<MeshRenderer>().material.color != materialBloqueado.color)
            {
                ultimaNaveJugador = navesJugador.transform.Find(colision.collider.gameObject.transform.parent.name);
                JugadorNivel.Instance.escogioNave = true;
            }
        }
    }

    public void NaveSeleccionada()
    {
        if(ultimaNaveTaller == null) {return;}
        if(MenuPrincipal.Instance.enTerminal)
        {
            // if(ultimaNaveTaller.Find("default").GetComponent<MeshRenderer>().material.color != materialBloqueado.color)
            {
                // StartCoroutine(Elevar(ultimaNaveTaller, true));
                for(int i = 0; i <= MenuPrincipal.Instance.nivelActual; i++)
                {
                    // if(nave[i].transform.name == ultimaNaveTaller.name){continue;}
                    if(elevados[int.Parse(nave[i].transform.name)]){StartCoroutine(Elevar(nave[i].transform, true));}
                }
            }
        }
        else if(MenuPrincipal.Instance.enTaller) 
        {
            seleccionarNave.SetActive(false);
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
    public void guardarMats()
    {
        PlayerPrefs.SetInt("MatsTallerV", PlayerPrefs.GetInt("MatsTallerV") + PlayerPrefs.GetInt("MatsV"));
        PlayerPrefs.SetInt("MatsTallerN", PlayerPrefs.GetInt("MatsTallerN") + PlayerPrefs.GetInt("MatsN"));
        PlayerPrefs.SetInt("MatsTallerR", PlayerPrefs.GetInt("MatsTallerR") + PlayerPrefs.GetInt("MatsR"));
    }

    public void ActualizarMatsTerminal()
    {
        textoVerde.text = PlayerPrefs.GetInt("MatsTallerV").ToString();
        textoNaranja.text = PlayerPrefs.GetInt("MatsTallerN").ToString();
        textoRosa.text = PlayerPrefs.GetInt("MatsTallerR").ToString();
    }
}
