using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SceneControl : MonoBehaviour
{
    [SerializeField]
    private Transform P1StartPoint;
    [SerializeField]
    private Transform P2StartPoint;

    private void Start()
    {
        GameManager.instance.Reposition(P1StartPoint, P2StartPoint);
    }

}
