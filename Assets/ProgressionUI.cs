using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressionUI : MonoBehaviour
{
    [SerializeField]
    private Slider p1Slider;
    [SerializeField]
    private Slider p2Slider;
    [SerializeField]
    private Transform startLevel;
    [SerializeField]
    private Transform Level2;
    [SerializeField]
    private Transform Level3;
    [SerializeField]
    private Transform Level4;
    [SerializeField]
    private Transform endLevel;
    [SerializeField]
    private Transform player1Transform;
    [SerializeField]
    private Transform player2Transform;
    [SerializeField]
    private Transform endLineTransform;
    [SerializeField]
    ObjectGrabbable oG;
    [SerializeField]
    private Vector3 endLinePosition;
    [SerializeField]
    private float fullDistance;


    // Start is called before the first frame update
    void Start()
    {
        endLinePosition = endLineTransform.position;
        fullDistance = Vector3.Distance(startLevel.position,endLevel.position);
        p1Slider.maxValue = fullDistance;
        p2Slider.maxValue = fullDistance;

    }

    // Update is called once per frame
    void Update()
    {
        float newDistanceP1 = GetP1Distance();
        float progressValueP1 = fullDistance - newDistanceP1;

        UpdateProgressFillP1(progressValueP1);

        float newDistanceP2 = GetP2Distance();
        float progressValueP2 = fullDistance - newDistanceP2;

        UpdateProgressFillP2(progressValueP2);

    }

    private float GetP1Distance()
    {
        player1Transform = oG.playerDir;
        return Vector3.Distance(player1Transform.position, endLinePosition);
    }

    private float GetP2Distance()
    {
        player2Transform = oG.player2Dir;
        return Vector3.Distance(player2Transform.position, endLinePosition);
    }

    private void UpdateProgressFillP1(float value)
    {
        p1Slider.value = value;

    }

    private void UpdateProgressFillP2(float value)
    {
        p2Slider.value = value;

    }
}
