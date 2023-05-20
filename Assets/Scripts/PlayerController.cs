using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 20;
    public float steerSpeed = 60;
    public float jumpSpeed = 100;
    public float shiftMultiplier = 2;

    public Rigidbody rb;
    public Vector3 jump;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hello World");
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0, 2, 0);
    }

    // Update is called once per frame
    void Update()
    {
        // Vector3.forward = 0 0 1
        // Vector3.right = 1 0 0
        // Vector3.up = 0 1 0

        // shift held down = speedboost
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            SpeedBoost();
        } else
        {
            NormalSpeed();
        }

        // jump        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(jump * jumpSpeed, ForceMode.Impulse);
        }       
        
    }

    public void NormalSpeed()
    {
        // forward and backward
        float gasValue = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        Vector3 positionChange = Vector3.forward * gasValue;
        transform.Translate(positionChange);

        // steering left and right
        float steerValue = Input.GetAxis("Horizontal") * steerSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up, steerValue);
    }

    public void SpeedBoost()
    {
        // forward and backward
        float gasValueBoosted = (Input.GetAxis("Vertical") * speed * Time.deltaTime) * shiftMultiplier;
        Vector3 positionChange = Vector3.forward * gasValueBoosted;
        transform.Translate(positionChange);

        // steering left and right
        float steerValue = (Input.GetAxis("Horizontal") * steerSpeed * Time.deltaTime) * shiftMultiplier;
        transform.Rotate(Vector3.up, steerValue);
    }


}
