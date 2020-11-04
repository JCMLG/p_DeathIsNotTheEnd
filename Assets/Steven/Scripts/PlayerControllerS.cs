using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerS : MonoBehaviour
{
    [Header("Basic Values")]

    [Range(0, .3f)] [SerializeField] private float movementSmoothing = .05f;
    public float runSpeed = 60f;

    [Range(1f, 15f)] public float jumpHeight = 8f;


    [Header("Advanced Modifiers")]
    public bool airControl;
    [SerializeField] private bool variableJumpHeight;
    [SerializeField] private float minJumpHeight;
    [SerializeField] private float fallMultiplier;


    [SerializeField] Transform groundCheck;
    const float GROUND_CHECK_RADIUS = .4f;

    private Vector3 baseVelocity = Vector3.zero;
    private Rigidbody2D rb;
    private float horizontalMovement = 0f;

    private bool jump;
    private bool isJumping;
    private bool isGrounded = false;
    private bool facingRight;


    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        //rb = RigidbodyInterpolation2D.Interpolate;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (!variableJumpHeight)
        {
            if (Input.GetButtonDown("Jump"))
            {
                jump = true;
            }

        }

        else if (variableJumpHeight)
        {
            if (Input.GetButtonDown("Jump"))
            {
                Debug.Log("IntialJump");
                isJumping = true;
                jump = true;

            }
        }

    }


    private void FixedUpdate()
    {

        isGrounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, GROUND_CHECK_RADIUS);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                isGrounded = true;
            }
        }

        Movement(horizontalMovement * Time.deltaTime, jump, variableJumpHeight);
        jump = false;
    }

    private void Movement(float move, bool jump, bool variableJump)
    {
        if (!variableJump)
        {
            if (isGrounded || airControl)
            {
                Vector3 targetVelocity = new Vector2(move * 10f, rb.velocity.y);
                rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref baseVelocity, movementSmoothing);

                if (isGrounded && jump)
                {
                    isGrounded = false;
                    rb.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
                }
            }
        }

        else if (variableJump)
        {
            Debug.Log("Variable Jumping Method Active");

            if (isGrounded || airControl)
            {
                Vector3 targetVelocity = new Vector2(move * 10f, rb.velocity.y);
                rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref baseVelocity, movementSmoothing);

                if (isGrounded)
                {
                    isJumping = false;

                    if (jump)
                    {
                        rb.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);

                        // rb.velocity = new Vector2(rb.velocity.x, 1 * jumpHeight);
                        isJumping = true;
                        isGrounded = false;

                    }

                }


                if (isJumping || !isGrounded)
                {
                    if (rb.velocity.y < 0)
                    {
                        Debug.Log("Fall Multiplier");
                        rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
                    }

                    else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
                    {
                        Debug.Log("Min Height");

                        rb.velocity += Vector2.up * Physics2D.gravity.y * (minJumpHeight - 1) * Time.deltaTime;
                    }
                }

                if (move > 0 && facingRight)
                {
                    // ... flip the player.
                    Flip();
                }
                // Otherwise if the input is moving the player left and the player is facing right...
                else if (move < 0 && facingRight)
                {
                    // ... flip the player.
                    Flip();
                }
            }
          
        }
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
