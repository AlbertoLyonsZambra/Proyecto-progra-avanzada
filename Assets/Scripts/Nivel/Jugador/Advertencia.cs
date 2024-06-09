
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Advertencia : GenericSingleton<Advertencia>
{
    [SerializeField] private GameObject advertenciaPrefab; // El prefab de advertencia
    private JugadorNivel jugador;
    public GameObject bateriaBaja;
    private Coroutine advertenciaCoroutine;
    private Transform playerTransform;
    private Vector3 offsetFromPlayer;
    private Quaternion advertenciaRotation;

    void Start()
    {
        jugador = JugadorNivel.Instance;
        playerTransform = jugador.transform;
        

        // Instanciar advertencia cerca del jugador
        offsetFromPlayer = new Vector3(0, -2f, 0); // Puedes ajustar esto seg�n necesites
        bateriaBaja = Instantiate(advertenciaPrefab, playerTransform.position + offsetFromPlayer, Quaternion.identity);
        bateriaBaja.SetActive(false); // Inicialmente desactivada
        advertenciaRotation = Quaternion.Euler(0, -180, 0);
    }

    void Update()
    {
        // Actualizar la posici�n de la advertencia para que siga al jugador
        bateriaBaja.transform.position = playerTransform.position + offsetFromPlayer;
        bateriaBaja.transform.rotation = advertenciaRotation;    

        GestionarAdvertencia();        
    }

    void GestionarAdvertencia()
    {
        if (jugador.bateria == 0 && !MuerteJugador.Instance.estaMuerto)
        {
            // Si la bater�a est� en cero, mantener la advertencia visible
            if (advertenciaCoroutine != null)
            {
                StopCoroutine(advertenciaCoroutine);
                advertenciaCoroutine = null;
            }
            bateriaBaja.SetActive(true);
        }
        else if (jugador.bateria < 20)
        {
            // Si la bater�a es menor que 20 pero mayor que 0, empezar la advertencia si no est� corriendo
            if (advertenciaCoroutine == null)
            {
                advertenciaCoroutine = StartCoroutine(AlternarAdvertencia());
            }
        }
        else
        {
            // Si la bater�a es 20 o mayor y la coroutine est� corriendo, la detenemos
            if (advertenciaCoroutine != null)
            {
                StopCoroutine(advertenciaCoroutine);
                advertenciaCoroutine = null;
                bateriaBaja.SetActive(false); // Aseg�rate de que el objeto est� desactivado
            }
        }
    }
    IEnumerator AlternarAdvertencia()
    {
        while (true)
        {
            bateriaBaja.SetActive(!bateriaBaja.activeSelf);
            yield return new WaitForSeconds(1f);
        }
    }
}