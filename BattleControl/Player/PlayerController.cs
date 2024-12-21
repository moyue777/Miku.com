using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public Transform ceillingCheck;
    public Transform groundCheck; 
    public LayerMask groundObjects;
    public float checkRadius;
    public int maxJumpCount;
    
    public Animator animator;
    private Rigidbody2D rb;
    private bool isJumping = false;
    private bool facingRight = true;
    private float moveDirection1;
    private float moveDirection2;
    private bool isGrounded;
    private int jumpCount;
    private void Awake() 
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        jumpCount = maxJumpCount;
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        // check if grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundObjects);
        if (isGrounded && jumpCount < maxJumpCount)
        {
            jumpCount = maxJumpCount;
        }

        // move
        rb.velocity = new Vector2(moveDirection1 * moveSpeed, rb.velocity.y);
        if (moveDirection1 * moveSpeed != 0)
        {
            animator.SetBool("isWalking",true);
        }else
        {
            animator.SetBool("isWalking",false);
        }

        // jump
        if (isJumping && isGrounded)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Force);
            jumpCount--;
            isJumping = false;
        }
    }

    void Update()
    {
        //Debug.Log(" isJumping: " + isJumping);
        // processing inputs
        moveDirection1 = Input.GetAxis("Horizontal");

        // processing inputs for jump
        if (Input.GetButtonDown("Jump") && jumpCount > 0)
        {
            isJumping = true;
        }

        // animate
        if (moveDirection1 < 0 && facingRight)
        {
            FlipCharacter();
        }
        else if (moveDirection1 > 0 && !facingRight)
        {
            FlipCharacter();
        }

    }

    private void FlipCharacter()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}