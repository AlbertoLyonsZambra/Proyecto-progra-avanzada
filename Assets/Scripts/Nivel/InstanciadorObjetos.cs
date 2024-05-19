using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InstanciadorObjetos : MonoBehaviour
{
    [SerializeField] GameObject carriles;
    public Vector2[,] matrizCarriles;
    [SerializeField] private SimpleObjectPool[] asteroides;
    [SerializeField] private SimpleObjectPool[] bateria;
    [SerializeField] private Transform[] generadoresAsteroidesPos;
    
    [SerializeField] private float tiempoMin = 1f;
    [SerializeField] private float tiempoMax = 3f;
    void Start()
    {
        matrizCarriles = MatrizCarriles.Instance.matrizCarriles;
        StartCoroutine(AparecerObjetosCoroutine(null, asteroides , "Asteroide", 1f, 3f));
        StartCoroutine(AparecerObjetosCoroutine(null, bateria, "Bateria" , 3f, 7f ));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Retorna lista con la posicion [fila, columna] y pasa el nombre del objeto a generar como string (sirve como debug)
    public List<int> GenerarPos(string objetoAGenerar)
    {
        List<int> pos = new List<int>();
        int fila = Random.Range(0, 3);
        int columna = Random.Range(0, 3);
        //print("["+objetoAGenerar+"] " + "Fila: " + fila + ", columna: " + columna + "\n");
        pos.Add(fila);
        pos.Add(columna);
        return pos;
    }

    IEnumerator AparecerObjetosCoroutine(List<int> posMatrizPrevia, SimpleObjectPool[] objetos, string nombre, float tiempoMin, float tiempoMax)
    {
        yield return new WaitForSeconds(Random.Range(tiempoMin, tiempoMax));
        if (posMatrizPrevia==null){posMatrizPrevia = GenerarPos("FIRST IF");}
        List<int> posMatriz = GenerarPos(nombre);
        if (!posMatrizPrevia.SequenceEqual(posMatriz))
        {
            GameObject objeto = objetos[Random.Range(0, objetos.Length-1)].GetPooledGameObject();
            objeto.transform.position = new Vector3(matrizCarriles[posMatriz[0], posMatriz[1]].x, matrizCarriles[posMatriz[0], posMatriz[1]].y, transform.position.z);
            objeto.SetActive(true);
            StartCoroutine(AparecerObjetosCoroutine(posMatriz, objetos, nombre, tiempoMin, tiempoMax));
        } 
        else if (posMatrizPrevia.SequenceEqual(posMatriz))
        {
            while (posMatrizPrevia.SequenceEqual(posMatriz)){posMatriz = GenerarPos("WHILE");}
            StartCoroutine(AparecerObjetosCoroutine(posMatriz, objetos, nombre, tiempoMin, tiempoMax));
        }
    }

    
}