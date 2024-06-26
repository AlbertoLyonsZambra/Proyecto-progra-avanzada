using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoLobo : MonoBehaviour
{
    public int rutina;
    public float cronometro;
    public Animator ani;
    public Quaternion angulo;
    public float grado;
    public GameObject target;
    public bool atacando;
    public int health = 3; // Variable de salud
    private DemoSpawnerControl spawner;
    public Collider attackCollider; // Agrega una referencia al collider
    [SerializeField] private GameObject particleSystemPrefab;
    [SerializeField] private float particleSystemDuration = 2f;
    private bool isDying = false;
    public GameObject[] dropMaterials; // Array de prefabs de materiales
    public int materialAmount = 2;
    public float dropHeightOffset = 1f;

    void Start()
    {
        ani = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player");
        spawner = DemoSpawnerControl.Instance;
        attackCollider.enabled = false; // Asegúrate de que el collider esté desactivado al inicio
        
    }

    // Update is called once per frame
    void Update()
    {
        Comportamiento();
    }

    public void Comportamiento()
    {
        if (Vector3.Distance(transform.position, target.transform.position) > 1000000000)
        {
            ani.SetBool("run", false);
            cronometro += 1 * Time.deltaTime;
            if (cronometro >= 4)
            {
                rutina = Random.Range(0, 2);
                cronometro = 0;
            }
            switch (rutina)
            {
                case 0:
                    ani.SetBool("walk", false);
                    break;
                case 1:
                    grado = Random.Range(0, 360);
                    angulo = Quaternion.Euler(0, grado, 0);
                    rutina++;
                    break;
                case 2:
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, angulo, 0.5f);
                    transform.Translate(Vector3.forward * 5 * Time.deltaTime); // Aumenta la velocidad de caminar
                    ani.SetBool("walk", true);
                    break;
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, target.transform.position) > 4 && !atacando)
            {
                var lookPos = target.transform.position - transform.position;
                lookPos.y = 0;
                var rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);
                ani.SetBool("walk", false);

                ani.SetBool("run", true);
                transform.Translate(Vector3.forward * 5 * Time.deltaTime); // Aumenta la velocidad de correr

                ani.SetBool("attack", false);
            }
            else
            {
                ani.SetBool("walk", false);
                ani.SetBool("run", false);

                ani.SetBool("attack", true);
                atacando = true;
            }
        }
    }

    public void FinalAni()
    {
        ani.SetBool("attack", false);
        atacando = false;
        attackCollider.enabled = false;
    }

    public void TakeDamage(int damage)
    {
        if (isDying) return; // Evita daño adicional si ya está muriendo

        health -= damage;
        if (health <= 0 && !isDying)
        {
            isDying = true;
            StartCoroutine(DieSequence());
        }
    }

    private IEnumerator DieSequence()
    {
        // Desactivar el collider y los scripts que controlan el comportamiento
        GetComponent<Collider>().enabled = false;
        this.enabled = false; // Desactiva este script

        // Reproducir partículas si existen
        if (particleSystemPrefab != null)
        {
            Vector3 particlePosition = transform.position;
            Quaternion particleRotation = Quaternion.Euler(-90, 0, 0);
            GameObject particleInstance = Instantiate(particleSystemPrefab, particlePosition, particleRotation);

            ParticleSystem ps = particleInstance.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                Destroy(particleInstance, ps.main.duration);
            }

            if(PlayerPrefs.GetInt("jugandoFrenesi",0) == 1)
            {
                for (int i = 0; i < materialAmount; i++)
                {
                    int randomIndex = Random.Range(0, dropMaterials.Length);
                    Vector3 dropPosition = new Vector3(transform.position.x, transform.position.y + dropHeightOffset, transform.position.z);
                    Instantiate(dropMaterials[randomIndex], dropPosition, Quaternion.identity);
                }
            }
        }

        // Detener animaciones
        if (ani != null)
        {
            ani.enabled = false;
        }

        // Reproducir el sonido de muerte
        Gestor_audio.Instance.EjecutarAudio(Gestor_audio.Instance.audioSourceSFX, Gestor_audio.Instance.laserSFX);

        // Desactivar el renderizador para hacer invisible al enemigo
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.enabled = false;
        }

        // Esperar un frame adicional para asegurar que todo se ha procesado
        yield return null;

        // Destruir el objeto
        Destroy(gameObject);
        Gestor_audio.Instance.EjecutarAudio(Gestor_audio.Instance.audioSourceSFX, Gestor_audio.Instance.recogerMaterialSFX);
    }

    // Método para activar el collider
    public void ActivarCollider()
    {
        attackCollider.enabled = true;
    }

    // Método para desactivar el collider
    public void DesactivarCollider()
    {
        attackCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // Reproducir el sonido de colisión
            Gestor_audio.Instance.EjecutarAudio(Gestor_audio.Instance.audioSourceSFX, Gestor_audio.Instance.naveSFX);
        }
    }
}
