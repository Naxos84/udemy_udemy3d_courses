﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("Player collision with " + other.gameObject.name);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player triggering with " + other.name);
    }
}
