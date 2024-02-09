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
    public Transform RespawnRotation;
    [SerializeField]
    public Transform RespawnRotation2;


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
    public GameObject phoneUI, dialogueBox, nameTag, nameTag1, WertherUI, NPC2UI, NPC3UI, nameTagNPC2,nameTagNPC3;

    [Header("Hub Start")]
    [SerializeField]
    private GameObject Comic1;
    [SerializeField]
    private GameObject phoneInstruction;
    [SerializeField]
    public bool dialogueFin;
    [SerializeField]
    private GameObject Weather;
    [SerializeField]
    public bool firstCustomer;

    private void Awake()
    {
        instance = this;

    }
    private void Start()
    {
        if(GameManager.instance.curSceneName != "TitleScene")
        {
            GameManager.instance.Reposition(P1StartPoint, P2StartPoint, P1Rotation, P2Rotation);
        }

        //LVNPC = Lv1;
        LV = lv2;

        //LVNPC.SetActive(false);




    }

    private void Update()
    {

        if (GameManager.instance.curSceneName == "HubStart")
        {
            HubStart();
        }

        if (Input.GetKey(KeyCode.E))
        {
            StopCoroutine(StartComicIntro());
            Comic1.SetActive(false);
            GameManager.instance.UnfreezePlayer();
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
    
    
    #region HubStart
    public void StartComic()
    {
        StartCoroutine(StartComicIntro());
    }

    IEnumerator StartComicIntro()
    {
        Comic1.SetActive(true);
        yield return new WaitForSeconds(29);
        Comic1.SetActive(false);
        GameManager.instance.UnfreezePlayer();



    }



    void HubStart()
    {
        if (GameManager.instance.firstTimeEnterHub == true)
        {
            StartComic();
            GameManager.instance.firstTimeEnterHub = false;
        }

        if (GameManager.instance.timesEnterHub >= 1)
        {
            Weather.SetActive(true);
            firstCustomer = true;
        }
        else
        {
            Weather.SetActive(false);
            firstCustomer = false;
        }

        if (GameManager.instance.showWertherInstruction)
        {
            WertherUI.SetActive(true);
        }
        else
        {
            WertherUI.SetActive(false);
        }

        if (GameManager.instance.showNPC2Instruction)
        {
            NPC2UI.SetActive(true);
        }
        else
        {
            NPC2UI.SetActive(false);
        }

        if (GameManager.instance.showNPC3Instruction)
        {
            NPC3UI.SetActive(true);
        }
        else
        {
            NPC3UI.SetActive(false);
        }

        if (GameManager.instance.ShowPhoneInstruction)
        {
            phoneInstruction.SetActive(true);
        }
        else
        {
            phoneInstruction.SetActive(false);
        }

        if (dialogueFin)
        {
            SwitchCameraToTV();
            //dialogueFin = false;
        }
    }


    #endregion





}
