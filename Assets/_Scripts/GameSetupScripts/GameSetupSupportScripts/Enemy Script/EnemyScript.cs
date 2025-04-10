using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] private float enemyMoveSpeed = 3f; // Movement speed
    [SerializeField] private float spawnXPosition = 8f; // Spawn position on the X-axis
    [SerializeField] private float despawnXPosition = -10f; // X position for despawning
    [SerializeField] private GameObject enemyBulletPrefab; // Bullet prefab
    [SerializeField] private Transform shootingPoint; // Bullet spawn point
    [SerializeField] private float minShootingInterval = 1f; // Minimum interval for shooting
    [SerializeField] private float maxShootingInterval = 3f; // Maximum interval for shooting

    private float randomYPosition;

    void Start()
    {
        SetRandomSpawnPosition();  
        StartCoroutine(ShootAtRandomIntervals());
    }

    void Update()
    {
        // Move leftward
        transform.Translate(Vector3.left * enemyMoveSpeed * Time.deltaTime);

        // Respawn if off-screen
        if (transform.position.x <= despawnXPosition)
        {
            TeleportToSpawn();
        }

        // Ensure the enemy always faces the X-axis
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }

    private void SetRandomSpawnPosition()
    {
        randomYPosition = Random.Range(-4f, 4f); // Adjust vertical range as needed
        transform.position = new Vector3(spawnXPosition, randomYPosition, 0f);
    }

    public void TeleportToSpawn()
    {
        SetRandomSpawnPosition();
        transform.rotation = Quaternion.identity; // Ensure the enemy's orientation is reset
        Debug.Log("Enemy hit");
    }

    private IEnumerator ShootAtRandomIntervals()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minShootingInterval, maxShootingInterval));

            if (enemyBulletPrefab != null && shootingPoint != null)
            {
                Instantiate(enemyBulletPrefab, shootingPoint.position, Quaternion.identity);
            }
        }
    }
    
}