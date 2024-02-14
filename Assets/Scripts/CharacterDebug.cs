using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDebug : MonoBehaviour
{
    [SerializeField] private List<Players> players;
    [SerializeField] private bool hitBoxesActive = false;
    [SerializeField] private bool slowMotion = false;
    [SerializeField] private bool playerTrail = false;
    [SerializeField] private bool showInputSpheres = false;
    [SerializeField] private int playerTrailLength = 8;
    [SerializeField] private float playerTrailSampling = 0.2f;
    [SerializeField] private GameObject trailObject;
    [SerializeField] private GameObject altTrailObject;
    private float lastSample = 0;
    private int currentSample = 0;
   
    private bool gameIsRunning = false;

    [SerializeField] private GameObject p1TrailParent;
    [SerializeField] private GameObject p2TrailParent;

    [SerializeField] private GameObject onSphereObj;
    [SerializeField] private GameObject offSphereObj;



    private bool hasStarted = false;

    private bool hasStopped = false;

    private void Start()
    {
        
        
        gameIsRunning = true;
        foreach (var p in players)
        {
            p.collider = p.obj.GetComponent<CapsuleCollider>();
            p.trailObjs = new GameObject[playerTrailLength];
            p.cc = p.obj.GetComponent<CharacterControl>();

            for (int i = 0; i < playerTrailLength; i++)
            {
               
                if (p == players[0])
                {
                    p.trailObjs[i] = Instantiate(trailObject, p.obj.transform.position, Quaternion.identity, p1TrailParent.transform);
                }
                else
                {
                    p.trailObjs[i] = Instantiate(altTrailObject, p.obj.transform.position, Quaternion.identity, p2TrailParent.transform);
                }
            }

        }
    }

    private void Update()
    {
        if (gameIsRunning)
        {
            if (playerTrail)
            {
                if (Time.time - lastSample >= playerTrailSampling)
                {
                    lastSample = Time.time;
                    //draw a new gizmo
                    foreach (var p in players)
                    {
                        if (p.obj.GetComponent<Rigidbody>().velocity.magnitude > 0)
                        {
                            p.trailObjs[p.trailNum].transform.position = p.obj.transform.position;
                            p.trailObjs[p.trailNum].transform.rotation = p.obj.transform.rotation;
                        }

                        if (p.trailNum < playerTrailLength - 1)
                        {
                            p.trailNum += 1;
                        }
                        else
                        {
                            p.trailNum = 0;
                        }

                    }

                }

                if (showInputSpheres)
                {
                    foreach (var p in players)
                    {
                        if (p.cc.stickValue.magnitude > 0)
                        {
                            //we inputted
                            if (!hasStarted)
                            {
                                hasStopped = false;
                                hasStarted = true;
                                Vector3 pos = new Vector3(p.obj.transform.position.x, p.obj.transform.position.y + 4, p.obj.transform.position.z);
                                Instantiate(onSphereObj, pos, Quaternion.identity);
                            }
                        }
                        else
                        {
                            if (!hasStopped)
                            {
                                hasStopped = true;
                                hasStarted = false;
                                Vector3 pos = new Vector3(p.obj.transform.position.x, p.obj.transform.position.y + 4, p.obj.transform.position.z);
                                Instantiate(offSphereObj, pos, Quaternion.identity);
                            }
                            //we let go
                        }
                    }
                }
            }
        }

    }

    private void OnDrawGizmos()
    {
        if (hitBoxesActive && gameIsRunning)
        {
            foreach (var p in players)
            {
                Gizmos.DrawWireCube(p.collider.gameObject.transform.position, new Vector3(p.collider.radius, p.collider.height, p.collider.radius));
            }
        }

    
    }

  
}

[System.Serializable]
class Players
{
    [HideInInspector] public CapsuleCollider collider;
    [SerializeField] public GameObject obj;
    [HideInInspector] public GameObject[] trailObjs;
    [HideInInspector] public CharacterControl cc;
    [HideInInspector] public int trailNum;
}
