using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField]
    private float jumpForce, fallMultiplier, lowJumpMultiplier, checkRadius, hangTime;
    [SerializeField]
    private LayerMask groundMask;
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private bool _isGrounded;

    private Rigidbody2D _playerRB;
    private float _hangCounter;

    public int flipFactor = 1;

    private void Awake()
    {
        _playerRB = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && _hangCounter > 0)
        {
            Jump(flipFactor);
            Debug.Log("Jump call");
        }

        //Ground Check
        if (Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundMask))
        {
            _isGrounded = true;
        }
        else
        {
            _isGrounded = false;
        }

        // Coyote Time
        if (_isGrounded)
        {
            _hangCounter = hangTime;
        }
        else
        {
            _hangCounter -= Time.deltaTime;
        }

        ////Better Jump
        ////Long Jump
        //if (_playerRB.velocity.y < 0)
        //{
        //    _playerRB.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier) * Time.deltaTime;
        //}
        ////Short Hop
        //else if (_playerRB.velocity.y > 0 && !Input.GetButton("Jump"))
        //{
        //    _playerRB.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier) * Time.deltaTime;
        //}
    }

    public void Jump(int direction)
    {
        _playerRB.AddForce(Vector2.up * jumpForce * direction, ForceMode2D.Impulse);
    }


}
