using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Timer timer; 
    public PlayerScript PS;
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

    void Update()
    {
        GameOver();
    }

    void GameOver()
    {
        // Get the current scene name
        string currentScene = SceneManager.GetActiveScene().name;

        // Only trigger Game Over if we're in "EndSceneA2"
        if (currentScene == "EndSceneA2")
        {
            // Stop the timer
            timer.StopTimer();

            // Update the best time and show on the End screen
            BestTimeManager.Instance.DisplayBestTimeOnEndScreen();
        }
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