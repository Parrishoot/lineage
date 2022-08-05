using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager: Singleton<TransitionManager>
{
    public Animator animator;

    public float transitionTime;
    
    private enum TRANSITION_STATES
    {
        WAITING,
        FADE_OUT,
        FADE_IN
    }

    private string sceneToLoad = null;

    public void BeginTransition(string sceneName)
    {
        StartCoroutine(LoadNextLevel(sceneName));
    }

    IEnumerator LoadNextLevel(string sceneName)
    {
        animator.SetInteger("transitionState", (int) TRANSITION_STATES.FADE_OUT);

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(sceneName);

        animator.SetInteger("transitionState", (int)TRANSITION_STATES.FADE_IN);

        yield return new WaitForSeconds(transitionTime);

        animator.SetInteger("transitionState", (int)TRANSITION_STATES.WAITING);
    }
}
