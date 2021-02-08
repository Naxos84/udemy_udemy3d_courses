using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestructor : MonoBehaviour
{
    [SerializeField] float timeTillDestruction = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Starting self destruction");
        Destroy(this.gameObject, timeTillDestruction);
    }
}
