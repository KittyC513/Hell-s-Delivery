using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [SerializeField] private AK.Wwise.Event scoreCardSequence;

    [Header ("Stickers")]
    [SerializeField] private GameObject happySticker;
    [SerializeField] private GameObject neutralSticker;
    [SerializeField] private GameObject sadSticker;
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
    private IEnumerator AnimationCycle()
    {
        yield return new WaitForSeconds(0.85f);

        //p1Stickers[0] = CheckScore(lvlData.p1Deaths, p1DeathSpot.position, categories.deaths);
        //p2Stickers[0] = CheckScore(lvlData.p2Deaths, p2DeathSpot.position, categories.deaths);
        CompareScore(lvlData.p1Deaths, lvlData.p2Deaths, categories.deaths, p1DeathSpot.position, p2DeathSpot.position);
        yield return new WaitForSeconds(0.44f);
        //scoreCardSequence.Post(this.gameObject);
        yield return new WaitForSeconds(0.31f);
        //p1Stickers[1] = CheckScore(lvlData.p1Deliver / (lvlData.p1Deliver + lvlData.p2Deliver), p1PackageSpot.position, categories.package);
        //p2Stickers[1] = CheckScore(lvlData.p2Deliver / (lvlData.p1Deliver + lvlData.p2Deliver), p2PackageSpot.position, categories.package);
        CompareScore(lvlData.p1Deliver, lvlData.p2Deliver, categories.deaths, p1PackageSpot.position, p2PackageSpot.position);
        yield return new WaitForSeconds(0.75f);
        //p1Stickers[2] = CheckScore(lvlData.completionTime, p1CompletionSpot.position, categories.completion);
        //p2Stickers[2] = CheckScore(lvlData.completionTime, p2CompletionSpot.position, categories.completion);

        yield return new WaitForSeconds(0.75f);
        FinalGrade(p1TotalSpot.position, p1Stickers);
        FinalGrade(p2TotalSpot.position, p2Stickers);
    }

    //put in both players score and determine which player had the higher score
    //put a thumbs up or down sticker on each player's category
    //give that player their earned points
    private void CompareScore(float p1Score, float p2Score, categories category, Vector3 p1StickerPoint, Vector3 p2StickerPoint, bool higherBetter)
    {
        GameObject sticker1 = null;
        GameObject sticker2 = null;

        if (p1Score > p2Score)
        {
            //give a thumbs up sticker to player 1 
            sticker1 = Instantiate(happySticker, p1StickerPoint, Quaternion.identity);
            sticker1.transform.rotation = RandomRotation();
            //give 50 points to player 1
            playerScoreData.p1Overall += 50;

            sticker2 = Instantiate(sadSticker, p2StickerPoint, Quaternion.identity);
            sticker2.transform.rotation = RandomRotation();
        }
        else if (p2Score > p1Score)
        {
            //give a thumbs down sticker to player 1
            sticker1 = Instantiate(sadSticker, p1StickerPoint, Quaternion.identity);
            sticker1.transform.rotation = RandomRotation();
            //do not give player 1 any points

            //give a thumbs up sticker to player 2
            sticker2 = Instantiate(happySticker, p2StickerPoint, Quaternion.identity);
            sticker2.transform.rotation = RandomRotation();
            //give player 2 50 points
            playerScoreData.p2Overall += 50;
        }
        else if (p1Score == p2Score)
        {
            //maybe give a neutral sticker ONLY if they have the exact same value of deaths or package time
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
