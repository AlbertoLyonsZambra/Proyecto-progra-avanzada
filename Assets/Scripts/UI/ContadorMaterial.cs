using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ContadorMaterial : MonoBehaviour
{
    private Vector3 rotacionRandom;
    [SerializeField] private Transform transformPantalla;
    private Vector3 posPantalla;
    private Vector3 posOriginal;
    [SerializeField] private GameObject[] numeros;
    private Quaternion originalRotation;
    void Start()
    {
        rotacionRandom = new Vector3(Random.Range(0.1f, 1f), Random.Range(0.1f, 1f), Random.Range(0.1f, 1f));
        posPantalla = transformPantalla.position;
        posOriginal = transform.position;
        originalRotation = transform.rotation;
    }

    void Update()
    {
        Rotar();
        mostrarPantalla();
        
    }
    void Rotar()
    {
        if(!tag.Contains("numero")){transform.Rotate(rotacionRandom * 40f * Time.deltaTime, Space.Self);} // Rotacion de los cubos
        else
        {// Movimiento del numero
            // float angleX = Mathf.Sin(Time.time * 2f) * 5f;
            // float angleY = Mathf.Sin(Time.time * 2f * 2.5f) * 10f;
            // float angleZ = Mathf.Sin(Time.time * 2f * 1.2f) * 4f;
            // Quaternion slightRotationX = Quaternion.AngleAxis(angleX, Vector3.right);
            // Quaternion slightRotationY = Quaternion.AngleAxis(angleY, Vector3.up);
            // Quaternion slightRotationZ = Quaternion.AngleAxis(angleZ, Vector3.forward);
            // transform.rotation = originalRotation * slightRotationX * slightRotationY * slightRotationZ;
        }
    }
    public void mostrarPantalla()
    {
        if( tag == "numeroV")
        {
            if(JugadorNivel.Instance.mostrandoV)
            {
                // if(tag=="numeroV"){transform.position = Vector3.MoveTowards(transform.position, posPantalla, 4f * Time.deltaTime);}
                transform.position = Vector3.MoveTowards(transform.position, posPantalla, 4f * Time.deltaTime);
                StartCoroutine(Ocultar("V"));
            }
            else
            {
                // if(tag=="numeroV"){transform.position = Vector3.MoveTowards(transform.position, posPantalla, 4f * Time.deltaTime);}
                transform.position = Vector3.MoveTowards(transform.position, posOriginal, 4f * Time.deltaTime);
            }
        }
        else if( tag == "numeroN")
        {
            if(JugadorNivel.Instance.mostrandoN)
            {
                // if(tag=="numeroN"){transform.position = Vector3.MoveTowards(transform.position, posPantalla, 4f * Time.deltaTime);}
                transform.position = Vector3.MoveTowards(transform.position, posPantalla, 4f * Time.deltaTime);
                StartCoroutine(Ocultar("N"));
            }
            else
            {
                // if(tag=="numeroN"){transform.position = Vector3.MoveTowards(transform.position, posPantalla, 4f * Time.deltaTime);}
                transform.position = Vector3.MoveTowards(transform.position, posOriginal, 4f * Time.deltaTime);
            }
        }
        else if( tag == "numeroR")
        {
            if(JugadorNivel.Instance.mostrandoR)
            {
                // if(tag=="numeroR"){transform.position = Vector3.MoveTowards(transform.position, posPantalla, 4f * Time.deltaTime);}
                transform.position = Vector3.MoveTowards(transform.position, posPantalla, 4f * Time.deltaTime);
                StartCoroutine(Ocultar("R"));
            }
            else
            {
                // if(tag=="numeroR"){transform.position = Vector3.MoveTowards(transform.position, posPantalla, 4f * Time.deltaTime);}
                transform.position = Vector3.MoveTowards(transform.position, posOriginal, 4f * Time.deltaTime);
            }
        }
        
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
