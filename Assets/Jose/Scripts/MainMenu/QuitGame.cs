using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
   
    //From Jose: This is my function to quit the game, after user clicks quit game button, the game will close.
    public void Quitgame()
    {
        Debug.Log("the player has quit the application. closing application");
        Application.Quit();
    }

}
