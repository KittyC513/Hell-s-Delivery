using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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


    [Header("werther Event")]
    [SerializeField]
    private GameObject werther;
    [SerializeField]
    public bool secondCustomer;
    [SerializeField]
    public bool wertherdialogueEnds;
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
    public bool wertherIsGone;


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
    [SerializeField]
    private GameObject devilSprite;

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
    [SerializeField]
    public DialogueRunner dRP1, dRP2, drAll;
    [SerializeField]
    public bool p1isKilling, p2isKilling, p1IsSaving, p2IsSaving;
    [SerializeField]
    public bool p1isInZone1, p2isInZone1;

    [Header("Tutorial")]
    [SerializeField]
    public bool packageDialogueEnd, packageDialogueStart;
    [SerializeField]
    public GameObject packageTutorial;
    [SerializeField]
    private GameObject tutorialSkipUI;
    [SerializeField]
    private bool skipTutorial;
    [SerializeField]
    private bool tutorialUIisShowed;
    [SerializeField]
    private bool skipTutorial1;

    [Header("Bark")]
    [SerializeField]
    public int multiple;
    [SerializeField]
    public int oriValue;

    [Header("Bark")]
    [SerializeField]
    public GameObject endCanvas;
    [SerializeField]
    public Animator endCanvasAnim;
    //public bool p1BarkTriggered;
    //public bool p2BarkTriggered;
    public bool play1WithPackageDialogue;
    public bool play2WithPackageDialogue;
    public bool play1WithoutPackageDialogue;
    public bool play2WithoutPackageDialogue;

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

        multiple = 1;
        oriValue = multiple;

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
                SkipwertherDialogue();

            }

            if (GameManager.instance.timesEnterHub == 3)
            {
                SkipwertherEndDialogue();
            }


        }

        if (GameManager.instance.curSceneName == GameManager.instance.scene3)
        {
            ShowHightlightedDoor();
        }

        if (GameManager.instance.curSceneName == "Level1")
        {
            PackageInstructionControl();
            SkipLevel1OverviewCutScene();
            DetectPackgeScore();
        }

        if (GameManager.instance.curSceneName == "Tutorial")
        {
            SkipTutorialLevelOverview();
            //SkipChoice();
        }

        if (GameManager.instance.curSceneName == "MVPLevel")
        {
            SkipMVPLevelOverviewCutscene();
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
        if (GameManager.instance.p1.isAnswered || GameManager.instance.p2.isAnswered)
        {
            
            if (GameManager.instance.timesEnterHub < 1)
            {
                if (GameManager.instance.p1.ReadSkipButton() || GameManager.instance.p2.ReadSkipButton())
                {
                    dR.Stop();
                    radialUI.SetActive(false);
                    dialogueFin = true;
                    skipTutorial1 = true;

                }

                if (dialogueFin && skipTutorial1)
                {
                    radialUI.SetActive(false);
                    GameManager.instance.changeSceneTimes += 2;
                    GameManager.instance.timesEnterHub += 1;
                    skipTutorial1 = false;
                }
            }

        }


    }

    void SkipTutorialLevelOverview()
    {
        if (!TutorialCamControl.instance.endTutorial)
        {
            radialUI.SetActive(true);
            if (!TutorialCamControl.instance.cutsceneIsCompleted)
            {
                if (GameManager.instance.p1.ReadSkipButton() || GameManager.instance.p2.ReadSkipButton())
                {
                    StartCoroutine(TutorialCamControl.instance.StopMoveCamStart1());
                    TutorialCamControl.instance.endTutorial = true;
                    radialUI.SetActive(false);
                }
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
                //devilSprite.SetActive(false);

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
            if (LalahdialogueEnds)
            {
                radialUI.SetActive(false);
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
            if (LalahdialogueEnds)
            {
                radialUI.SetActive(false);
            }
        }
    }

    void SkipwertherDialogue()
    {
        if (GameManager.instance.p1.Dialogue1 || GameManager.instance.p2.Dialogue1)
        {
            if (!wertherdialogueEnds)
            {
                radialUI.SetActive(true);
            }
            if (GameManager.instance.p1.ReadSkipButton() || GameManager.instance.p2.ReadSkipButton())
            {
                dR.Stop();
                SwitchCameraToMain();
                radialUI.SetActive(false);
                wertherdialogueEnds = true;
                GameManager.instance.p1.isFreeze = false;
                GameManager.instance.p2.isFreeze = false;
            }
            if (wertherdialogueEnds)
            {
                radialUI.SetActive(false);
            }
        }
    }

    void SkipwertherEndDialogue()
    {
        if (GameManager.instance.p1.Dialogue1_2 || GameManager.instance.p2.Dialogue1_2)
        {
            if (!wertherdialogueEnds)
            {
                radialUI.SetActive(true);
            }
            if (GameManager.instance.p1.ReadSkipButton() || GameManager.instance.p2.ReadSkipButton())
            {
                dR.Stop();
                SwitchCameraToMain();
                radialUI.SetActive(false);
                wertherdialogueEnds = true;
                wertherLeave();
            }
            if (wertherdialogueEnds)
            {
                radialUI.SetActive(false);
            }
        }
    }

    void SkipLevel1OverviewCutScene()
    {
        if (!Level1CamControl.instance.cutsceneIsCompleted)
        {
            radialUI.SetActive(true);

            if (GameManager.instance.p1.ReadSkipButton() || GameManager.instance.p2.ReadSkipButton())
            {
                StartCoroutine(Level1CamControl.instance.StopMoveCam1());
                //TutorialCamControl.instance.endTutorial = true;
                radialUI.SetActive(false);       
            }
        }
        if (Level1CamControl.instance.cutsceneIsCompleted)
        {
            radialUI.SetActive(false);
        }
    }

    void SkipMVPLevelOverviewCutscene()
    {
        if (!Level1CamControl.instance.cutsceneIsCompleted)
        {
            radialUI.SetActive(true);

            if (GameManager.instance.p1.ReadSkipButton() || GameManager.instance.p2.ReadSkipButton())
            {
                StartCoroutine(Level1CamControl.instance.StopMoveCam1());
                //TutorialCamControl.instance.endTutorial = true;
                radialUI.SetActive(false);
            }
        }
        if (Level1CamControl.instance.cutsceneIsCompleted)
        {
            radialUI.SetActive(false);
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

        if (GameManager.instance.timesEnterHub >= 1)
        {
            phonePiece.SetActive(false);
        }

        //Lalah
        if (GameManager.instance.timesEnterHub == 1)
        {
            GameManager.instance.p1.isFreeze = false;
            GameManager.instance.p2.isFreeze = false;
            Lalah.SetActive(true);

            firstCustomer = true;
            
        }
        else if (GameManager.instance.timesEnterHub != 1 && GameManager.instance.timesEnterHub != 2)
        {
            Lalah.SetActive(false);
            firstCustomer = false;
        }

        if (GameManager.instance.timesEnterHub == 2 && lalahTrigger.isLeaving == false)
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

        //werther
        if (GameManager.instance.timesEnterHub == 2 && lalahIsGone && !secondCustomer)
        {
            StartCoroutine(Showwerther());
            print("werther showing up");
        }

        //if(GameManager.instance.timesEnterHub == 3)
        //{
        //    werther.SetActive(true);
        //} 
        //else
        if (GameManager.instance.timesEnterHub != 2 && GameManager.instance.timesEnterHub != 3)
        {
            werther.SetActive(false);
            secondCustomer = false;
        }

        IEnumerator Showwerther()
        {
            yield return new WaitForSeconds(2f);
            werther.SetActive(true);
            secondCustomer = true;
        }

        if (GameManager.instance.timesEnterHub == 3 && NPCTrigger.isLeaving == false)
        {
            showPackage1 = true;
            NPCTrigger.npcArrived = true;
            if (!wertherIsGone)
            {

            }
            else
            {
                werther.SetActive(false);
            }
        }

        //Skip Tutorial
        //if (dialogueFin)
        //{
        //    StartCoroutine(SwitchCamToTutorialLevel());
        //    //dialogueFin = false;
        //}

        if (GameManager.instance.showWertherInstruction && !wertherdialogueEnds)
        {
            WertherUI.SetActive(true);
            //print("showWertherInstruction" + GameManager.instance.showWertherInstruction);
        }
        else if (!GameManager.instance.showWertherInstruction || wertherdialogueEnds)
        {
            WertherUI.SetActive(false);
            //print("showWertherInstruction" + GameManager.instance.showWertherInstruction);
        }

        if (GameManager.instance.showLalahInstruction && !LalahdialogueEnds)
        {
            LalahUI.SetActive(true);

        }
        else if (LalahdialogueEnds || !GameManager.instance.showLalahInstruction)
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


        if (wertherdialogueEnds && !showPackage1)
        {
            normalPackage.SetActive(true);
            showPackage1 = true;
        }
        else if (!wertherdialogueEnds)
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
        if (!p1AtDoor && !p2AtDoor)
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
        if (GameManager.instance.player1 != null && GameManager.instance.player2 != null)
        {
            hightlightedDoor.SetActive(true);
            //print("Door");
        }

    }


    #endregion

    #region Tutorial
    public void PackageDilaogueEnds()
    {
        packageDialogueEnd = true;
        packageDialogueStart = false;
    }

    public void PackageDilaogueStarts()
    {
        packageDialogueStart= true;

    }

    public void ShowTutorialSkipUI()
    {
        tutorialSkipUI.SetActive(true);
        tutorialUIisShowed = true;
    }

    public void SkipChoice()
    {
        if (tutorialUIisShowed)
        {
            if (GameManager.instance.p1.ReadPushButton() || GameManager.instance.p2.ReadPushButton())
            {
                skipTutorial = true;
                GameManager.instance.timesEnterHub += 1;
                GameManager.instance.changeSceneTimes += 1;
            }

            if (GameManager.instance.p1.ReadActionButton() || GameManager.instance.p2.ReadActionButton())
            {
                skipTutorial = false;

                if (dialogueFin && !skipTutorial)
                {
                    StartCoroutine(SwitchCamToTutorialLevel());
                    //dialogueFin = false;
                }
            }

        }



    }

    IEnumerator NPCShowUp()
    {
        yield return new WaitForSeconds(0.3f);
        tutorialSkipUI.SetActive(false);
        tutorialUIisShowed = false;

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

    public void wertherLeave()
    {
        NPCTrigger.dialogueEnd = true;
        wertherIsGone = true;
        GameManager.instance.p1.isFreeze = false;
        GameManager.instance.p2.isFreeze = false;
        TurnOnCanvas();
    }
    #endregion

    #region Bark
    void DetectPackgeScore()
    {
        if (ScoreCount.instance != null && ScoreCount.instance != null)
        {
            if (ScoreCount.instance.lvlData.p1Deliver >= 120 * multiple || ScoreCount.instance.lvlData.p2Deliver >= 120 * multiple)
            {
                if (!SceneControl.instance.p1isKilling && !SceneControl.instance.p2isKilling && !SceneControl.instance.p1IsSaving && !SceneControl.instance.p2IsSaving)
                {
                    {
                        if (ScoreCount.instance.lvlData.p1Deliver >= 120 * multiple)
                        {
                            if(!play1WithPackageDialogue && !play2WithoutPackageDialogue)
                            {
                                int i = Random.Range(1, 10);
                                print("i = " + i);
                                if (i <= 5)
                                {
                                    //p1BarkTriggered = true;
                                    //PlayRandomPackageDialogue1();
                                    multiple += 1;
                                    play1WithPackageDialogue = true;

                                }
                                else
                                {
                                    //p2BarkTriggered = true;
                                    //PlayRandomPackageDialogue4();
                                    oriValue = multiple;
                                    multiple += 1;
                                    play2WithoutPackageDialogue = true;
                                }
                            }

                         }                        
                         else if (ScoreCount.instance.lvlData.p2Deliver >= 120 * multiple)
                         {
                            if(!play2WithPackageDialogue &&!play1WithoutPackageDialogue)
                            {
                                int i = Random.RandomRange(1, 10);
                                print("i = " + i);

                                if (i <= 5)
                                {
                                    //PlayRandomPackageDialogue2();
                                    //oriValue = multiple;
                                    //p2BarkTriggered = true;
                                    multiple += 1;
                                    play2WithPackageDialogue = true;
                                }
                                else
                                {
                                    //PlayRandomPackageDialogue3();
                                    //oriValue = multiple;
                                    //p1BarkTriggered = true;
                                    multiple += 1;
                                    play1WithoutPackageDialogue = true;
                                }
                            }
                            
                        }

                    }
                }
            }

        }
    }

    #endregion


    #region Quit
    public void TurnOnCanvas()
    {
        endCanvas.SetActive(true);
        endCanvasAnim.SetTrigger("End");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    #endregion
}
