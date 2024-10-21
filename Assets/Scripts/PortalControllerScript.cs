using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalControllerScript : MonoBehaviour
{
    public Transform destinationPortal;
    public float distance = 0.4f; //distance between player and portal needed to teleport
    GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            //only teleport if minimum diatnce is met between the player and portal
            if (Vector2.Distance(other.transform.position, transform.position) > distance)
            {
                player.transform.position = destinationPortal.transform.position;
                Debug.Log($"Transporting {distance}");
            }
        }
    }
 
}
