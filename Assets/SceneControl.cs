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
    public Transform P1Rotation;
    [SerializeField]
    public Transform P2Rotation;

    [SerializeField]
    public Transform closeShootTV;
    [SerializeField]
    public Transform closeShootWerther;
    [SerializeField]
    public Transform closeShootNPC2;
    [SerializeField]
    public Transform closeShootNPC3;
    [SerializeField]
    public Transform mainCam;
    [SerializeField]
    public GameObject mainCamera;
    [SerializeField]
    public GameObject WertherCam;
    [SerializeField]
    public GameObject Npc2Cam;
    [SerializeField]
    public GameObject Npc3Cam;

    [SerializeField]
    public static GameObject LVNPC, LV;
    [SerializeField]
    public DialogueRunner dR;
    [SerializeField]
    public GameObject Lv1, lv2;
    [SerializeField]
    public GameObject phoneUI, dialogueBox, nameTag, nameTag1, WertherDialogue;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        GameManager.instance.Reposition(P1StartPoint, P2StartPoint, P1Rotation, P2Rotation);
        //LVNPC = Lv1;
        LV = lv2;

        //LVNPC.SetActive(false);
     
    }

    private void Update()
    {
        if (GameManager.instance.curSceneName == "HubStart")
        {
            if (GameManager.instance.showWertherInstruction)
            {
                WertherDialogue.SetActive(true);
            }
            else
            {
                WertherDialogue.SetActive(false);
            }
           
        }
    }


    public void SwitchCameraToTV()
    {
        MoveCamera(closeShootTV);
    }

    public void SwitchCameraToNpc()
    {
        mainCamera.SetActive(false);
        WertherCam.SetActive(true);
   
    }

    public void SwitchCameraToNpc2()
    {
        mainCamera.SetActive(false);
        Npc2Cam.SetActive(true);

    }

    public void SwitchCameraToNpc3()
    {
        mainCamera.SetActive(false);
        Npc3Cam.SetActive(true);

    }

    public void SwitchCameraToMain()
    {
        mainCamera.SetActive(true);
        WertherCam.SetActive(false);
        Npc2Cam.SetActive(false);
        Npc3Cam.SetActive(false);
    }


    public void MoveCamera(Transform newPos)
    {
        float lerpSpeed = 5f;
        mainCam.transform.position = Vector3.Lerp(mainCam.transform.position, newPos.position, Time.deltaTime * lerpSpeed);
        mainCam.transform.rotation = Quaternion.Lerp(mainCam.transform.rotation, newPos.rotation, Time.deltaTime * lerpSpeed);
        //print("Camera");
    }





}
