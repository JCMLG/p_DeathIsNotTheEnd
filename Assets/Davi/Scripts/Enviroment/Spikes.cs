using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{


    private GameManager gm;

    private const string PLAYER_STR = "Player";
    private int playerLayer = 0;

    CollisionInteraction playerSkele;
    [SerializeField]
    private GameObject bloodParticles;
    [SerializeField]
    private GameObject boneParticles;
    [SerializeField]
    private GameObject playerGO;
    [SerializeField]
    private Rigidbody2D playerRB;
    [SerializeField]
    private float particleTime;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && playerRB.velocity.y > 0)
        {
            GameObject boneSplat = Instantiate(boneParticles, collision.transform.position, Quaternion.identity) as GameObject;
            Destroy(boneSplat, particleTime);
            playerGO.SetActive(false);
        }
    }
}