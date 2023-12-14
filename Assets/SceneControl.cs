using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;


public class SceneControl : MonoBehaviour
{
    public static SceneControl instance;
   
    [SerializeField]
    public Transform P1StartPoint;
    [SerializeField]
    public Transform P2StartPoint;

    [SerializeField]
    public Transform closeShootTV;
    [SerializeField]
    public Transform closeShootWerther;
    [SerializeField]
    public Transform mainCam;

    [SerializeField]
    public static GameObject LVNPC, LV;
    [SerializeField]
    public DialogueRunner dR;
    [SerializeField]
    public GameObject Lv1, lv2;
    [SerializeField]
    public GameObject phoneUI, dialogueBox;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        GameManager.instance.Reposition(P1StartPoint, P2StartPoint);
        //LVNPC = Lv1;
        LV = lv2;

        //LVNPC.SetActive(false);
     
    }

    private void Update()
    {

    }


    public void SwitchCameraToTV()
    {
        MoveCamera(closeShootTV);
    }

    public void SwitchCameraToNpc()
    {
        MoveCamera(closeShootWerther);
    }

    public void SwitchCameraToMain()
    {
        MoveCamera(mainCam);
    }


    public void MoveCamera(Transform newPos)
    {
        float lerpSpeed = 5f;
        mainCam.transform.position = Vector3.Lerp(mainCam.transform.position, newPos.position, Time.deltaTime * lerpSpeed);
        mainCam.transform.rotation = Quaternion.Lerp(mainCam.transform.rotation, newPos.rotation, Time.deltaTime * lerpSpeed);
        //print("Camera");
    }

}
