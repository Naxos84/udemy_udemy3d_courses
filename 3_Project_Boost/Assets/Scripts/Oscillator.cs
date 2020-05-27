using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent] // only one script of this type is allowed on a gameobject
public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 movementVector = new Vector3(0, 0, 10);
    [SerializeField] float period = 2f;

    float movementFactor = 0; // 0 for not moved, 1 for fully moved

    private Vector3 startingPosition;

    // Start is called before the first frame update
    void Start()
    {
        this.startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon)
        {
            return;
        }

        float cycles = Time.time / this.period;
        const float tau = Mathf.PI * 2;
        float rawSinWave = Mathf.Sin(tau * cycles);

        this.movementFactor = rawSinWave / 2f + .5f;
        Vector3 offset = this.movementVector * this.movementFactor;
        this.transform.position = this.startingPosition + offset;
    }
}
