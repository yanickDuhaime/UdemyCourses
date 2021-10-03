using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropper : MonoBehaviour
{
    [SerializeField] private float waitTime = 3;
    private Rigidbody myRigidbody;
    private MeshRenderer myRenderer;
    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        myRenderer = GetComponent<MeshRenderer>();
        myRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > waitTime)
        {
            myRigidbody.useGravity = true;
            myRenderer.enabled = true;
        }
    }
}
