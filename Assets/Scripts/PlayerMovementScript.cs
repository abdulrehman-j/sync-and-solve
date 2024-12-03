using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    private float horizontal;
    public float speed = 6f;
    public float jumpingPower = 4.5f;  // Assuming a value for jumping power
    public float jumpTime = 0.1f;
    private float jumpTimeValue;
    private bool isJumping;
    private bool isFacingRight = false;

    public bool grounded;
    public BoxCollider2D groundCheck;
    public LayerMask groundMask;

    private Rigidbody2D rb;

    public bool isOnPlatform;
    public Rigidbody2D platformRb;

    public float maxFallingSpeed = -10f;

    public Animator animator;

    private PlayerScript playerScript;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerScript = GetComponent<PlayerScript>();
    }

    private void Update()
    {

        if (playerScript.isDead) //do diable movement 
        {
            animator.SetFloat("Speed", 0f);
            animator.SetBool("isFalling", false);
            animator.SetBool("isJumping", false);

            rb.gravityScale = 0;
            rb.velocity = Vector2.zero;

           return;
        }

        horizontal = Input.GetAxis("Horizontal");

        // If on platform, add the platform's horizontal velocity
        if (isOnPlatform)
        {
            rb.velocity = new Vector2((horizontal * speed) + platformRb.velocity.x, rb.velocity.y);
            animator.SetFloat("Speed", Mathf.Abs(horizontal * speed));
        }
        else
        {
            float airControlFactor = grounded ? 1f : 0.5f; // Full control on ground, limited in air
            float currentSpeed = horizontal * speed * airControlFactor;

            rb.velocity = new Vector2(currentSpeed, rb.velocity.y);
            animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        }

        if (rb.velocity.y < -0.5f) // We are falling
            animator.SetBool("isFalling", true);

        LimitFallingSpeed();
        CheckGround();
        Flip();
        Jump();
    }

    void LimitFallingSpeed()
    {
        // Check if the player's falling speed exceeds the maximum limit
        if (rb.velocity.y < maxFallingSpeed)
        {
            // Limit the falling speed to the maximum allowed value
            rb.velocity = new Vector2(rb.velocity.x, maxFallingSpeed);
        }
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector2 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void Jump()
    {
        if ((Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && grounded)
        {
            isJumping = true;
            animator.SetBool("isJumping", true);
            jumpTimeValue = jumpTime;
            rb.velocity = Vector2.up * jumpingPower;
        }

        if ((Input.GetButton("Jump") || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && isJumping)
        {
            if (jumpTimeValue > 0)
            {
                animator.SetBool("isJumping", true);
                jumpTimeValue -= Time.deltaTime;
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower); // Keep horizontal velocity constant
            }
            else
            {
                isJumping = false;
            }
        }

        if (Input.GetButtonUp("Jump") || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W))
        {
            isJumping = false;
        }
    }

    void CheckGround()
    {
        grounded = Physics2D.OverlapAreaAll(groundCheck.bounds.min, groundCheck.bounds.max, groundMask).Length > 0;
        if (grounded)
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", false);
        }
    }
}
