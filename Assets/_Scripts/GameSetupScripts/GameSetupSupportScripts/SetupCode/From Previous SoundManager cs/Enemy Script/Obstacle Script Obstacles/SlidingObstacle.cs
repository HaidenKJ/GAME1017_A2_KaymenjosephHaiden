using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingObstacle : MonoBehaviour
{
    [SerializeField] private float ObstacleMoveSpeed = 7f;
    [SerializeField] private float spawnXPosition = 15f; // Spawn position on the X-axis
    [SerializeField] private float despawnXPosition = -15f; // X position for despawning

    private void Start()
    {
        SetFixedSpawnPosition();
    }

    private void Update()
    {
        // Move leftward
        transform.Translate(Vector3.left * ObstacleMoveSpeed * Time.deltaTime);

        // Respawn if off-screen
        if (transform.position.x <= despawnXPosition)
        {
            TeleportToSpawn();
        }
    }

    public void SetFixedSpawnPosition()
    {
        transform.position = new Vector3(spawnXPosition, -3.5f, 0f); 
    }

    public void TeleportToSpawn()
    {
        SetFixedSpawnPosition();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            PlayerScript player = collider.GetComponent<PlayerScript>();
            if (player != null)
            {
                player.TakeDamage(); // Decrease the player's lives and update UI
            }
            TeleportToSpawn();
        }
    }
}
