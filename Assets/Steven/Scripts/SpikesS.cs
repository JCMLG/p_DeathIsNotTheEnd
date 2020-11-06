using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesS : MonoBehaviour
{
    private GameManager gm;

    private const string PLAYER_STR = "Player";
    private int playerLayer = 0;

    CollisionInteraction playerSkele;



    private void Awake()
    {
       // playerSkele = GetComponent<CollisionInteraction>();
        playerLayer = LayerMask.NameToLayer(PLAYER_STR);
        Debug.Log(playerLayer);
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == playerLayer &&
            collision.attachedRigidbody.velocity.y > 0)
        {
          //  playerSkele = collision.GetComponent<CollisionInteraction>();


            //if (playerSkele.isSkeleton)
            //{
            //    Debug.Log("Layer of Collision: " + collision.gameObject.layer);
            //   // collision.TryGetComponent<CollisionInteraction>();
            //    Debug.Log("Player is now inactive");

            //    playerSkele.boneEffect.transform.parent = null;
            //    playerSkele.boneEffect.SetActive(true);
            //    gm.goGone = true;
            //    playerSkele.gameObject.SetActive(false);

            //}

        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {

       // playerSkele.isSkeleton = true;


    }

}
