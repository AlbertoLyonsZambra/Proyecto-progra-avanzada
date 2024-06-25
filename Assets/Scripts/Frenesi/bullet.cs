using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public AudioClip impactSound; // Clip de sonido de impacto
    private AudioSource audioSource;

    void Start()
    {
        // Agrega un componente AudioSource y configúralo
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = impactSound;
    }

    void OnTriggerEnter(Collider other)
    {
        // Debug.Log($"Colision detectada con: {other.name}");

        if (other.CompareTag("Enemy"))
        {
            // Debug.Log($"Impacto con enemigo: {other.name}");
            Enemigo enemigo = other.GetComponent<Enemigo>();
            if (enemigo != null)
            {
                enemigo.TakeDamage(1); // Reduce la salud del enemigo en 1
            }

            EnemigoLobo enemigo1 = other.GetComponent<EnemigoLobo>();
            if (enemigo1 != null)
            {
                enemigo1.TakeDamage(1); // Reduce la salud del enemigo en 1
            }

            // Reproduce el sonido de impacto
            audioSource.Play();

            // Destruye el disparo después de que el sonido haya terminado de reproducirse
            Destroy(gameObject);
        }
    }
}

