using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DualButton: MonoBehaviour
{
    [SerializeField]
    private TestCube[] players;

    private TestCube activePlayer;

    [SerializeField]
    private float radius = 1f;
    [SerializeField]
    public Transform b1;
    public Transform b2;
    [SerializeField]
    private LayerMask playerMask;

    [SerializeField]
    private UnityEvent onSummon;
    [SerializeField]
    private UnityEvent onExit;
    [SerializeField]
    private bool hasPlayer;
    [SerializeField]
    private bool summoningActive = false;
    [SerializeField]
    private Collider[] playerCollider1;
    private Collider[] playerCollider2;
    [SerializeField]
    private int numOfPlayer;
    public Material BonPush;
    public Material BDefault;
    public Material OonPush;
    public Material ODefault;
    Renderer matChange1;
    Renderer matChange2;
    public bool debug;
    [SerializeField]
    private int num;
    //public Animator Square;
    public bool blueActive;
    public bool orangeActive;
    public GameObject b1Cyl;
    public GameObject b2Cyl;
    public Animator b1Anim;
    public Animator b2Anim;


    private void Start()
    {

        matChange1 = b1Cyl.GetComponent<Renderer>();
        matChange2 = b2Cyl.GetComponent<Renderer>();



        players = new TestCube[2];
        //matChange.material = Default;
        onExit.Invoke();
        num = 0;
    }

    private void Update()
    {

        DetectPlayer();


        if (summoningActive)
        {
            onSummon.Invoke();

        } else
        {
            onExit.Invoke();

        }

    }

    private void FixedUpdate()
    {

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(b1.position, radius);
        Gizmos.DrawWireSphere(b2.position, radius);
    }

    private void DetectPlayer()
    {
        playerCollider1 = Physics.OverlapSphere(b1.position, radius, playerMask);
        playerCollider2 = Physics.OverlapSphere(b2.position, radius, playerMask);

        //numOfPlayer = num;


        if (playerCollider1.Length > 0 && playerCollider2.Length > 0)
        {

                summoningActive = true;


        } else
        {
            summoningActive = false;

        }


        if (playerCollider1.Length > 0)
        {

            matChange1.material = BonPush;
            blueActive = true;
            b1Anim.SetBool("Active", true);

        } else
        {
            matChange1.material = BDefault;
            b1Anim.SetBool("Active", false);
        }

        if (playerCollider2.Length > 0)
        {

            matChange2.material = OonPush;
            blueActive = true;
            b2Anim.SetBool("Active", true);

        }
        else
        {
            matChange2.material = ODefault;
            b2Anim.SetBool("Active", false);
        }

    }
}




