using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;
using UnityEditor;

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

    public LevelData lvlData;
    [SerializeField]
    public LevelData lvl1Data;
    [SerializeField] public LevelData lvl2Data;
    [SerializeField] public LevelData lvl3Data;

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

    [Space, Header("UI Elements")]
    [SerializeField] public RectTransform p1MailSlot;
    [SerializeField] public RectTransform p2MailSlot;
    [SerializeField] public Image p1MailImage;
    [SerializeField] public Image p2MailImage;
    [SerializeField] public TextMeshProUGUI p1MailCount;
    [SerializeField] public TextMeshProUGUI p2MailCount;

    private int p1LocalMail;
    private int p2LocalMail;

    private float mailTime = 0;
    private Vector3 originalImgSize;

    private float p1TargetScaleMultiplier = 1;
    private float p1ScaleMultiplier = 1;

    private float p2TargetScaleMultiplier = 1;
    private float p2ScaleMultiplier = 1;

    [SerializeField] private float mailScaleSpeed = 1;

    [Space, Header("Scene Names")]
    [SerializeField] private string level1 = "Level1";
    [SerializeField] private string level2 = "MVPLevel";
    [SerializeField] private string level3 = "Level3";
    //need score for each value
    //need an expected outcome for each player
    //need a final value for each player

    //deaths

    //if we have deaths based on 3 values each one has a percentage attached
    //need to input all 3 and return a percentage based on that

    private void Awake()
    {
        instance = this;
        gameManager = Object.FindAnyObjectByType<GameManager>();

        StartLevel();
    }

    

    private void ResetValues()
    {
        lvlData.p1Deaths = 0;
        lvlData.p2Deaths = 0;
        lvlData.p1Deliver = 0;
        lvlData.p2Deliver = 0;
        lvlData.p1Collectable = false;
        lvlData.p2Collectable = false;
        lvlData.p1MailCount = 0;
        lvlData.p2MailCount = 0;
        lvlData.p1Badges = new BadgeInfo[3];
        lvlData.p2Badges = new BadgeInfo[3];
        lvlData.p1WalkDist = 0;
        lvlData.p2WalkDist = 0;
        lvlData.p1GlideDist = 0;
        lvlData.p2GlideDist = 0;
        lvlData.p1Jumps = 0;
        lvlData.p2Jumps = 0;
        lvlData.p1Buttons = 0;
        lvlData.p2Buttons = 0;
        lvlData.p1FallDist = 0;
        lvlData.p2FallDist = 0;
        lvlData.p1Pushes = 0;
        lvlData.p2Pushes = 0;
        lvlData.p1Pushed = 0;
        lvlData.p2Pushed = 0;
    }

    public void StartLevel()
    {
        StartLevelTime();


        if (gameManager.curSceneName == level1)
        {
            lvlData = lvl1Data;
        }
        else if (gameManager.curSceneName == level2)
        {
            lvlData = lvl2Data;
        }
        else if (gameManager.curSceneName == level3)
        {
            lvlData = lvl3Data;
        }
        else
        {
            lvlData = lvl1Data;
        }


        ResetValues();
    }

    void Start()
    {
        lastKnobValue = 0;
        knobValue = 0;

        p1PackageTime = 0;
        p2PackageTime = 0;
        p1Score = InitialScore;
        p2Score = p1Score;
        p1scoreEffect.SetActive(false);
        p2scoreEffect.SetActive(false);
        

        if (p1MailImage != null)
        {
            originalImgSize = p1MailImage.transform.localScale;
        }
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
        lvlData.p1MailCount = gameManager.p1.mailCount;

        if (gameManager.p2 != null)
        {
            lvlData.p2MailCount = gameManager.p2.mailCount;
        }
       

        p1MailCount.text = lvlData.p1MailCount.ToString();

        p2MailCount.text = lvlData.p2MailCount.ToString();

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
        AnimateMailImage();
        gameManager.lastLevelData = lvlData;
    }

    private void FixedUpdate()
    {
        //calculateScore(p1Deaths, p1PackageTime, p2Deaths, p2PackageTime, completionTime);
        //TotalScoreCal();
    }

    //return a vector 2, x is player 1 score y is player 2 score
    //private void calculateScore(int p1Death, float p1PTime, float p2Death, float p2PTime, float compTime)
    //{
    //    //k factor is always 32 idk tbh 
    //    float kFactor = 32;

    //    //each point type has a weight, for example the maximum points are 1200
    //    //completion points takes 0.3 or 30% of 1200
    //    //meaning if you get a maximum completion points score you get 30% of 1200 for it
    //    int completionPoints = Mathf.RoundToInt(completionWeight * totalPoints);
    //    int packagePoints = Mathf.RoundToInt(packageTimeWeight * totalPoints);
    //    int deathPoints = Mathf.RoundToInt(deathWeight * totalPoints);

    //    int p1DeathScore = CalculateScore(p1Death, lvlData.goodDeathValue, lvlData.avgDeathValue, goodRatio, avgRatio, badRatio, deathPoints, false);
    //    int p2DeathScore = CalculateScore(p2Death, lvlData.goodDeathValue, lvlData.avgDeathValue, goodRatio, avgRatio, badRatio, deathPoints, false);
    //    int p1PackageScore = CalculateScore(p1PTime / (p1PTime + p2PTime), lvlData.goodPackageTime, lvlData.avgPackageTime, goodRatio, avgRatio, badRatio, packagePoints, true);
    //    int p2PackageScore = CalculateScore(p2PTime / (p1PTime + p2PTime), lvlData.goodPackageTime, lvlData.avgPackageTime, goodRatio, avgRatio, badRatio, packagePoints, true);
    //    int completionScore = CalculateScore(compTime/60, lvlData.goodCompletionTime, lvlData.avgCompletionTime, goodRatio, avgRatio, badRatio, completionPoints, false);

    //    float p1Ra = p1DeathScore + p1PackageScore + completionScore;
    //    float p2Ra = p2DeathScore + p2PackageScore + completionScore;

    //    float p1Expected = 1 / (1 + 10 * (p2Ra - p1Ra) / 400);
    //    float p2Expected = 1 / (1 + 10 * (p1Ra - p2Ra) / 400);

    //    float p1Ratio = p1Ra + kFactor * (1 - p1Expected);
    //    float p2Ratio = p2Ra + kFactor * (1 - p2Expected);

    //    p1CalculatedScore = p1Ratio;
    //    p2CalculatedScore = p2Ratio;

    //}

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

    void AddTimer()
    {
        
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
       

        }


        if (p2AddScore)
        {
            p2scoreEffect.SetActive(true);
            StartCoroutine(RotateToPositionP2(knobValue, 0.3f));

            StartCoroutine(ScoreEffectP2());
    

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

    private void AnimateMailImage()
    {

        p1TargetScaleMultiplier = Mathf.Clamp(p1TargetScaleMultiplier, 1, 2);
        p2TargetScaleMultiplier = Mathf.Clamp(p2TargetScaleMultiplier, 1, 2);

        p1ScaleMultiplier = Mathf.Clamp(p1ScaleMultiplier, 1, 2);
        p2ScaleMultiplier = Mathf.Clamp(p2ScaleMultiplier, 1, 2);

        if (p1LocalMail != lvlData.p1MailCount)
        {


            StartCoroutine(P1AddScale());
        }

        if (p2LocalMail != lvlData.p2MailCount)
        {

            StartCoroutine(P2AddScale());
        }

        if (p1ScaleMultiplier < p1TargetScaleMultiplier)
        {
            p1ScaleMultiplier += (mailScaleSpeed * 2.5f) * Time.deltaTime;
        }
        else if (p1ScaleMultiplier > 1)
        {
            p1ScaleMultiplier -= (mailScaleSpeed) * Time.deltaTime;
            p1TargetScaleMultiplier = p1ScaleMultiplier;
        }


        if (p2ScaleMultiplier < p2TargetScaleMultiplier)
        {
            p2ScaleMultiplier += (mailScaleSpeed * 2.5f) * Time.deltaTime;
        }
        else if (p2ScaleMultiplier > 1)
        {
            p2ScaleMultiplier -= (mailScaleSpeed) * Time.deltaTime;
            p2TargetScaleMultiplier = p2ScaleMultiplier;
        }

        p1MailImage.transform.localScale = new Vector3(originalImgSize.x * p1ScaleMultiplier, originalImgSize.y * p1ScaleMultiplier, originalImgSize.z);
        p2MailImage.transform.localScale = new Vector3(originalImgSize.x * p2ScaleMultiplier, originalImgSize.y * p2ScaleMultiplier, originalImgSize.z);

        p1TargetScaleMultiplier = Mathf.Clamp(p1TargetScaleMultiplier, 1, 2);
        p2TargetScaleMultiplier = Mathf.Clamp(p2TargetScaleMultiplier, 1, 2);

        p1ScaleMultiplier = Mathf.Clamp(p1ScaleMultiplier, 1, 2);
        p2ScaleMultiplier = Mathf.Clamp(p2ScaleMultiplier, 1, 2);
    }

    private IEnumerator P1AddScale()
    {
        p1LocalMail = lvlData.p1MailCount;

        yield return new WaitForSeconds(0.22f);

        p1TargetScaleMultiplier += 0.2f;
    }

    private IEnumerator P2AddScale()
    {
        p2LocalMail = lvlData.p2MailCount;

        yield return new WaitForSeconds(0.22f);

        p2TargetScaleMultiplier += 0.2f;
    }

    public void AddBadgeValue(BadgeManager.BadgeValues valueType, int value, bool isPlayer1)
    {
        if (isPlayer1)
        {
            switch (valueType)
            {
                case BadgeManager.BadgeValues.walkDist:
                    lvlData.p1WalkDist += value;
                    break;
                case BadgeManager.BadgeValues.glideDist:
                    lvlData.p1GlideDist += value;
                    break;
                case BadgeManager.BadgeValues.numJumps:
                    lvlData.p1Jumps += value;
                    break;
                case BadgeManager.BadgeValues.numButtons:
                    lvlData.p1Buttons += value;
                    break;
                case BadgeManager.BadgeValues.fallDist:
                    lvlData.p1FallDist += value;
                    break;
                case BadgeManager.BadgeValues.numPushes:
                    lvlData.p1Pushes += value;
                    break;
                case BadgeManager.BadgeValues.numPushed:
                    lvlData.p1Pushed += value;
                    break;
            }
        }
        else
        {
            switch (valueType)
            {
                case BadgeManager.BadgeValues.walkDist:
                    lvlData.p2WalkDist += value;
                    break;
                case BadgeManager.BadgeValues.glideDist:
                    lvlData.p2GlideDist += value;
                    break;
                case BadgeManager.BadgeValues.numJumps:
                    lvlData.p2Jumps += value;
                    break;
                case BadgeManager.BadgeValues.numButtons:
                    lvlData.p2Buttons += value;
                    break;
                case BadgeManager.BadgeValues.fallDist:
                    lvlData.p2FallDist += value;
                    break;
                case BadgeManager.BadgeValues.numPushes:
                    lvlData.p2Pushes += value;
                    break;
                case BadgeManager.BadgeValues.numPushed:
                    lvlData.p2Pushed += value;
                    break;
            }
        }
      
    }
}
