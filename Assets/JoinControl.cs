using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoinControl : MonoBehaviour
{
    [SerializeField]
    private GameObject gameManager;
    [SerializeField]
    private GameObject player1;
    [SerializeField]
    private GameObject player2;
    [SerializeField]
    private GameObject player1Canvas;
    [SerializeField]
    private GameObject player2Canvas;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Player1Join();
        Player2Join();
        DeleteModel();

    }


    private void DeleteModel()
    {
        if(GameManager.instance.player1Joined && GameManager.instance.player2Joined)
        {
            StartCoroutine(RemovePlayer());
        }
    }
    private void Player1Join()
    {
        Animator anim = player1.GetComponent<Animator>();

        if (GameManager.instance.player1Joined)
        {
            anim.SetTrigger("Join");
            player1Canvas.SetActive(false);
            StartCoroutine(ReplacePlayer1());
        }

    }

    private void Player2Join()
    {
        Animator anim = player2.GetComponent<Animator>();
        if (GameManager.instance.player2Joined)
        {
            anim.SetTrigger("Join");
            player2Canvas.SetActive(false);
            StartCoroutine(ReplacePlayer2());
        }
    }

    IEnumerator ReplacePlayer1()
    {
        yield return new WaitForSeconds(10f);
        player1.transform.rotation = Quaternion.Euler(90, this.transform.eulerAngles.y, this.transform.eulerAngles.z);
    }

    IEnumerator ReplacePlayer2()
    {
        yield return new WaitForSeconds(10f);
        player2.transform.rotation = Quaternion.Euler(90, this.transform.eulerAngles.y, this.transform.eulerAngles.z);
    }

    IEnumerator RemovePlayer()
    {
        yield return new WaitForSeconds(3f);
        player1.SetActive(false);
        player2.SetActive(false);

    }

}
