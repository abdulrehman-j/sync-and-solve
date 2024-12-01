using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour
{
    PlayerScript playerScript;
    
    private void Awake()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        Debug.Log(playerScript);
    }

    private void OnTriggerEnter2D (Collider2D other) 
    { 
        if (other.CompareTag("Player"))
        {
            playerScript.isDead = true;
            Debug.Log("Entered laser");
        }
    }
}
