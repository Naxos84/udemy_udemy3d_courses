using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.AddNonTriggerBoxCollider();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void AddNonTriggerBoxCollider()
    {
        BoxCollider collider = GetComponent<BoxCollider>();
        if (collider == null)
        {
            collider = this.gameObject.AddComponent<BoxCollider>();
            collider.isTrigger = false;
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("Enemy hit " + other.name);
        Destroy(this.gameObject);
    }
}
