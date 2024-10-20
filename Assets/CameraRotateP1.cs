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

    // Start is called before the first frame update
    void Start()
    {
        currentZoomDistance = Vector3.Distance(transform.position, target.position);
    }

    // Update is called once per frame
    void Update()
    {
        RotateCam();
    }

    void RotateCam()
    {
        if (Input.GetKey(KeyCode.B))
        {
            GameManager.instance.p1.isFreeze = true;
            float inputH = -Input.GetAxis("Horizontal");
            if (inputH != 0)
            {
                // Rotate around the Y-axis (up) of the target
                transform.RotateAround(target.position, Vector3.up, inputH * rotationSpeed * Time.deltaTime);
            }

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

            transform.LookAt(target);

        }
        else
        {
            GameManager.instance.p1.isFreeze = false;
        }
    }


}
