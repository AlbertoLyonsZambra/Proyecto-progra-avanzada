using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidRaycast : MonoBehaviour
{
    private GameObject lastHitPlayer; // Referencia al último objeto "Player" golpeado

    void Update()
    {
        // Define una dirección fija hacia adelante en el espacio mundial
        Vector3 direccionFija = -Vector3.forward;

        // Crea un rayo que comienza desde la posición del objeto, pero en una dirección fija hacia adelante
        Ray rayo = new Ray(transform.position, direccionFija);
        RaycastHit hitInfo;

        // Verifica si el rayo golpea algo
        if (Physics.Raycast(rayo, out hitInfo, 20f))
        {
            if (hitInfo.collider.CompareTag("Player"))
            {
                // Obtener el material del objeto golpeado
                MeshRenderer meshRenderer = hitInfo.collider.GetComponent<MeshRenderer>();
                if (meshRenderer != null)
                {
                    // Cambiar el color del material a rojo
                    meshRenderer.material.color = Color.red;

                    // Almacenar la referencia al objeto "Player" golpeado
                    lastHitPlayer = hitInfo.collider.gameObject;
                }
                else
                {
                    Debug.LogWarning("El objeto " + hitInfo.collider.name + " no tiene un MeshRenderer adjunto.");
                }
            }
            else
            {
                // Si el rayo golpea algo que no es "Player", restablecer el color del último objeto "Player" golpeado
                if (lastHitPlayer != null)
                {
                    MeshRenderer lastPlayerMeshRenderer = lastHitPlayer.GetComponent<MeshRenderer>();
                    if (lastPlayerMeshRenderer != null)
                    {
                        lastPlayerMeshRenderer.material.color = Color.white; // Cambia el color a blanco (o el color que desees)
                    }
                    lastHitPlayer = null; // Restablecer la referencia
                }
            }
        }
        else
        {
            // Si el rayo no golpea nada, restablecer el color del último objeto "Player" golpeado
            if (lastHitPlayer != null)
            {
                MeshRenderer lastPlayerMeshRenderer = lastHitPlayer.GetComponent<MeshRenderer>();
                if (lastPlayerMeshRenderer != null)
                {
                    lastPlayerMeshRenderer.material.color = Color.white; // Cambia el color a blanco (o el color que desees)
                }
                lastHitPlayer = null; // Restablecer la referencia
            }
        }

        // Dibuja el rayo
        Debug.DrawRay(rayo.origin, rayo.direction * 20f, Color.red);
    }
}