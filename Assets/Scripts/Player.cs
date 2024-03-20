using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float velocidad = 0.01f;
    //private Transform transform
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        //Movimiento a la izquierda
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
                transform.Translate(-velocidad, 0, 0); 
        }
        //Movimiento a la derecha
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
            transform.Translate(velocidad, 0, 0);
        }
        //Movimiento hacia arriba
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) {
                transform.Translate(0, velocidad, 0); 
        }
        //Movimiento hacia abajo
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) {
                transform.Translate(0, -velocidad, 0); 
        }
    }
}
