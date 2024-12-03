using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    public Transform player; // Assign the player's transform in the Inspector
    public float smoothSpeed = 0.125f; // Smoothing speed for camera movement
    public Vector3 offset; // Offset to maintain player's relative position
    public PlayerMovementScript playerMovementScript;

    private float halfCameraWidth;

    private void Start()
    {
        Camera mainCamera = Camera.main;
        halfCameraWidth = mainCamera.orthographicSize * mainCamera.aspect;
        playerMovementScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovementScript>();
    }

    private void LateUpdate()
    {
        Vector3 desiredPosition = transform.position;

        // If the player is at the middle of the camera view, start shifting the camera
        float cameraRight = transform.position.x + halfCameraWidth / 4;
        float cameraLeft = transform.position.x - halfCameraWidth / 4;  

        if (player.position.x - cameraRight > 0)
        {
            // Move the camera horizontally so that the player is 1/4th to the left of the camera
            desiredPosition.x = player.position.x + halfCameraWidth * 0.8f;
            Vector3 velocity = Vector3.zero;
            transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
        }
        if (player.position.x - cameraLeft < 0)
        {
            desiredPosition.x = player.position.x - halfCameraWidth * 0.8f;
            Vector3 velocity = Vector3.zero;
            transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);

        }
    }
}
