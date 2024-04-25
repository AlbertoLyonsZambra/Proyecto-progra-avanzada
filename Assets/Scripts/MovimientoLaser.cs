using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoLaser : MonoBehaviour
{
    [SerializeField] private float velocidad = 0.5f;

    // Update is called once per frame
    void Update()
    {
        Vector3 movimiento = new Vector3(0,0,velocidad) * Time.deltaTime;
        transform.Translate(movimiento);
    }
}
