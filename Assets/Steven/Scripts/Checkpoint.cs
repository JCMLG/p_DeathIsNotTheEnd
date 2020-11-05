using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private const string PLAYER_STR = "Player";
    private int playerLayer = 0;
    private GameManager gm;
    Transform storedPoint;
    //public int[] amtOfCheckpoints;
    [SerializeField]private Transform currentPoint;
    public Vector3 currentPointVect;

    public GameObject player;

    //private CollisionInteraction storedPoint;

    public bool activeCheckpoint;

    /// <summary>
    /// Player touches a checkpoint, which records their progress.
    /// If they die a second time, they reset at the last recorded point.
    /// </summary>
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        playerLayer = LayerMask.NameToLayer(PLAYER_STR);
        //currentPoint.position = gameObject.transform.position;

    }


     void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == playerLayer)
        {
            gm.checkpointData = transform.position;
        }
    }

}
