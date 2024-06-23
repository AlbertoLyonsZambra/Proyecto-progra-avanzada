using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
            if(gameObject.transform.parent.name == "0"){cadenciaLaser = 0.25f;}
            else if(gameObject.transform.parent.name == "1"){cadenciaLaser = 0.10f;}
            else if(gameObject.transform.parent.name == "2"){cadenciaLaser = 0.10f;}
            else if(gameObject.transform.parent.name == "3"){cadenciaLaser = 0.15f;}
            else if(gameObject.transform.parent.name == "4"){cadenciaLaser = 0.2f;}
        }

    }
    void Update()
    {
        temporizadorDisparoLaser += Time.deltaTime;
        //disparoLaser();
        ControlarMotores();
    }
    public void disparoLaser()
    {
        if (temporizadorDisparoLaser > cadenciaLaser && MenuPrincipal.Instance.jugando)
        { 
            Gestor_audio.Instance.EjecutarAudio(Gestor_audio.Instance.audioSourceSFX, Gestor_audio.Instance.laserSFX);
            if (PlayerPrefs.GetInt("pasoTutorial") == 0) { Tutorial.Instance.disparoLaser = Tutorial.Instance.disparoLaser + 1; }
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
            mats[contTriggerV] = Mathf.RoundToInt(Random.Range(2, 8) * multiplicadorMaterial);
            contTriggerV++;
            if(contTriggerV == 8)
            {
                PlayerPrefs.SetInt("MatsV", PlayerPrefs.GetInt("MatsV") + Mathf.RoundToInt(Promediar(mats)) + Random.Range(-1, 2));
                contTriggerV = 0;
                Gestor_audio.Instance.EjecutarAudio(Gestor_audio.Instance.audioSourceSFX, Gestor_audio.Instance.recogerMaterialSFX);
            }
        }
        if(other.gameObject.CompareTag("ConsumibleN"))
        {
            mostrandoN = true;
            mats[contTriggerN] = Mathf.RoundToInt(Random.Range(2, 7) * multiplicadorMaterial);
            contTriggerN++;
            if(contTriggerN == 8)
            {
                PlayerPrefs.SetInt("MatsN", PlayerPrefs.GetInt("MatsN") + Mathf.RoundToInt(Promediar(mats)) + Random.Range(-2, 2));
                contTriggerN = 0;
                Gestor_audio.Instance.EjecutarAudio(Gestor_audio.Instance.audioSourceSFX, Gestor_audio.Instance.recogerMaterialSFX);
            }
        }
        if(other.gameObject.CompareTag("ConsumibleR"))
        {
            mostrandoR = true;
            mats[contTriggerR] = Mathf.RoundToInt(Random.Range(2, 5) * multiplicadorMaterial);
            contTriggerR++;
            if(contTriggerR == 8)
            {
                PlayerPrefs.SetInt("MatsR", PlayerPrefs.GetInt("MatsR") + Mathf.RoundToInt(Promediar(mats)) + Random.Range(-2, 1));
                contTriggerR = 0;
                Gestor_audio.Instance.EjecutarAudio(Gestor_audio.Instance.audioSourceSFX, Gestor_audio.Instance.recogerMaterialSFX);
            }
        }
        if(other.gameObject.CompareTag("Consumible"))
        {
            other.gameObject.SetActive(false);
            //Debug.Log("Colision ");
            Gestor_audio.Instance.EjecutarAudio(Gestor_audio.Instance.audioSourceSFX, Gestor_audio.Instance.bateriaSFX);
            bateria += 30;
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
