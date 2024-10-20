using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public Transform player;       // Reference to the player's transform
    public Vector3 offset = new Vector3(0, 5, -10); // Offset from the player's position
    public float smoothSpeed = 0.125f; // Speed of the camera's smooth follow

    private void LateUpdate()
    {
        // Calculate the desired position behind the player
        Vector3 desiredPosition = player.position + offset;

        // Smoothly interpolate between the camera's current position and the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Set the camera's position
        transform.position = smoothedPosition;

        // Ensure the camera is looking at the player
        //transform.LookAt(player);
    }
}
