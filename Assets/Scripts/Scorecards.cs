using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scorecards : MonoBehaviour
{
    [Header ("Data")]
    [SerializeField]
    private float player1Score;
    [SerializeField]
    private float player2Score;
    [SerializeField]
    public LevelData lvlData;
    [SerializeField]
    public PlayerScoreData playerScoreData;
    [SerializeField] private int thumbsUpBonus = 50;
    [SerializeField] private int badgeBonus = 50;
    [SerializeField] private bool canContinue = false;
    [SerializeField] private GameObject continueText;

    [SerializeField] private AK.Wwise.Event scoreCardSequence;
    private enum scoreSection { scoreCards, badge, leader }
    private scoreSection section = scoreSection.scoreCards;

    [Header("Objects")]
    [SerializeField] private GameObject cardLeft;
    [SerializeField] private GameObject cardRight;
    [SerializeField] private GameObject pedastols;

    [Header ("Stickers")]
    [SerializeField] private GameObject happySticker;
    [SerializeField] private GameObject neutralSticker;
    [SerializeField] private GameObject sadSticker;
    [SerializeField] private GameObject blankBadge;
    //[SerializeField] private GameObject aGrade;
    //[SerializeField] private GameObject bGrade;
    //[SerializeField] private GameObject cGrade;
    //[SerializeField] private GameObject dGrade;

    [Header ("Sticker Spots")]
    [SerializeField] private Transform p1DeathSpot;
    [SerializeField] private Transform p2DeathSpot;
    [SerializeField] private Transform p1PackageSpot;
    [SerializeField] private Transform p2PackageSpot;
    //[SerializeField] private Transform p1CompletionSpot;
    //[SerializeField] private Transform p2CompletionSpot;

    [SerializeField] private Transform p1TotalSpot;
    [SerializeField] private Transform p2TotalSpot;

    private enum stickerType { happy, neutral, sad }
    private stickerType[] p1Stickers;
    private stickerType[] p2Stickers;
    private enum categories { deaths, package, completion }

    [Header("Cameras")]
    [SerializeField] private Camera cardCam;
    [SerializeField] private Camera badgePillarCam;
    [SerializeField] private Camera player1Lead;
    [SerializeField] private Camera player2Lead;


    private bool hasStartedLeader = false;

    private void Start()
    {
        player1Score = lvlData.p1FinalScore;
        player2Score = lvlData.p2FinalScore;
        //first run animation 
        StartCoroutine(AnimationCycle());
        //check our player's scores and place the circle accordingly
        p1Stickers = new stickerType[3];
        p2Stickers = new stickerType[3];
    }

    private Quaternion RandomRotation()
    {
         return transform.rotation = Quaternion.Euler(0f, 0f, Random.Range(-15, 15));
      
    }

    private void Update()
    {
        //let the player know when they can press the button
        if (canContinue) continueText.SetActive(true);
        else continueText.SetActive(false);

        SectionUpdate();
    }

    public void NextSection()
    {
        //if the player presses A progress to the section
        //this could be from scorecards to employee of the month or from employee to exit scene
        if (section == scoreSection.scoreCards && canContinue)
        {
            section = scoreSection.badge;
            StartCoroutine(BadgeCycle());
        }
        else if (section == scoreSection.badge && canContinue)
        {
            //section = scoreSection.leader;
            
        }
        else if (section == scoreSection.leader && canContinue)
        {
            //if (canContinue) SceneManager.LoadScene("HubEnd");
            if (GameManager.instance.p1.ReadCloseTagButton() || GameManager.instance.p2.ReadCloseTagButton())
            {
                GameManager.instance.timesEnterHub += 1;
                Loader.Load(Loader.Scene.HubStart);
            }
        }
    }

    private void SectionUpdate()
    {
        switch (section)
        {
            case scoreSection.scoreCards:
                if (canContinue)
                {
                    //skip to next scene
                    
                    
                }

                break;
            case scoreSection.badge:
                //start the coroutine
                break;
            case scoreSection.leader:
               
                if (!hasStartedLeader)
                {
                    hasStartedLeader = true;
                    StartCoroutine(CurrentLeaderCycle());
                }
                cardRight.GetComponent<Animator>().SetBool("Skipped", true);
                cardLeft.GetComponent<Animator>().SetBool("Skipped", true);
                break;
        }
    }

    private IEnumerator AnimationCycle()
    {
        yield return new WaitForSeconds(0.85f);
        //cards are on screen, first category will play with sound
        //p1Stickers[0] = CheckScore(lvlData.p1Deaths, p1DeathSpot.position, categories.deaths);
        //p2Stickers[0] = CheckScore(lvlData.p2Deaths, p2DeathSpot.position, categories.deaths);
        CompareScore(lvlData.p1Deaths, lvlData.p2Deaths, categories.deaths, p1DeathSpot.position, p2DeathSpot.position, false);
        yield return new WaitForSeconds(0.44f);
        //scoreCardSequence.Post(this.gameObject);
        //start the audio so that it lines up with the animations
        yield return new WaitForSeconds(0.31f);
        //second category
        //p1Stickers[1] = CheckScore(lvlData.p1Deliver / (lvlData.p1Deliver + lvlData.p2Deliver), p1PackageSpot.position, categories.package);
        //p2Stickers[1] = CheckScore(lvlData.p2Deliver / (lvlData.p1Deliver + lvlData.p2Deliver), p2PackageSpot.position, categories.package);
        CompareScore(lvlData.p1Deliver, lvlData.p2Deliver, categories.deaths, p1PackageSpot.position, p2PackageSpot.position, true);
        yield return new WaitForSeconds(0.75f);
        //third category
        //p1Stickers[2] = CheckScore(lvlData.completionTime, p1CompletionSpot.position, categories.completion);
        //p2Stickers[2] = CheckScore(lvlData.completionTime, p2CompletionSpot.position, categories.completion);

        yield return new WaitForSeconds(0.75f);
        //give grade
        FinalGrade(p1TotalSpot.position, p1Stickers);
        FinalGrade(p2TotalSpot.position, p2Stickers);
        canContinue = true;
    }

    private IEnumerator BadgeCycle()
    {
        //transition camera to the back of the characters on the pedastols
        canContinue = false;
        pedastols.SetActive(true);
        badgePillarCam.gameObject.SetActive(true);
        cardCam.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.45f);

        //badge 1
        yield return new WaitForSeconds(0.45f);
        //badge 2

        yield return new WaitForSeconds(0.45f);
        //badge 3


        //wait for player to be able to read badge info
        yield return new WaitForSeconds(4);
        //transition to player 2 badges
        badgePillarCam.gameObject.GetComponent<Animator>().SetBool("p2Badge", true);
        yield return new WaitForSeconds(0.25f);
        //badge 1
        yield return new WaitForSeconds(0.75f);
        //badge 2
        yield return new WaitForSeconds(0.75f);
        //badge 3

        yield return new WaitForSeconds(4);

        //transition to pillars scene
        badgePillarCam.gameObject.GetComponent<Animator>().SetBool("pillars", true);

        yield return new WaitForSeconds(3);

        section = scoreSection.leader;

    }

    public IEnumerator CurrentLeaderCycle()
    {
        canContinue = false;

      
       

        yield return new WaitForSeconds(3);

        badgePillarCam.gameObject.SetActive(false);

        if (playerScoreData.p1Overall > playerScoreData.p2Overall)
        {
            //player 1 score is higher
            player1Lead.gameObject.SetActive(true);
        
        }
        else if (playerScoreData.p1Overall == playerScoreData.p2Overall)
        {
            //have some tie state
          
        }
        else
        {
            //player 2 score is higher
            player2Lead.gameObject.SetActive(true);
      
        }

        yield return new WaitForSeconds(1);

        canContinue = true;
        //transition to winning player cam
    }
    //put in both players score and determine which player had the higher score
    //put a thumbs up or down sticker on each player's category
    //give that player their earned points
    private void CompareScore(float p1Score, float p2Score, categories category, Vector3 p1StickerPoint, Vector3 p2StickerPoint, bool higherBetter)
    {
        GameObject sticker1 = null;
        GameObject sticker2 = null;

        if (!higherBetter)
        {
            p1Score *= -1;
            p2Score *= -1;
        }

        if (p1Score > p2Score)
        {
            //give a thumbs up sticker to player 1 
            sticker1 = Instantiate(happySticker, p1StickerPoint, Quaternion.identity, cardLeft.transform);
            sticker1.transform.rotation = RandomRotation();
            //give 50 points to player 1
            playerScoreData.p1Overall += 50;

            sticker2 = Instantiate(sadSticker, p2StickerPoint, Quaternion.identity, cardRight.transform);
            sticker2.transform.rotation = RandomRotation();
        }
        else if (p2Score > p1Score)
        {
            //give a thumbs down sticker to player 1
            sticker1 = Instantiate(sadSticker, p1StickerPoint, Quaternion.identity, cardLeft.transform);
            sticker1.transform.rotation = RandomRotation();
            //do not give player 1 any points

            //give a thumbs up sticker to player 2
            sticker2 = Instantiate(happySticker, p2StickerPoint, Quaternion.identity, cardRight.transform);
            sticker2.transform.rotation = RandomRotation();
            //give player 2 50 points
            playerScoreData.p2Overall += 50;
        }
        else if (p1Score == p2Score)
        {
            //maybe give a neutral sticker ONLY if they have the exact same value of deaths or package time
     
            sticker1 = Instantiate(neutralSticker, p1StickerPoint, Quaternion.identity, cardLeft.transform);
            sticker1.transform.rotation = RandomRotation();
      
            sticker2 = Instantiate(neutralSticker, p2StickerPoint, Quaternion.identity, cardRight.transform);
            sticker2.transform.rotation = RandomRotation();
       
        }
    }

    

    

    //each category needs to have it's score checked
    //score values of good and neutral
    //face point to put down the face
    //final score needs a point too but that can be a dif function
    private stickerType CheckScore(float score, Vector3 stickerPoint, categories category)
    {
        GameObject sticker = null;
        switch (category)
        {
     
            case categories.deaths:
                float value = score;
                bool higherBetter = false;
                float goodValue = lvlData.goodDeathValue;
                float avgValue = lvlData.avgDeathValue;

                if (higherBetter)
                {
                    if (value >= goodValue)
                    {
                        sticker = Instantiate(happySticker, stickerPoint, Quaternion.identity);
                        sticker.transform.rotation = RandomRotation();
                        return stickerType.happy;
                    }
                    else if (value >= avgValue)
                    {
                        sticker = Instantiate(neutralSticker, stickerPoint, Quaternion.identity);
                        sticker.transform.rotation = RandomRotation();
                        return stickerType.neutral;
                    }
                    else if (value < avgValue)
                    {
                        sticker = Instantiate(sadSticker, stickerPoint, Quaternion.identity);
                        sticker.transform.rotation = RandomRotation();
                        return stickerType.sad;
                    }
                }
                else
                {
                    if (value > avgValue)
                    {
                        sticker = Instantiate(sadSticker, stickerPoint, Quaternion.identity);
                        sticker.transform.rotation = RandomRotation();
                        return stickerType.sad;

                    }
                    else if (value <= avgValue && value > goodValue)
                    {
                        sticker = Instantiate(neutralSticker, stickerPoint, Quaternion.identity);
                        sticker.transform.rotation = RandomRotation();
                        return stickerType.neutral;
                    }
                    else if (value <= goodValue)
                    {
                        sticker = Instantiate(happySticker, stickerPoint, Quaternion.identity);
                        sticker.transform.rotation = RandomRotation();
                        return stickerType.happy;
                    }
                }
                break;
            case categories.package:
                value = score;
                higherBetter = true;
                goodValue = lvlData.goodPackageTime;
                avgValue = lvlData.avgPackageTime;

                if (higherBetter)
                {
                    if (value >= goodValue)
                    {
                        sticker = Instantiate(happySticker, stickerPoint, Quaternion.identity);
                        sticker.transform.rotation = RandomRotation();
                        return stickerType.happy;
                    }
                    else if (value >= avgValue)
                    {
                        sticker = Instantiate(neutralSticker, stickerPoint, Quaternion.identity);
                        sticker.transform.rotation = RandomRotation();
                        return stickerType.neutral;
                    }
                    else if (value < avgValue)
                    {
                        sticker = Instantiate(sadSticker, stickerPoint, Quaternion.identity);
                        sticker.transform.rotation = RandomRotation();
                        return stickerType.sad;
                    }
                }
                else
                {
                    if (value > avgValue)
                    {
                        sticker = Instantiate(sadSticker, stickerPoint, Quaternion.identity);
                        sticker.transform.rotation = RandomRotation();
                        return stickerType.sad;
                    }
                    else if (value <= avgValue && value > goodValue)
                    {
                        sticker = Instantiate(neutralSticker, stickerPoint, Quaternion.identity);
                        sticker.transform.rotation = RandomRotation();
                        return stickerType.neutral;
                    }
                    else if (value <= goodValue)
                    {

                        sticker = Instantiate(happySticker, stickerPoint, Quaternion.identity);
                        sticker.transform.rotation = RandomRotation();
                        return stickerType.happy;
                    }
                }
                break;
            case categories.completion:
                value = score;
                higherBetter = false;
                goodValue = lvlData.goodCompletionTime;
                avgValue = lvlData.avgCompletionTime;

                if (higherBetter)
                {
                    if (value >= goodValue)
                    {
                        sticker = Instantiate(happySticker, stickerPoint, Quaternion.identity);
                        sticker.transform.rotation = RandomRotation();
                        return stickerType.happy;
                    }
                    else if (value >= avgValue)
                    {
                        sticker = Instantiate(neutralSticker, stickerPoint, Quaternion.identity);
                        sticker.transform.rotation = RandomRotation();
                        return stickerType.neutral;
                    }
                    else if (value < avgValue)
                    {
                        sticker = Instantiate(sadSticker, stickerPoint, Quaternion.identity);
                        sticker.transform.rotation = RandomRotation();
                        return stickerType.sad;
                    }
                }
                else
                {
                    if (value > avgValue)
                    {
                        sticker = Instantiate(sadSticker, stickerPoint, Quaternion.identity);
                        sticker.transform.rotation = RandomRotation();
                        return stickerType.sad;
                    }
                    else if (value <= avgValue && value > goodValue)
                    {
                        sticker = Instantiate(neutralSticker, stickerPoint, Quaternion.identity);
                        sticker.transform.rotation = RandomRotation();
                        return stickerType.neutral;
                    }
                    else if (value <= goodValue)
                    {
                        sticker = Instantiate(happySticker, stickerPoint, Quaternion.identity);
                        sticker.transform.rotation = RandomRotation();
                        return stickerType.happy;
                    }
                }
                break;
        }

        

        return stickerType.neutral;
    }

    private void FinalGrade(Vector3 stickerPoint, stickerType[] stickers)
    {
        int score = 0;

        foreach (var sticker in stickers)
        {
            if (sticker == stickerType.happy)
            {
                score += 3;
            }
            else if (sticker == stickerType.neutral)
            {
                score += 2;
            }
            else if (sticker == stickerType.sad)
            {
                score += 1;
            }
        }

        //if (score == 9)
        //{
        //    Instantiate(aGrade, stickerPoint, Quaternion.identity);
        //}
        //else if (score >= 6)
        //{
        //    Instantiate(bGrade, stickerPoint, Quaternion.identity);
        //}
        //else if (score >= 4)
        //{
        //    Instantiate(cGrade, stickerPoint, Quaternion.identity);
        //}
        //else if (score <= 3)
        //{
        //    Instantiate(dGrade, stickerPoint, Quaternion.identity);
        //}
    }
}
