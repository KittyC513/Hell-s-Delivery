using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SceneInfo", menuName = "Persistence")]
public class SaveData : ScriptableObject
{
    [SerializeField]
    public float player1Score;
    [SerializeField]
    public float player2Score;
    [SerializeField]
    public bool p1Deliver;
    [SerializeField]
    public bool p2Deliver;


}


