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
    public GameObject phoneUI, dialogueBox, nameTag, nameTag1, WertherUI, LalahUI, MichaelUI, nameTagNPC2, nameTagNPC3;

    [Header("Title Page")]
    [SerializeField]
    private GameObject hightlightedDoor;

    [Header("Hub Start")]
    [SerializeField]
    private GameObject Comic1;
    [SerializeField]
    private GameObject phoneInstruction;
    [SerializeField]
    private GameObject phoneRingText;
    [SerializeField]
    public bool dialogueFin;
    [SerializeField]
    public GameObject ConfirmText;
    [SerializeField]
    public bool ConfirmTextisActivated;
    [SerializeField]
    public bool deliveryTextisActivated;
    [SerializeField]
    public bool p1AtDoor;
    [SerializeField]
    public bool p2AtDoor;
    [SerializeField]
    public GameObject radialUI;
    [SerializeField]
    public GameObject radialUI2;


    [Header("Weather Event")]
    [SerializeField]
    private GameObject Weather;
    [SerializeField]
    public bool secondCustomer;
    [SerializeField]
    public bool WeatherdialogueEnds;
    [SerializeField]
    public GameObject normalPackage;
    [SerializeField]
    public bool showPackage;
    [SerializeField]
    public bool showPackage1;
    [SerializeField]
    public GameObject phonePiece;
    [SerializeField]
    public GameObject deliveryText;
    [SerializeField]
    private NPCTrigger NPCTrigger;
    [SerializeField]
    public bool weatherIsGone;


    [Header("Lalah Event")]
    [SerializeField]
    private GameObject Lalah;
    [SerializeField]
    public bool firstCustomer;
    [SerializeField]
    public bool LalahdialogueEnds;
    [SerializeField]
    public GameObject heavyPackage;
    [SerializeField]
    public bool showHeavyPackage;
    [SerializeField]
    public bool comicShowed;
    [SerializeField]
    private LalahTrigger lalahTrigger;
    [SerializeField]
    public bool lalahIsGone;

    [Header("Level 1")]
    [SerializeField]
    private GameObject packageInstruction;
    [SerializeField]
    private GameObject packageInstruction2;
    [SerializeField]
    public bool firstButtonIsTriggered;
    [SerializeField]
    public bool firstButtonIsTriggered2;
    [SerializeField]
    public bool firstButtonIsTriggered3;
    [SerializeField]
    public bool inDropArea;
    [SerializeField]
    public ObjectGrabbable ob;


    private void Awake()
    {
        instance = this;



    }
    private void Start()
    {
        if (GameManager.instance.curSceneName != "TitleScene" && GameManager.instance.player1 != null && GameManager.instance.player2 != null)
        {
            GameManager.instance.Reposition(P1StartPoint, P2StartPoint, P1Rotation, P2Rotation);
        }

        //LVNPC = Lv1;
        LV = lv2;

        //LVNPC.SetActive(false);

        if (GameManager.instance.curSceneName == GameManager.instance.scene1)
        {
            phonePiece.SetActive(false);
            phoneRingText.SetActive(false);
            deliveryText.SetActive(false);
        }
        if (GameManager.instance.p1 != null && GameManager.instance.p2 != null)
        {
            GameManager.instance.p1.withinPackageRange = false;
            GameManager.instance.p2.withinPackageRange = false;
        }

    }

    private void Update()
    {

        if (GameManager.instance.curSceneName == GameManager.instance.scene1)
        {
            HubStart();
            SkipComic();
            SkipDevilDialogue();

            if (GameManager.instance.timesEnterHub == 1)
            {
                SkipLalahDialogue();
                
            }


            if (GameManager.instance.timesEnterHub == 2)
            {
                SkipLalahEndDialogue();
                SkipWeatherDialogue();

            }

            if (GameManager.instance.timesEnterHub == 3)
            {
                SkipWeatherEndDialogue();
            }


        }

        if(GameManager.instance.curSceneName == GameManager.instance.scene3)
        {
            ShowHightlightedDoor();
        }

        if(GameManager.instance.curSceneName == "Level1")
        {
            PackageInstructionControl();
        }

        if(GameManager.instance.curSceneName == "Tutorial")
        {
            SkipTutorialLevelOverview();
        }
    }

    #region Skip Function
    void SkipComic()
    {
        if (Input.GetKey(KeyCode.E) && GameManager.instance.timesEnterHub < 1)
        {
            StopCoroutine(StartComicIntro());
            Comic1.SetActive(false);
            GameManager.instance.UnfreezePlayer();

            phonePiece.SetActive(true);
            phoneRingText.SetActive(true);
        }

        if (GameManager.instance.p1.ReadSkipButton() || GameManager.instance.p2.ReadSkipButton())
        {
            if (GameManager.instance.timesEnterHub < 1 && comicShowed)
            {
                StopCoroutine(StartComicIntro());
                Comic1.SetActive(false);
                GameManager.instance.UnfreezePlayer();

                phonePiece.SetActive(true);
                phoneRingText.SetActive(true);
                comicShowed = false;
            }

        }

    }

    void SkipDevilDialogue()
    {
        if(GameManager.instance.p1.isAnswered || GameManager.instance.p2.isAnswered)
        {
            if(GameManager.instance.timesEnterHub < 1)
            {
                if (GameManager.instance.p1.ReadSkipButton() || GameManager.instance.p2.ReadSkipButton())
                {
                    dR.Stop();
                    radialUI.SetActive(false);
                    dialogueFin = true;
                }
            }

        }
    }

    void SkipTutorialLevelOverview()
    {
        if (GameManager.instance.p1.isAnswered || GameManager.instance.p2.isAnswered)
        {
            if (TutorialCamControl.instance.atStart)
            {
                StartCoroutine(TutorialCamControl.instance.StopMoveCam());
                TutorialCamControl.instance.endTutorial = true;
                
            }
        }
        
    }

    void SkipLalahDialogue()
    {
        if (GameManager.instance.p1.Dialogue3 || GameManager.instance.p2.Dialogue3)
        {
            if (!LalahdialogueEnds)
            {
                radialUI.SetActive(true);
            }
            if (GameManager.instance.p1.ReadSkipButton() || GameManager.instance.p2.ReadSkipButton())
            {
                dR.Stop();
                SwitchCameraToMain();
                radialUI.SetActive(false);
                LalahdialogueEnds = true;
                GameManager.instance.p1.isFreeze = false;
                GameManager.instance.p2.isFreeze = false;
            }
        }
    }

    void SkipLalahEndDialogue()
    {
        if (GameManager.instance.p1.Dialogue3_2 || GameManager.instance.p2.Dialogue3_2)
        {
            if (!LalahdialogueEnds)
            {
                radialUI.SetActive(true);
            }
            if (GameManager.instance.p1.ReadSkipButton() || GameManager.instance.p2.ReadSkipButton())
            {
                dR.Stop();
                SwitchCameraToMain();
                radialUI.SetActive(false);
                LalahdialogueEnds = true;
                LalahLeave();
            }
        }
    }

    void SkipWeatherDialogue()
    {
        if (GameManager.instance.p1.Dialogue1 || GameManager.instance.p2.Dialogue1)
        {
            if (!WeatherdialogueEnds)
            {
                radialUI.SetActive(true);
            }
            if (GameManager.instance.p1.ReadSkipButton() || GameManager.instance.p2.ReadSkipButton())
            {
                dR.Stop();
                SwitchCameraToMain();
                radialUI.SetActive(false);
                WeatherdialogueEnds = true;
                GameManager.instance.p1.isFreeze = false;
                GameManager.instance.p2.isFreeze = false;
            }
        }
    }

    void SkipWeatherEndDialogue()
    {
        if (GameManager.instance.p1.Dialogue1_2 || GameManager.instance.p2.Dialogue1_2)
        {
            if (!WeatherdialogueEnds)
            {
                radialUI.SetActive(true);
            }
            if (GameManager.instance.p1.ReadSkipButton() || GameManager.instance.p2.ReadSkipButton())
            {
                dR.Stop();
                SwitchCameraToMain();
                radialUI.SetActive(false);
                WeatherdialogueEnds = true;
                LalahLeave();
            }
        }
    }

    #endregion

    #region Camera Switching
    public void SwitchCameraToTV()
    {
        MoveCamera(closeShootTV);

    }

    IEnumerator SwitchCamToTutorialLevel()
    {
        SwitchCameraToTV();
        yield return new WaitForSeconds(2f);
        ShowDialogue.TutorialLevel();
        dialogueFin = false;
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

    #endregion

    #region HubStart
    public void StartComic()
    {
        StartCoroutine(StartComicIntro());
        comicShowed = true;
    }

    IEnumerator StartComicIntro()
    {
        Comic1.SetActive(true);
        yield return new WaitForSeconds(28);
        Comic1.SetActive(false);
        GameManager.instance.UnfreezePlayer();
        phonePiece.SetActive(true);
        phoneRingText.SetActive(true);

    }

    void HubStart()
    {
        //Comic
        if (GameManager.instance.firstTimeEnterHub == true)
        {
            StartComic();
            GameManager.instance.firstTimeEnterHub = false;
        }

        //Lalah
        if (GameManager.instance.timesEnterHub == 1)
        {
            GameManager.instance.p1.isFreeze = false;
            GameManager.instance.p2.isFreeze = false;
            Lalah.SetActive(true);
            firstCustomer = true;
            phonePiece.SetActive(false);
        }
        else if (GameManager.instance.timesEnterHub != 1 && GameManager.instance.timesEnterHub != 2)
        {
            Lalah.SetActive(false);
            firstCustomer = false;
        }

        if(GameManager.instance.timesEnterHub == 2 && lalahTrigger.isLeaving == false)
        {
            showPackage = true;
            lalahTrigger.npcArrived = true;
            if (!lalahIsGone)
            {
                
            }
            else
            {
                Lalah.SetActive(false);
            }

        }

        //Weather
        if (GameManager.instance.timesEnterHub == 2 && lalahIsGone && !secondCustomer)
        {
            StartCoroutine(ShowWeather());
            print("Weather showing up");
        }

        //if(GameManager.instance.timesEnterHub == 3)
        //{
        //    Weather.SetActive(true);
        //} 
        //else
        if (GameManager.instance.timesEnterHub != 2 && GameManager.instance.timesEnterHub != 3)
        {
            Weather.SetActive(false);
            secondCustomer = false;
        }

        IEnumerator ShowWeather()
        {
            yield return new WaitForSeconds(2f);
            Weather.SetActive(true);
            secondCustomer = true;
        }

        if (GameManager.instance.timesEnterHub == 3 && NPCTrigger.isLeaving == false)
        {
            showPackage1 = true;
            NPCTrigger.npcArrived = true;
            if (!weatherIsGone)
            {

            }
            else
            {
                Weather.SetActive(false);
            }
        }

        if (GameManager.instance.showWertherInstruction && !WeatherdialogueEnds)
        {
            WertherUI.SetActive(true);
            print("showWertherInstruction" + GameManager.instance.showWertherInstruction);
        }
        else if(!GameManager.instance.showWertherInstruction || WeatherdialogueEnds)
        {
            WertherUI.SetActive(false);
            print("showWertherInstruction" + GameManager.instance.showWertherInstruction);
        }

        if (GameManager.instance.showLalahInstruction && !LalahdialogueEnds)
        {
            LalahUI.SetActive(true);

        }
        else if(LalahdialogueEnds || !GameManager.instance.showLalahInstruction)
        {
            LalahUI.SetActive(false);
        }

        if (GameManager.instance.showMichaelInstruction)
        {
            MichaelUI.SetActive(true);
        }
        else
        {
            MichaelUI.SetActive(false);
        }

        if (GameManager.instance.ShowPhoneInstruction)
        {
            phoneInstruction.SetActive(true);
            if (GameManager.instance.answeredPhone)
            {
                phoneRingText.SetActive(false);
                phonePiece.SetActive(false);
            }
        }
        else
        {
            phoneInstruction.SetActive(false);
        }

        if (dialogueFin)
        {
            StartCoroutine(SwitchCamToTutorialLevel());
            //dialogueFin = false;
        }

        if (WeatherdialogueEnds && !showPackage1)
        {
            normalPackage.SetActive(true);
            showPackage1 = true;
        }
        else if(!WeatherdialogueEnds)
        {
            normalPackage.SetActive(false);
        }

        if (LalahdialogueEnds && !showPackage)
        {
            heavyPackage.SetActive(true);
            showHeavyPackage = true;
        }
        else if (!LalahdialogueEnds)
        {
           heavyPackage.SetActive(false);
        }
        TextControl();
    }

    // delivery area text 
    public void ShowDeliveryText()
    {
        deliveryText.SetActive(true);
        deliveryTextisActivated = true;
    }

    public void CloseDeliveryText()
    {
        deliveryText.SetActive(false);
        deliveryTextisActivated = false;
    }

    public void ShowConfirmDeliveryText()
    {
        ConfirmText.SetActive(true);
        ConfirmTextisActivated = true;
    }

    public void CloseConfirmDeliveryText()
    {
        ConfirmText.SetActive(false);
        ConfirmTextisActivated = false;
    }

    void TextControl()
    {
        if(!p1AtDoor && !p2AtDoor)
        {
            CloseConfirmDeliveryText();
            CloseDeliveryText();
        }
    }

    public void EnableUI()
    {
        radialUI.SetActive(true);
    }


    #endregion

    #region TitlePage

    private void ShowHightlightedDoor()
    {
        if(GameManager.instance.player1 != null && GameManager.instance.player2 != null)
        {
            hightlightedDoor.SetActive(true);
            print("Door");
        }

    }


    #endregion


    #region Level 1

    public void ShowPackageInstruction()
    {
        packageInstruction.SetActive(true);
    }

    public void ShowPackageInstruction2()
    {
        packageInstruction2.SetActive(true);
    }

    public void ClosePackageInstruction()
    {
        packageInstruction.SetActive(false);
    }

    public void ClosePackageInstruction2()
    {
        packageInstruction2.SetActive(false);
    }
    public void PackageInstructionControl()
    {
        if(GameManager.instance.p1.withinPackageRange)
        {
            if (!inDropArea)
            {
                ShowPackageInstruction();
            }

        } 
        else
        {
            ClosePackageInstruction();
        }

        if(!GameManager.instance.p1.withinPackageRange && !GameManager.instance.p2.withinPackageRange)
        {
            ClosePackageInstruction();
            ClosePackageInstruction2();
        }

        if (GameManager.instance.p2.withinPackageRange)
        {
            if (!inDropArea)
            {
                ShowPackageInstruction2();
            }
        }
        else
        {
            ClosePackageInstruction2();
        }

        if(ob.P1TakePackage || ob.P2TakePackage)
        {
            ClosePackageInstruction();
            ClosePackageInstruction2();
        }
    }

    public void TriggerFirstButton()
    {
        firstButtonIsTriggered = true;
    }
    public void TriggerFirstButton2()
    {
        firstButtonIsTriggered2 = true;
    }
    public void TriggerFirstButton3()
    {
        firstButtonIsTriggered3 = true;
    }

    public void NonTriggerFirstButton()
    {
        firstButtonIsTriggered = false;
    }
    public void NonTriggerFirstButton3()
    {
        firstButtonIsTriggered3 = false;
    }

    public void LalahLeave()
    {
        lalahTrigger.dialogueEnd = true;
        lalahIsGone = true;
        GameManager.instance.p1.isFreeze = false;
        GameManager.instance.p2.isFreeze = false;
    }
    #endregion

    #region MVP Level

    public void WeatherLeave()
    {
        NPCTrigger.dialogueEnd = true;
        weatherIsGone = true;
        GameManager.instance.p1.isFreeze = false;
        GameManager.instance.p2.isFreeze = false;
    }
    #endregion


}
