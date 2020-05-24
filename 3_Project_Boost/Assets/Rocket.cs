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
    [SerializeField] AudioClip mainEngine = null;
    [SerializeField] AudioClip death = null;
    [SerializeField] AudioClip success = null;

    [SerializeField] GameObject destroyedVersion = null;
    // Start is called before the first frame update
    void Start()
    {
        print("starting level");
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
        ProcessThrustInput();
        ProcessRotationInput();
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
                this.Transcend();
                break;
            default:
                this.KillPlayer();
                break;
        }
    }

    private void KillPlayer()
    {
        print("Dead");
        // this.state = State.Dying;
        // this.rigidBody.constraints = RigidbodyConstraints.None;
        // this.audioSource.Stop();
        // this.audioSource.PlayOneShot(this.death); //This is played by destroyed object.
        this.ExplodePlayer();
        Invoke("ResetLevel", levelTransitionWaitTime);
    }

    private void ExplodePlayer()
    {
        Instantiate(this.destroyedVersion, transform.position, transform.rotation);
        gameObject.SetActive(false);
    }

    private void Transcend()
    {
        print("Finished");
        this.state = State.Transcending;
        this.audioSource.Stop();
        this.audioSource.PlayOneShot(this.success);
        Invoke("LoadNextLevel", levelTransitionWaitTime);
    }

    private void LoadNextLevel()
    {
        // TODO allow for more levels
        SceneManager.LoadScene(1);
    }

    private void ResetLevel()
    {
        print("resetting level");
        SceneManager.LoadScene(0);
    }

    private void ProcessThrustInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            ApplyThrust();
        }
        else
        {
            audioSource.Stop();
        }
    }

    private void ApplyThrust()
    {
        this.rigidBody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!this.audioSource.isPlaying)
        {
            this.audioSource.PlayOneShot(this.mainEngine);
        }
    }

    private void ProcessRotationInput()
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
