﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CollisionInteraction : MonoBehaviour
{
    private GameManager gm;
    private const string FLIP_LAYER = "Flip";
    private int flipLayer = 0;

    private const string DEATH_LAYER = "Hazard";
    private int deathLayer = 0;

    private const string CHECKPOINT = "Checkpoint";
    private int checkpointLayer = 0;


    PlayerControllerS playerController;

    private SpriteRenderer spriteRend;
    private float flipAngle = 180;

    public bool isSkeleton = false;

    [SerializeField]
    float respawnInSeconds = 4f;

    [SerializeField]
    private GameObject bloodEffect;

    [SerializeField]
    private GameObject boneEffect;

    public Transform respawnPoint;
    //[SerializeField]
    //private float particleTime;


    private void Awake()
    {

        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();

        transform.position = gm.checkpointData;

        respawnPoint.position = gameObject.transform.position;

        flipLayer = LayerMask.NameToLayer(FLIP_LAYER); 
        deathLayer = LayerMask.NameToLayer(DEATH_LAYER); 
        checkpointLayer = LayerMask.NameToLayer(CHECKPOINT);

        Debug.Log(checkpointLayer);

        playerController = GetComponent<PlayerControllerS>();
        spriteRend = GetComponent<SpriteRenderer>();

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == flipLayer)
        {
            FlipGravity(-1);
            Debug.Log(playerController.flippedGravity);
            RipFlesh();
        }

        else if(collision.gameObject.layer == deathLayer && 
            playerController.rb.velocity.y > 0)
        {
            RipFlesh();
            // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            gm.goGone = true;
            gameObject.SetActive(false);
            //gameObject.transform.position = respawnPoint.transform.position;

        }

        ///When colliding with an object with the checkpoint layer,
        ///record the new respawn position by storing the vectors of the checkpoint.
        else if(collision.gameObject.layer == checkpointLayer)
        {
            Debug.Log("Transform Detected");
            //respawnPoint.position = collision.gameObject.transform.position;
        }
    }

    public void FlipGravity(float flipFactor)
    {
        playerController.flippedGravity = !playerController.flippedGravity;
        playerController.rb.gravityScale = playerController.rb.gravityScale * flipFactor;
        playerController.FlipVertical(flipAngle); //flips the whole transform
        playerController.jumpHeight *= flipFactor;
        //playerController.flipDirection *= flipFactor;
    }

    public void RipFlesh()
    {
        //Object Pool some of this stuff later.
        Debug.Log("Ripping Flesh...");
        spriteRend.material.SetFloat("_Alpha", 0f);

        if (!isSkeleton)
        {
            bloodEffect.transform.parent = null;
            bloodEffect.SetActive(true);
            isSkeleton = true;
        }
        else
        {
            boneEffect.transform.parent = null;
            boneEffect.SetActive(true);
        }
    }


    
}