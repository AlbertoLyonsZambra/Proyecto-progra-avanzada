using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento_Asteroides : MonoBehaviour
{
    [SerializeField] private float velocidad;
    void Start()
    {
        
    }

    void Update()
    {
        transform.position += new Vector3(0, 0, -velocidad) * Time.deltaTime;
    }
    
}
