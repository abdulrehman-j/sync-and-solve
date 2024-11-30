using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    PlayerScript playerScript;
    public bool isLocked = true;  // Starts as locked
    private bool playerInTrigger = false;  // Tracks if player is near the door
    public Animator rocketAnimator;  // Animator for the rocket door
    public GameObject levelCompleteUI;  // UI for "Level Complete"

    private void Awake()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = true;
            if (playerScript.hasKey)
            {
                isLocked = false;
                playerScript.hasKey = false;  // Key is used
                Debug.Log("Door Unlocked");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = false;
        }
    }

    private void Update()
    {
        // Check if player is near and presses "E" to complete the level
        if (playerInTrigger && !isLocked && Input.GetKeyDown(KeyCode.E))
        {
            CompleteLevel();
            playerScript.gameObject.SetActive(false);
        }
    }

    private void CompleteLevel()
    {
        Debug.Log("Level Complete!");
        rocketAnimator.SetTrigger("TakeOff");  // Play the rocket animation
        ShowLevelCompleteUI();
    }

    private void ShowLevelCompleteUI()
    {
        if (levelCompleteUI != null)
            levelCompleteUI.SetActive(true);
    }
}
