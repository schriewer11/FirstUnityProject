using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10;
    public float steerSpeed = 30;
    public float jumpSpeed = 100;
    public float shiftMultiplier = 2;

    public Rigidbody rb;
    public Vector3 jump;
    private Vector3 startPosition;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hello World");
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0, 2, 0);
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Vector3.forward = 0 0 1
        // Vector3.right = 1 0 0
        // Vector3.up = 0 1 0

        HandleResetPosition();

        HandleMovement();
        
    }

    void HandleMovement()
    {
        float gasValue = (Input.GetAxis("Vertical") * speed * Time.deltaTime) * shiftMultiplier;
        float steerValue = (Input.GetAxis("Horizontal") * steerSpeed * Time.deltaTime) * shiftMultiplier;

        // if the GasValue is positive it'll get multiplied by 1
        // if negative it'll get multiplied by negative 1
        // this allows the car to reverse in the direction of the key that was pressed
        steerValue *= Mathf.Sign(gasValue);

        if (gasValue == 0)
        {
            steerValue = 0;
        }

        // shift held down = speedboost
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            steerValue *= shiftMultiplier;
            gasValue *= shiftMultiplier;
        }

        // forward and backward
        Vector3 positionChange = Vector3.forward * gasValue;
        transform.Translate(positionChange);

        // steering left and right        
        transform.Rotate(Vector3.up, steerValue);

        // jump        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(jump * jumpSpeed, ForceMode.Impulse);
        }
    }

    void HandleResetPosition()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                ResetPosition(startPosition);
            }
            else
            {
                ResetPosition();
            }
        }
    }

    void ResetPosition(Vector3 newPosition)
    {
        transform.transform.position = newPosition;
        transform.rotation = Quaternion.identity;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    void ResetPosition()
    {
        Vector3 newPosition = transform.position;
        newPosition.y = 5;
        ResetPosition(newPosition);
    }


}
