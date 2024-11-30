using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonDoorScript : MonoBehaviour
{
    public GameObject door;
    private SpriteRenderer spriteRenderer;
    Color oldColour, newColor;

    void Start()
    {
        // Get the SpriteRenderer component
        spriteRenderer = GetComponent<SpriteRenderer>();
        oldColour=spriteRenderer.color;
        newColor = Color.black;
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if ((other.gameObject.CompareTag("Player")) || (other.gameObject.CompareTag("Box")))
        {
            door.SetActive(false);
            spriteRenderer.color = newColor;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") || (other.gameObject.CompareTag("Box")))
        {
            door.SetActive(true);
            spriteRenderer.color = oldColour;   
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if ((other.gameObject.CompareTag("Player")) || (other.gameObject.CompareTag("Box")))
        {
            door.SetActive(false);
            spriteRenderer.color = newColor;
        }
    }
}
