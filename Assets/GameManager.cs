using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public string layerNameToFind1 = "P1Collider";
    public string layerNameToFind2 = "P2Collider";
    GameObject player;
    [SerializeField]
    private TestCube p1;
    [SerializeField]
    private TestCube p2;

    private void Start()
    {


    }

    private void Update()
    {
        FindPlayer();
    }


    void FindPlayer()
    {
        int layerToFind1 = LayerMask.NameToLayer(layerNameToFind1);
        int layerToFind2 = LayerMask.NameToLayer(layerNameToFind2);
        GameObject[] objectsInScene = GameObject.FindObjectsOfType<GameObject>();

        foreach (GameObject obj in objectsInScene)
        {
            if (obj.layer == layerToFind1 && p1 == null)
            {
                p1 = obj.GetComponent<TestCube>();
            }

            if (obj.layer == layerToFind2 && p2 == null)
            {
                p2 = obj.GetComponent<TestCube>();
            }
        }
    }
    public void FreezePlayer()
    {
        if (p1 != null && p2 != null)
        {
            p1.isFreeze = true;
            p2.isFreeze = true;
        }

        if (p1 != null && p2 == null)
        {
            p1.isFreeze = true;
        }

        if (p1 == null && p2 != null)
        {
            p2.isFreeze = true;
        }


    }

    public void UnfreezePlayer()
    {
        if (p1 != null && p2 != null)
        {
            p1.isFreeze = false;
            p2.isFreeze = false;
        }

        if (p1 != null && p2 == null)
        {
            p1.isFreeze = false;
        }

        if (p1 == null && p2 != null)
        {
            p2.isFreeze = false;
        }
    }

}






