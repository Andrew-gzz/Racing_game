using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnerRocas : MonoBehaviour
{
    public Triggers near, far;
  
    public GameObject enemyPrefab;  // Asigna el prefab del enemigo en el inspector.
    public float spawnRangeZ = 10f; // Rango máximo en el eje Z.
    public float spawnInterval = 5f; // Intervalo de spawn en segundos.
    private bool isSpawning = false; // Bandera para controlar el inicio del spawn.

    private void Update()
    {
        // Verifica si el jugador está cerca y si no se ha iniciado el spawn.
        if (near.PlayerNear && !isSpawning)
        {
            isSpawning = true;          
            // Inicia la generación de enemigos.
            InvokeRepeating(nameof(SpawnEnemy), spawnInterval, spawnInterval);
        }
        if (far.PlayerFar && isSpawning)
        {
            isSpawning = false;
            near.PlayerNear = false;
            CancelInvoke(nameof(SpawnEnemy));
            
        }    
    }

    private void SpawnEnemy()
    {
        // Genera una posición aleatoria solo en el eje Z.
        Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + Random.Range(-spawnRangeZ, spawnRangeZ));

        // Instancia el enemigo en la posición generada.
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}