using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterHUB : MonoBehaviour
{
    [SerializeField]
    private bool isPlayer;

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            //when player is pressed "B", the scene loads to Hub Start
        }
    }

}
