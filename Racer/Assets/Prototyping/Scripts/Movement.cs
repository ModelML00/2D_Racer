using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float topSpeed;
    public float reverseSpeed;
    public float acceleration;
    public float decceleration;

    public float wheelSpeed;
    public float handling;

    public Rigidbody rb;

    float currentSpeed;
    float currrentDirection;

    float accelVel;
    float decelVel;

    float rotationVel;



    void Update()
    {
        if(Input.GetKey(KeyCode.Space) && !Input.GetKey(KeyCode.LeftControl)) {
            currentSpeed = Mathf.SmoothDamp(currentSpeed, topSpeed, ref accelVel, acceleration * Time.deltaTime);
        }
        if(!Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.LeftControl)) {
            currentSpeed = Mathf.SmoothDamp(currentSpeed, -reverseSpeed, ref decelVel, decceleration * Time.deltaTime);
        }
        if(!Input.GetKey(KeyCode.Space) && !Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.LeftControl)){
            currentSpeed = Mathf.SmoothDamp(currentSpeed, 0f, ref decelVel, decceleration * Time.deltaTime);
        }


        if(Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)) {
            currrentDirection = Mathf.SmoothDamp(currrentDirection, -1f, ref rotationVel, wheelSpeed * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A)) {
            currrentDirection = Mathf.SmoothDamp(currrentDirection, 1f, ref rotationVel, wheelSpeed * Time.deltaTime);
        }
        if(!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)) {
            currrentDirection = Mathf.SmoothDamp(currrentDirection, 0f, ref rotationVel, wheelSpeed * Time.deltaTime);
        }
    }


    void FixedUpdate()
    {


        
        Vector3 force = transform.forward * currentSpeed;

        rb.AddForce(force);



        float turn = handling * currrentDirection;

        turn = turn * (currentSpeed / topSpeed);

        Quaternion rotation = transform.rotation * Quaternion.Euler(0f, turn, 0f);

        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 10f);

    }


}
