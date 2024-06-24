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
    [SerializeField] public SimpleObjectPool[] kits;
    [SerializeField] private Transform[] generadoresAsteroidesPos;
    [SerializeField] private SimpleObjectPool[] finalNivel;

    void Start()
    {
        // Tiempos para nivel 0
        float tiempoMinAsteroides = 0;
        float tiempoMaxAsteroides = 3;
        float tiempoMinAsteroidesColor = 7;
        float tiempoMaxAsteroidesColor = 8;
        float tiempoMinBateria = 5;
        float tiempoMaxBateria = 15;
        float tiempoMinKit = 30;
        float tiempoMaxKit = 35;

        int nivel = MenuPrincipal.Instance.nivelActual;
        if (nivel == 1)
        {
            tiempoMinAsteroides = 0;
            tiempoMinAsteroides = 1f;
            tiempoMinAsteroidesColor = 5;
            tiempoMinAsteroidesColor = 6;
            tiempoMinBateria = 7;
            tiempoMaxBateria = 17;
            tiempoMinKit = 32;
            tiempoMaxKit = 37;
        }
        if (nivel == 2)
        {
            List<float> tiempoDecimal = new List<float> {0.1f, 0.2f, 0.3f, 0.4f, 0.5f, 0.6f, 0.7f};
            int indice = Random.Range(0, tiempoDecimal.Count);

            float decimalt = tiempoDecimal[indice];
            tiempoMinAsteroides = 0;
            tiempoMinAsteroides = 0.5f + decimalt;
            tiempoMinAsteroidesColor = 4;
            tiempoMinAsteroidesColor = 6;
            tiempoMinBateria = 8;
            tiempoMaxBateria = 19;
            tiempoMinKit = 34;
            tiempoMaxKit = 39;
        }
        // Estos son los tiempos base antes de haber aplicado el escalador
        if (nivel == 3)
        {
            List<float> tiempoDecimal = new List<float> { 0.1f, 0.2f, 0.3f, 0.4f, 0.5f};
            int indice = Random.Range(0, tiempoDecimal.Count);

            float decimalt = tiempoDecimal[indice];
            tiempoMinAsteroides = 0;
            tiempoMinAsteroides = 0.3f + decimalt;
            tiempoMinAsteroidesColor = 4;
            tiempoMinAsteroidesColor = 6;
            tiempoMinBateria = 10;
            tiempoMaxBateria = 20;
            tiempoMinKit = 35;
            tiempoMaxKit = 40;
        }

        matrizCarriles = MatrizCarriles.Instance.matrizCarriles;
        StartCoroutine(AparecerObjetosCoroutine(null, asteroides, "Asteroide", tiempoMinAsteroides, tiempoMaxAsteroides));
        StartCoroutine(AparecerObjetosCoroutine(null, verdes, "Verde", tiempoMinAsteroidesColor, tiempoMaxAsteroidesColor));
        if (nivel >= 1) { StartCoroutine(AparecerObjetosCoroutine(null, naranjas, "Naranja", tiempoMinAsteroidesColor, tiempoMaxAsteroidesColor)); }
        if (nivel >= 3) { StartCoroutine(AparecerObjetosCoroutine(null, rosas, "Rosa", tiempoMinAsteroidesColor, tiempoMaxAsteroidesColor)); }
        StartCoroutine(AparecerObjetosCoroutine(null, bateria, "Bateria", tiempoMinBateria, tiempoMaxBateria));
        StartCoroutine(AparecerObjetosCoroutine(null, kits, "Kit Reparacion", tiempoMinBateria, tiempoMaxBateria));
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
        if (posMatrizPrevia == null) { posMatrizPrevia = GenerarPos("FIRST IF"); }
        List<int> posMatriz = GenerarPos(nombre);
        if (!posMatrizPrevia.SequenceEqual(posMatriz))
        {
            GameObject objeto = objetos[Random.Range(0, objetos.Length - 1)].GetPooledGameObject();
            if (!objeto.GetComponent<MeshRenderer>().enabled) { objeto.GetComponent<MeshRenderer>().enabled = true; }
            objeto.transform.position = new Vector3(matrizCarriles[posMatriz[0], posMatriz[1]].x, matrizCarriles[posMatriz[0], posMatriz[1]].y, transform.position.z);
            objeto.SetActive(true);
            StartCoroutine(AparecerObjetosCoroutine(posMatriz, objetos, nombre, tiempoMin, tiempoMax));
        }
        else if (posMatrizPrevia.SequenceEqual(posMatriz))
        {
            while (posMatrizPrevia.SequenceEqual(posMatriz)) { posMatriz = GenerarPos("WHILE"); }
            StartCoroutine(AparecerObjetosCoroutine(posMatriz, objetos, nombre, tiempoMin, tiempoMax));
        }
    }

    public void GenerarUnObjeto(SimpleObjectPool[] objetos, string nombre)
    {
        List<int> posMatriz = GenerarPos(nombre);
        GameObject objeto = objetos[Random.Range(0, objetos.Length - 1)].GetPooledGameObject();
        if (!objeto.GetComponent<MeshRenderer>().enabled) { objeto.GetComponent<MeshRenderer>().enabled = true; }
        objeto.transform.position = new Vector3(matrizCarriles[posMatriz[0], posMatriz[1]].x, matrizCarriles[posMatriz[0], posMatriz[1]].y, transform.position.z);
        objeto.SetActive(true);

    }

}