using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SceneControl : MonoBehaviour
{
    public static SceneControl instance;
   
    [SerializeField]
    public Transform P1StartPoint;
    [SerializeField]
    public Transform P2StartPoint;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        GameManager.instance.Reposition(P1StartPoint, P2StartPoint);
    }

}
