using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    PlayerScript playerScript;

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
            Debug.Log("Grabbed key");
            this.gameObject.SetActive(false);
            //playerScript.hasKey = true;
            // Update player's key state and notify listeners
            playerScript.TriggerKeyChange(true);
        }
    }


}
