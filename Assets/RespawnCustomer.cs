using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnCustomer : MonoBehaviour
{
    public static RespawnCustomer instance;

    [SerializeField]
    private GameObject ghost;
    [SerializeField]
    public Transform[] respawnPoints;
    [SerializeField]
    private float minRespawnTime = 3f;
    [SerializeField]
    private float maxRespawnTime = 10f;
    [SerializeField]
    private GameObject spawnedObject;
    [SerializeField]
    private float nextRespawnTime;



    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        ScheduleRespawn();
    }

    void ScheduleRespawn()
    {
        // Generate a random respawn time within the specified range
        nextRespawnTime = Time.time + Random.Range(minRespawnTime, maxRespawnTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextRespawnTime)
        {
            // Respawn the object
            Respawn();

            // Schedule the next respawn
            ScheduleRespawn();
        }
    }

    public void Respawn()
    {
        // Destroy or deactivate the current object
        if (spawnedObject != null)
        {
            Destroy(spawnedObject);
        }

        // Choose a random respawn point
        Transform respawnPoint = respawnPoints[Random.Range(0, respawnPoints.Length)];

        // Respawn the object at the chosen respawn point
        spawnedObject = Instantiate(ghost, respawnPoint.position, respawnPoint.rotation);
    }
}
