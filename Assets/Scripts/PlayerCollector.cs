using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    [HideInInspector] public ScoreCount scoreCount;
    [HideInInspector] public bool isPlayer1 = false;
    [SerializeField] public TestCube player;
    [HideInInspector] public Camera cam;
    // Start is called before the first frame update
    private void Start()
    {
        
        isPlayer1 = player.isPlayer1;
        cam = player.playerCamera;
    }

    public void OnLevelStart()
    {
        scoreCount = FindFirstObjectByType<ScoreCount>();
    }

}
