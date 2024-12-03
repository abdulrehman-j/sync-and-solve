using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour
{
    private PlayerScript playerScript;
    public bool render = true;

    public float activeTime = 3f; // Time for which the laser is active
    public float inactiveTime = 2f; // Time for which the laser is inactive

    private Collider2D laserCollider; // The laser's collider component
    private Renderer laserRenderer; // The laser's renderer component

    private void Awake()
    {
        laserCollider = GetComponent<Collider2D>(); // Get the collider of the laser
        laserRenderer = transform.GetChild(0).GetComponent<Renderer>();

        // Start the laser cycle
        StartCoroutine(LaserCycle());
    }
    private IEnumerator Start()
    {
       
        // Wait until the player is spawned in the scene
        while (GameObject.FindGameObjectWithTag("Player") == null)
        {
            yield return null; // Wait one frame
        }
        // Once the player is found, get the PlayerScript component
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
    }

    private IEnumerator LaserCycle()
    {
        if (render)
        {
            while (true)
            {
                // Laser is active
                laserCollider.enabled = true; // Enable collision detection
                laserRenderer.enabled = true; // Make the laser visible
                yield return new WaitForSeconds(activeTime); // Wait for the active time

                // Laser is inactive (disappears and is safe for the player to pass through)
                laserCollider.enabled = false; // Disable collision detection
                laserRenderer.enabled = false; // Make the laser invisible
                yield return new WaitForSeconds(inactiveTime); // Wait for the inactive time
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && laserCollider.enabled) // Only trigger if the laser is active
        {
            playerScript.isDead = true; // Kill the player
            Debug.Log("Player touched the laser");
        }
    }
}
