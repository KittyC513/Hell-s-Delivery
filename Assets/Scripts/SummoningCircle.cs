using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SummoningCircle : MonoBehaviour
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
    [SerializeField]
    private int num;
    public bool playActive;
    public Animator circle;
    [SerializeField]
    private GameObject instruction;

    private void Start()
    {

        matChange = GetComponent<Renderer>();


        players = new TestCube[2];
    }

    private void Update()
    {
        DetectPlayer();
        //detect the player
        //if the player is detected read its run input, if the run input is active we want to set the player to a hold button state

        //if we detect the player in our circle
        for (int i = 0; i < numOfPlayer; i++)
        {
            //and just a double check that we have a player script attached to our player
            if (players[i] != null)
            {
                //if the player presses the action button (run)
                if (players[i].ReadActionButton() && !summoningActive)
                {
                    //activate summoning for this script at the player script
                    summoningActive = true;         
                    activePlayer = players[i];
                    players[i].OnSummoningEnter(this.gameObject);
                    matChange.material = onPush;
                    circle.SetBool("Activate", true);
                    //circle.SetFloat("Multiplier", 1);
                    //circle.SetTrigger("On");

                    //when two players trigger the button at the same time, the button is broken, need to fix!!!!
              
                }

            }
            else
            {
                summoningActive = false;
                activePlayer.OnSummoningExit();
                onExit.Invoke();
                activePlayer = null;
                matChange.material = Default;
                //circle.SetBool("Activate", false);
                //circle.SetFloat("Multiplier", -1);
                //circle.SetTrigger("On");
            }

        }


        if (activePlayer != null && summoningActive && !activePlayer.ReadActionButton())
        {
            //if summoning is active and we let go of the action button exit the summon
            summoningActive = false;
            activePlayer.OnSummoningExit();
            onExit.Invoke();
            activePlayer = null;
            matChange.material = Default;
            circle.SetBool("Activate", false);
            //circle.SetFloat("Multiplier", -1);
           // circle.SetTrigger("On");
            
        }

        //if(numOfPlayer == 0)
        //{
        //    summoningActive = false;
        //    activePlayer.OnSummoningExit();
        //    onExit.Invoke();
        //    activePlayer = null;
        //    matChange.material = Default;
        //}




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

        numOfPlayer = num;


        // Debug.Log("numOfPlayer = " + numOfPlayer);
        // Debug.Log("playerCollider = " + playerCollider.Length);

        if (numOfPlayer <= playerCollider.Length)
        {

            for (int i = 0; i < playerCollider.Length; i++)
            {
                GameObject playerObj = playerCollider[i].gameObject;
                players[i] = playerObj.GetComponent<TestCube>();
                num = i + 1;

            }


        }
        else if (numOfPlayer > playerCollider.Length)
        {
            for (int i = 0; i < players.Length; i++)
            {

                if (players[i] != null)
                {
                    num = i;
                    players[i].OnSummoningExit();
                    //Debug.Log("player! =" + players[i]);
                }
                players[i] = null;
                //activePlayer = null;

                //Debug.Log("Number = " + i);
                //if (players[i] != null)
                //{
                //    Debug.Log("player! =" + players[i]);
                //    players[i].OnSummoningExit();
                //}
                //players[i] = null;
            }
        }
        else if(playerCollider.Length == 0)
        {
            players[0] = null;
            players[1] = null;
            num = 0;


        }

        if(num == 0)
        {
            instruction.SetActive(false);
        }
        else
        {
            instruction.SetActive(true);
        }
     
    }
}





        ////check a circular area for a collider with the player layermask
        //Collider[] playerCollider = Physics.OverlapSphere(origin.position, radius, playerMask);


        //if (playerCollider.Length > 0)
        //{

        //    for (int i = 0; i < playerCollider.Length; i++)
        //    {
        //        GameObject playerObj = playerCollider[i].gameObject;
        //        players[i] = playerObj.GetComponent<TestCube>();

        //    }
        //}
        //else if(playerCollider.Length <= 0)
        //{
        //    for (int i = 0; i < players.Length - 1; i++)
        //    {
        //        if (players[i] != null)
        //        {
        //            //Debug.Log("player! =" + players[i]);
        //            players[i].OnSummoningExit();
        //        }
        //        players[i] = null;
        //        Debug.Log("player =" + players[i]);
        //    }

    

    //if we detect a player grab our player object and script for use otherwise exit the player from their summoning state if they are in it and get rid of our player reference
    //    if (playerCollider.Length > 0)
    //{
    //    Debug.Log(playerCollider.Length);
    //    for (int i = 0; i < playerCollider.Length; i++)
    //    {
    //        GameObject playerObj = playerCollider[i].gameObject;
    //        players[i] = playerObj.GetComponent<TestCube>();

    //    }
    //}
    //else
    //{
    //    for (int i = 0; i < players.Length - 1; i++)
    //    {
    //        if (players[i] != null)
    //        {
    //            Debug.Log("player! =" + players[i]);
    //            players[i].OnSummoningExit();
    //        }
    //        players[i] = null;
    //        Debug.Log("player =" + players[i]);
    //    }
    //}

    //check if we are still colliding with target player
    //if we aren't get rid of the reference

