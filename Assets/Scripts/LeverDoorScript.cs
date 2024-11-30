using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverDoorScript : MonoBehaviour
{
    private float closedScaleY = 1f; // Full height (door is closed)
    private float openScaleY = 0f; // Minimal height (door is open)
    private float changeSpeed = 2f; // Speed of scaling the door

    private bool isOpening = false; // Track if the door is opening
    private bool isClosing = false; // Track if the door is closing

    private void Start()
    {
       closedScaleY=transform.localScale.y;
    }

    void Update()
    {
        if (isOpening)
        {
            Vector3 newScale = transform.localScale;
            Vector3 newPosition = transform.position;

            // Calculate how much the scale is changing
            float scaleChange = Mathf.MoveTowards(newScale.y, openScaleY, changeSpeed * Time.deltaTime) - newScale.y;

            newScale.y += scaleChange; // Adjust the scale upwards (for opening)
            newPosition.y -= scaleChange / 2f; // Move the door upwards (move opposite of scale change)

            transform.localScale = newScale;
            transform.position = newPosition;

            if (Mathf.Approximately(newScale.y, openScaleY))
                isOpening = false; // Stop opening when fully open
        }
        else if (isClosing)
        {
            Vector3 newScale = transform.localScale;
            Vector3 newPosition = transform.position;

            // Calculate how much the scale is changing
            float scaleChange = Mathf.MoveTowards(newScale.y, closedScaleY, changeSpeed * Time.deltaTime) - newScale.y;

            newScale.y += scaleChange; // Adjust the scale downwards (for closing)
            newPosition.y -= scaleChange / 2f; // Move the door downwards (move opposite of scale change)

            transform.localScale = newScale;
            transform.position = newPosition;

            if (Mathf.Approximately(newScale.y, closedScaleY))
                isClosing = false; // Stop closing when fully closed
        }
    }

    public void OpenDoor()
    {
        isOpening = true;
        isClosing = false; // Stop closing if currently closing
    }

    public void CloseDoor()
    {
        isClosing = true;
        isOpening = false; // Stop opening if currently opening
    }
}
