using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InstanciadorObjetos : MonoBehaviour
{
    [SerializeField] GameObject carriles;
    public Vector2[,] matrizCarriles;
    [SerializeField] private SimpleObjectPool[] asteroides;
    [SerializeField] private Transform[] generadoresAsteroidesPos;
    
    [SerializeField] private float tiempoMin = 1f;
    [SerializeField] private float tiempoMax = 3f;
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
        StartCoroutine(AparecerAsteroideCoroutine(null));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Retorna lista con la posicion [fila, columna] y pasa el nombre del objeto a generar como string (sirve como debug)
    List<int> GenerarPos(string objetoAGenerar)
    {
        List<int> pos = new List<int>();
        int fila = Random.Range(0, 3);
        int columna = Random.Range(0, 3);
        print("["+objetoAGenerar+"] " + "Fila: " + fila + ", columna: " + columna + "\n");
        pos.Add(fila);
        pos.Add(columna);
        return pos;
    }
    IEnumerator AparecerAsteroideCoroutine(List<int> posMatrizPrevia)
    {
        yield return new WaitForSeconds(Random.Range(tiempoMin, tiempoMax));
        if (posMatrizPrevia==null){posMatrizPrevia = GenerarPos("FIRST IF");}
        List<int> posMatriz = GenerarPos("Asteroide obstaculo");
        if (!posMatrizPrevia.SequenceEqual(posMatriz))
        {
            GameObject asteroide = asteroides[Random.Range(0, 5)].GetPooledGameObject();
            asteroide.transform.position = new Vector3(matrizCarriles[posMatriz[0], posMatriz[1]].x, matrizCarriles[posMatriz[0], posMatriz[1]].y, transform.position.z);
            asteroide.SetActive(true);
            StartCoroutine(AparecerAsteroideCoroutine(posMatriz));
        } 
        else if (posMatrizPrevia.SequenceEqual(posMatriz))
        {
            while (posMatrizPrevia.SequenceEqual(posMatriz)){posMatriz = GenerarPos("WHILE");}
            StartCoroutine(AparecerAsteroideCoroutine(posMatriz));
        }
    }
}