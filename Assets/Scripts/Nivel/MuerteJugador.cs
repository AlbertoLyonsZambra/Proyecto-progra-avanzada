using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuerteJugador : MonoBehaviour
{
    [SerializeField] private GameObject explosion;
    [SerializeField] private float fuerzaX;
    [SerializeField] private float fuerzaY;
    [SerializeField] private AudioClip sonidoMuerte;
    [SerializeField] private GameObject pantallaMuerte;
    private bool estaMuerto;
    private int sentidoX;
    private int sentidoY;
    // Update is called once per frame
    void Update()
    {
        if (estaMuerto)
        {
            Muerte();
        }
    }
    private void Awake()
    {
        while (sentidoX == 0 || sentidoY == 0)
        {
            sentidoX = Random.Range(-1, 2);
            sentidoY = Random.Range(-1, 2);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obs_Asteroide"))
        {
            estaMuerto = true;
            InstanciarMuerte();
        }
    }
    private void Muerte()
    {
        
        transform.Translate(Vector3.right * fuerzaX * sentidoX * Time.deltaTime, Space.World);
        transform.Translate(Vector3.up * fuerzaY * sentidoY * Time.deltaTime, Space.World);
        Vector3 rotacion = new Vector3(Random.Range(20, 50), Random.Range(20, 50), Random.Range(20, 50));
        transform.Rotate(rotacion,Space.Self);
    }
    private void InstanciarMuerte()
    {
        print("X: "+sentidoX);
        print("Y: "+sentidoY);
        GameObject.Find("Sistema carriles").transform.Find("Instanciador_objetos").gameObject.SetActive(false);
        JugadorNivel.Instance.enabled = false;
        MovimientoCarriles.Instance.enabled = false;
        Quaternion rotacionExplosion = Quaternion.identity;
        Instantiate(explosion, gameObject.transform.position, rotacionExplosion);
        Gestor_audio.Instance.cambioSilenciadoMusica(true);
        Gestor_audio.Instance.EjecutarAudio(Gestor_audio.Instance.audioSourceSFX, sonidoMuerte);
        StartCoroutine(tiempoEspera());

    }
    IEnumerator tiempoEspera()
    {
        yield return new WaitForSeconds(2.5f);
        Time.timeScale = 0;
        pantallaMuerte.SetActive(true);
    }
}
