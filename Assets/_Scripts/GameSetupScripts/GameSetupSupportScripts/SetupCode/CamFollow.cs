using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // The player to follow
    public float smoothSpeed = 0.125f; // Smooth follow speed
    public Vector3 offset; // Offset from the player (position of camera)

    void LateUpdate()
    {
        // Calculate the desired position based on player position and offset
        Vector3 desiredPosition = player.position + offset;
        // Smooth the movement
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        // Update the camera position
        transform.position = smoothedPosition;
    }
}