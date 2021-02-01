using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [Tooltip("The amount of time (in seconds) before the next level is loaded.")] [SerializeField] float levelLoadDelay = 1f;
    [Tooltip("The object to activate on collision.")] [SerializeField] GameObject explosionFx = null;

    private PlayerController playerController;
    void Start()
    {
        this.playerController = GetComponent<PlayerController>();
    }
    void OnCollisionEnter(Collision other)
    {
        // Does not occur in this case.
        // Check https://docs.unity3d.com/Manual/CollidersOverview.html
        Debug.Log("Player collision with " + other.gameObject.name);
    }

    void OnTriggerEnter(Collider other)
    {
        StartDeathSequence();
    }

    private void StartDeathSequence()
    {
        Debug.Log("Player dying");
        this.playerController.DisableControls();
        explosionFx.SetActive(true);
        Invoke("NextLevel", this.levelLoadDelay);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
