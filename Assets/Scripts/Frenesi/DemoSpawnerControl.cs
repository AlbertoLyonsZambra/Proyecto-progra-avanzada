using UnityEngine;
using System.Collections;

public class DemoSpawnerControl: GenericSingleton<DemoSpawnerControl> {

	public Transform[] spawners;
	public GameObject enemyMeleePrefab, enemyMeleePrefab2;
	[HideInInspector]public int enemyCount, specialEnemyCount;
	
	public  int maxEnemyCount = 10;
	private float spawnMeleeNext = 0.0f;
	private float spawnMeleeRate = 1.0f;
	
	void Update () {
		
		SpawnEnemy();
	}
	

	
	private void SpawnEnemy(){
		if(Time.time > spawnMeleeNext && enemyCount <= maxEnemyCount){
			spawnMeleeNext = Time.time + spawnMeleeRate;
            GameObject[] enemyPrefabs = new GameObject[] { enemyMeleePrefab, enemyMeleePrefab2};
			if (spawners != null && spawners.Length > 0)
			{
				int rand = Random.Range(0, spawners.Length);

				if (spawners[rand] != null)
				{
					Vector3 spawnPos = spawners[rand].position;
					// Resto del código de spawnEnemyPrefab
					int randomIndex = Random.Range(0, enemyPrefabs.Length);

					// Asignar el prefab seleccionado a spawnEnemyPrefab
					GameObject spawnEnemyPrefab = enemyPrefabs[randomIndex];

					Instantiate(spawnEnemyPrefab, spawnPos, Quaternion.identity);
                    float randEnemy = Random.value;
                    enemyCount++;
				}
				

                else
                {
                    Debug.LogWarning("El elemento seleccionado de spawners es nulo.");
                }
            }
            else
            {
                Debug.LogWarning("El arreglo spawners no está inicializado o no contiene elementos.");
            }

            // Generar un índice aleatorio para seleccionar un prefab
        }
		
	}
}