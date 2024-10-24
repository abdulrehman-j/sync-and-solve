using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    PlayerScript playerScript;

    private void Awake()
    {
        playerScript =GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Grabbed key");
            this.gameObject.SetActive(false);
            playerScript.hasKey = true;
        }
    }
}
