using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Trigger : MonoBehaviour
{
    public DialogueRunner dR;
    [SerializeField]
    private LayerMask layer1;
    [SerializeField]
    private LayerMask layer2;

    public bool isTriggered, canDestroy;

    // Start is called before the first frame update
    void Start()
    {
        layer1 = LayerMask.NameToLayer("P1Collider");
        layer2 = LayerMask.NameToLayer("P2Collider");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if( other.gameObject.layer ==layer1)
        {
            Debug.Log("Other gameobject1 = " + other.gameObject);
            //dR.StartDialogue("HubStart");
            isTriggered = true;
        }

        if (other.gameObject.layer == layer2 && isTriggered)
        {
            Debug.Log("Other gameobject2 = " + other.gameObject);
            dR.StartDialogue("HubStart");
            canDestroy = true;

        }
    }

    public void DestroyThis()
    {
        if (canDestroy)
        {
            Destroy(this.gameObject);
        }
    }
}