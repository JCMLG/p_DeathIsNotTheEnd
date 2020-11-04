using UnityEngine;

public class GravityFlip : MonoBehaviour
{
    private GameObject _playerGO;
    private PlayerMovement _playerMove;
    private PlayerJump _playerJump;
    private float _flipAngle = 180;

    private void Awake()
    {
        _playerGO = gameObject;
        _playerMove = GetComponent<PlayerMovement>();
        _playerJump = GetComponent<PlayerJump>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Flip"))
        {
            FlipGravity(1);
        }
    }

    public void FlipGravity(float flipFactor)
    {
        Physics2D.gravity = new Vector2(0, 9.81f * flipFactor);
        _playerMove.FlipPlayer(_flipAngle);
        _playerJump.flipFactor = -1;
    }
}
