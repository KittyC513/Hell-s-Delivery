using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMainCam : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 5f;

    void Update()
    {
        // Check if the main camera exists
        if (Camera.main != null)
        {
            // Get the camera's rotation
            Quaternion cameraRotation = Camera.main.transform.rotation;

            // Convert the camera rotation to Euler angles
            Vector3 eulerCameraRotation = cameraRotation.eulerAngles;

            // Make the object's rotation match the camera's Y rotation
            // You can modify this line if you want rotation on different axes
            transform.rotation = Quaternion.Euler(0f, eulerCameraRotation.y, 0f);

            // Optionally, rotate the object continuously over time
            // Uncomment the line below if you want the object to rotate continuously
            // transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }
        else
        {
            Debug.LogError("Main camera not found!");
        }
    }
}
