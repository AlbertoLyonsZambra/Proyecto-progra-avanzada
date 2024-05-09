using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanciadorObjetos : MonoBehaviour
{
    [SerializeField] GameObject carriles;
    public Vector2[,] matrizCarriles;
    [SerializeField] private GameObject[] asteroides;
    
    void Start()
    {
        matrizCarriles = new Vector2[3, 3];
        matrizCarriles[0, 0] = new Vector2(carriles.transform.Find("Columna 1").position.x, carriles.transform.Find("Fila 1").position.y);
        matrizCarriles[0, 1] = new Vector2(carriles.transform.Find("Fila 1").position.x, carriles.transform.Find("Fila 1").position.y);
        matrizCarriles[0, 2] = new Vector2(carriles.transform.Find("Columna 3").position.x, carriles.transform.Find("Fila 1").position.y);
        matrizCarriles[1, 0] = new Vector2(carriles.transform.Find("Columna 1").position.x, carriles.transform.Find("Columna 1").position.y);
        matrizCarriles[1, 1] = new Vector2(carriles.transform.Find("Fila 2").position.x, carriles.transform.Find("Fila 2").position.y);
        matrizCarriles[1, 2] = new Vector2(carriles.transform.Find("Columna 3").position.x, carriles.transform.Find("Columna 3").position.y);
        matrizCarriles[2, 0] = new Vector2(carriles.transform.Find("Columna 1").position.x, carriles.transform.Find("Fila 3").position.y);
        matrizCarriles[2, 1] = new Vector2(carriles.transform.Find("Fila 3").position.x, carriles.transform.Find("Fila 3").position.y);
        matrizCarriles[2, 2] = new Vector2(carriles.transform.Find("Columna 3").position.x, carriles.transform.Find("Fila 3").position.y);
        AparecerObjeto();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void AparecerObjeto()
    {
        int fila = Random.Range(0, 3);
        int columna = Random.Range(0, 3);
        print("FILA " + fila + "\nColumna " + columna + "\n");
        Vector3 posicion = new Vector3(matrizCarriles[fila, columna].x, matrizCarriles[fila, columna].y, transform.position.z);
        Instantiate(asteroides[Random.Range(0,asteroides.Length-1)], posicion, Quaternion.identity);
        Invoke("AparecerObjeto",Random.Range(1,4));
    }
}
