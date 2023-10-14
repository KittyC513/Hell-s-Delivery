using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public string layerNameToFind1 = "P1Collider";
    public string layerNameToFind2 = "P2Collider";
    public string tagToFindCam = "Camera";
    GameObject player;
    Transform cam;

    public bool cam1TurnOff, cam2TurnOff;


    void AccessCamera()
    {

        int layerToFind1 = LayerMask.NameToLayer(layerNameToFind1);
        int layerToFind2 = LayerMask.NameToLayer(layerNameToFind2);
        GameObject[] objectsInLayer = GameObject.FindObjectsOfType<GameObject>();
        foreach (GameObject obj in objectsInLayer)
        {
            if (obj.layer == layerToFind1 && obj == null)
            {
                player = obj;
                Debug.Log("Found GameObject on layer: " + obj.name);

                Transform parentTransform = player.transform;

                foreach (Transform child in parentTransform)
                {
                    if (child.CompareTag(tagToFindCam) && child == null)
                    {
                        cam = child;
                        Debug.Log("Found GameObject on Tag: " + child.gameObject.name);
                    }

                }
                Camera cameraComponent = cam.GetComponent<Camera>();

                if (cameraComponent != null)
                {
                    cameraComponent.enabled = false;
                    cam1TurnOff = true;
                }

            }
        }

        foreach (GameObject obj in objectsInLayer)
        {
            if (obj.layer == layerToFind2 && obj == null)
            {
                player = obj;
                Debug.Log("Found GameObject on layer: " + obj.name);

                Transform parentTransform = player.transform;

                foreach (Transform child in parentTransform)
                {
                    if (child.CompareTag(tagToFindCam) && child == null)
                    {
                        cam = child;
                        Debug.Log("Found GameObject on Tag: " + child.gameObject.name);
                    }

                }
                Camera cameraComponent = cam.GetComponent<Camera>();

                if (cameraComponent != null)
                {
                    cameraComponent.enabled = false;
                    cam2TurnOff = true;
                }

            }
        }
    }
}

  
