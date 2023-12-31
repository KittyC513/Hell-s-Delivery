using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;


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
    public TextMeshProUGUI timeIndicatorTextLayout;

    [SerializeField] private int p1Deaths = 0;
    [SerializeField] private float p1PackageTime = 0;

    [SerializeField] private int p2Deaths = 0;
    [SerializeField] private float p2PackageTime = 0;

    [SerializeField] private float completionTime = 0;
    private bool shouldCountTime = false;

    [SerializeField]
    public LevelData lvlData;

    [Space, Header("Ratio Values")]
    [SerializeField]
    private float goodRatio = 1;
    [SerializeField]
    private float avgRatio = 0.7f;
    [SerializeField]
    private float badRatio = 0.5f;
    [SerializeField]
    private int totalPoints = 1200;
    [SerializeField]
    private float deathWeight = 0.3f;
    [SerializeField]
    private float packageTimeWeight = 0.4f;
    [SerializeField]
    private float completionWeight = 0.3f;


    private float p1CalculatedScore;
    private float p2CalculatedScore;

    [SerializeField]
    private Slider slider;
    [SerializeField]
    private RectTransform knob;
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private float p1Score;
    [SerializeField]
    private float p2Score;
    [SerializeField]
    private float InitialScore;
    [SerializeField]
    public float time;
    [SerializeField]
    GameManager gameManager;
    [SerializeField]
    public float knobValue;
    [SerializeField]
    public float newRotation;
    [SerializeField]
    public float lastKnobValue;
    [SerializeField]
    private bool p1AddScore;
    [SerializeField]
    private bool p2AddScore;

    [SerializeField]
    public GameObject p1scoreEffect;
    [SerializeField]
    public GameObject p2scoreEffect;


    //need score for each value
    //need an expected outcome for each player
    //need a final value for each player

    //deaths

    //if we have deaths based on 3 values each one has a percentage attached
    //need to input all 3 and return a percentage based on that

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        lastKnobValue = 0;
        knobValue = 0;
        StartLevelTime();
        gameManager = Object.FindAnyObjectByType<GameManager>();

        p1Score = InitialScore;
        p2Score = p1Score;
        p1scoreEffect.SetActive(false);
        p2scoreEffect.SetActive(false);
    }   

    // Update is called once per frame
    void Update()
    {
        int minutes = Mathf.FloorToInt(completionTime / 60);
        int seconds = Mathf.FloorToInt(completionTime % 60);
        timeIndicatorText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        //timeIndicatorTextLayout.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        //timeIndicatorText.text = completionTime.ToString("f2");

        lvlData.p1Deaths = p1Deaths;
        lvlData.p2Deaths = p2Deaths;
        lvlData.p1Deliver = p1PackageTime;
        lvlData.p2Deliver = p2PackageTime;
        lvlData.completionTime = completionTime;
        lvlData.p1FinalScore = p1CalculatedScore;
        lvlData.p2FinalScore = p2CalculatedScore;

        //Debug.Log(p1CalculatedScore);
        //Debug.Log(p2CalculatedScore);

        if (Input.GetKeyDown(KeyCode.C))
        {
            SceneManager.LoadScene("ScoreCards");
        }

        if (shouldCountTime)
        {
            completionTime += Time.deltaTime;
        }

        AddScore();
    }

    private void FixedUpdate()
    {
        calculateScore(p1Deaths, p1PackageTime, p2Deaths, p2PackageTime, completionTime);
        //TotalScoreCal();
    }

    //return a vector 2, x is player 1 score y is player 2 score
    private void calculateScore(int p1Death, float p1PTime, float p2Death, float p2PTime, float compTime)
    {
        //k factor is always 32 idk tbh 
        float kFactor = 32;

        //each point type has a weight, for example the maximum points are 1200
        //completion points takes 0.3 or 30% of 1200
        //meaning if you get a maximum completion points score you get 30% of 1200 for it
        int completionPoints = Mathf.RoundToInt(completionWeight * totalPoints);
        int packagePoints = Mathf.RoundToInt(packageTimeWeight * totalPoints);
        int deathPoints = Mathf.RoundToInt(deathWeight * totalPoints);

        int p1DeathScore = CalculateScore(p1Death, lvlData.goodDeathValue, lvlData.avgDeathValue, goodRatio, avgRatio, badRatio, deathPoints, false);
        int p2DeathScore = CalculateScore(p2Death, lvlData.goodDeathValue, lvlData.avgDeathValue, goodRatio, avgRatio, badRatio, deathPoints, false);
        int p1PackageScore = CalculateScore(p1PTime / (p1PTime + p2PTime), lvlData.goodPackageTime, lvlData.avgPackageTime, goodRatio, avgRatio, badRatio, packagePoints, true);
        int p2PackageScore = CalculateScore(p2PTime / (p1PTime + p2PTime), lvlData.goodPackageTime, lvlData.avgPackageTime, goodRatio, avgRatio, badRatio, packagePoints, true);
        int completionScore = CalculateScore(compTime/60, lvlData.goodCompletionTime, lvlData.avgCompletionTime, goodRatio, avgRatio, badRatio, completionPoints, false);

        float p1Ra = p1DeathScore + p1PackageScore + completionScore;
        float p2Ra = p2DeathScore + p2PackageScore + completionScore;

        float p1Expected = 1 / (1 + 10 * (p2Ra - p1Ra) / 400);
        float p2Expected = 1 / (1 + 10 * (p1Ra - p2Ra) / 400);

        float p1Ratio = p1Ra + kFactor * (1 - p1Expected);
        float p2Ratio = p2Ra + kFactor * (1 - p2Expected);

        p1CalculatedScore = p1Ratio;
        p2CalculatedScore = p2Ratio;

    }

    private int CalculateScore(float value, float goodValue, float avgValue, float goodRatio, float avgRatio, float badRatio, int totalPoints, bool higherBetter)
    {
        //value = your raw value
        //good value = the level's good value standard
        //avg value = the level's average value standard
        //good ratio = the ratio of points you get for a good rated score
        //avgratio = the ratio of points you get for an average rated score
        //badRatio = the ratio of points you get for a bad rated score
        //totalPoints = all the points available for this value
        //higher better = is a higher score or a lower score better for this value

        //the function will return your final score value based on the ratio
        float ratio = 1;

        if (higherBetter)
        {
            if (value >= goodValue)
            {
                ratio = goodRatio;
            }
            else if (value >= avgValue)
            {
                ratio = avgRatio;
            }
            else if (value < avgValue)
            {
                ratio = badRatio;
            }
        }
        else
        {
            if (value > avgValue)
            {
                ratio = badRatio;
            }
            else if (value <= avgValue && value > goodValue)
            {
                ratio = avgRatio;
            }
            else if (value <= goodValue)
            {
                ratio = goodRatio;
            }
        }
       

        return Mathf.RoundToInt(ratio * totalPoints);

    }

    public void EndLevel()
    {
        lvlData.p1Deaths = p1Deaths;
        lvlData.p2Deaths = p2Deaths;
        lvlData.p1Deliver = p1PackageTime;
        lvlData.p2Deliver = p2PackageTime;
        lvlData.completionTime = completionTime;
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
        //p1Score += value;
        

        knobValue -= value;
        //StartCoroutine(RotateToPositionP2(-knobValue, 0.3f));
        p2Score += value;
        p2AddScore = true;
        p1Deaths += 1;
        //p1Score += p1Deaths;

        //deathCountP1.text = "Player1 Score: " + scoreValueP1.ToString();
    }

    public void AddPointToP1Package(int value)
    {
        //if player1 has the package add to their package time


        knobValue += value;
        //StartCoroutine(RotateToPosition(knobValue, 0.3f));
        p1Score += value;
        p1AddScore = true;
      
    }

    public void AddTimeToP1Package(float time)
    {
        p1PackageTime = time;
    }

    public void AddTimeToP2Package(float time)
    {
        p2PackageTime = time;
    }

    //IEnumerator P1PackageTimer(int value)
    //{
    //    time += Time.deltaTime;
    //    if (time >= 10)
    //    {
    //        p1Score += value;
    //        time = 0;
    //        StartCoroutine(ActivateP1UIForDuration(3));
    //    }

    //    yield return null;
    //}
    //IEnumerator P2PackageTimer(int value)
    //{
    //    time += Time.deltaTime;
    //    if (time >= 10)
    //    {
    //        p2Score += value;
    //        time = 0;
    //        StartCoroutine(ActivateP2UIForDuration(3));
    //    }

    //    yield return null;
    //}

    public void AddDeathsToP2(int value)
    {

        
        knobValue += value;
        //p2Score += value;
        //StartCoroutine(RotateToPosition(knobValue, 0.3f));
        p1Score += value;
        p1AddScore = true;

        p2Deaths += 1;
        //p2Score += p2Deaths;
        //deathCountP2.text = "Player2 Score: " + scoreValueP2.ToString();
    }

    public void AddPointToP2Package(int value)
    {
        
        knobValue -= value;
        //StartCoroutine(RotateToPositionP2(-knobValue, 0.3f));
        p2Score += value;
        p2AddScore = true;
      
    }

    void AddScore()
    {
        if (p1AddScore)
        {
            
            StartCoroutine(RotateToPosition(knobValue, 0.3f));
            p1scoreEffect.SetActive(true);

            StartCoroutine(ScoreEffectP1());
            print("31");

        }


        if (p2AddScore)
        {
            p2scoreEffect.SetActive(true);
            StartCoroutine(RotateToPositionP2(knobValue, 0.3f));

            StartCoroutine(ScoreEffectP2());
            print("33");

        }

    }

    IEnumerator RotateToPosition(float targetRotation, float rotationTime)
    {

        float elapsedTime = 0f;
        float startingRotation = knob.localEulerAngles.z;
        if(startingRotation > 180)
        {
            startingRotation -= 360;
        } 
        else if(startingRotation < -180)
        {
            startingRotation += 360;
        }



        while (elapsedTime < rotationTime)
        {

            if (knobValue >= 90)
            {
                knob.localEulerAngles = new Vector3(knob.localEulerAngles.x, knob.localEulerAngles.y, 90);


            }
            else if (knobValue <= -90)
            {
                knob.localEulerAngles = new Vector3(knob.localEulerAngles.x, knob.localEulerAngles.y, -90);

            }
            else if (knobValue < 90)
            {
                if (knobValue > -90)
                {
                    newRotation = Mathf.Lerp(startingRotation, targetRotation, elapsedTime / rotationTime);
                    //print("startingRotation" + startingRotation);
                    knob.localEulerAngles = new Vector3(knob.localEulerAngles.x, knob.localEulerAngles.y, newRotation);
  
                }

            }



            elapsedTime += Time.deltaTime;

            yield return null;

        }
        if(knobValue >= 90)
        {
            knob.localEulerAngles = new Vector3(knob.localEulerAngles.x, knob.localEulerAngles.y, 90);
           
        }
        else if(knobValue <= -90)
        {
            knob.localEulerAngles = new Vector3(knob.localEulerAngles.x, knob.localEulerAngles.y, -90);
            
        }
        else if(knobValue < 90) 
        {
            if(knobValue > -90)
            {
                knob.localEulerAngles = new Vector3(knob.localEulerAngles.x, knob.localEulerAngles.y, targetRotation);
        
            }

        }


       
        p1AddScore = false;

        //print("Moving" + targetRotation);



    }

    IEnumerator RotateToPositionP2(float targetRotation, float rotationTime)
    {
        float elapsedTime = 0f;
        float startingRotation = knob.localEulerAngles.z;
        
        float shortestRotation = targetRotation - startingRotation;
       
        //print("KnobValue" + knobValue);
        // Adjust for the shortest rotation
        if (shortestRotation > 180f)
        {
            shortestRotation -= 360f;
        }
        else if (shortestRotation < -180f)
        {
            shortestRotation += 360f;
        }



        while (elapsedTime < rotationTime)
        {


            if ( knobValue >= 90)
            {
                knob.localEulerAngles = new Vector3(knob.localEulerAngles.x, knob.localEulerAngles.y, 90);

            }
            else if (knobValue <= -90)
            {
                knob.localEulerAngles = new Vector3(knob.localEulerAngles.x, knob.localEulerAngles.y, -90);

            }
            else if (knobValue < 90)
            {
                if (knobValue > -90)
                {
                    float currentRotation = Mathf.Lerp(startingRotation, startingRotation + shortestRotation, elapsedTime / rotationTime);
                    knob.localEulerAngles = new Vector3(knob.localEulerAngles.x, knob.localEulerAngles.y, currentRotation);
                    //print("startingRotation" + startingRotation);
                }

            }


   

            // Ensure rotation does not go below -90
            elapsedTime += Time.deltaTime;
            yield return null;
        }


        if (knobValue <= -90)
        {
            knob.localEulerAngles = new Vector3(knob.localEulerAngles.x, knob.localEulerAngles.y, -90);
      
        }
        else if(knobValue >= 90)
        {
            knob.localEulerAngles = new Vector3(knob.localEulerAngles.x, knob.localEulerAngles.y, 90);

        }
        else if(knobValue > -90) 
        {
            if(knobValue < 90)
            {
                knob.localEulerAngles = new Vector3(knob.localEulerAngles.x, knob.localEulerAngles.y, targetRotation);

            }

        }


        p2AddScore = false;
        
    }


    IEnumerator ScoreEffectP1()
    {

        yield return new WaitForSeconds(3f);
        
        p1scoreEffect.SetActive(false);
    }

    IEnumerator ScoreEffectP2()
    {

        yield return new WaitForSeconds(3f);

        p2scoreEffect.SetActive(false);
    }





}
