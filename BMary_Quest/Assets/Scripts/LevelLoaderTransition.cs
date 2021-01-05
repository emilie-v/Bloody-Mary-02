using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoaderTransition : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1.0f;

    public void Start()
    {
        transition.SetBool("Close", true);
    }
    
    public void LoadGameScene()
    {
        StartCoroutine(TransitionAnimation());
    }

    private IEnumerator TransitionAnimation()
    {
        transition.SetBool("Close", false);
        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene("GameBoard");
    }
}
