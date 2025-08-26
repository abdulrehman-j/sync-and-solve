using System.Collections;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    public Transform player; 
    public float smoothSpeed = 0.2f; 
    public Vector3 offset; 

    private Vector3 velocity = Vector3.zero; // persistent velocity
    private float fixedY; // keeps camera’s Y stable

    private IEnumerator Start()
    {
        Camera mainCamera = Camera.main;

        // Wait until the player is spawned
        while (GameObject.FindGameObjectWithTag("Player") == null)
        {
            yield return null;
        }

        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Fix Y position once at start
        fixedY = transform.position.y;
    }

    private void LateUpdate()
    {
        if (player == null) return;

        // Only follow player on X, Y stays fixed
        Vector3 desiredPosition = new Vector3(player.position.x + offset.x, fixedY + offset.y, transform.position.z);

        // Smooth movement
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);

        transform.position = smoothedPosition;
    }
}
