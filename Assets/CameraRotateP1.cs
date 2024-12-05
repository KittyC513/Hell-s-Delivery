using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotateP1 : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float rotationSpeed = 100;
    [SerializeField]
    private float zoomSpeed = 10f;
    [SerializeField]
    private float minZoom = 2f;
    [SerializeField]
    private float maxZoom = 80f;
    [SerializeField]
    private float currentZoomDistance;
    [SerializeField]
    private Vector3 offset;
    [SerializeField]
    private float smoothSpeed = 0.125f;
    [SerializeField]
    Vector3 desiredPosition;
    [SerializeField]
    Vector3 smoothedPosition;
    [SerializeField]
    private float yaw = 0.0f;



    // Start is called before the first frame update
    void Start()
    {
        yaw = -90;
        currentZoomDistance = 10f;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(GameManager.instance.curSceneName == "Level1" || GameManager.instance.curSceneName == "Tutorial" || GameManager.instance.curSceneName == "MVPLevel")
        {
            RotateCam();
        }

    }

    void RotateCam()
    {
        if (Input.GetKey(KeyCode.H))
        {
            GameManager.instance.p1.isFreeze = true;
            if (Input.GetKey(KeyCode.A))  // Rotate left
            {
                yaw -= rotationSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.D))  // Rotate right
            {
                yaw += rotationSpeed * Time.deltaTime;
            }

            //if (Input.GetKey(KeyCode.W))  // Zoom in
            //{
            //    currentZoom -= zoomSpeed * Time.deltaTime;
            //}
            //if (Input.GetKey(KeyCode.S))  // Zoom out
            //{
            //    currentZoom += zoomSpeed * Time.deltaTime;
            //}
            //currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
            //GameManager.instance.p1.isFreeze = true;
            //float inputH = -Input.GetAxis("Horizontal");
            //if (inputH != 0)
            //{
            //    // Rotate around the Y-axis (up) of the target
            //    transform.RotateAround(target.position, Vector3.up, inputH * rotationSpeed * Time.deltaTime);
            //}

            float inputV = Input.GetAxis("Vertical");
            if (inputV != 0)
            {
                // Adjust the zoom distance based on the input
                currentZoomDistance -= inputV * zoomSpeed * Time.deltaTime;
                currentZoomDistance = Mathf.Clamp(currentZoomDistance, minZoom, maxZoom);

                // Update the camera position to reflect the new zoom distance
                Vector3 direction = (transform.position - target.position).normalized;
                transform.position = target.position + direction * currentZoomDistance;
            }


        }
        else
        {
            GameManager.instance.p1.isFreeze = false;
        }

        Quaternion rotation = Quaternion.Euler(30, yaw, 0);
        offset = new Vector3(0, 0, -currentZoomDistance);
        desiredPosition = target.position + rotation * offset;
        smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
        transform.LookAt(target);

    }



}
