using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEngine.Rendering.ReloadAttribute;

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

    public string layerNameToFind3 = "P1ItemContainer";
    public string layerNameToFind4 = "P2ItemContainer";

    [SerializeField]
    bool findPlayer1;
    [SerializeField]
    bool findPlayer2;
    [SerializeField]
    bool findP1Container;
    [SerializeField]
    bool findP2Container;
    [SerializeField]
    public GameObject p1ItemC;
    [SerializeField]
    public GameObject p2ItemC;
    [SerializeField]
    public bool P1TakePackage;
    [SerializeField]
    public bool P2TakePackage;
    [SerializeField]
    public GameObject checkPoint;
    [SerializeField]
    Vector3 respawnPoint;
    [SerializeField]
    public GameObject p1Package;
    [SerializeField]
    public GameObject p2Package;



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

    private void Start()
    {
        respawnPoint = this.transform.position;
    }

    private void Update()
    {
        FindGameObject();
        FindItemContainer();
        Move();

    }

    private void FixedUpdate()
    {
        AddScore();
        

    }

    private void Move()
    {
        if (objectGrabpo != null)
        {
            float lerpspeed = 30;
            //smooth moving
            Vector3 newPosition = Vector3.Lerp(transform.position, objectGrabpo.position, Time.deltaTime * lerpspeed); ;
            rb.MovePosition(newPosition);

            if (objectGrabpo.position == p1ItemC.transform.position)
            {
                P1TakePackage = true;
                P2TakePackage = false;
            }
            else if (objectGrabpo.position == p2ItemC.transform.position)
            {
                P1TakePackage = false;
                P2TakePackage = true;
            }
        }
    }

    void AddScore()
    {
        if (P1TakePackage)
        {
            ScoreCount.instance.AddPointToP1Package(1);
        } else if(P2TakePackage)
        {
            ScoreCount.instance.AddPointToP2Package(1);
        }



    }



    /*
    player2 = GameObject.FindGameObjectWithTag("Player2");
    player2Dir = GameObject.FindGameObjectWithTag("Player2Dir").transform;
    */




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

        P1TakePackage = false;
        P2TakePackage = false;

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


        P1TakePackage = false;
        P2TakePackage = false;

        InventoryManager.Instance.Remove(item);
    }

    void FindGameObject()
    {
        int layerToFind1 = LayerMask.NameToLayer(layerNameToFind1);
        int layerToFind2 = LayerMask.NameToLayer(layerNameToFind2);
        GameObject[] objectsInLayer = GameObject.FindObjectsOfType<GameObject>();
        foreach (GameObject obj in objectsInLayer)
        {
            if (obj.layer == layerToFind1 && !findPlayer1)
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
                        findPlayer1 = true;
                    }
                }

                rb.velocity = player.GetComponent<Rigidbody>().velocity;
                rb.AddForce(playerDir.forward * dropForce, ForceMode.Impulse);
                rb.AddForce(playerDir.up * dropUpForce, ForceMode.Impulse);

            }

            if (obj.layer == layerToFind2 && !findPlayer2)
            {
                player2 = obj;
                Debug.Log("Found GameObject on layer: " + obj.name);

                Transform parentTransform = player2.transform;

                foreach (Transform child in parentTransform)
                {
                    if (child.CompareTag(tagToFind))
                    {
                        player2Dir = child;
                        Debug.Log("Found GameObject on Tag: " + child.gameObject.name);
                        findPlayer2 = true;
                    }

                }
                rb.velocity = player2.GetComponent<Rigidbody>().velocity;
                rb.AddForce(player2Dir.forward * dropForce, ForceMode.Impulse);
                rb.AddForce(player2Dir.up * dropUpForce, ForceMode.Impulse);

            }
        }

    }


    void FindItemContainer()
    {
        int layerToFind1 = LayerMask.NameToLayer(layerNameToFind3);
        int layerToFind2 = LayerMask.NameToLayer(layerNameToFind4);
        GameObject[] objectsInLayer = GameObject.FindObjectsOfType<GameObject>();
        foreach (GameObject obj in objectsInLayer)
        {
            if (obj.layer == layerToFind1 && !findP1Container)
            {
                p1ItemC = obj;
                findP1Container = true;
                Debug.Log("Found GameObject on layer: " + obj.name);
            }

            if (obj.layer == layerToFind2 && !findP2Container)
            {
                p2ItemC = obj;
                findP2Container = true;
                Debug.Log("Found GameObject on layer: " + obj.name);
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hazard"))
        {
            if (!P1TakePackage && !P1TakePackage)
            {
                this.transform.position = respawnPoint;

            }
        }
        else if (other.tag == "CheckPoint")
        {
            respawnPoint = other.transform.position;
        }
    }
}
