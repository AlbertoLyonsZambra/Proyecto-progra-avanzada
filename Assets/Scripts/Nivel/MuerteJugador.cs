using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuerteJugador : MonoBehaviour
{
    [SerializeField] private GameObject particulasMuerte;
    [SerializeField] private float fuerzaX;
    [SerializeField] private float fuerzaY;
    private bool estaMuerto;
    // Update is called once per frame
    void Update()
    {
        if (estaMuerto)
        {
            Muerte();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obs_Asteroide"))
        {
            estaMuerto = true;
        }
    }
    private void Muerte()
    {
        int sentidoX = Random.Range(-1, 1);
        int sentidoY = Random.Range(-1, 1);
        JugadorNivel.Instance.enabled = false;
        transform.Translate(Vector3.right * fuerzaX * Time.deltaTime, Space.World);
        transform.Translate(Vector3.up * fuerzaY * Time.deltaTime, Space.World);
        Vector3 rotacion = new Vector3(Random.Range(20, 180), Random.Range(20, 180), Random.Range(20, 180));
        transform.Rotate(rotacion,Space.Self);
    }
}
