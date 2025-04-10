using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab; // The enemy prefab to spawn

    void Start()
    {
        if (enemyPrefab == null)
        {
            Debug.LogError("Enemy Prefab is missing!");
            return;
        }

        // Spawn the first enemy at the beginning of the game
        SpawnNewEnemy(new Vector3(8f, Random.Range(-4f, 4f), 0f)); // Initial spawn at random position
    }
    public void SpawnNewEnemy(Vector3 spawnPosition)
{
    if (enemyPrefab != null)
    {
        // Spawn the new enemy
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
    else
    {
        Debug.LogError("Enemy Prefab is missing or destroyed!");
    }
}
}
