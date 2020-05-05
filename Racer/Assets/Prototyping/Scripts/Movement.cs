using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    public float topSpeed;
    
    public float acceleration;
    public float decceleration;
    public float brakePower;

    public float wheelSpeed;
    public float handling;

    public float vehicleWeight;

    public GameObject brakeLights;
    public GameObject reverseLights;

    public Animator lTire;
    public Animator rTire;

    Rigidbody rb;
    Transform vehicleBody;
    LayerMask whatIsGround;

    float currentSpeed;
    float reverseSpeed;
    float currrentDirection;

    float accelVel;
    float brakeVel;
    float decelVel;

    float rotationVel;

    bool grounded;
    bool smoke;
    Vector3 normal;

    public static float speed;
    public static float speedometerSpeed;

    private void Start() {
        brakeLights.SetActive(false);
        reverseLights.SetActive(false);
        rb = GetComponent<Rigidbody>();
        vehicleBody = transform.GetChild(0).GetComponent<Transform>();
        whatIsGround = LayerMask.GetMask("Ground");
        reverseSpeed = topSpeed * 0.5f;
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.Space) && !Input.GetKey(KeyCode.LeftControl)) {
            currentSpeed = Mathf.SmoothDamp(currentSpeed, topSpeed, ref accelVel, (acceleration * 10f) * Time.deltaTime);
            if(brakeLights.activeInHierarchy) {
                brakeLights.SetActive(false);
            }
            if(reverseLights != null && reverseLights.activeInHierarchy) {
                reverseLights.SetActive(false);
            }
            if(!smoke) {
                lTire.gameObject.SetActive(true);
                rTire.gameObject.SetActive(true);
                lTire.SetTrigger("Smoke");
                rTire.SetTrigger("Smoke");
                smoke = true;
            }
        }
        if(!Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.LeftControl)) {
            currentSpeed = Mathf.SmoothDamp(currentSpeed, -reverseSpeed, ref brakeVel, (brakePower * 10f) * Time.deltaTime);
            if(!brakeLights.activeInHierarchy && currentSpeed >= 0f) {
                brakeLights.SetActive(true);
            } else if(currentSpeed < 0f) {
                brakeLights.SetActive(false);
            }
            smoke = false;
        }
        if(Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.LeftControl)){
            currentSpeed = Mathf.SmoothDamp(currentSpeed, 0f, ref brakeVel, (brakePower * 10f) * Time.deltaTime);
            if(!brakeLights.activeInHierarchy) {
                brakeLights.SetActive(true);
            }
            smoke = false;
        }
        if(!Input.GetKey(KeyCode.Space) && !Input.GetKey(KeyCode.LeftControl)) {
            currentSpeed = Mathf.SmoothDamp(currentSpeed, 0f, ref decelVel, (decceleration * 10f) * Time.deltaTime);
            if(brakeLights.activeInHierarchy) {
                brakeLights.SetActive(false);
            }
            if(reverseLights != null && reverseLights.activeInHierarchy) {
                reverseLights.SetActive(false);
            }
            smoke = false;
        }
        
        if(reverseLights != null && currentSpeed < 0f && !reverseLights.activeInHierarchy) {
            reverseLights.SetActive(true);
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
        speed = Vector3.Dot(rb.velocity, transform.forward);
        speed = speed >= 0f ? speed : speed * -1f;

        speedometerSpeed = speed / topSpeed;
        
        Vector3 force = transform.forward * currentSpeed;

        if(!grounded) {
            rb.AddForce(Vector3.up * -vehicleWeight, ForceMode.Acceleration);
        }
        
        rb.AddForce(force);

        float turnPercentage = 0f;
        float cSPos = currentSpeed >= 0f ? currentSpeed : currentSpeed * -1f;
        if((cSPos / topSpeed) > .05f && (cSPos / topSpeed) < .70f) {
            turnPercentage = cSPos / topSpeed;
        } else if((cSPos / topSpeed) > .70f) {
            turnPercentage = .50f;
        }

        float turn = (handling * currrentDirection) * turnPercentage;

        Quaternion rotation = transform.rotation * Quaternion.Euler(0f, turn, 0f);

        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 10f);

        

        Ray ray = new Ray(vehicleBody.transform.position, -vehicleBody.up);

        RaycastHit hit;

        grounded = Physics.Raycast(ray, out hit, 1f, whatIsGround);

        if(grounded) {
            normal = hit.normal.normalized;
        }

        Vector3 project = Vector3.ProjectOnPlane(vehicleBody.forward, normal);
        Quaternion lookRotation = Quaternion.LookRotation(project, normal);

        Quaternion slopeRotation = Quaternion.Euler(0f, 0f, lookRotation.eulerAngles.z);

        vehicleBody.localRotation = Quaternion.Slerp(vehicleBody.localRotation, slopeRotation, Time.deltaTime * 10f);

        //vehicleBody.rotation = Quaternion.Slerp(vehicleBody.rotation, lookRotation, Time.deltaTime * 10f);
        //Quaternion spriteRotation = Quaternion.Euler(vehicleSprite.localEulerAngles.x - vehicleBody.localEulerAngles.x, 0f, vehicleSprite.localEulerAngles.z);
        //vehicleSprite.localRotation = spriteRotation;
    }


}
