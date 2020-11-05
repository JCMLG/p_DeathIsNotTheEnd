using UnityEngine;

public class GravityFlip : MonoBehaviour
{
    private GameObject _playerGO;
    private Rigidbody2D _playerRB;
    private PlayerMovement _playerMove;
    private PlayerJump _playerJump;
    private SpriteRenderer _spriteRend;
    private float _flipAngle = 180;

    [SerializeField]
    private GameObject bloodEffect;
    [SerializeField]
    private float particleTime;

    private void Awake()
    {
        _playerGO = gameObject;
        _playerMove = GetComponent<PlayerMovement>();
        _playerJump = GetComponent<PlayerJump>();
        _spriteRend = GetComponent<SpriteRenderer>();
        _playerRB = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Flip") && _playerRB.velocity.y < 0)
        {
            FlipGravity(-1);
            RipFlesh();
        }
    }

    public void FlipGravity(float flipFactor)
    {
        //Physics2D.gravity = new Vector2(0, 9.81f * flipFactor);
        _playerRB.gravityScale = _playerRB.gravityScale * flipFactor;
        _playerMove.FlipPlayer(_flipAngle);
        _playerJump.flipFactor = -1;
        _playerJump.flipDirection = -1;
    }

    public void RipFlesh()
    {
        _spriteRend.material.SetFloat("_Alpha", 0f);
        GameObject bloodSplat = Instantiate(bloodEffect, gameObject.transform.position, Quaternion.identity);
        Destroy(bloodSplat, particleTime);

    }
}
