using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimiento_obstaculos : MonoBehaviour
{
    private float velocidad = 3f;

    // Update is called once per frame
    void Update()
    {
        Vector3 movimiento = new Vector3(0, 0, -1) * velocidad * Time.deltaTime;
        transform.Translate(movimiento);
    }
}
