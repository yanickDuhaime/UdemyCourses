using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceReceiver : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private float  drag;
    
    
    private float verticalVelocity;

    private Vector3 impact;
    private Vector3 dampingVelocity;
    public Vector3 Movement => impact + Vector3.up * verticalVelocity;
    
    private void Update()
    {
        if (verticalVelocity < 0f && controller.isGrounded)
        {
            verticalVelocity = Physics.gravity.y * Time.deltaTime;
        }
        else
        {
            //Makes it accelerate downwards
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }

        impact = Vector3.SmoothDamp(impact, Vector3.zero, ref dampingVelocity,drag);
    }

    public void AddForce(Vector3 force)
    {
        impact += force;
    }
}
