using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlatformMovementScript : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 5.0f;
    private Vector3 targetPos;

    PlayerMovementScript playerMovementScript;
    Rigidbody2D rb; //rb of platform
    Vector3 moveDirection;

    Rigidbody2D playerRb;

    private float originalGravity;

    private IEnumerator Start()
    {
        targetPos = pointB.position;
        DirectionCalculate();
        // Wait until the player is spawned in the scene
        while (GameObject.FindGameObjectWithTag("Player") == null)
        {
            yield return null; // Wait one frame
        }

        playerMovementScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovementScript>();
        //rb = GetComponent<Rigidbody2D>();
        playerRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        originalGravity = playerRb.gravityScale; // Store original gravity scale
    }

    private void Awake()
    {
        //playerMovementScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovementScript>();       
        rb = GetComponent<Rigidbody2D>();
        //playerRb=GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();

        //originalGravity = playerRb.gravityScale; // Store original gravity scale
    }
    //// Start is called before the first frame update
    //void Start()
    //{
    //    targetPos = pointB.position;
    //    DirectionCalculate();
    //}

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, pointA.position) <= 0.1f)
        {
            if (playerMovementScript.grounded && playerMovementScript.isOnPlatform)
            {
                StartCoroutine(ChangeGravityTemporarily()); //to temporarly change gravity
            }

            targetPos = pointB.position;
            DirectionCalculate();

        }
        if (Vector2.Distance(transform.position, pointB.position) <= 0.1f)
        {
            if (playerMovementScript.grounded && playerMovementScript.isOnPlatform)
            {
                StartCoroutine(ChangeGravityTemporarily()); //to temporarly change gravity
            }
            targetPos = pointA.position;
            DirectionCalculate();
        }

        // Check if the platform has passed pointA or pointB and reverse direction if necessary
        if (HasPassedPoint(pointA.position))
        {
            targetPos = pointB.position;
            DirectionCalculate();
        }
        else if (HasPassedPoint(pointB.position))
        {
            targetPos = pointA.position;
            DirectionCalculate();
        }
    }
    private void FixedUpdate()
    {
        rb.velocity = moveDirection * speed;

    }
    private void DirectionCalculate()
    {
        moveDirection = (targetPos - transform.position).normalized;
    }
    private bool HasPassedPoint(Vector3 point)
    {
        // Check if the platform has passed a point by comparing the current position with the point
        // We check this based on the direction of movement

        float dotProduct = Vector3.Dot(moveDirection, point - transform.position);

        // If the dot product is less than 0, the platform has passed the point (it is moving away from it)
        return dotProduct < 0;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerMovementScript.isOnPlatform = true;
            playerMovementScript.platformRb = rb;
          

            Debug.Log("On Trigger Enter");
        }
        
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerMovementScript.isOnPlatform = false;
           // playerRb.gravityScale = playerRb.gravityScale / 10;
            Debug.Log("On Trigger EXIT");
        }

    }
    private void OnDrawGizmos()
    {
        //for visulization
        if(pointA!= null && pointB!=null)
        {
            Gizmos.DrawLine(transform.position, pointA.position);
            Gizmos.DrawLine(transform.position, pointB.position);
        }
    }

    // Coroutine to change gravity scale temporarily
    private IEnumerator ChangeGravityTemporarily()
    {
        //Debug.Log(originalGravity);
        playerRb.gravityScale = 10; // Set gravity to 10
        yield return new WaitForSeconds(0.05f); // Wait for 1 second
        playerRb.gravityScale = originalGravity; // Restore original gravity scale
    }
}
