using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SceneInfo", menuName = "Persistence")]
public class SaveData : ScriptableObject
{
    [SerializeField]
    public float p1Deaths;
    [SerializeField]
    public float p2Deaths;
    [SerializeField]
    public bool p1Deliver;
    [SerializeField]
    public bool p2Deliver;
    [SerializeField]
    public float completionTime;
    [SerializeField]
    public float p1FinalScore;
    [SerializeField]
    public float p2FinalScore;
}


