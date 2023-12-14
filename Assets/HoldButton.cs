using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HoldButton : MonoBehaviour
{
    [SerializeField]
    private TestCube[] players;

    private TestCube activePlayer;

    [SerializeField]
    private float radius = 1f;
    [SerializeField]
    private Transform origin;
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
    private Collider[] playerCollider;
    [SerializeField]
    private int numOfPlayer;
    public Material onPush;
    public Material Default;
    Renderer matChange;
    public bool debug;
    [SerializeField]
    private int num;
    public Animator Square;

    private void Start()
    {

        matChange = GetComponent<Renderer>();

        players = new TestCube[2];
        matChange.material = Default;
        onExit.Invoke();
        num = 0;
    }

    private void Update()
    {



        DetectPlayer();
        //detect the player
        //if the player is detected read its run input, if the run input is active we want to set the player to a hold button state

        //if we detect the player in our circle
        for (int i = 0; i < players.Length; i++)
        {
            //and just a double check that we have a player script attached to our player
            if (players[i] != null)
            {
                //activate summoning
                summoningActive = true;
                activePlayer = players[i];
                matChange.material = onPush;
                Square.SetBool("Activate", true);

            }
            //else
            //{
            //    summoningActive = false;

            //    onExit.Invoke();
            //    activePlayer = null;
            //    matChange.material = Default;
            //}

        }



        //Debug.Log(summoningActive);

        //if summoning is active run functions
        if (summoningActive)
        {
            onSummon.Invoke();
        }
    }

    private void FixedUpdate()
    {

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(origin.position, radius);
    }

    private void DetectPlayer()
    {
        playerCollider = Physics.OverlapSphere(origin.position, radius, playerMask);

        //if (playerCollider.Length == 0)
        //{
        //    num = 0;
        //    numOfPlayer = num;
        //}
        //else if (num != 0)
        //{
        //    numOfPlayer = num;
        //}
        numOfPlayer = num;


        //Debug.Log("numOfPlayer = " + numOfPlayer);
        //Debug.Log("playerCollider = " + playerCollider.Length);

        if (numOfPlayer < playerCollider.Length)
        {

            for (int i = 0; i < playerCollider.Length; i++)
            {
                GameObject playerObj = playerCollider[i].gameObject;
                players[i] = playerObj.GetComponent<TestCube>();
                num = i + 1;
            }


        }
        
    }
}





