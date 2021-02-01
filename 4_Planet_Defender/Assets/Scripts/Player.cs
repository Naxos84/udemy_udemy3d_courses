using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [Tooltip("in m/s")] [SerializeField] float xSpeed = 15f;
    [Tooltip("in m/s")] [SerializeField] float ySpeed = 15f;
    [SerializeField] float xRange = 10f;
    [SerializeField] float yRangeMin = -6f;
    [SerializeField] float yRangeMax = 4f;

    [SerializeField] float positionPitchFactor = -1.75f;
    [SerializeField] float controlPitchFactor = -15f;
    [SerializeField] float positionYawFactor = 2f;

    [SerializeField] float controlRollFactor = -30f;

    float xThrow = 0f;
    float yThrow = 0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
        ProcessTranslation();
        ProcessRotation();
    }

    private void ProcessInput()
    {
        this.xThrow = Input.GetAxis("Horizontal");
        this.yThrow = Input.GetAxis("Vertical");
    }

    private void ProcessTranslation()
    {
        float xOffset = this.xThrow * this.xSpeed * Time.deltaTime;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        float yOffset = this.yThrow * this.ySpeed * Time.deltaTime;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, yRangeMin, yRangeMax);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    private void ProcessRotation()
    {
        float pitchFromPosition = transform.localPosition.y * this.positionPitchFactor;
        float pitchFromControlThrow = this.yThrow * this.controlPitchFactor;
        float pitch = pitchFromPosition + pitchFromControlThrow;

        float yawFromPosition = transform.localPosition.x * this.positionYawFactor;
        float yaw = yawFromPosition;

        float roll = this.xThrow * this.controlRollFactor;
        this.transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
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
