using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ContadorMaterial : MonoBehaviour
{
    // [HideInInspector] public int contTriggerV = 0;
    // [HideInInspector] public int contTriggerN = 0;
    // [HideInInspector] public int contTriggerR = 0;
    // [HideInInspector] public bool mostrandoV = false;
    // [HideInInspector] public bool mostrandoN = false;
    // [HideInInspector] public bool mostrandoR = false;
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
    void Rotar()
    {
        if(tag != "numero"){transform.Rotate(rotacionRandom * 40f * Time.deltaTime, Space.Self);}
    }
    public void mostrarPantalla()
    {
        if(tag == "MatNormal" && JugadorNivel.Instance.mostrandoV)
        {
            transform.position = Vector3.MoveTowards(transform.position, posPantalla, 4f * Time.deltaTime);
            StartCoroutine(Ocultar("V"));
        }
        else if(tag == "MatNormal" && !JugadorNivel.Instance.mostrandoV){transform.position = Vector3.MoveTowards(transform.position, posOriginal, 4f * Time.deltaTime);}
        
        if(tag == "MatRaro" && JugadorNivel.Instance.mostrandoN)
        {
            transform.position = Vector3.MoveTowards(transform.position, posPantalla, 4f * Time.deltaTime);
            StartCoroutine(Ocultar("N"));
        }
        else if(tag == "MatRaro" && !JugadorNivel.Instance.mostrandoN){transform.position = Vector3.MoveTowards(transform.position, posOriginal, 4f * Time.deltaTime);}
        
        if(tag == "MatSuper" && JugadorNivel.Instance.mostrandoR)
        {
            transform.position = Vector3.MoveTowards(transform.position, posPantalla, 4f * Time.deltaTime);
            StartCoroutine(Ocultar("R"));
        }
        else if(tag == "MatSuper" && !JugadorNivel.Instance.mostrandoR){transform.position = Vector3.MoveTowards(transform.position, posOriginal, 4f * Time.deltaTime);}
    }
    IEnumerator Ocultar(string tipoMat)
    {
        yield return new WaitForSeconds(5f);
        transform.position = Vector3.MoveTowards(transform.position, posOriginal, 5f * Time.deltaTime);
        if(tipoMat == "V"){JugadorNivel.Instance.mostrandoV = false;}
        if(tipoMat == "N"){JugadorNivel.Instance.mostrandoN = false;}
        if(tipoMat == "R"){JugadorNivel.Instance.mostrandoR = false;}
        
    }
}
