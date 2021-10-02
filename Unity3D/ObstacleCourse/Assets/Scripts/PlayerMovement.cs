using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]private float playerSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float xValue = Input.GetAxis("Horizontal")* playerSpeed * Time.deltaTime;
        float zValue = Input.GetAxis("Vertical") * playerSpeed * Time.deltaTime;
        transform.Translate(xValue,0f,zValue);
    }
}
