using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroidScript : MonoBehaviour
{
    [SerializeField] private float AstroidMoveSpeed = 5f;
    [SerializeField] private float spawnXPosition = 8f; // Spawn position on the X-axis
    [SerializeField] private float despawnXPosition = -10f; // X position for despawning
    // [SerializeField] private Animator asteroidAnimator; // Reference to the Animator component

    private float randomYPosition;

    void Start()
    {
        SetRandomSpawnPosition();
    }

    void Update()
    {
        // Move leftward
        transform.Translate(Vector3.left * AstroidMoveSpeed * Time.deltaTime);

        // Respawn if off-screen
        if (transform.position.x <= despawnXPosition)
        {
            TeleportToSpawn();
        }
    }

    public void SetRandomSpawnPosition()
    {
        randomYPosition = Random.Range(-4f, 4f); // Adjust vertical range as needed
        transform.position = new Vector3(spawnXPosition, randomYPosition, 0f);
    }

    public void TeleportToSpawn()
    {
        SetRandomSpawnPosition();
    }

    // Triggered when the asteroid collides with the player (using trigger event)
    private void OnTriggerEnter2D(Collider2D collider)
    {
        // Check if the collision is with the player
        if (collider.CompareTag("Player"))
        {
            // Call the player's TakeDamage method
            PlayerScript player = collider.GetComponent<PlayerScript>();
            if (player != null)
            {
                player.TakeDamage(); // Decrease the player's lives and update UI
            }
            TeleportToSpawn();
        }
    }
}