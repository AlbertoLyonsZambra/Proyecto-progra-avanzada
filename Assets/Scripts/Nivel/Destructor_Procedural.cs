using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructor_Procedural : MonoBehaviour
{
    public GameObject Campo;
    public GameObject Generador;
    void Start()
    {
        
    }


    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other){
        if (other.gameObject.CompareTag("Collider_DP")){
            Campo.SetActive(false);
            Destroy(Campo);
            Destroy(Generador);
            Destroy(gameObject);
        }
    }
}
