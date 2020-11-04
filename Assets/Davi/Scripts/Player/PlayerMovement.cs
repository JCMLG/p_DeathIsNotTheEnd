using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D _playerRB;

    [SerializeField]
    private float moveSpeed;

    public float flipDirection;


    private void Awake()
    {
        _playerRB = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        Vector2 direction = new Vector2(x, y);

        Move(direction);
    }

    public void Move(Vector2 direction)
    {
        _playerRB.velocity = new Vector2(direction.x * moveSpeed, _playerRB.velocity.y);

        float x = Input.GetAxisRaw("Horizontal");
        switch (x)
        {
            case 1:
                gameObject.transform.rotation = Quaternion.Euler(0 + flipDirection, 0, 0);
                break;

            case -1:
                gameObject.transform.rotation = Quaternion.Euler(0 + flipDirection, 180, 0);
                break;
        }
    }

    public void FlipPlayer(float value)
    {
        Physics2D.gravity = new Vector2(0, 9.81f);
        gameObject.transform.rotation = Quaternion.Euler(value, 0, 0);
        flipDirection = value;
    }
}
