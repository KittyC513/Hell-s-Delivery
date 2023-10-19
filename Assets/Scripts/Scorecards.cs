using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scorecards : MonoBehaviour
{
    private int player1Score;
    private int player2Score;

    [SerializeField] private int badScore = 100;
    [SerializeField] private int neutralScore = 300;
    [SerializeField] private int goodScore = 500;
    [SerializeField] private int greatScore = 800;

    [SerializeField] private GameObject p1Circle;
    [SerializeField] private GameObject p2Circle;
    [SerializeField] private Transform[] p1Points;
    [SerializeField] private Transform[] p2Points;

    private void Start()
    {
        player1Score = 50;
        player2Score = 600;
        //first run animation 
        StartCoroutine(AnimationCycle());
        //check our player's scores and place the circle accordingly
        
    }

    private IEnumerator AnimationCycle()
    {
        yield return new WaitForSeconds(0.85f);

        //start circle animation
        CheckScore(player1Score, p1Circle, p1Points);
        CheckScore(player2Score, p2Circle, p2Points);
    }

    private void CheckScore(int score, GameObject circle, Transform[] circlePoints)
    {
        if (score <= badScore)
        {
            circle.transform.position = circlePoints[0].position;
        }
        else if (score <= neutralScore)
        {
            circle.transform.position = circlePoints[1].position;
        }
        else if (score <= goodScore)
        {
            circle.transform.position = circlePoints[2].position;
        }
        else
        {
            circle.transform.position = circlePoints[3].position;
        }

        circle.SetActive(true);
    }
}
