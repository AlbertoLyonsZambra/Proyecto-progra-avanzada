using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float cadenciaDisparo = 0.2f;
    [SerializeField] private SimpleObjectPool laserPorDisparar;
    [SerializeField] private Transform generadorLaserPos;
    
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
        if (Input.GetButtonDown("Fire1") && temporizadorDisparoLaser > cadenciaDisparo)
        { 
            temporizadorDisparoLaser = 0;
            GameObject laser = laserPorDisparar.GetPooledGameObject();
            laser.transform.position = generadorLaserPos.transform.position;
            laser.SetActive(true);
        }

    }
}
