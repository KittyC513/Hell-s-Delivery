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
    [SerializeField]
    private GameObject player1;
    [SerializeField]
    private GameObject player2;

    [SerializeField]
    private GameObject character1;
    [SerializeField]
    private GameObject character2;
    [SerializeField]
    private bool readyToStart;
    [SerializeField]
    private Animator p1Ani;
    [SerializeField]
    private Animator p2Ani;
    [SerializeField]
    private Animator titleAni;
    [SerializeField]
    private float destroyTime;

    [SerializeField]
    public bool sceneChanged;

    private void Start()
    {
        sceneChanged = false;
        character1.SetActive(false);
        character2.SetActive(false);
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
                player1 = obj;
                p1 = obj.GetComponent<TestCube>();
                character1.SetActive(true);


            }

            if (obj.layer == layerToFind2 && p2 == null)
            {
                player2 = obj;
                p2 = obj.GetComponent<TestCube>();
                character2.SetActive(true);
            }
        }

        //two players join the game, it loads to the Title Scene
        if(p1 != null && p2 != null)
        {
            p1Ani.SetBool("GameStart", true);
            p2Ani.SetBool("GameStart", true);
            titleAni.SetBool("GameStart", true);
            StartCoroutine(DestroyAfterDelay());
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

    public void DestroyObject()
    {
        if (player != null && player2 != null)
        {
            Destroy(player1);
            Destroy(player2);
        }

        if (player1 != null && player2 == null)
        {
            Destroy(player1);
        }

        if (player1 == null && player2 != null)
        {
            Destroy(player2);
        }
    }

    IEnumerator DestroyAfterDelay()
    {
        // Wait for the specified time
        yield return new WaitForSeconds(destroyTime);

        // Destroy the GameObject this script is attached to
        Destroy(character1);
        Destroy(character2);
    }

}






