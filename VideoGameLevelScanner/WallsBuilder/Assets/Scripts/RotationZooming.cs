using UnityEngine;
using System.Collections;

public class RotationZooming : MonoBehaviour
{

    public Transform target;
    public float distance = 10f;
    public float xSpeed = 250f;
    public float ySpeed = 120f;
    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;
    private float x = 0f;
    private float y = 0f;
    private bool rightDown = false;

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        // Make the rigid body not change rotation
        if (GetComponent<Rigidbody>())
            GetComponent<Rigidbody>().freezeRotation = true;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
            rightDown = true;
        else if (Input.GetMouseButtonUp(1))
            rightDown = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (target && rightDown)
        {
            x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

            y = ClampAngle(y, yMinLimit, yMaxLimit);

            Quaternion rotation = Quaternion.Euler(y, x, 0);
            Vector3 vectDistance = new Vector3(0.0f, 0.0f, -distance);
            Vector3 position = rotation * vectDistance + target.position;

            transform.rotation = rotation;
            transform.position = position;
        }
    }

    static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }
}

