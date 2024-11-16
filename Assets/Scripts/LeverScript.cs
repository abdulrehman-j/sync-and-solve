using UnityEngine;

public class LeverScript : MonoBehaviour
{
    public LeverDoorScript door; // Reference to the door script
    public float tiltAngle = 25f; // Maximum tilt angle for the lever

    private bool isPlayerOnLeft = false; // Is player on the left
    private bool isPlayerOnRight = false; // Is player on the right
    private bool isLeverActive = false; // Is lever active

    public BoxCollider2D blockCollider; // Separate collider to block the player

    void Start()
    {
        // Find the blocking collider (should be a child of the lever)
        blockCollider.enabled = false; // Disable it initially
        TiltLever(tiltAngle); // Tilt lever right
    }

    void Update()
    {
        if (isPlayerOnLeft)
        {
            TiltLever(-tiltAngle); // Tilt lever left
            if (!isLeverActive)
            {
                isLeverActive = true;
                door.OpenDoor(); // Open door
            }
        }
        else if (isPlayerOnRight)
        {
            TiltLever(tiltAngle); // Tilt lever right
            if (isLeverActive)
            {
                isLeverActive = false;
                door.CloseDoor(); // Close door
            }
        }
    }

    private void TiltLever(float angle)
    {
        transform.rotation = Quaternion.Euler(0, 0, angle); // Rotate the lever

        // Enable the blocking collider to block player movement
        blockCollider.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            float playerPosition = other.transform.position.x;
            float leverPosition = transform.position.x;

            if (playerPosition < leverPosition)
            {
                isPlayerOnLeft = true;
                isPlayerOnRight = false;
            }
            else if (playerPosition > leverPosition)
            {
                isPlayerOnRight = true;
                isPlayerOnLeft = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerOnLeft = false;
            isPlayerOnRight = false;

            // Disable the blocking collider to allow free movement
            blockCollider.enabled = true;
        }
    }
}
