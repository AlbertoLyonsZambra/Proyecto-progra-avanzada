using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrizCarriles : GenericSingleton<MatrizCarriles>
{
    [SerializeField] private GameObject carriles;
    private Vector2[,] matrizCarriles;
 
    protected override void Initialization()
    {
        this.matrizCarriles = new Vector2[3, 3];
        this.matrizCarriles[0, 0] = new Vector2(carriles.transform.Find("Columna 1").position.x, carriles.transform.Find("Fila 1").position.y);
        this.matrizCarriles[0, 1] = new Vector2(carriles.transform.Find("Fila 1").position.x, carriles.transform.Find("Fila 1").position.y);
        this.matrizCarriles[0, 2] = new Vector2(carriles.transform.Find("Columna 3").position.x, carriles.transform.Find("Fila 1").position.y);
        this.matrizCarriles[1, 0] = new Vector2(carriles.transform.Find("Columna 1").position.x, carriles.transform.Find("Columna 1").position.y);
        this.matrizCarriles[1, 1] = new Vector2(carriles.transform.Find("Fila 2").position.x, carriles.transform.Find("Fila 2").position.y);
        this.matrizCarriles[1, 2] = new Vector2(carriles.transform.Find("Columna 3").position.x, carriles.transform.Find("Columna 3").position.y);
        this.matrizCarriles[2, 0] = new Vector2(carriles.transform.Find("Columna 1").position.x, carriles.transform.Find("Fila 3").position.y);
        this.matrizCarriles[2, 1] = new Vector2(carriles.transform.Find("Fila 3").position.x, carriles.transform.Find("Fila 3").position.y);
        this.matrizCarriles[2, 2] = new Vector2(carriles.transform.Find("Columna 3").position.x, carriles.transform.Find("Fila 3").position.y);
    }
    public Vector2 getPosicion(int fila, int columna)
    {
        return (this.matrizCarriles[fila, columna]);
    }
    public Vector2[,] getMatriz()
    {
        return this.matrizCarriles;
    }
 }
