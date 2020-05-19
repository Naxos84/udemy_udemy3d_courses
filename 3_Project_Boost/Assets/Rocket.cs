using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{

    Rigidbody rigidBody;
    [SerializeField] float rotationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        this.rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    private void ProcessInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            this.rigidBody.AddRelativeForce(Vector3.up);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * this.rotationSpeed * Time.deltaTime);
        } else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.back * this.rotationSpeed * Time.deltaTime);
        }
    }
}
