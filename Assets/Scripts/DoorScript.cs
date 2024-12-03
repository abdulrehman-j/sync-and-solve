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
    public GameObject scoreUI;  // UI for "Score"

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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = true;
            if (playerScript.hasKey)
            {
                isLocked = false;
                playerScript.hasKey = false;  // Key is used
                rocketAnimator.SetBool("isLocked", false);
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
        rocketAnimator.updateMode = AnimatorUpdateMode.UnscaledTime; // Ensure the animator uses unscaled time
        rocketAnimator.SetTrigger("TakeOff");  // Play the rocket animation
        ShowLevelCompleteUI();
        Time.timeScale = 0;
        StartCoroutine(ResetAnimatorAfterAnimation());
    }

    private void ShowLevelCompleteUI()
    {
        if (levelCompleteUI != null)
        {
            levelCompleteUI.SetActive(true);
            scoreUI.SetActive(false);
        }
     
    }

    private IEnumerator ResetAnimatorAfterAnimation()
    {
        yield return new WaitForSecondsRealtime(3f); // Wait for animation (use real-time since Time.timeScale = 0)
        rocketAnimator.updateMode = AnimatorUpdateMode.Normal; // Reset to default mode
    }

}