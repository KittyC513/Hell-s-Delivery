using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class BadgeManager : MonoBehaviour
{
    public enum BadgeType { gold, silver }
    public enum BadgeCondition { more, less, specific, stat, nonsense }
    public enum BadgeValues { walkDist, glideDist, numJumps, numButtons, fallDist, numPushes, numPushed }

    //all the badges we want to use in the game
    [SerializeField] public BadgeInfo[] allBadges;
    [SerializeField] private int goldBadgesPerLevel = 4;
    //the badges that can be earned in the current level
    private List<BadgeInfo> allSilverBadges;
    [SerializeField] private List<BadgeInfo> earnableSilverBadges;
    private List<BadgeInfo> allGoldbadges;
    private List<BadgeInfo> availableGoldBadges;
    [SerializeField] private BadgeInfo[] earnableGoldBadges;

    private List<BadgeInfo> p1CompletedBadges;
    private List<BadgeInfo> p2CompletedBadges;

    private LevelData lvlData;

    private void Start()
    {
        //set our maximum number of earnable gold badges per level
        earnableGoldBadges = new BadgeInfo[goldBadgesPerLevel];
        allSilverBadges = new List<BadgeInfo>();
        allGoldbadges = new List<BadgeInfo>();
        earnableSilverBadges = new List<BadgeInfo>();
        availableGoldBadges = new List<BadgeInfo>();
        p1CompletedBadges = new List<BadgeInfo>();
        p2CompletedBadges = new List<BadgeInfo>();
        lvlData = ScoreCount.instance.lvlData;

        foreach (var badge in allBadges)
        {
            if (badge.badgeType == BadgeType.silver)
            {
                allSilverBadges.Add(badge);
            }
            else
            {
                //add to a list that we can choose from randomly
                allGoldbadges.Add(badge);
                availableGoldBadges.Add(badge);
            }
        }

        //this is temporary, normally we want this to run on level start only
        ChooseLevelBadges();
           
    }
    private void ChooseLevelBadges()
    {
        //this function must only run once
        int[] randomIndex = new int[goldBadgesPerLevel];

        earnableSilverBadges.Clear();


        foreach (var badge in allSilverBadges)
        {
            //we can earn all silver badges per level
            earnableSilverBadges.Add(badge);
        }

        for (int i = 0; i < randomIndex.Length; i++)
        {
            //get a random gold badge (a value between 0 and the number of gold badges)
            //Do this for each earnable badge we have
            int randomValue = Random.Range(0, availableGoldBadges.Count);
            randomIndex[i] = randomValue;
            //make our earnable badges set to the number of index in the available badges list
            earnableGoldBadges[i] = availableGoldBadges[randomValue];
            //take that badge out of the badge pool
            availableGoldBadges.RemoveAt(randomValue);
        }
        
        
    }

    private void CheckCompletion()
    {
        //plays once at the end of the level
        //do not run this function in update

        foreach (var badge in earnableGoldBadges)
        {
            switch (badge.valueToRead)
            {
                case BadgeValues.walkDist:

                    switch (badge.condition)
                    {
                        case BadgeCondition.more:
                            
                            break;
                        case BadgeCondition.less:

                            break;
                        case BadgeCondition.specific:

                            break;
                        case BadgeCondition.stat:
                            p1CompletedBadges.Add(badge);
                            p2CompletedBadges.Add(badge);
                            break;
                        case BadgeCondition.nonsense:
                            p1CompletedBadges.Add(badge);
                            p2CompletedBadges.Add(badge);
                            break;
                    }

                    break;
                case BadgeValues.glideDist:

                    break;
                case BadgeValues.numJumps:

                    break;
                case BadgeValues.numButtons:

                    break;
                case BadgeValues.fallDist:

                    break;
                case BadgeValues.numPushes:

                    break;
                case BadgeValues.numPushed:
                    break;
            }
        }

        foreach (var badge in earnableSilverBadges)
        {

        }
    }
}
