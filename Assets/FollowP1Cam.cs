using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowP1Cam : MonoBehaviour
{
    [SerializeField]
    Camera p1Cam;
    [SerializeField]
    private Camera mainCam;

    private void Start()
    {

    }
    void Update()
    {
        if(GameManager.instance.curSceneName == "Level1" || GameManager.instance.curSceneName == "MVPLevel")
        {
            if (!boxingMinigame.instance.isboxing)
            {
                if (p1Cam != null)
                {
                    // Get the camera's rotation
                    Quaternion cameraRotation = p1Cam.transform.rotation;

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
                    p1Cam = GameManager.instance.cam1.GetComponent<Camera>();
                }

            }
            else
            {

                if(Level1CamControl.instance.miniCam != null)
                {
                    Quaternion cameraRotation = Level1CamControl.instance.miniCam.transform.rotation;
                    Vector3 eulerCameraRotation = cameraRotation.eulerAngles;
                    transform.rotation = Quaternion.Euler(0f, eulerCameraRotation.y, 0f);
                }
                else
                {
                    Level1CamControl.instance.minigameCam();
                }
            }
        }
        else
        {
              
            if(mainCam != null)
            {
                Quaternion cameraRotation = mainCam.transform.rotation;
                Vector3 eulerCameraRotation = cameraRotation.eulerAngles;
                transform.rotation = Quaternion.Euler(0f, this.transform.position.y, 0f);
            }
            else
            {
                mainCam = Camera.main;
            }
        }



    }
}
