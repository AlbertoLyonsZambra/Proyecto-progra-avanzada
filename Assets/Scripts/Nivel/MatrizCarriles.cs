using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrizCarriles : MonoBehaviour
{
    [SerializeField] GameObject carriles;
    public Vector2[,] matrizCarriles;
    void Start()
    {
        matrizCarriles = new Vector2[3,3];
        matrizCarriles[0, 0] = new Vector2(carriles.transform.Find("Columna 1").position.x, carriles.transform.Find("Fila 1").position.y);
        matrizCarriles[0, 1] = new Vector2(0, carriles.transform.Find("Fila 1").position.y);
        matrizCarriles[0, 2] = new Vector2(carriles.transform.Find("Columna 3").position.x, carriles.transform.Find("Fila 1").position.y);
        matrizCarriles[1, 0] = new Vector2(carriles.transform.Find("Columna 1").position.x,0);
        matrizCarriles[1, 1] = new Vector2(0,0);
        matrizCarriles[1, 2] = new Vector2(carriles.transform.Find("Columna 3").position.x,0);
        matrizCarriles[2, 0] = new Vector2(carriles.transform.Find("Columna 1").position.x, carriles.transform.Find("Fila 3").position.y);
        matrizCarriles[2, 1] = new Vector2(0, carriles.transform.Find("Fila 3").position.y);
        matrizCarriles[2, 2] = new Vector2(carriles.transform.Find("Columna 3").position.x, carriles.transform.Find("Fila 3").position.y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
