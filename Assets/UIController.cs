using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private Canvas canvas2;

    public List<TargetIndicator> targetIndicators = new List<TargetIndicator>();

    private GameObject player1;
    private GameObject player2;
    private Camera p1Cam;
    private Camera p2Cam;


    [SerializeField]
    private GameObject TargetIndicatorPrefab;

    // Start is called before the first frame update
    void Start()
    {
        p1Cam = GameManager.instance.cam1.GetComponent<Camera>();
        p2Cam = GameManager.instance.cam2.GetComponent<Camera>();
       
    }

    // Update is called once per frame
    void Update()
    {
        if(targetIndicators.Count > 0)
        {
            for(int i = 0; i < targetIndicators.Count; i++)
            {
                targetIndicators[i].UpdateIndicator();
            }
        }
        
    }

    public void AddTargetIndicator(GameObject target)
    {
        TargetIndicator indicator = GameObject.Instantiate(TargetIndicatorPrefab, canvas.transform).GetComponent<TargetIndicator>();
        indicator.InitializeIndicator(target, player1, player2, p1Cam, p2Cam, canvas, canvas2);
        targetIndicators.Add(indicator);
    }
}
