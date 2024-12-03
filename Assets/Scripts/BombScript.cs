using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    public Transform pointA; // Assign PointA in the Inspector
    public Transform pointB; // Assign PointB in the Inspector
    public float speed = 2f; // Movement speed
    bool isMoving = true;

    PlayerScript playerScript;

    private Vector3 targetPosition; // Current target position
    Animator animator;

    private void Start()
    {
        
        //// Wait until the player is spawned in the scene
        //while (GameObject.FindGameObjectWithTag("Player") == null)
        //{
        //    yield return null; // Wait one frame
        //}

        // Once the player is found, get the PlayerScript component
       targetPosition = transform.position;
    }
   
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (playerScript == null) {
            playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        }
        if (!isMoving)
            return;
        // Move the prefab towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        Debug.Log(targetPosition);
        // Check if the prefab has reached the target position
        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            // Switch the target position to the other point
            if (targetPosition == pointA.position)
            {
                targetPosition = pointB.position;
            }
            else
            {
                targetPosition = pointA.position;
            }
        }
    }

    private void OnTriggerEnter2D (Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            playerScript.isDead = true; // Kill the player
            isMoving = false; 
            animator.SetBool("isTriggered", true);
            Debug.Log("Player touched the laser");

            StartCoroutine(HandleExplosionAnimation());
        }
    }

    private IEnumerator HandleExplosionAnimation()
    {
        // Get the length of the explosion animation
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        float animationLength = stateInfo.length;

        yield return new WaitForSeconds(animationLength);

        animator.SetBool("isTriggered", false); // Reset the trigger

        Renderer bombRenderer = GetComponent<Renderer>();

        bombRenderer.enabled = false;
        
    }

}
