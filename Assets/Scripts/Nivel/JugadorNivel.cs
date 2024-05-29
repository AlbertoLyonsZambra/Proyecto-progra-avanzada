using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugadorNivel : GenericSingleton<JugadorNivel>
{
    [SerializeField] private float cadenciaDisparo = 0.2f;
    [SerializeField] private SimpleObjectPool laserPorDisparar;
    [SerializeField] private Transform generadorLaserPos;
    [SerializeField] private float multiplicadorMaterial = 1f;
    [HideInInspector] public bool escogioNave;
    [HideInInspector] public int contTriggerV = 0;
    [HideInInspector] public int contTriggerN = 0;
    [HideInInspector] public int contTriggerR = 0;
    [HideInInspector] public bool mostrandoV = false;
    [HideInInspector] public bool mostrandoN = false;
    [HideInInspector] public bool mostrandoR = false;
    public float bateria = 100;
    private float temporizadorDisparoLaser;
    private Rigidbody rb;
    [HideInInspector] public Transform transformJug;
    private int[] mats;
    protected override void Initialization()
    {
        transformJug = transform;
        rb = gameObject.GetComponent<Rigidbody>();
        mats = new int[8];
        
    }
    void Start()
    {
        
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
            GameObject laser;
            if(laserPorDisparar != null)
            {
                laser = laserPorDisparar.GetPooledGameObject();
                laser.transform.position = generadorLaserPos.transform.position;
                laser.SetActive(true);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("ConsumibleV"))
        {
            mostrandoV = true;
            mats[contTriggerV] = Mathf.RoundToInt(Random.Range(2, 9) * multiplicadorMaterial);
            contTriggerV++;
            if(contTriggerV == 8){print(Mathf.RoundToInt(Promediar(mats))); contTriggerV = 0;}
        }
        if(other.gameObject.CompareTag("ConsumibleN"))
        {
            mostrandoN = true;
            mats[contTriggerN] = Mathf.RoundToInt(Random.Range(2, 9) * multiplicadorMaterial);
            contTriggerN++;
            if(contTriggerN == 8){print(Mathf.RoundToInt(Promediar(mats))); contTriggerN = 0;}
        }
        if(other.gameObject.CompareTag("ConsumibleR"))
        {
            mostrandoR = true;
            mats[contTriggerR] = Mathf.RoundToInt(Random.Range(2, 9) * multiplicadorMaterial);
            contTriggerN++;
            if(contTriggerR == 8){print(Mathf.RoundToInt(Promediar(mats))); contTriggerR = 0;}
        }
        if(other.gameObject.CompareTag("Consumible"))
        {
            other.gameObject.SetActive(false);
            //Debug.Log("Colision ");
            bateria += 20;
            if(bateria >= 100){bateria = 100;}
        }
    }
    private float Promediar(int[] valores)
    {
        int suma = 0;
        for (int i = 0; i < valores.Length; i++){suma += valores[i];}
        return (float) suma / valores.Length;
    }
}
