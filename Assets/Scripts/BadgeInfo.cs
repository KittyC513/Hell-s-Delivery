using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BadgeInfo : ScriptableObject
{
    [Header("Visuals/Info")]
    [SerializeField] public string badgeName;
    [TextArea(5, 5)]
    [SerializeField] public string description;
    [SerializeField] public Sprite image;
    [Header ("Functionality")]
    [SerializeField] public BadgeManager.BadgeType badgeType;
    [SerializeField] public BadgeManager.BadgeCondition condition;
    [SerializeField] public BadgeManager.BadgeValues valueToRead;
    [Header ("Only used if condition is set to specific")]
    [SerializeField] public float specifiedValue = 0;
}
