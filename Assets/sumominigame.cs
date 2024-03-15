using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SumoMinigame : MonoBehaviour
{

    string sceneString;
    //public RespawnControl rs;
    public boxingMinigame bm;

    private void Start()
    {
        Scene scene = SceneManager.GetActiveScene();


        sceneString = scene.name;
    }

    private void Update()
    {
        
        if (sceneString == "MVPLevel")
        {
            // Find all game objects with the tag "Findscript"
            GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("FindScript");

            // Loop through each object found
            foreach (GameObject obj in objectsWithTag)
            {
                // Check if the object has a component of type RespawnControl
                RespawnControl respawnControl = obj.GetComponent<RespawnControl>();

                // If the RespawnControl component is found, do something with it
                if (respawnControl != null)
                {
                    // You can access methods and properties of the RespawnControl script here
                    // For example:
                    if (respawnControl.Player1Die || respawnControl.Player2Die)
                    {

                        bm.endMinigame();

                    }
                }
            }

        }



    }






}




