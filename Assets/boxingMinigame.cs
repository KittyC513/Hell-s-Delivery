using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxingMinigame : MonoBehaviour
{

    public GameObject waypointp1;
    public GameObject waypointp2;
    public Vector3 spawnpointp1;
    public Vector3 spawnpointp2;
    public GameObject[] players;
    public Level1CamControl cm;
    public bool isboxing = false;


    // Start is called before the first frame update
    void Start()
    {
        spawnpointp1 = waypointp1.transform.position;
        spawnpointp2 = waypointp2.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void StartMinigame()
    {
        isboxing = true;
        cm.minigameCam();
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
                respawnControl.startminigame();
            }
        }
    }
}
