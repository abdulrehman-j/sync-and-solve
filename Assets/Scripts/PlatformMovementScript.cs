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


    private void Awake()
    {
        playerMovementScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovementScript>();       
        rb = GetComponent<Rigidbody2D>();
        playerRb=GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();

        originalGravity = playerRb.gravityScale; // Store original gravity scale
    }
    // Start is called before the first frame update
    void Start()
    {
        targetPos = pointB.position;
        DirectionCalculate();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, pointA.position) < 0.02f)
        {
            if (playerMovementScript.grounded)
            {
                StartCoroutine(ChangeGravityTemporarily()); //to temporarly change gravity
            }

            targetPos = pointB.position;
            DirectionCalculate();

        }
        if (Vector2.Distance(transform.position, pointB.position) < 0.02f)
        {
            if (playerMovementScript.grounded)
            {
                StartCoroutine(ChangeGravityTemporarily()); //to temporarly change gravity
            }
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
        Debug.Log(originalGravity);
        playerRb.gravityScale = 10; // Set gravity to 10
        yield return new WaitForSeconds(0.05f); // Wait for 1 second
        playerRb.gravityScale = originalGravity; // Restore original gravity scale
    }
}
