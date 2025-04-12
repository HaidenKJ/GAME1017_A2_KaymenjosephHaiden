using UnityEngine;

public class RollUnderObstacle : MonoBehaviour
{
    public float rayLength = 5f;
    private int rollUnderCount = 0;
    private bool wasPlayerBelowLastFrame = false;

    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, rayLength);

        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            PlayerScript player = hit.collider.GetComponent<PlayerScript>();
            if (player != null && player.isCrouching)
            {
                Debug.Log("Player crouched under obstacle."); // ← Debug every frame it's hitting

                bool isNowBelow = hit.collider.transform.position.y < transform.position.y;

                if (isNowBelow && !wasPlayerBelowLastFrame)
                {
                    rollUnderCount++;
                    Debug.Log($"Player Rolled Under Obstacle! Count: {rollUnderCount}");

                    if (rollUnderCount == 10)
                    {
                        Debug.Log(" Player Rolled Under Obstacle 10 Times!");
                    }
                }

                wasPlayerBelowLastFrame = isNowBelow;
            }
        }
        else
        {
            wasPlayerBelowLastFrame = false;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * rayLength);
    }
}
