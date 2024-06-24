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
		if(Time.time > spawnMeleeNext && enemyCount != maxEnemyCount){
			spawnMeleeNext = Time.time + spawnMeleeRate;
            GameObject[] enemyPrefabs = new GameObject[] { enemyMeleePrefab, enemyMeleePrefab2};
            int rand = Random.Range(0, spawners.Length);
			Vector3 spawnPos = spawners[rand].position;
			float randEnemy = Random.value;
            
            // Generar un índice aleatorio para seleccionar un prefab
            int randomIndex = Random.Range(0, enemyPrefabs.Length);

            // Asignar el prefab seleccionado a spawnEnemyPrefab
            GameObject spawnEnemyPrefab = enemyPrefabs[randomIndex];
           
            Instantiate(spawnEnemyPrefab,spawnPos,Quaternion.identity);
			enemyCount++;
		}
	}
}