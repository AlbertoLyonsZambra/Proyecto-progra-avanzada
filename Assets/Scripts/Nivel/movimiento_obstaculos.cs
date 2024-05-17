using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimiento_obstaculos : MonoBehaviour
{
    [SerializeField] private float velocidad = 3f;
    [SerializeField] private float aumentoInicial  = 0.0000000000000001f;
    [SerializeField] private float aumentoMaximo = 0.1f; // Maximum speed increase rate
    [SerializeField] private float tiempoMaximo = 60f; // Maximum time before reaching maximum speed increase rate

    void Start()
    {
        
    }
    void Update()
    {
        float tiempoPasado = Time.time;
        float aumentoVelocidad = Mathf.Lerp(aumentoInicial, aumentoMaximo, tiempoPasado / tiempoMaximo);

        velocidad += aumentoVelocidad * Time.deltaTime;
        Vector3 movimiento = new Vector3(0, 0, -1) * velocidad * Time.deltaTime;
        
        transform.Translate(movimiento,Space.World);
        Vector3 rotacion = new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
        transform.Rotate(rotacion * Time.deltaTime, Space.Self);

    }
}
