using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ScoreCount : MonoBehaviour
{

    public static ScoreCount instance;
    [SerializeField]
    public float scoreValueP1 = 0;
    [SerializeField]
    public float scoreValueP2 = 0;
    [SerializeField]
    public TextMeshProUGUI timeIndicatorText;

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
    private float time;
    [SerializeField]
    GameManager gameManager;



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
        StartLevelTime();
        gameManager = Object.FindAnyObjectByType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        int minutes = Mathf.FloorToInt(completionTime / 60);
        int seconds = Mathf.FloorToInt(completionTime % 60);
        timeIndicatorText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        //timeIndicatorText.text = completionTime.ToString("f2");

        lvlData.p1Deaths = p1Deaths;
        lvlData.p2Deaths = p2Deaths;
        lvlData.p1Deliver = p1PackageTime;
        lvlData.p2Deliver = p2PackageTime;
        lvlData.completionTime = completionTime;
        lvlData.p1FinalScore = p1CalculatedScore;
        lvlData.p2FinalScore = p2CalculatedScore;

        Debug.Log(p1CalculatedScore);
        Debug.Log(p2CalculatedScore);

        if (Input.GetKeyDown(KeyCode.C))
        {
            SceneManager.LoadScene("ScoreCards");
        }

        if (shouldCountTime)
        {
            completionTime += Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        calculateScore(p1Deaths, p1PackageTime, p2Deaths, p2PackageTime, completionTime);
        TotalScoreCal();
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
        p1Deaths += value;
        //p1Deaths += value;
        //p1Score += p1Deaths;

        //deathCountP1.text = "Player1 Score: " + scoreValueP1.ToString();
    }

    public void AddPointToP1Package(int value)
    {
        //if player1 has the package add to their package time
        time += Time.deltaTime;
        if(time >= 10)
        {

            p1PackageTime += value * Time.fixedDeltaTime;
            p1Score += p1PackageTime;
            time = 0;

            StartCoroutine(ActivateP1UIForDuration(3f));
        }


        //deathCountP1.text = "Player1 Score: " + scoreValueP1.ToString();
    }

    public void AddDeathsToP2(int value)
    {

        p2Deaths += value;
        //p2Deaths -= value;
        //p2Score += p2Deaths;
        //deathCountP2.text = "Player2 Score: " + scoreValueP2.ToString();
    }

    public void AddPointToP2Package(int value)
    {
        time += Time.deltaTime;
        if(time >= 10)
        {
            p2PackageTime += value * Time.fixedDeltaTime;
            p2Score += p2PackageTime;
            time = 0;
            StartCoroutine(ActivateP2UIForDuration(3f));
        }
        //add to player 2 package Time

        //deathCountP2.text = "Player2 Score: " + scoreValueP2.ToString();
    }

    void TotalScoreCal()
    {
        if (p1CalculatedScore > p2CalculatedScore)
        {
            float difference = (p1CalculatedScore - p2CalculatedScore)/10;

            if (knob.localEulerAngles.z >= 90)
            {
                knob.localEulerAngles = new Vector3(0, 0, 90);
            }

            StartCoroutine(RotateToPosition(difference, 2));

        } 
        else if (p2CalculatedScore > p1CalculatedScore)
        {
            float difference = (p2CalculatedScore - p1CalculatedScore) / 10;

            if (knob.localEulerAngles.z <= -90)
            {
                knob.localEulerAngles = new Vector3(0, 0, -90);
            }

            StartCoroutine(RotateToPosition(-difference, 2));
        }
        else if(p1CalculatedScore == p2CalculatedScore)
        {
            knob.localEulerAngles = new Vector3(0, 0, 0);
        }
    }


    IEnumerator ActivateP1UIForDuration(float duration)
    {
        gameManager.p1UI.SetActive(true);

        // Wait for the specified duration
        yield return new WaitForSeconds(duration);

        // Deactivate the UI after the specified duration
        gameManager.p1UI.SetActive(false);
    }

    IEnumerator ActivateP2UIForDuration(float duration)
    {
        gameManager.p2UI.SetActive(true);

        // Wait for the specified duration
        yield return new WaitForSeconds(duration);

        // Deactivate the UI after the specified duration
        gameManager.p2UI.SetActive(false);
    }


    //float knobPosition = percentage * slider.GetComponent<RectTransform>().rect.width;

    //Vector3 newPosition = knob.localPosition;
    //newPosition.x = knobPosition;
    //knob.localPosition = newPosition;
    IEnumerator RotateToPosition(float targetRotation, float rotationTime)
    {

        float elapsedTime = 0f;
        float startingRotation = knob.localEulerAngles.z;


        while (elapsedTime < rotationTime)
        {
            float newRotation = Mathf.Lerp(startingRotation, targetRotation, elapsedTime / rotationTime);
            knob.localEulerAngles = new Vector3(knob.localEulerAngles.x, knob.localEulerAngles.y, newRotation);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the object reaches the exact target position at the end
        knob.localEulerAngles = new Vector3(knob.localEulerAngles.x, knob.localEulerAngles.y, targetRotation);

    }


}
