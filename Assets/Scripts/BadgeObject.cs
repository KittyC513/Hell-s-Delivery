using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadgeObject : MonoBehaviour
{
    [SerializeField] private Sprite badgeImage;
    SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void InitializeBadge(Sprite image)
    {
        sr.sprite = image;
    }
}
