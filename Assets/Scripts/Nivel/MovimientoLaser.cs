using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovimientoLaser : MonoBehaviour
{
    [SerializeField] private float velocidad = 0.5f;
    public bool canMove;

    // Update is called once per frame
    void Update()
    {
        if(canMove) 
        {
            Vector3 movimiento = new Vector3(0,0,velocidad) * Time.deltaTime;
            transform.Translate(movimiento);
        }
    }

    void OnEnable()    
    {
        canMove = true;
    }

    void OnDisable()    
    {
        canMove = false;
        this.transform.position = new Vector3(0,0,0);
    }

}
