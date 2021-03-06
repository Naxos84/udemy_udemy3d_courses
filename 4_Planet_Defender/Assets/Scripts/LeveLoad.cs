﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeveLoad : MonoBehaviour
{
    [SerializeField] float levelTransitionWaitTime = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("LoadNextLevel", levelTransitionWaitTime);
        Debug.Log("Hello World");
    }

    void LoadNextLevel()
    {
        // TODO allow for more levels
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int sceneCount = SceneManager.sceneCountInBuildSettings;
        int nextSceneIndex = currentSceneIndex;
        if (currentSceneIndex >= sceneCount)
        {
            nextSceneIndex = 0;
        }
        else
        {
            nextSceneIndex = currentSceneIndex + 1;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
