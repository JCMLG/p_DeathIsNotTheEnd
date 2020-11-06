using System.Collections;
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
    public GameObject boneEffect;

    public Transform respawnPoint;
    //[SerializeField]
    //private float particleTime;
    private AudioManager audioClip;



    private void Awake()
    {
        audioClip = FindObjectOfType<AudioManager>();

        gm = GameObject.FindGameObjectWithTag("Respawn").GetComponent<GameManager>();
        transform.position = gm.checkpointData;

        respawnPoint.position = gameObject.transform.position;

        flipLayer = LayerMask.NameToLayer(FLIP_LAYER);
        deathLayer = LayerMask.NameToLayer(DEATH_LAYER);
        checkpointLayer = LayerMask.NameToLayer(CHECKPOINT);

        Debug.Log(checkpointLayer);

        playerController = GetComponent<PlayerControllerS>();
        spriteRend = GetComponent<SpriteRenderer>();

    }



    /*
     *  IF YOU WANT THE PLAYER TURN INTO THE SKELETON, USE THE FLIP LAYER
     *  
     *  TO DO THE OPPOSITE, USE TILES IN THE HAZARD LAYER
     * 
     * 
     * 
     */

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bloodEffect.SetActive(false);

        if (collision.gameObject.layer == flipLayer || collision.gameObject.layer == deathLayer)
        {

            FlipGravity(-1);

            bloodEffect.transform.SetParent(gameObject.transform, false);

        }

        if ((isSkeleton && collision.gameObject.layer == flipLayer) || (!isSkeleton && collision.gameObject.layer == deathLayer))
        {
            if (!isSkeleton)
            {
                audioClip.Play("BoneBreak");
                audioClip.Play("Flesh");
                // bloodEffect.transform.position = gameObject.transform.position;
                bloodEffect.transform.SetParent(gameObject.transform, false);

                bloodEffect.transform.parent = null;
                bloodEffect.SetActive(true);

                boneEffect.transform.parent = null;
                boneEffect.SetActive(true);

                gameObject.SetActive(false);


            }
            else
            {
                audioClip.Play("BoneBreak");

                boneEffect.transform.parent = null;
                boneEffect.SetActive(true);

                gameObject.SetActive(false);
            }

            gm.goGone = true;
        }

        ///When colliding with an object with the checkpoint layer,
        ///record the new respawn position by storing the vectors of the checkpoint.
    }

    private void OnTriggerExit2D(Collider2D collision)
    {


        if (!isSkeleton)
        {
            if (collision.gameObject.layer == flipLayer)
            {

                SpriteChange(true);
                isSkeleton = true;
            }
        }

        else if (isSkeleton)
        {

            if (collision.gameObject.layer == deathLayer)
            {
                SpriteChange(false);
                isSkeleton = false;

            }
        }
    }

    public void FlipGravity(float flipFactor)
    {
        playerController.flippedGravity = !playerController.flippedGravity;
        playerController.rb.gravityScale = playerController.rb.gravityScale * flipFactor;
        playerController.FlipVertical(flipAngle); //flips the whole transform
        playerController.jumpHeight *= flipFactor;
        //StartCoroutine(Invincibility());
        //playerController.flipDirection *= flipFactor;
    }


    public void SpriteChange(bool isSkeleton)
    {
        //Object Pool some of this stuff later.
        Debug.Log("Changing Sprite...");

        Debug.Log("is Skeleton? " + isSkeleton);

        if (isSkeleton)
        {
            audioClip.Play("Flesh");
            spriteRend.material.SetFloat("_Alpha", 0f);
            bloodEffect.transform.parent = null;
            bloodEffect.SetActive(true);
            //isSkeleton = true;
        }

        else if (!isSkeleton)
        {

            spriteRend.material.SetFloat("_Alpha", 1f);

            //isSkeleton = false;
        }


    }

}
