using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public Transform ceillingCheck;
    public Transform groundCheck;
    public LayerMask groundObjects;
    private float moveSpeed = 5;
    private bool canMove = false;
    private float jumpForce;
    private float checkRadius;
    private int maxJumpCount;

    public Animator animator;
    private Rigidbody2D rb;
    private bool isJumping = false;
    private bool facingRight = true;
    private float moveDirection1;
    private bool isGrounded;
    private int jumpCount;
    public void Set_canMove(bool set)
    {
        canMove = set;
    }

    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
        jumpCount = maxJumpCount;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
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
        if (canMove)
        {
            rb.velocity = new Vector2(moveDirection1 * moveSpeed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        if (canMove && moveDirection1 * moveSpeed != 0)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SuperController.Instance.CallSystem();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            gameObject.transform.position = new Vector2(0, 0);
        }
        // processing inputs
        moveDirection1 = Input.GetAxis("Horizontal");

        // processing inputs for jump
        if (Input.GetButtonDown("Jump") && jumpCount > 0)
        {
            isJumping = true;
        }

        // animate
        if (canMove)
        {
            if (moveDirection1 < 0 && facingRight)
            {
                FlipCharacter();
            }
            else if (moveDirection1 > 0 && !facingRight)
            {
                FlipCharacter();
            }
        }
    }

    private void FlipCharacter()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}