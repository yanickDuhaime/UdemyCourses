using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] AudioClip mainEngine;
    [SerializeField] float mainThrustSpeed = 1000f;
    [SerializeField] float rotationThrustSpeed = 50f;
    
    [SerializeField] ParticleSystem mainThrusterParticles;
    [SerializeField] ParticleSystem leftThrusterParticles;
    [SerializeField] ParticleSystem rightThrusterParticles;
    
    Rigidbody myRigidbody;
    AudioSource myAudioSource;
 
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        myAudioSource = GetComponent<AudioSource>();
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
            StartThrusting();
        }

        else
        {
            StopThrusting();
        }
    }
    
    
    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotation();
        }
    }

    
    void StartThrusting()
    {
        if (!myAudioSource.isPlaying)
            myAudioSource.PlayOneShot(mainEngine);

        if (!mainThrusterParticles.isPlaying)
            mainThrusterParticles.Play();

        myRigidbody.AddRelativeForce(Vector3.up * mainThrustSpeed * Time.deltaTime);
    }
    
    
    void StopThrusting()
    {
        mainThrusterParticles.Stop();
        myAudioSource.Stop();
    }
    
    
    void RotateLeft()
    {
        ApplyRotation(Vector3.forward);
        if (!rightThrusterParticles.isPlaying)
            rightThrusterParticles.Play();
    }

    
    void RotateRight()
    {
        ApplyRotation(-Vector3.forward);
        if (!leftThrusterParticles.isPlaying)
            leftThrusterParticles.Play();
    }

    
    void StopRotation()
    {
        leftThrusterParticles.Stop();
        rightThrusterParticles.Stop();
    }

    
    void ApplyRotation(Vector3 speed)
    {
        myRigidbody.freezeRotation = true; //freezing rotation so we can manually rotate
        transform.Rotate(speed * rotationThrustSpeed * Time.deltaTime);
        myRigidbody.freezeRotation = false; 

    }
}
