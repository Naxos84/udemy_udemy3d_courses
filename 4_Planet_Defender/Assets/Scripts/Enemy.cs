using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] GameObject deathFX;
    [SerializeField] Transform parent;
    private bool isDying;
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
        if (this.isDying)
        {
            return;
        }
        this.isDying = true;
        Debug.Log(this.gameObject.name + " hit " + other.name);
        GameObject fx = Instantiate(this.deathFX, this.transform.position, Quaternion.identity);
        fx.transform.SetParent(parent);
        Destroy(this.gameObject);
        var scoreBoards = FindObjectsOfType<Scoreboard>();
        foreach (Scoreboard scoreBoard in scoreBoards)
        {
            scoreBoard.ScoreHit();
        }
    }
}
