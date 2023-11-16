using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class ScoreCount : MonoBehaviour
{

    public static ScoreCount instance;
    [SerializeField]
    public float scoreValueP1 = 0;
    [SerializeField]
    public float scoreValueP2 = 0;
    [SerializeField]
    public TextMeshProUGUI timeIndicatorText;

    [SerializeField]
    public SaveData sceneInfo;
    // Start is called before the first frame update

    [SerializeField] private int p1Deaths = 0;
    [SerializeField] private float p1PackageTime = 0;

    [SerializeField] private int p2Deaths = 0;
    [SerializeField] private float p2PackageTime = 0;

    [SerializeField] private float completionTime = 0;
    private bool shouldCountTime = false;

    //need to calculate score
    //each category can have a maximum number of points
    //this lets us weight things

    //death should have 50 points maximum
    //package time could have 125 points maximum
    //completion time can have 100 points 

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        StartLevelTime();
    }

    // Update is called once per frame
    void Update()
    {

        timeIndicatorText.text = completionTime.ToString("f2");
 
        sceneInfo.player1Score = scoreValueP1;
        sceneInfo.player2Score = scoreValueP2;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("ScoreCards");
        }

        if (shouldCountTime)
        {
            completionTime += Time.deltaTime;
        }
    }

    public void StartLevelTime()
    {
        shouldCountTime = true;
    }

    public void PauseLevelTime()
    {
        shouldCountTime = false;
    }

    public void AddDeathsToP1(int value)
    {
        //when the player dies (referenced in the respawn script) add to deaths, same with p2 later
        p1Deaths += value;

        //deathCountP1.text = "Player1 Score: " + scoreValueP1.ToString();
    }

    public void AddPointToP1Package(int value)
    {
        //if player1 has the package add to their package time
        p1PackageTime += value * Time.fixedDeltaTime;
        
        //deathCountP1.text = "Player1 Score: " + scoreValueP1.ToString();
    }

    public void AddDeathsToP2(int value)
    {
        p2Deaths += value;
        
        //deathCountP2.text = "Player2 Score: " + scoreValueP2.ToString();
    }

    public void AddPointToP2Package(int value)
    {
        //add to player 2 package Time
        p2PackageTime += value * Time.fixedDeltaTime;

        //deathCountP2.text = "Player2 Score: " + scoreValueP2.ToString();
    }


}
