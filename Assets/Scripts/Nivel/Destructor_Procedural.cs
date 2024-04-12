using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructor_Procedural : MonoBehaviour
{
    public GameObject Campo1;
    public GameObject Campo2;
    void Start()
    {
        
    }


    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other){
        if (other.gameObject.CompareTag("Collider_DP")){
            Destroy(Campo1);
            Destroy(Campo2);
            Destroy(gameObject);
        }
    }
}
