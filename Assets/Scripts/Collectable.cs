using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] private LevelData levelData;
    private bool collected = false;

    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (!collected && collision.CompareTag("Player"))
        {
            OnCollect(collision.gameObject.GetComponent<TestCube>());
        }

    }


    private void OnCollect(TestCube player)
    {
        collected = true;
        if (player.isPlayer1)
        {
            //collect the object for this player
            levelData.p1Collectable = true;
        }
        else if (player.isPlayer2)
        {
            //collect the object for this player
            levelData.p2Collectable = true;
        }
        DeactivateObject();
        
    }

    private void DeactivateObject()
    {
        //for now we just set to false
        //will play animation later
        this.gameObject.SetActive(false);
    }
}
