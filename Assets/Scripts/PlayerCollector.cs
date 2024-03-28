using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    [HideInInspector] public ScoreCount scoreCount;
    public bool isPlayer1 = false;
    [SerializeField] public TestCube player;
    [HideInInspector] public Camera cam;
    // Start is called before the first frame update
    private void Start()
    {
        
        
        cam = player.playerCamera;
    }
    private void Update()
    {
        isPlayer1 = player.isPlayer1;
    }

    public void OnLevelStart()
    {
        scoreCount = FindFirstObjectByType<ScoreCount>();
    }

}
