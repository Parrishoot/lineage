using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager: Singleton<TransitionManager>
{
    public Animator animator;

    public float transitionTime;

    private int currentSpawnIndex = 0;
    
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

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Vector3 spawnPosition = SpawnPoints.GetInstance().spawnPoints[currentSpawnIndex].position;

        // Get the player and Spawn them at the right location
        GameObject.FindGameObjectWithTag("Player").transform.position = spawnPosition;
        CameraController.GetInstance().SetHardPosition(spawnPosition);

        // Set the NPC Controllers of the NPC's that exist in the scene
        NPCMasterManager.GetInstance().ResetControllers();

        // Set up the scene with the ongoing quests
        QuestLogManager.GetInstance().InitActiveQuestOnSceneTransition();
    }

    IEnumerator LoadNextLevel(string sceneName, int spawnIndex)
    {
        animator.SetInteger("transitionState", (int) TRANSITION_STATES.FADE_OUT);

        yield return new WaitForSeconds(transitionTime);

        currentSpawnIndex = spawnIndex;

        SceneManager.LoadScene(sceneName);

        animator.SetInteger("transitionState", (int)TRANSITION_STATES.FADE_IN);

        yield return new WaitForSeconds(transitionTime);

        animator.SetInteger("transitionState", (int)TRANSITION_STATES.WAITING);

        
    }
}
