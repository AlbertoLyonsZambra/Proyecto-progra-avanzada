using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugadorNivel : GenericSingleton<JugadorNivel>
{
    [HideInInspector] private float cadenciaLaser = 6000f;
    [SerializeField] private SimpleObjectPool laserSolo;
    private int posActualLaser = 0;
    [SerializeField] private Transform[] generadoresLaser;
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
    [SerializeField] private List<ParticleSystem> motores;
    protected override void Initialization()
    {
        transformJug = transform;
        rb = gameObject.GetComponent<Rigidbody>();
        mats = new int[8];
        
    }
    void Start()
    {
        if(gameObject.transform.parent != null)
        {
            temporizadorDisparoLaser = 0f;
            if(gameObject.transform.parent.name == "0"){cadenciaLaser = 0.4f;}
            else if(gameObject.transform.parent.name == "1"){cadenciaLaser = 0.2f;}
            else if(gameObject.transform.parent.name == "2"){cadenciaLaser = 0.2f;}
            else if(gameObject.transform.parent.name == "3"){cadenciaLaser = 0.2f;}
            else if(gameObject.transform.parent.name == "4"){cadenciaLaser = 0.2f;}
        }

    }
    void Update()
    {
        temporizadorDisparoLaser += Time.deltaTime;
        disparoLaser();
        ControlarMotores();
    }
    private void disparoLaser()
    {
        if (Input.GetButtonDown("Fire1") && temporizadorDisparoLaser > cadenciaLaser && MenuPrincipal.Instance.jugando)
        { 
            Gestor_audio.Instance.EjecutarAudio(Gestor_audio.Instance.audioSourceSFX, Gestor_audio.Instance.laserSFX);
            temporizadorDisparoLaser = 0f;
            GameObject laser;
            if(laserSolo != null)
            {
                laser = laserSolo.GetPooledGameObject();
                laser.transform.position = generadoresLaser[posActualLaser].transform.position;
                laser.SetActive(true);
                posActualLaser = (posActualLaser + 1) % generadoresLaser.Length;
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
            if(contTriggerV == 8)
            {
                print(Mathf.RoundToInt(Promediar(mats)));
                contTriggerV = 0;
                Gestor_audio.Instance.EjecutarAudio(Gestor_audio.Instance.audioSourceSFX, Gestor_audio.Instance.recogerMaterialSFX);
            }
        }
        if(other.gameObject.CompareTag("ConsumibleN"))
        {
            mostrandoN = true;
            mats[contTriggerN] = Mathf.RoundToInt(Random.Range(2, 9) * multiplicadorMaterial);
            contTriggerN++;
            if(contTriggerN == 8)
            {
                print(Mathf.RoundToInt(Promediar(mats))); 
                contTriggerN = 0;
                Gestor_audio.Instance.EjecutarAudio(Gestor_audio.Instance.audioSourceSFX, Gestor_audio.Instance.recogerMaterialSFX);
            }
        }
        if(other.gameObject.CompareTag("ConsumibleR"))
        {
            mostrandoR = true;
            mats[contTriggerR] = Mathf.RoundToInt(Random.Range(2, 9) * multiplicadorMaterial);
            contTriggerN++;
            if(contTriggerR == 8)
            {
                print(Mathf.RoundToInt(Promediar(mats))); 
                contTriggerR = 0;
                Gestor_audio.Instance.EjecutarAudio(Gestor_audio.Instance.audioSourceSFX, Gestor_audio.Instance.recogerMaterialSFX);
            }
        }
        if(other.gameObject.CompareTag("Consumible"))
        {
            other.gameObject.SetActive(false);
            //Debug.Log("Colision ");
            Gestor_audio.Instance.EjecutarAudio(Gestor_audio.Instance.audioSourceSFX, Gestor_audio.Instance.bateriaSFX);
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
    public void ControlarMotores()
    {
        if(motores != null)
        {
            if(bateria <= 0)
            {
                for (int i = 0; i < motores.Count; i++)
                {
                    if (motores[i] != null){motores[i].Stop();}
                }
            }
            else if(bateria > 0 && !motores[0].isPlaying)
            {
                for (int i = 0; i < motores.Count; i++)
                {
                    if (motores[i] != null){motores[i].Play();}
                }
            }
        }
        
    }
}
