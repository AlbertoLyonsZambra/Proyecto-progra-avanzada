using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Player : MonoBehaviour
{
    [SerializeField] private Transform origenLaserIzq;
    [SerializeField] private Transform origenLaserDer;
    private float rangoLaser = 1f;
    private float cadenciaDisparoLaser = 0.2f;
    private float duracionLaser = 0.05f;
    LineRenderer laser;
    [SerializeField] private GameObject laserPorDisparar;
    
    float temporizadorDisparoLaser;
    public GameObject seccionAsteroides1;
    // public GameObject seccionAsteroides2;
    private float velocidadWASD = 5f;
    private void OnTriggerEnter(Collider other){
        //if (other.gameObject.CompareTag("Collider_GP_1")){ Instantiate(seccionAsteroides1, new Vector3(0, 0, 1000), Quaternion.identity); }
        //if (other.gameObject.CompareTag("Collider_GP_2")){ Instantiate(seccionAsteroides1, new Vector3(0, 0, 1000), Quaternion.identity); }
        // if (other.gameObject.CompareTag("Collider_GP_3")){ Instantiate(seccionAsteroides, new Vector3(0, 0, 970*1), Quaternion.identity); }
    }
    
    void Awake()
    {
        laser = GetComponent<LineRenderer>();
    }
    void Start()
    {
        
    }
    void Update()
    {
        temporizadorDisparoLaser += Time.deltaTime;
        //MovimientoWASD();
        disparoLaser();
    }
    private void MovimientoWASD(){
        //Movimiento a la izquierda
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
            Vector3 movimiento = new Vector3(-1, 0, 0) * velocidadWASD * Time.deltaTime;
            transform.Translate(movimiento);
        }
        //Movimiento a la derecha
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
            Vector3 movimiento = new Vector3(1, 0, 0) * velocidadWASD * Time.deltaTime;
            transform.Translate(movimiento);
        }
        //Movimiento hacia arriba
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) {
            Vector3 movimiento = new Vector3(0, 0, 1) * velocidadWASD * Time.deltaTime;
            transform.Translate(movimiento);
        }
        //Movimiento hacia abajo
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) {
            Vector3 movimiento = new Vector3(0, 0, -1) * velocidadWASD * Time.deltaTime;
            transform.Translate(movimiento);
        }
    }
    private void disparoLaser()
    {
        if (Input.GetButtonDown("Fire1") && temporizadorDisparoLaser > cadenciaDisparoLaser)
        { 
            temporizadorDisparoLaser = 0;
            GameObject laser = Instantiate(laserPorDisparar);
            laser.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -210.6f);
            StartCoroutine(dispararLaser());
        }

        /*
        if (Input.GetButtonDown("Fire1") && temporizadorDisparoLaser > cadenciaDisparoLaser)
        {
            temporizadorDisparoLaser = 0;
            laser.SetPosition(0, origenLaserIzq.position);
            laser.SetPosition(0, origenLaserDer.position);
            StartCoroutine(dispararLaser());
        }
        */
    }
    IEnumerator dispararLaser()
    {
        laser.enabled = true;
        yield return new WaitForSecondsRealtime(duracionLaser);
        laser.enabled = false;
    }
}
