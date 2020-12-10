using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoaderTransition : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1.0f;
    
    public void LoadGameScene()
    {
        StartCoroutine(TransitionAnimation());
    }

    IEnumerator TransitionAnimation()
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene("GameBoard");
    }
}
