using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Colision detectada con: {other.name}");

        if (other.CompareTag("Enemy"))
        {
            Debug.Log($"Impacto con enemigo: {other.name}");
            Destroy(other.gameObject); // Destruye el enemigo
            Destroy(gameObject); // Destruye el disparo
        }
    }
}
