using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public Animator animator;

    private int levelToLoad;

    public void FadeToNextLevel()
    {
        FadeToLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }


    public void FadeToLevel (int levelIndex)
    {
        levelToLoad = levelIndex;
        Debug.Log("Fade Out");
        animator.SetTrigger("FadeOut");
    }


    public void OnFadeComplete ()
    {
        SceneManager.LoadScene(levelToLoad);
    }


}
