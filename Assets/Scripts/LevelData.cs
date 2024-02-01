using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LevelData : ScriptableObject
{
    [SerializeField]
    public string levelName;

    [Space, Header("Level Completion Time")]
    [SerializeField]
    public float avgCompletionTime = 11;
    [SerializeField]
    public float goodCompletionTime = 8;

    [Space, Header("Score Info")]
    [SerializeField]
    public int avgDeathValue = 5;
    [SerializeField]
    public int goodDeathValue = 1;
    [SerializeField]
    public float avgPackageTime = 0.5f;
    [SerializeField]
    public float goodPackageTime = 0.8f;

    [Space, Header("Player Level Data (do not change in editor)")]
    [SerializeField]
    public float p1Deaths;
    [SerializeField]
    public float p2Deaths;
    [SerializeField]
    public float p1Deliver;
    [SerializeField]
    public float p2Deliver;
    [SerializeField]
    public float completionTime;
    [SerializeField]
    public float p1FinalScore;
    [SerializeField]
    public float p2FinalScore;
    [SerializeField]
    public bool p1Collectable;
    [SerializeField]
    public bool p2Collectable;
}
