using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cpmanager : MonoBehaviour
{

    [SerializeField]
    private List<GameObject> cps = new List<GameObject>();
    public GameObject cpParent;
    [SerializeField]
    CheckpointControl cpc;
    int childCount;
    bool[] Activatecheck;
    bool checker;
    int i = 0;
    int o = 0;
    Transform currentActive;

    // Start is called before the first frame update
    void Start()
    {


        Transform parentTransform = cpParent.transform;
        childCount = parentTransform.childCount;


        foreach (Transform child in cpParent.transform)
        {
            cps.Add(child.gameObject);
            //cpc[i] = child.GetComponent<CheckpointControl>();

            //i++;
        }

        // You can iterate through the array or perform any other operations as needed.

    }

    // Update is called once per frame
    void Update()
    {
        foreach (Transform child in cpParent.transform)
        {

            cpc = child.GetComponent<CheckpointControl>();

            if (cpc != null && cpc.activate)
            {

                currentActive = child;

                foreach (Transform other in cpParent.transform)
                {
                    if (other != currentActive)
                    {
                        cpc.deActivate = true;
                    }
                }

                cpc.activate = false;
                break;

            } 
            
        }

        


    }
}
