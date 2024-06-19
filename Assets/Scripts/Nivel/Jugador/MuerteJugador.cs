using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuerteJugador : GenericSingleton<MuerteJugador>
{
    [SerializeField] private GameObject explosion;
    [SerializeField] private float fuerzaX;
    [SerializeField] private float fuerzaY;
    [SerializeField] private AudioClip sonidoMuerte;
    [SerializeField] private GameObject pantallaMuerte;
    public bool estaMuerto;
    private int sentidoX;
    private int sentidoY;
    
    // Update is called once per frame
    void Start()
    {
        
    }
    void Update()
    {
        if (estaMuerto)
        {
            Muerte();
        }
    }
    
    protected override void Initialization()
    {
        while (sentidoX == 0 || sentidoY == 0)
        {
            sentidoX = Random.Range(-1, 2);
            sentidoY = Random.Range(-1, 2);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Obs_Asteroide") || other.gameObject.CompareTag("MatNormal") || other.gameObject.CompareTag("MatRaro") || other.gameObject.CompareTag("MatSuper") || other.gameObject.CompareTag("MatTutorial"))
        {
            if (!estaMuerto)
            {
                estaMuerto = true;
                Advertencia.Instance.bateriaBaja.SetActive(false);
                MenuPrincipal.Instance.jugando = false;
                GestorTaller.Instance.guardarMats();
                InstanciarMuerte();
            }
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
        GameObject.Find("Sistema carriles").transform.Find("Instanciador_objetos").gameObject.SetActive(false);
        GameObject.Find("Animaciones").gameObject.SetActive(false);
        GameObject.Find("UI").transform.Find("Pantalla tutorial").gameObject.SetActive(false);
        JugadorNivel.Instance.enabled = false;
        MovimientoCarriles.Instance.enabled = false;
        // A partir de aqui se puede separar en distintos tipos de muerte
        pantallaMuerte.SetActive(true);
        pantallaMuerte.transform.Find("Vidrios").gameObject.SetActive(true);
        
        Quaternion rotacionExplosion = Quaternion.identity;
        Instantiate(explosion, gameObject.transform.position, rotacionExplosion);
        Gestor_audio.Instance.silenciadoMuerte();
        Gestor_audio.Instance.EjecutarAudio(Gestor_audio.Instance.audioSourceSFX, sonidoMuerte);
        StartCoroutine(tiempoEspera());

    }
    IEnumerator tiempoEspera()
    {
        yield return new WaitForSeconds(2.5f);
        Time.timeScale = 0;
        pantallaMuerte.transform.Find("Texto y boton").gameObject.SetActive(true);
    }
}
