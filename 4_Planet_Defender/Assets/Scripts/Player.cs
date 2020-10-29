using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [Tooltip("in m/s")] [SerializeField] float xSpeed = 10f;
    [Tooltip("in m/s")] [SerializeField] float ySpeed = 10f;
    [SerializeField] float xRange = 10f;
    [SerializeField] float yRangeMin = -6f;
    [SerializeField] float yRangeMax = 4f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float xThrow = Input.GetAxis("Horizontal");
        float xOffset = xThrow * this.xSpeed * Time.deltaTime;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        float yThrow = Input.GetAxis("Vertical");
        float yOffset = yThrow * this.ySpeed * Time.deltaTime;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, yRangeMin, yRangeMax);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }
}
