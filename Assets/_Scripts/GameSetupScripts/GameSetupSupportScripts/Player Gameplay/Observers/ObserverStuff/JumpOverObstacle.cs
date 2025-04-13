using UnityEngine;

public class JumpOverObstacle : MonoBehaviour
{
    public float rayLength = 5f; // Length of the raycast

    private int jumpOverCount = 0;
    private bool playerLeftLastFrame = false;  // Tracks the player's position relative to the ray in the previous frame
    [SerializeField] public AudioClip AchievementSucceded;

    private AudioSource audioSource;

    void Update()
    {
        // Raycast straight left from this GameObject
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, rayLength);

        // Check if the raycast hits something and that the object has the "Player" tag
        bool playerLeftNow = hit.collider != null && hit.collider.CompareTag("Player") && hit.collider.transform.position.x < transform.position.x; // Player is to the right of the obstacle (crossing from right to left)

        // We only want to count when the player crosses the ray's line from right to left
        if (playerLeftNow && !playerLeftLastFrame)
        {
            jumpOverCount++;
            // Debug.Log($"Player Jumped Over Obstacle! Count: {jumpOverCount}");

            if (jumpOverCount >= 20) // 20 times cuz it triggers on the way down too, meaning 10 up, 10 down
            {
                Debug.Log("🏆 Player Jumped Over Obstacle 10 Times!");
                // audioSource.PlayOneShot(AchievementSucceded);
            }
        }

        // Update the flag for the next frame to detect crossing
        playerLeftLastFrame = playerLeftNow;
    }

    void OnDrawGizmosSelected()
    {
        // Debugging aid: Draw the raycast line in the scene view
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.left * rayLength);
    }
}
