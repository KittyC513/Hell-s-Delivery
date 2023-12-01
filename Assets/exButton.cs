using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class exButton : MonoBehaviour
{
    [SerializeField]
    //private TestCube[] players;

    private TestCube activePlayer;

    [SerializeField]
    private float radius = 1f;
    [SerializeField]
    private LayerMask playerMask;

    public LayerMask wrongPlayer;

    [SerializeField]
    private UnityEvent onSummon;
    [SerializeField]
    private UnityEvent onExit;
    [SerializeField]
    private bool hasPlayer;
    [SerializeField]
    private bool summoningActive = false;
    [SerializeField]
    private Collider[] playerCollider;
    [SerializeField]
    private Collider[] wrongCollider;
    [SerializeField]
    private int numOfPlayer;
    public Material OnPush;
    public Material Default;
    Renderer matChange1;
    public bool debug;
    [SerializeField]
    private int num;
    //public Animator Square;
    public bool blueActive;
    public bool orangeActive;
    public GameObject b1Cyl;
    public Animator Anim;
    public Animator x;
    bool switch1;


    private void Start()
    {

        matChange1 = b1Cyl.GetComponent<Renderer>();
        switch1 = true;


        //players = new TestCube[2];
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

        }
        else
        {
            onExit.Invoke();

        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    private void DetectPlayer()
    {
        playerCollider = Physics.OverlapSphere(transform.position, radius, playerMask);
        wrongCollider = Physics.OverlapSphere(transform.position, radius, wrongPlayer);

        //numOfPlayer = num;



        if (playerCollider.Length > 0)
        {
            if (switch1)
            {
                summoningActive = true;
                matChange1.material = OnPush;
                blueActive = true;
                Anim.SetBool("Activate", true);
                switch1 = false;
            }
            

        }
        else
        {
            if (!switch1)
            {
                summoningActive = false;
                matChange1.material = Default;
                Anim.SetBool("Activate", false);
                switch1 = true;
            }
            
        }

        if (wrongCollider.Length > 0 && playerCollider.Length <= 0)
        {
            x.SetBool("Activate", true);
        }
        else
        {
            x.SetBool("Activate", false);
        }



    }
}




