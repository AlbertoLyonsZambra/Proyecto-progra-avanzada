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
    
    public float bateria = 100;
    private float temporizadorDisparoLaser;
    private Rigidbody rb;
    public Transform transformJug;
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
    private int contTrigger = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ConsumibleV"))
        {
            mats[contTrigger] = Mathf.RoundToInt(Random.Range(2, 9) * multiplicadorMaterial);
            contTrigger++;
            if(contTrigger == 8){print(Mathf.RoundToInt(Promediar(mats))); contTrigger = 0;}
        }
        if (other.gameObject.CompareTag("ConsumibleN"))
        {
            mats[contTrigger] = Mathf.RoundToInt(Random.Range(2, 9) * multiplicadorMaterial);
            contTrigger++;
            if(contTrigger == 8){print(Mathf.RoundToInt(Promediar(mats))); contTrigger = 0;}
        }
        if (other.gameObject.CompareTag("ConsumibleR"))
        {
            mats[contTrigger] = Mathf.RoundToInt(Random.Range(2, 9) * multiplicadorMaterial);
            contTrigger++;
            if(contTrigger == 8){print(Mathf.RoundToInt(Promediar(mats))); contTrigger = 0;}
        }
        if (other.gameObject.CompareTag("Consumible"))
        {
            other.gameObject.SetActive(false);
            //Debug.Log("Colision ");

            bateria += 20;

            if (bateria >= 100)
            {
                bateria = 100;
            }

        }


    }
    private float Promediar(int[] valores)
    {
        int suma = 0;
        for (int i = 0; i < valores.Length; i++){suma += valores[i];}
        return (float) suma / valores.Length;
    }
}
