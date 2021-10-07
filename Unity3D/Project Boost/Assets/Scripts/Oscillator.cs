using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    float movementFactor;
    [SerializeField] float period = 2f;
    
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(period <= Mathf.Epsilon) return;
        //Every period(seconds) cycles will go up
        float cycles = Time.time / period;
        const float tau = Mathf.PI * 2; // constant value of 6.283
        float rawSineWave = Mathf.Sin(cycles * tau);
        // Pour rendre le sineWave entre 0 et 1 plutôt que -1 et 1
        movementFactor = (rawSineWave + 1f) / 2f;
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
        
    }
}
