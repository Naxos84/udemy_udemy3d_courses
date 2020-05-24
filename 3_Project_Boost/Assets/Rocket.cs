using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    State state = State.Alive;

    Rigidbody rigidBody;
    AudioSource audioSource;
    [SerializeField] float rotationSpeed = 1.0f;
    [SerializeField] float mainThrust = 1.0f;
    [SerializeField] float levelTransitionWaitTime = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        this.rigidBody = GetComponent<Rigidbody>();
        this.audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        //TODO stop sound on death
        if (state == State.Alive) 
        {
            ProcessInput();
        }
    }

    private void ProcessInput()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive)
        {
            return;
        }
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                print("OK");
                break;
            case "Finish":
                print("Finished");
                this.state = State.Transcending;
                Invoke("LoadNextLevel", levelTransitionWaitTime);
                break;
            default:
                print("Dead");
                this.state = State.Dying;
                this.rigidBody.constraints = RigidbodyConstraints.None;
                Invoke("ResetLevel", levelTransitionWaitTime);
                break;
        }
    }

    private void LoadNextLevel()
    {
        // TODO allow for more levels
        SceneManager.LoadScene(1);
    }

    private void ResetLevel()
    {
        SceneManager.LoadScene(0);
    }

    private void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            this.rigidBody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }

    private void ProcessRotation()
    {

        rigidBody.freezeRotation = true; // take manual control of rotation
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * this.rotationSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.back * this.rotationSpeed * Time.deltaTime);
        }
        rigidBody.freezeRotation = false; // resume physics control of rotation
    }

}

enum State { Alive, Dying, Transcending }
