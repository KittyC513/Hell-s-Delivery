using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStart : MonoBehaviour
{
    //this script initializes the level score data because it only runs at start and spawns in levels where it is
    //destroyed after the level is unloaded


    private void Awake()
    {
    }
    void Start()
    {
        ScoreCount.instance.StartLevel();

        GameManager.instance.p1.gameObject.GetComponent<CharacterControl>().OnLevelStart();
        GameManager.instance.p2.gameObject.GetComponent<CharacterControl>().OnLevelStart();

        GameManager.instance.p1.gameObject.GetComponent<CharacterControl>().playerCollector.OnLevelStart();
        GameManager.instance.p2.gameObject.GetComponent<CharacterControl>().playerCollector.OnLevelStart();
    }

}
