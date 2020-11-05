using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{

    /*
 * PROCESS:
 * 
 * PLAYER FINDS CHECKPOINT > CHECKPOINT FEEDS DATA OF HIT CHECKPOINT HERE
 * 
 * GAME MANAGER STORES DATA UNTIL OVERWRITTEN BY NEXT CHECKPOINT >
 * 
 * WHEN PLAYER DIES, GM DESTROYS PLAYER > GM CAUSES THE SCREEN TO FADE TO BLACK USING CANVAS
 * 
 * > WHILE SCREEN IS BLACK, RE-INSTANTIATE PLAYER, CAMERA FINDS PLAYER
 * 
 * 
 * 
 */


    [SerializeField]
    private float respawnSeconds;

    public static GameManager instance;
    public Vector2 checkpointData;

    public bool goGone = false;


    // Start is called before the first frame update
    void Awake()
    {
        
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);

        }
        else
        {
            Destroy(gameObject);
        }
    }

    //THIS IS REALLY, REALLY BAD. I have to find some sort of other collision or something.
    private void Update()
    {
        if (goGone)
        {
            StartCoroutine(RespawnTimer(respawnSeconds));
        }
    }
    public IEnumerator RespawnTimer(float seconds)
    {
        goGone = false;

        yield return new WaitForSeconds(respawnSeconds);
       // Debug.Log();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

}
