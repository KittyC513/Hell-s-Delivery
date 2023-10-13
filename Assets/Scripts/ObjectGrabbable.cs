using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ObjectGrabbable : MonoBehaviour
{
    public Rigidbody rb;
    public BoxCollider bC;
    private Transform objectGrabpo;
    [SerializeField]
    private float dropForce, dropUpForce;
    public GameObject player;
    public Transform playerDir;
    public GameObject player2;
    public Transform player2Dir;

    public string layerNameToFind1 = "P1Collider";
    public string layerNameToFind2 = "P2Collider";
    public string tagToFind = "PlayerDir";
    public string tagToFindPlayer = "Player";
    public Item item;

    [SerializeField]
    bool findPlayer;




    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        bC = GetComponent<BoxCollider>();
    }
    // Start is called before the first frame update

    //when a pickable item is picked, it transform to player's head
    public void Grab(Transform objectGrabpo)
    {

        this.objectGrabpo = objectGrabpo;
        //set the gravity to zero when it's picked
        rb.useGravity = false;
        rb.isKinematic = true;
        bC.enabled = false;

        InventoryManager.Instance.Add(item);


    }

    private void Update()
    {
        FindGameObject();
    }

    private void FixedUpdate()
    {

        if (objectGrabpo != null)
        {
            float lerpspeed = 10;
            //smooth moving
            Vector3 newPosition = Vector3.Lerp(transform.position, objectGrabpo.position, Time.deltaTime * lerpspeed); ;
            rb.MovePosition(newPosition);
        }
        


        /*
        player2 = GameObject.FindGameObjectWithTag("Player2");
        player2Dir = GameObject.FindGameObjectWithTag("Player2Dir").transform;
        */
    }


    public void P1Drop()
    {

        this.objectGrabpo = null;
        rb.useGravity = true;
        rb.isKinematic = false;
        bC.enabled = true;

        rb.velocity = player.GetComponent<Rigidbody>().velocity;

        rb.AddForce(playerDir.forward * dropForce, ForceMode.Impulse);
        rb.AddForce(playerDir.up * dropUpForce, ForceMode.Impulse);

        float random = Random.Range(-1, 1);
        rb.AddTorque(new Vector3(random, random, random));

        InventoryManager.Instance.Remove(item);
    }

    public void P2Drop()
    {
        this.objectGrabpo = null;
        rb.useGravity = true;
        rb.isKinematic = false;
        bC.enabled = true;

        rb.velocity = player2.GetComponent<Rigidbody>().velocity;

        rb.AddForce(player2Dir.forward * dropForce, ForceMode.Impulse);
        rb.AddForce(player2Dir.up * dropUpForce, ForceMode.Impulse);

        float random = Random.Range(-1, 1);
        rb.AddTorque(new Vector3(random, random, random));

        InventoryManager.Instance.Remove(item);
    }

    void FindGameObject()
    {
        int layerToFind1 = LayerMask.NameToLayer(layerNameToFind1);
        int layerToFind2 = LayerMask.NameToLayer(layerNameToFind2);
        GameObject[] objectsInLayer = GameObject.FindObjectsOfType<GameObject>();
        foreach(GameObject obj in objectsInLayer)
        {
            if(obj.layer == layerToFind1)
            {
                player = obj;
                Debug.Log("Found GameObject on layer: " + obj.name);

                Transform parentTransform = player.transform;

                foreach (Transform child in parentTransform)
                {
                    if (child.CompareTag(tagToFind))
                    {
                        playerDir = child;
                        Debug.Log("Found GameObject on Tag: " + child.gameObject.name);
                    }

                }                            

                rb.velocity = player.GetComponent<Rigidbody>().velocity;
                rb.AddForce(playerDir.forward * dropForce, ForceMode.Impulse);
                rb.AddForce(playerDir.up * dropUpForce, ForceMode.Impulse);

            }

            if (obj.layer == layerToFind2)
            {
                player2 = obj;
                Debug.Log("Found GameObject on layer: " + obj.name);

                Transform parentTransform = player.transform;

                foreach (Transform child in parentTransform)
                {
                    if (child.CompareTag(tagToFind))
                    {
                        player2Dir = child;
                        Debug.Log("Found GameObject on Tag: " + child.gameObject.name);
                    }

                }

                rb.velocity = player2.GetComponent<Rigidbody>().velocity;
                rb.AddForce(player2Dir.forward * dropForce, ForceMode.Impulse);
                rb.AddForce(player2Dir.up * dropUpForce, ForceMode.Impulse);

            }
        }

    }
}
