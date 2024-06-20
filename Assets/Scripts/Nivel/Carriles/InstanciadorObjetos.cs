using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InstanciadorObjetos : GenericSingleton<InstanciadorObjetos>
{
    [SerializeField] GameObject carriles;
    public Vector2[,] matrizCarriles;
    [SerializeField] private SimpleObjectPool[] asteroides;
    [SerializeField] private SimpleObjectPool[] verdes;
    [SerializeField] private SimpleObjectPool[] naranjas;
    [SerializeField] private SimpleObjectPool[] rosas;
    [SerializeField] public SimpleObjectPool[] bateria;
    [SerializeField] private Transform[] generadoresAsteroidesPos;
    [SerializeField] private SimpleObjectPool[] finalNivel;
    void Start()
    {
        matrizCarriles = MatrizCarriles.Instance.matrizCarriles;
        StartCoroutine(AparecerObjetosCoroutine(null, asteroides, "Asteroide", 0, 1.5f));
        StartCoroutine(AparecerObjetosCoroutine(null, verdes, "Verde", 5f, 7f));
        if(MenuPrincipal.Instance.nivelActual >= 1){StartCoroutine(AparecerObjetosCoroutine(null, naranjas, "Naranja", 5f, 7f));}
        if(MenuPrincipal.Instance.nivelActual >= 3){StartCoroutine(AparecerObjetosCoroutine(null, rosas, "Rosa", 5f, 7f));}
        StartCoroutine(AparecerObjetosCoroutine(null, bateria, "Bateria" , 10f, 20f ));
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
            if(!objeto.GetComponent<MeshRenderer>().enabled){objeto.GetComponent<MeshRenderer>().enabled = true;}
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

    public void GenerarUnObjeto(SimpleObjectPool[] objetos, string nombre)
    {
        List<int> posMatriz = GenerarPos(nombre);
        GameObject objeto = objetos[Random.Range(0, objetos.Length-1)].GetPooledGameObject();
        if(!objeto.GetComponent<MeshRenderer>().enabled){objeto.GetComponent<MeshRenderer>().enabled = true;}
        objeto.transform.position = new Vector3(matrizCarriles[posMatriz[0], posMatriz[1]].x, matrizCarriles[posMatriz[0], posMatriz[1]].y, transform.position.z);
        objeto.SetActive(true);
        
    }
    
}