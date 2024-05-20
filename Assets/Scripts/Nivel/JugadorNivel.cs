using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugadorNivel : GenericSingleton<JugadorNivel>
{
    [SerializeField] private float cadenciaDisparo = 0.2f;
    [SerializeField] private SimpleObjectPool laserPorDisparar;
    [SerializeField] private Transform generadorLaserPos;
    private float temporizadorDisparoLaser;
    private Rigidbody rb;
    [HideInInspector] public int nivelActual;
    [HideInInspector] public bool victoria = false;
    protected override void Initialization()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }
    void Start()
    {
        nivelActual = PlayerPrefs.GetInt("nivelActual");
    }
    void Update()
    {
        temporizadorDisparoLaser += Time.deltaTime;
        disparoLaser();
    }
    private void disparoLaser()
    {
        if (Input.GetButtonDown("Fire1") && temporizadorDisparoLaser > cadenciaDisparo && MenuPrincipal.Instance.jugando)
        { 
            Gestor_audio.Instance.EjecutarAudio(Gestor_audio.Instance.audioSourceSFX, Gestor_audio.Instance.laserSFX);
            temporizadorDisparoLaser = 0f;
            GameObject laser = laserPorDisparar.GetPooledGameObject();
            laser.transform.position = generadorLaserPos.transform.position;
            laser.SetActive(true);
        }
    }
    public void Victoria()
    {
        victoria = true;
        PlayerPrefs.SetInt("nivelActual", nivelActual + 1);
        nivelActual = PlayerPrefs.GetInt("nivelActual");
        // Hacer mas logica aqui despues :]
        print(" ganaste el nivel congrats ");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Consumible"))
        {

        }
    }
}
