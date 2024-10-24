using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    private float horizontal;
    public float speed = 8f;
    public float jumpingPower = 5f;
    public float jumpTime=0.5f;
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

    private PlayerScript playerScript; // Reference to the Player script
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerScript = GetComponent<PlayerScript>();
    }

    private void Update()
    {
        if (playerScript.isDead) //do diable movement 
            return;

        horizontal = Input.GetAxis("Horizontal");
        if (isOnPlatform)
        {
            rb.velocity = new Vector2((horizontal * speed) + platformRb.velocity.x, rb.velocity.y);
            animator.SetFloat("Speed", Mathf.Abs(horizontal*speed));
        }
        else
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
            animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        }
        if (rb.velocity.y < -0.5f) //we are falling
            animator.SetBool("isFalling", true);


        LimitFallingSpeed();
        CheckGround();
        flip();
        jump();
    }
    void LimitFallingSpeed()
    {
        // Check if the player's falling speed exceeds the maximum limit
        if (rb.velocity.y < maxFallingSpeed)
        {
            //Debug.Log($"falling velocity {rb.velocity.y}");
            // Limit the falling speed to the maximum allowed value
            rb.velocity = new Vector2(rb.velocity.x, maxFallingSpeed);
        }
    }

    private void flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector2 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void jump()
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
                rb.velocity = Vector2.up * (jumpingPower);
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
        if(grounded)
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", false);
        }
    }

}
