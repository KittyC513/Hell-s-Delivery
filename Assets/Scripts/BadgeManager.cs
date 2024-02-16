using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadgeManager : MonoBehaviour
{
    public enum BadgeType { gold, silver }
    public enum BadgeCondition { more, less, specific, stat, nonsense }
    public enum BadgeValues { walkDist, glideDist, numJumps, numButtons, fallDist }

    //all the badges we want to use in the game
    [SerializeField] public BadgeInfo[] allBadges;
    [SerializeField] private int goldBadgesPerLevel = 4;
    //the badges that can be earned in the current level
    private List<BadgeInfo> allSilverBadges;
    private List<BadgeInfo> earnableSilverBadges;
    private List<BadgeInfo> allGoldbadges;
    private BadgeInfo[] earnableGoldBadges;


    private void Start()
    {
        //set our maximum number of earnable gold badges per level
        earnableGoldBadges = new BadgeInfo[goldBadgesPerLevel];

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
            }
        }
           
    }
    private void ChooseLevelBadges()
    {
        //this function must only run once
        int[] randomIndex = new int[goldBadgesPerLevel];

        earnableSilverBadges.Clear();


        foreach (var badge in allSilverBadges)
        {
            earnableSilverBadges.Add(badge);
        }

        for (int i = 0; i < randomIndex.Length; i++)
        {

        }
        

    }
}
