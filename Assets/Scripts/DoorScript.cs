using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    PlayerScript playerScript;
    public bool isLocked=true;

    private void Awake()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && playerScript.hasKey)
        {
            isLocked = false;
            playerScript.hasKey = false;
            Debug.Log("Door Unlocked");
        }
    }
}
