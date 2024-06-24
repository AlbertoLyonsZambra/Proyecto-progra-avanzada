using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        //Debug.Log($"Colision detectada con: {other.name}");

        if (other.CompareTag("Enemy"))
        {
            //Debug.Log($"Impacto con enemigo: {other.name}");
            Enemigo enemigo = other.GetComponent<Enemigo>();
            if (enemigo != null)
            {
                enemigo.TakeDamage(1); // Reduce la salud del enemigo en 1
            }
            Destroy(gameObject); // Destruye el disparo


            EnemigoLobo enemigo1 = other.GetComponent<EnemigoLobo>();
            if (enemigo1 != null)
            {
                enemigo1.TakeDamage(1); // Reduce la salud del enemigo en 1
            }
            Destroy(gameObject); // Destruye el disparo
        }
    }
}
