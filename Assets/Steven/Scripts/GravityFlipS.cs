using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityFlipS : MonoBehaviour
{
    private const string FLIP_LAYER = "Flip";
    private int flipLayer = 0;

    PlayerControllerS playerController;

    private SpriteRenderer spriteRend;
    private float flipAngle = 180;

    private bool isSkeleton = false;

    [SerializeField]
    private GameObject bloodEffect;

    [SerializeField]
    private GameObject boneEffect;
    //[SerializeField]
    //private float particleTime;


    private void Awake()
    {
        flipLayer = LayerMask.NameToLayer(FLIP_LAYER); //converts 0 to 11
        playerController = GetComponent<PlayerControllerS>();
        spriteRend = GetComponent<SpriteRenderer>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == flipLayer)
        {
            FlipGravity(-1);
            RipFlesh();
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
        //  GameObject bloodSplat = Instantiate(bloodEffect, gameObject.transform.position, Quaternion.identity);
        // Destroy(bloodSplat, particleTime);
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
