using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerS : MonoBehaviour
{
    [Header("Basic Values")]


    [Tooltip("Higher values makes initial inputs read slower, allowing acceleration to be more noticable.")]
    [Range(0, .3f)] [SerializeField] private float movementSmoothing = .05f;
    public float runSpeed = 60f;

    [Range(1f, 15f)] public float jumpHeight = 8f;

    [Header("Advanced Modifiers")]
    [Tooltip("Toggles whether or not the player is able to move in the horizontal axis in mid-air.")]
    public bool airControl;

    [Tooltip("Toggles if the player is able to control their jump height with holding down the jump button.")]
    [SerializeField] public bool variableJumpHeight;

    [Tooltip("Controls the force applied to make the minimum jumps smaller." +
        "Higher values = smaller minimum jump")]
    [SerializeField] private float minJumpHeight;

    [Tooltip("The force of gravity applied to make the player fall faster when they let go of jump.")]
    [SerializeField] private float fallMultiplier;


    [SerializeField] Transform groundCheck;
    const float GROUND_CHECK_RADIUS = .2f;

    private Vector3 baseVelocity = Vector3.zero;
    [HideInInspector]
    public Rigidbody2D rb;
    private float horizontalMovement = 0f;

    private AudioManager audioClip;


    public bool flippedGravity = false;

    private bool jump;
    private bool isJumping;
    private bool isGrounded = false;
    private bool facingRight = true;


    // Start is called before the first frame update
    void Awake()
    {
       audioClip = FindObjectOfType<AudioManager>();

        // Debug.Log(flippedGravity);
        rb = GetComponent<Rigidbody2D>();
        //audioInstance = FindObjectOfType<AudioManager>();
    //rb = RigidbodyInterpolation2D.Interpolate;
}

//update reads input, passes jump as true, which in turn allows the physics based calculations to be active within
//Movement() which is nested within FixedUpdate()
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
               // Debug.Log("IntialJump");
                isJumping = true;
                jump = true;

                //audioInstance.Play("Jump");

            }
        }

    }


    private void FixedUpdate()
    {
        isGrounded = false;


        //Reads colliders inside gameobject, is able to read even if there are multiple colliders within said player body
        //can be converted to a circle collider in the scene
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
        if (isGrounded || airControl)
        {
            Vector3 targetVelocity = new Vector2(move * 10f, rb.velocity.y);
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref baseVelocity, movementSmoothing);

            #region Flip Sprite Horizontally
            if (move > 0 && !facingRight)
            {
               // Debug.Log("Flip to right");
                // ... flip the player.
                FlipHorizontal();
            }
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (move < 0 && facingRight)
            {
               // Debug.Log("Flip to left");
                // ... flip the player.
                FlipHorizontal();
            }
            #endregion
        }
        if (!variableJump)
        {
            DefaultJump(isGrounded, jump, variableJump);
        }

        else if (variableJump)
        {
            

            if (isGrounded)
            {
                isJumping = false;

                if (jump)
                {
                    rb.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
                    audioClip.Play("Jump");

                    isJumping = true;
                    isGrounded = false;
                }
            }
            VariableJump(isGrounded, isJumping, flippedGravity);
        }

    }



    public void DefaultJump(bool isGrounded, bool jump, bool inverse)
    {
        if (isGrounded && jump && !inverse)
        {
            isGrounded = false;
            rb.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
        }

        /* else if
         {
             rb.AddForce(Vector2.down * jumpHeight, ForceMode2D.Impulse);
         }
         */

    }


    private bool isFalling()
    {
        if (flippedGravity)
        {

            return rb.velocity.y < 0 ? true : false;
        }

        else
        {
            return rb.velocity.y > 0 ? true : false;
        }

    }

    private float getFlipFactor()
    {
        return flippedGravity ? -1f : 1f;

        #region Reference for inline code
        //if (flippedGravity)
        //    return -1f;

        //else
        //    return 1f;
        #endregion

    }


    public void VariableJump(bool isGrounded, bool isJumping, bool inverse)
    {
        //Something was wrong...
        if (isJumping || !isGrounded)
        {
            #region Old Jump Code
            if (!inverse)
            {
                //We're jumping upwards here.
                if (rb.velocity.y < 0)
                {
                    rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
                }

                else if (!Input.GetButton("Jump") && rb.velocity.y > 0)
                {
                    rb.velocity += Vector2.up * Physics2D.gravity.y * (minJumpHeight - 1) * Time.deltaTime;
                }

                
            }

            else if (inverse)
            {
                //Debug.Log("Inverse Gravity");
                if (rb.velocity.y > 0)
                {
                //    Debug.Log("Inverted Jump");
                    rb.velocity += Vector2.down * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
                   // Debug.Log(Physics2D.gravity.y);
                }

                else if (rb.velocity.y < 0 && !Input.GetButton("Jump"))
                {
                 //   Debug.Log("Inverted Short Hop");

                    rb.velocity += Vector2.down * Physics2D.gravity.y * (minJumpHeight - 1) * Time.deltaTime;

                  //  Debug.Log(Physics2D.gravity.y);
                }
            }
            #endregion

            #region New Jump Code
            //if (isFalling())
            //{
            //    rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime * getFlipFactor();
            //}

            //else if (!Input.GetButton("Jump"))
            //{
            //    rb.velocity += Vector2.up * Physics2D.gravity.y * (minJumpHeight - 1) * Time.deltaTime * getFlipFactor();
            //}
            #endregion

        }
    }


    public void FlipVertical(float value)
    {

        //Debug.Log("FLIPPING SPRITE = " + value);
        // float flipDirection;

        if (flippedGravity)
        {
            gameObject.transform.rotation = Quaternion.Euler(value, 0, 0);
        }

        else
        {
            gameObject.transform.rotation = Quaternion.Euler(value * 2, 0, 0);

        }



        //flipDirection = value;
    }

    //flips sprite horizontally
    private void FlipHorizontal()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

}
