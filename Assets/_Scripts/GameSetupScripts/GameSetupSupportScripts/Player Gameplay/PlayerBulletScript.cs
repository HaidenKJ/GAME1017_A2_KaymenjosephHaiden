using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletScript : MonoBehaviour
{
    [SerializeField] private float bulletMoveSpeed = 10f;

    void Update()
    {
        transform.Translate(bulletMoveSpeed * Time.deltaTime, 0f, 0f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) // Check if the collider is an enemy
        {
            EnemyScript enemy = collision.gameObject.GetComponent<EnemyScript>();
            if (enemy != null)
            {
                enemy.TeleportToSpawn(); // Call the correct method
            }

            Destroy(gameObject); // Destroy the player bullet
        }
    }
}
