using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{

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
        this.playerController.disableControls();
    }
}
