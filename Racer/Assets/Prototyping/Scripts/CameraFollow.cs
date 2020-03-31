using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public Transform camRig;

    public float followDistance;

    public float cameraAngle;
    public float cameraHeight;

    Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = new Vector3(transform.position.x, cameraHeight, followDistance) - target.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.rotation = target.rotation;
        transform.position = target.position;
    }

    [ContextMenu("Set Camera")]
    public void SetCamera() {
        transform.rotation = target.rotation;
        transform.position = target.position;
        camRig.localPosition = new Vector3(0f, cameraHeight, -followDistance);
        camRig.localRotation = Quaternion.Euler(cameraAngle, 0f, 0f);
    }
}
