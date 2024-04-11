using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float velocidad = 5f;
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        //Movimiento a la izquierda
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
            Vector3 movimiento = new Vector3(-1, 0, 0) * velocidad * Time.deltaTime;
            transform.Translate(movimiento);
        }
        //Movimiento a la derecha
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
            Vector3 movimiento = new Vector3(1, 0, 0) * velocidad * Time.deltaTime;
            transform.Translate(movimiento);
        }
        //Movimiento hacia arriba
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) {
            Vector3 movimiento = new Vector3(0, 0, 1) * velocidad * Time.deltaTime;
            transform.Translate(movimiento);
        }
        //Movimiento hacia abajo
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) {
            Vector3 movimiento = new Vector3(0, 0, -1) * velocidad * Time.deltaTime;
            transform.Translate(movimiento);
        }
    }
}
