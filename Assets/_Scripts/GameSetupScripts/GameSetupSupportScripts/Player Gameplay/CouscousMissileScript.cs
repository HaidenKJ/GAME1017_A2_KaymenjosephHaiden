using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CouscousMissileScript : MonoBehaviour
{
    public float speed = 10f; // Missile movement speed
    public float explosionRadius = 5f; // Radius in which enemies get destroyed
    public float trackingStrength = 8f; // Adjusts how fast it turns towards enemies
    public AudioClip SpecialWeaponSFX;

    private Transform target;
    private Rigidbody2D rb;
    private AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        // Play the roar sound on launch
        if (audioSource && SpecialWeaponSFX)
        {
            audioSource.PlayOneShot(SpecialWeaponSFX);
        }

        // Find the nearest enemy when the missile spawns
        target = FindClosestEnemy();
    }

    void FixedUpdate()
    {
        if (target == null)
        {
            target = FindClosestEnemy(); // Keep searching for a target if lost
            if (target == null) return; // If no target, missile keeps moving forward
        }

        // Seek towards the enemy
        Vector2 direction = ((Vector2)target.position - rb.position).normalized;
        Vector2 newVelocity = Vector2.Lerp(rb.velocity, direction * speed, trackingStrength * Time.fixedDeltaTime);
        rb.velocity = newVelocity;
    }

    Transform FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Transform closestEnemy = null;
        float minDistance = Mathf.Infinity;
        Vector2 currentPosition = transform.position;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(currentPosition, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestEnemy = enemy.transform;
            }
        }

        return closestEnemy;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Explode();
        }
    }

    void Explode()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                Destroy(enemy.gameObject); // Destroy all enemies within the explosion radius
            }
        }

        Destroy(gameObject); // Destroy the missile itself
    }

    void OnDrawGizmosSelected()
    {
        // Draw explosion radius in the Scene view
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
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
