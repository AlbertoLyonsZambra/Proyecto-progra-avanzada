using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ContadorMaterial : GenericSingleton<ContadorMaterial>
{
    [HideInInspector] public int contTriggerV = 0;
    [HideInInspector] public int contTriggerN = 0;
    [HideInInspector] public int contTriggerR = 0;
    [HideInInspector] public bool mostrandoV;
    [HideInInspector] public bool mostrandoN;
    [HideInInspector] public bool mostrandoR;
    private Vector3 rotacionRandom;
    [SerializeField] private Transform transformPantalla;
    private Vector3 posPantalla;
    private Vector3 posOriginal;
    void Start()
    {
        rotacionRandom = new Vector3(Random.Range(0.1f, 1f), Random.Range(0.1f, 1f), Random.Range(0.1f, 1f));
        posPantalla = transformPantalla.position;
        posOriginal = transform.position;
    }

    void Update()
    {
        Rotar();
        mostrarPantalla();
        
    }
    void Rotar(){transform.Rotate(rotacionRandom * 40f * Time.deltaTime, Space.Self);}
    public void mostrarPantalla()
    {       
        if(tag == "MatNormal" && mostrandoV)
        {print("ENTRO");
            transform.position = Vector3.MoveTowards(transform.position, posPantalla, 4f * Time.deltaTime);
            StartCoroutine(Ocultar());
        }
        else if(tag == "MatNormal" && !mostrandoV){transform.position = Vector3.MoveTowards(transform.position, posOriginal, 4f * Time.deltaTime);}
        if(tag == "MatRaro" && mostrandoR)
        {
            transform.position = Vector3.MoveTowards(transform.position, posPantalla, 4f * Time.deltaTime);
            StartCoroutine(Ocultar());
        }
        else if(tag == "MatRaro" && !mostrandoR){transform.position = Vector3.MoveTowards(transform.position, posOriginal, 4f * Time.deltaTime);}
        if(tag == "MatSuper" && mostrandoR)
        {
            transform.position = Vector3.MoveTowards(transform.position, posPantalla, 4f * Time.deltaTime);
            StartCoroutine(Ocultar());
        }
        else if(tag == "MatSuper" && !mostrandoR){transform.position = Vector3.MoveTowards(transform.position, posOriginal, 4f * Time.deltaTime);}
    }
    IEnumerator Ocultar()
    {
        yield return new WaitForSeconds(5f);
        // transform.position = Vector3.MoveTowards(transform.position, posOriginal, 5f * Time.deltaTime);
        mostrandoV = false;
        print("welta");
    }
}
