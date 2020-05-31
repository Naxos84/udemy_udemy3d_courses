using System;
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
    [SerializeField] AudioClip death = null; // handled by Destroyable Player
    [SerializeField] AudioClip success = null;
    [SerializeField] ParticleSystem[] mainEngineParticles = null;
    [SerializeField] ParticleSystem deathParticles = null; // handled by Destroyable Player
    [SerializeField] ParticleSystem successParticles = null;

    // [SerializeField] GameObject destroyedVersion = null;
    bool collisionEnabled = true;
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
        if (Debug.isDebugBuild)
        {
            ProcessDebugInput();
        }
    }

    void ProcessDebugInput()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        } else if (Input.GetKeyDown(KeyCode.C))
        {
            this.ToggleCollision();
        }
    }

    void ToggleCollision()
    {
        this.collisionEnabled = !this.collisionEnabled;
    }

    private void ProcessInput()
    {
        ProcessThrustInput();
        ProcessRotationInput();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive || !collisionEnabled)
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
        this.state = State.Dying;
        this.rigidBody.constraints = RigidbodyConstraints.None;
        this.audioSource.Stop();
        
        this.ExplodePlayer();
        Invoke("ResetLevel", levelTransitionWaitTime);
    }

    private void ExplodePlayer()
    {
        this.audioSource.PlayOneShot(this.death);
        this.deathParticles.Play();
        // var gameObject = Instantiate(this.destroyedVersion, transform.position, transform.rotation);
        //var rocket = gameObject.GetComponent<Rocket>();
        //rocket.collisionEnabled = this.collisionEnabled;
        // this.gameObject.SetActive(false);
    }

    private void Transcend()
    {
        print("Finished");
        this.state = State.Transcending;
        this.audioSource.Stop();
        this.audioSource.PlayOneShot(this.success);
        this.successParticles.Play();
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
            foreach (ParticleSystem mainEngineParticle in this.mainEngineParticles)
            {
                if (mainEngineParticle.isPlaying)
                {
                    mainEngineParticle.Stop();
                }
            }
        }
    }

    private void ApplyThrust()
    {
        this.rigidBody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!this.audioSource.isPlaying)
        {
            this.audioSource.PlayOneShot(this.mainEngine);
        }
        foreach(ParticleSystem mainEngineParticle in this.mainEngineParticles)
        {
            if (mainEngineParticle.isStopped)
            {
                mainEngineParticle.Play();
            }
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
