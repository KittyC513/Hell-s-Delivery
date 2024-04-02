using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowP2Cam : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    Camera p2Cam;
    // Start is called before the first frame update
    void Start()
    {
        p2Cam = GameManager.instance.cam2.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the main camera exists
        if (p2Cam != null)
        {
            // Get the camera's rotation
            Quaternion cameraRotation = p2Cam.transform.rotation;

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
            p2Cam = GameManager.instance.cam2.GetComponent<Camera>();
            Debug.LogError("p2Cam not found!");
        }
    }
    
}

