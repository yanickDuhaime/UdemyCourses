using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody myRigidbody;

    [SerializeField] private float mainThrustSpeed = 1000f;

    [SerializeField] private float rotationThrustSpeed = 50f;
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            myRigidbody.AddRelativeForce(Vector3.up * mainThrustSpeed * Time.deltaTime);
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(Vector3.forward);
        }
        else if (Input.GetKey(KeyCode.D)) 
        {
            transform.Rotate(-Vector3.forward);
        }
    }

    private void ApplyRotation(Vector3 speed)
    {
        myRigidbody.freezeRotation = true; //freezing rotation so we can manually rotate
        transform.Rotate(speed * rotationThrustSpeed * Time.deltaTime);
        myRigidbody.freezeRotation = false; 

    }
}
