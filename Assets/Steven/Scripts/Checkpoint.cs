using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour
{
    private const string PLAYER_STR = "Player";
    private int playerLayer = 0;
    private GameManager gm;

    private SceneHandler transition;

    public Vector3 currentPointVect;

    public GameObject player;

    public GameObject continueMenu;

    public bool isGoal = false;

    /// <summary>
    /// Player touches a checkpoint, which records their progress.
    /// If they die a second time, they reset at the last recorded point.
    /// </summary>
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("Respawn").GetComponent<GameManager>();
        transition = GameObject.FindGameObjectWithTag("GameController").GetComponent<SceneHandler>();
        playerLayer = LayerMask.NameToLayer(PLAYER_STR);
    }


     void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == playerLayer)
        {
            gm.checkpointData = transform.position;
        }

        if(other.gameObject.layer == playerLayer && isGoal == true)
        {
            other.attachedRigidbody.velocity = Vector2.zero;
            other.gameObject.GetComponent<PlayerControllerS>().enabled = false;


            continueMenu.SetActive(true);
         //   Debug.Log("Goal Reached");
           // transition.FadeToNextLevel();
        }

    }

}
