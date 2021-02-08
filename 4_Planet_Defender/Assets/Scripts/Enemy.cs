using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] GameObject deathFX;
    [SerializeField] Transform parent;
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
        GameObject fx = Instantiate(this.deathFX, this.transform.position, Quaternion.identity);
        fx.transform.SetParent(parent);
        Destroy(this.gameObject);
    }
}
