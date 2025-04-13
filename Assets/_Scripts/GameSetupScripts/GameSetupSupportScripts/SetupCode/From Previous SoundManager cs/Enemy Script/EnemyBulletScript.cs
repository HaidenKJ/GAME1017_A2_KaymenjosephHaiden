using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    [SerializeField] private float bulletMoveSpeed = 8f;

    void Update()
    {
        transform.Translate(-bulletMoveSpeed * Time.deltaTime, 0f, 0f);
    }
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
        }
    }
}

