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
    [SerializeField] private BadgeInfo blankBadge;
    private List<BadgeInfo> p1CompletedGoldBadges;
    private List<BadgeInfo> p2CompletedGoldBadges;
    private List<BadgeInfo> p1CompletedBadges;
    private List<BadgeInfo> p2CompletedBadges;

    private LevelData lvlData;
    private bool ranFinalCheck = false;
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
        p1CompletedGoldBadges = new List<BadgeInfo>();
        p2CompletedGoldBadges = new List<BadgeInfo>();
       

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

    private void Update()
    {
        lvlData = ScoreCount.instance.lvlData;
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

    public void RunFinalCheck()
    {
        if (!ranFinalCheck)
        {
            foreach (var badge in earnableGoldBadges)
            {
                switch (badge.valueToRead)
                {
                    case BadgeValues.walkDist:
                        CheckCompletion(lvlData.p1WalkDist, lvlData.p2WalkDist, badge);
                        break;
                    case BadgeValues.glideDist:
                        CheckCompletion(lvlData.p1GlideDist, lvlData.p2GlideDist, badge);
                        break;
                    case BadgeValues.numJumps:
                        CheckCompletion(lvlData.p1Jumps, lvlData.p2Jumps, badge);
                        break;
                    case BadgeValues.numButtons:
                        CheckCompletion(lvlData.p1Buttons, lvlData.p2Buttons, badge);
                        break;
                    case BadgeValues.fallDist:
                        CheckCompletion(lvlData.p1FallDist, lvlData.p2FallDist, badge);
                        break;
                    case BadgeValues.numPushes:
                        CheckCompletion(lvlData.p1Pushes, lvlData.p2Pushes, badge);
                        break;
                    case BadgeValues.numPushed:
                        CheckCompletion(lvlData.p1Pushed, lvlData.p2Pushed, badge);
                        break;

                }
            }

            foreach (var badge in earnableSilverBadges)
            {
                switch (badge.valueToRead)
                {
                    case BadgeValues.walkDist:
                        CheckCompletion(lvlData.p1WalkDist, lvlData.p2WalkDist, badge);
                        break;
                    case BadgeValues.glideDist:
                        CheckCompletion(lvlData.p1GlideDist, lvlData.p2GlideDist, badge);
                        break;
                    case BadgeValues.numJumps:
                        CheckCompletion(lvlData.p1Jumps, lvlData.p2Jumps, badge);
                        break;
                    case BadgeValues.numButtons:
                        CheckCompletion(lvlData.p1Buttons, lvlData.p2Buttons, badge);
                        break;
                    case BadgeValues.fallDist:
                        CheckCompletion(lvlData.p1FallDist, lvlData.p2FallDist, badge);
                        break;
                    case BadgeValues.numPushes:
                        CheckCompletion(lvlData.p1Pushes, lvlData.p2Pushes, badge);
                        break;
                    case BadgeValues.numPushed:
                        CheckCompletion(lvlData.p1Pushed, lvlData.p2Pushed, badge);
                        break;

                }
            }

            SelectFinalBadges();
            ranFinalCheck = true;
        }
      
    }

    private void CheckCompletion(int p1Value, int p2Value, BadgeInfo badge)
    {
        //plays once at the end of the level
        //do not run this function in update

        if (badge.badgeType == BadgeType.gold)
        {
            switch (badge.condition)
            {
                case BadgeCondition.more:
                    if (p1Value > p2Value)
                    {
                        p1CompletedGoldBadges.Add(badge);
                    }
                    else if (p2Value > p1Value)
                    {
                        p2CompletedGoldBadges.Add(badge);
                    }
                    break;
                case BadgeCondition.less:
                    if (p1Value < p2Value)
                    {
                        p1CompletedGoldBadges.Add(badge);
                    }
                    else if (p2Value < p1Value)
                    {
                        p2CompletedGoldBadges.Add(badge);
                    }
                    break;
                case BadgeCondition.specific:
                    if (p1Value == badge.specifiedValue)
                    {
                        p1CompletedGoldBadges.Add(badge);
                    }

                    if (p2Value == badge.specifiedValue)
                    {
                        p2CompletedGoldBadges.Add(badge);
                    }
                    break;
                case BadgeCondition.stat:
                    p1CompletedGoldBadges.Add(badge);
                    p2CompletedGoldBadges.Add(badge);
                    break;
                case BadgeCondition.nonsense:
                    p1CompletedGoldBadges.Add(badge);
                    p2CompletedGoldBadges.Add(badge);
                    break;
            }
        }
        else
        {
            switch (badge.condition)
            {
                case BadgeCondition.more:
                    if (p1Value > p2Value)
                    {
                        p1CompletedBadges.Add(badge);
                    }
                    else if (p2Value > p1Value)
                    {
                        p2CompletedBadges.Add(badge);
                    }
                    break;
                case BadgeCondition.less:
                    if (p1Value < p2Value)
                    {
                        p1CompletedBadges.Add(badge);
                    }
                    else if (p2Value < p1Value)
                    {
                        p2CompletedBadges.Add(badge);
                    }
                    break;
                case BadgeCondition.specific:
                    if (p1Value == badge.specifiedValue)
                    {
                        p1CompletedBadges.Add(badge);
                    }

                    if (p2Value == badge.specifiedValue)
                    {
                        p2CompletedBadges.Add(badge);
                    }
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
        }
     
    }

    private void SelectFinalBadges()
    {
        for (int i = 0; i < lvlData.p1Badges.Length; i++)
        {
            //first check if we have a badge here
            if (lvlData.p1Badges[i] == null)
            {
                //if we don't have a badge first look for a gold badge
                if (p1CompletedGoldBadges.Count > 0)
                {
                    //if we still have completed gold badges add a completed gold badge in this spot
                    lvlData.p1Badges[i] = p1CompletedGoldBadges[0];
                    p1CompletedGoldBadges.Remove(p1CompletedGoldBadges[0]);
                }
                else
                {
                    //otherwise look for a completed silver badge
                    if (p1CompletedBadges.Count > 0)
                    {
                        int randValue = Random.Range(0, p1CompletedBadges.Count);
                        lvlData.p1Badges[i] = p1CompletedBadges[randValue];
                        p1CompletedBadges.RemoveAt(randValue);
                    }
                    else
                    {
                        //empty badge
                        lvlData.p1Badges[i] = blankBadge;
                    }
                }
            }
        }

        for (int i = 0; i < lvlData.p2Badges.Length; i++)
        {
            //first check if we have a badge here
            if (lvlData.p2Badges[i] == null)
            {
                //if we don't have a badge first look for a gold badge
                if (p2CompletedGoldBadges.Count > 0)
                {
                    //if we still have completed gold badges add a completed gold badge in this spot
                    lvlData.p2Badges[i] = p2CompletedGoldBadges[0];
                    p2CompletedGoldBadges.Remove(p2CompletedGoldBadges[0]);
                }
                else
                {
                    //otherwise look for a completed silver badge
                    if (p2CompletedBadges.Count > 0)
                    {
                        int randValue = Random.Range(0, p1CompletedBadges.Count);
                        lvlData.p2Badges[i] = p2CompletedBadges[randValue];

                        p2CompletedBadges.RemoveAt(randValue);
                    }
                    else
                    {
                        //empty badge
                        lvlData.p2Badges[i] = blankBadge;
                    }
                }
            }
        }
    }
}
