using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager: Singleton<TransitionManager>
{
    public Animator animator;

    public float transitionTime;

    private int currentSpawnIndex = -1;
    
    private enum TRANSITION_STATES
    {
        WAITING,
        FADE_OUT,
        FADE_IN
    }

    public void BeginTransition(string sceneName, int spawnIndex)
    {
        StartCoroutine(LoadNextLevel(sceneName, spawnIndex));
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Vector3 spawnPosition = SpawnPoints.GetInstance().spawnPoints[currentSpawnIndex].position;

        GameObject.FindGameObjectWithTag("Player").transform.position = spawnPosition;
        CameraController.GetInstance().SetHardPosition(spawnPosition);
    }

    IEnumerator LoadNextLevel(string sceneName, int spawnIndex)
    {
        animator.SetInteger("transitionState", (int) TRANSITION_STATES.FADE_OUT);

        yield return new WaitForSeconds(transitionTime);

        currentSpawnIndex = spawnIndex;

        SceneManager.LoadScene(sceneName);
        SceneManager.sceneLoaded += OnSceneLoaded;

        animator.SetInteger("transitionState", (int)TRANSITION_STATES.FADE_IN);

        yield return new WaitForSeconds(transitionTime);

        animator.SetInteger("transitionState", (int)TRANSITION_STATES.WAITING);

        
    }
}
