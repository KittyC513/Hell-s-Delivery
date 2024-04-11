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
    public GameObject phoneUI, dialogueBox, nameTag, nameTag1, WertherUI, LalahUI, MichaelUI, nameTagNPC2, nameTagNPC3, TVinstruction;


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
    [SerializeField]
    public bool accept;
    [SerializeField]
    public bool reject;
    [SerializeField]
    public bool accept1;
    [SerializeField]
    public bool reject1;
    [SerializeField]
    private float delayTimer;
    [SerializeField]
    private float delayTimer1;
    [SerializeField]
    private bool secondTimeStarts;
    [SerializeField]
    private AK.Wwise.Event stopComicSFX;
    [SerializeField]
    private GameObject minigameUI;

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
    [SerializeField]
    public GameObject WertherTalkUI;
    [SerializeField]
    private BoxCollider wertherCollider;
    [SerializeField]
    public bool level2Overview;
    [SerializeField]
    public Transform Werther;
    [SerializeField]
    public bool UI2turnOff;
    [SerializeField]
    public Transform overviewCamWerther;
    [SerializeField]
    private GameObject WertherOverviewUI;
    [SerializeField]
    private GameObject WertherOverviewDescriptionUI;
    [SerializeField]
    public bool startLevel2;
    [SerializeField]
    public bool WertherConversationStart;


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
    private GameObject LalahOverviewUI;
    [SerializeField]
    private GameObject LalahOverviewDescriptionUI;
    [SerializeField]
    private BoxCollider lalahCollider;
    [SerializeField]
    public Transform overviewCamLalah;
    [SerializeField]
    public bool level1Overview;
    [SerializeField]
    public bool startLevel1;
    [SerializeField]
    public bool UITurnOff;
    [SerializeField]
    public GameObject LalahTalkUi;
    [SerializeField]
    public bool LalahConversationStart;


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
    private bool notSkipTutorial;
    [SerializeField]
    private bool tutorialUIisShowed;
    [SerializeField]
    private bool skipTutorial1;
    [SerializeField]
    private bool isSkipped;
    [SerializeField]
    private bool isntSkipped;
    [SerializeField]
    public bool canRespawn;


    [Header("Bark")]
    [SerializeField]
    public int multiple;
    [SerializeField]
    public int oriValue;
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
        UITurnOff = true;
        UI2turnOff = true;

        if (GameManager.instance.curSceneName != "TitleScene" && GameManager.instance.player1 != null && GameManager.instance.player2 != null)
        {
            GameManager.instance.Reposition(P1StartPoint, P2StartPoint, P1Rotation, P2Rotation);
        }

        //LVNPC = Lv1;
        LV = lv2;

        //LVNPC.SetActive(false);

        if (GameManager.instance.curSceneName == "HubStart")
        {
            if(!GameManager.instance.LalahRequestWasCompleted && !GameManager.instance.WertherRequestWasCompleted)
            {
                phonePiece.SetActive(false);
                phoneRingText.SetActive(false);
            }

            deliveryText.SetActive(false);
        }
        if (GameManager.instance.p1 != null && GameManager.instance.p2 != null)
        {
            GameManager.instance.p1.withinPackageRange = false;
            GameManager.instance.p2.withinPackageRange = false;
        }

        multiple = 1;
        oriValue = multiple;

        if(GameManager.instance.timesEnterHub >= 2)
        {
            GameManager.instance.p1.isFreeze = false;
            GameManager.instance.p2.isFreeze = false;
        }
    }

    private void Update()
    {
        if (GameManager.instance.curSceneName == GameManager.instance.scene1)
        {
            HubStart();
            SkipComic();
            SkipDevilDialogue();
            SkipChoice();


            if(GameManager.instance.timesEnterHub >= 1)
            {
                nameTag.SetActive(false);
                SkipLalahDialogue();
                ShowLevel1Overview();
                ShowLevel2Overview();
                SkipwertherDialogue();
                SkipLalahEndDialogue();
                SkipwertherDialogue();
                SkipwertherEndDialogue();
            }
        }

        if (GameManager.instance.curSceneName == "HubStart" && GameManager.instance.LalahLeft)
        {
            Lalah.SetActive(false);
        }

        if (GameManager.instance.curSceneName == "HubStart" && GameManager.instance.WertherLeft)
        {
            werther.SetActive(false);
        }

        if(GameManager.instance.timesEnterHub >= 2 && !secondTimeStarts)
        {
            radialUI.SetActive(false);
            GameManager.instance.p1.isFreeze = false;
            GameManager.instance.p2.isFreeze = false;
            secondTimeStarts = true;
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
        }

        if (GameManager.instance.curSceneName == "MVPLevel")
        {
            SkipMVPLevelOverviewCutscene();
            //print("SkipMVPLevel");
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
                stopComicSFX.Post(this.gameObject);
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
            if (!GameManager.instance.LalahRequestWasCompleted && !GameManager.instance.WertherRequestWasCompleted)
            {
                phoneRingText.SetActive(false);
            }


            if (GameManager.instance.timesEnterHub < 1)
            {
                //if (isntSkipped)
                //{
                //    StartCoroutine(SwitchCamToTutorialLevel());
                //}

                if (GameManager.instance.p1.ReadSkipButton() || GameManager.instance.p2.ReadSkipButton())
                {
                    dR.Stop();
                    radialUI.SetActive(false);
                    dialogueFin = true;
                    skipTutorial1 = true;
                    GameManager.instance.p1.isFreeze = true;
                    GameManager.instance.p2.isFreeze = true;
                }


                if (dialogueFin && skipTutorial1 && !isSkipped && !isntSkipped)
                {
                    radialUI.SetActive(false);
                    ShowTutorialSkipUI();

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
            if (!LalahdialogueEnds && LalahConversationStart)
            {
                radialUI.SetActive(true);
                //devilSprite.SetActive(false);
                if (GameManager.instance.p1.ReadSkipButton() || GameManager.instance.p2.ReadSkipButton())
                {
                    if (!GameManager.instance.p1.Dialogue3_2 && !GameManager.instance.p2.Dialogue3_2)
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

            if (LalahdialogueEnds || !LalahConversationStart)
            {
                radialUI.SetActive(false);
            }
        }
    }

    void SkipLalahEndDialogue()
    {
        if (GameManager.instance.p1.Dialogue3_2 || GameManager.instance.p2.Dialogue3_2)
        {
            if (!LalahdialogueEnds && LalahConversationStart)
            {
                radialUI.SetActive(true);
                if (GameManager.instance.p1.ReadSkipButton() || GameManager.instance.p2.ReadSkipButton())
                {
                    dR.Stop();
                    SwitchCameraToMain();
                    radialUI.SetActive(false);
                    //LalahdialogueEnds = true;
                    LalahLeave();
                    
                }
            }

            if (LalahdialogueEnds || !LalahConversationStart)
            {
                radialUI.SetActive(false);
            }
        }
    }

    void SkipwertherDialogue()
    {
        if (GameManager.instance.p1.Dialogue1 || GameManager.instance.p2.Dialogue1)
        {
            if (!wertherdialogueEnds && WertherConversationStart)
            {
                radialUI.SetActive(true);
                if (GameManager.instance.p1.ReadSkipButton() || GameManager.instance.p2.ReadSkipButton())
                {
                    if (!GameManager.instance.p1.Dialogue1_2 && !GameManager.instance.p2.Dialogue1_2)
                    {
                        dR.Stop();
                        SwitchCameraToMain();
                        radialUI.SetActive(false);
                        wertherdialogueEnds = true;
                        GameManager.instance.p1.isFreeze = false;
                        GameManager.instance.p2.isFreeze = false;
                    }

                }
            }

            if (wertherdialogueEnds || !WertherConversationStart)
            {
                radialUI.SetActive(false);
            }
        }
    }

    void SkipwertherEndDialogue()
    {
        if (GameManager.instance.p1.Dialogue1_2 || GameManager.instance.p2.Dialogue1_2)
        {
            if (!wertherdialogueEnds && WertherConversationStart)
            {
                radialUI.SetActive(true);
                if (GameManager.instance.p1.ReadSkipButton() || GameManager.instance.p2.ReadSkipButton())
                {
                    dR.Stop();
                    SwitchCameraToMain();
                    radialUI.SetActive(false);
                    wertherdialogueEnds = true;
                    wertherLeave();
                }
            }

            if (wertherdialogueEnds || !WertherConversationStart)
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
        tutorialSkipUI.SetActive(false);
        SwitchCameraToTV();
        yield return new WaitForSeconds(3f);
        ShowDialogue.TutorialLevel();
        dialogueFin = false;
    }

    public void SwitchCameraToNpc()
    {
        mainCamera.SetActive(false);
        WertherCam.SetActive(true);

        if (overviewCamWerther.gameObject != null)
        {
            overviewCamWerther.gameObject.SetActive(false);
        }
        if (WertherOverviewDescriptionUI != null)
        {
            WertherOverviewDescriptionUI.SetActive(false);
        }


    }

    public void SwitchCameraToNpc2()
    {
        mainCamera.SetActive(false);
        Npc2Cam.SetActive(true);
        if(overviewCamLalah.gameObject != null)
        {
            overviewCamLalah.gameObject.SetActive(false);
        }
        if (LalahOverviewDescriptionUI != null)
        {
            LalahOverviewDescriptionUI.SetActive(false);
        }

    }

    public void SwitchCameraToLalahCam()
    {
        mainCamera.SetActive(false);
        overviewCamLalah.gameObject.SetActive(true);
        Npc2Cam.SetActive(false);
        LalahOverviewDescriptionUI.SetActive(true);

    }

    public void SwitchCameraToWertherCam()
    {
        mainCamera.SetActive(false);
        overviewCamWerther.gameObject.SetActive(true);
        WertherCam.SetActive(false);
        WertherOverviewDescriptionUI.SetActive(true);

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

        print("Switch to Main Cam"); 

        if (overviewCamLalah.gameObject != null)
        {
            overviewCamLalah.gameObject.SetActive(false);
        }

        if(LalahOverviewDescriptionUI != null)
        {
            LalahOverviewDescriptionUI.SetActive(false);
        }

        if(overviewCamWerther.gameObject != null)
        {
            overviewCamWerther.gameObject.SetActive(false);
        }

        if(WertherOverviewDescriptionUI != null)
        {
            WertherOverviewDescriptionUI.SetActive(false);
        }

    }

    public void MoveCamera(Transform newPos)
    {
        float lerpSpeed = 3f;
        mainCam.transform.position = Vector3.Lerp(mainCam.transform.position, newPos.position, Time.deltaTime * lerpSpeed);
        mainCam.transform.rotation = Quaternion.Lerp(mainCam.transform.rotation, newPos.rotation, Time.deltaTime * lerpSpeed);
        //print("Camera");
    }

    public void MoveCameraLalah(Transform newPos)
    {
        float lerpSpeed = 3f;
        Npc2Cam.transform.position = Vector3.Lerp(mainCam.transform.position, newPos.position, Time.deltaTime * lerpSpeed);
        Npc2Cam.transform.rotation = Quaternion.Lerp(mainCam.transform.rotation, newPos.rotation, Time.deltaTime * lerpSpeed);
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
        yield return new WaitForSeconds(24);
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

        if(GameManager.instance.timesEnterHub >= 1)
        {
            tutorialUIisShowed = false;

            if (!lalahIsGone)
            {
                if(!level2Overview && !GameManager.instance.LalahLeft)
                {
                    Lalah.SetActive(true);
                }
                firstCustomer = true;
            }
            else
            {
                Lalah.SetActive(false);
                firstCustomer = false;
            }

            if (!wertherIsGone)
            {
                if (!level1Overview && !GameManager.instance.WertherLeft)
                {
                    werther.SetActive(true);
                }
                secondCustomer = true;

            }
            else
            {
                secondCustomer = false;
                werther.SetActive(false);
            }

        }
        else
        {
            Lalah.SetActive(false);
            firstCustomer = false;
            secondCustomer = false;
            werther.SetActive(false);

        }


        if (GameManager.instance.showWertherInstruction && !wertherdialogueEnds && !GameManager.instance.WertherRequestWasCompleted && !showHeavyPackage)
        {
            if(GameManager.instance.LalahRequestWasCompleted && lalahIsGone)
            {
                if(!level1Overview && !level2Overview)
                {
                    WertherUI.SetActive(true);
                    wertherCollider.enabled = true;
                }
            } 
            else if(!GameManager.instance.LalahRequestWasCompleted && !GameManager.instance.WertherRequestWasCompleted)
            {
                if (!level1Overview && !level2Overview)
                {
                    WertherUI.SetActive(true);
                    wertherCollider.enabled = true;
                }

            }


            //print("showWertherInstruction" + GameManager.instance.showWertherInstruction);
        }
        else if (!GameManager.instance.showWertherInstruction || wertherdialogueEnds || GameManager.instance.WertherRequestWasCompleted || showHeavyPackage)
        {
            WertherUI.SetActive(false);
            wertherCollider.enabled = false;
            //print("showWertherInstruction" + GameManager.instance.showWertherInstruction);
        }

        if (GameManager.instance.showLalahInstruction && !LalahdialogueEnds && !GameManager.instance.LalahRequestWasCompleted && !showPackage1)
        {
            if(GameManager.instance.WertherRequestWasCompleted && wertherIsGone)
            {
                if (!level1Overview && !level2Overview)
                {
                    LalahUI.SetActive(true);
                    lalahCollider.enabled = true;
                }

            } 
            else if (!GameManager.instance.WertherRequestWasCompleted && !GameManager.instance.LalahRequestWasCompleted)
            {
                if (!level1Overview && !level2Overview)
                {
                    LalahUI.SetActive(true);
                    lalahCollider.enabled = true;
                }
            }

        }
        else if (LalahdialogueEnds || !GameManager.instance.showLalahInstruction || GameManager.instance.LalahRequestWasCompleted || showPackage1)
        {
            LalahUI.SetActive(false);
            lalahCollider.enabled = false;
        }


        if(GameManager.instance.showLalahInstruction && GameManager.instance.LalahRequestWasCompleted && !LalahConversationStart)
        {
            LalahTalkUi.SetActive(true);
            
            if (!lalahTrigger.isLeaving)
            {          
                WertherUI.SetActive(false);
                wertherCollider.enabled = false;
            } 
            else
            {
                //LalahTalkUi.SetActive(false);
                WertherUI.SetActive(true);
                wertherCollider.enabled = true;
            }
        }
        else if(!GameManager.instance.showLalahInstruction || !GameManager.instance.LalahRequestWasCompleted || LalahConversationStart)
        {
            LalahTalkUi.SetActive(false);
        }

        if (GameManager.instance.showWertherInstruction && GameManager.instance.WertherRequestWasCompleted && !WertherConversationStart)
        {
            WertherTalkUI.SetActive(true);
            if (!NPCTrigger.isLeaving)
            {
                //WertherTalkUI.SetActive(true);
                LalahUI.SetActive(false);
                lalahCollider.enabled = false;
            }
            else
            {
                //WertherTalkUI.SetActive(false);
                LalahUI.SetActive(true);
                lalahCollider.enabled = true;
            }
        }
        else if (!GameManager.instance.showWertherInstruction || !GameManager.instance.WertherRequestWasCompleted || WertherConversationStart)
        {
            WertherTalkUI.SetActive(false);
        }

        //if (GameManager.instance.showMichaelInstruction)
        //{
        //    MichaelUI.SetActive(true);
        //}
        //else
        //{
        //    MichaelUI.SetActive(false);
        //}

        if (GameManager.instance.ShowPhoneInstruction)
        {
            phoneInstruction.SetActive(true);
            if (GameManager.instance.answeredPhone)
            {
                if(!GameManager.instance.LalahRequestWasCompleted && !GameManager.instance.WertherRequestWasCompleted)
                {
                    phoneRingText.SetActive(false);
                    phonePiece.SetActive(false);
                }

            }
        }
        else
        {
            phoneInstruction.SetActive(false);
        }


        if (wertherdialogueEnds && !showPackage1 && !GameManager.instance.WertherRequestWasCompleted)
        {
            normalPackage.SetActive(true);
            showPackage1 = true;
            
        }
        else if (!wertherdialogueEnds)
        {
            normalPackage.SetActive(false);
        }


        if (!GameManager.instance.LalahRequestWasCompleted && !showHeavyPackage && LalahdialogueEnds )
        {
            heavyPackage.SetActive(true);
            showHeavyPackage = true;
        }
        else if (!LalahdialogueEnds)
        {
            heavyPackage.SetActive(false);
        }

        //if (LalahdialogueEnds)
        //{
        //    GameManager.instance.oriTimesBacktoHub = GameManager.instance.timesEnterHub;
        //} 
        //else if (GameManager.instance.oriTimesBacktoHub == GameManager.instance.timesEnterHub + 1 && !lalahTrigger.isLeaving)
        //{
        //    LalahLeave();
        //}

        TextControl();

        if(GameManager.instance.p1.turnOnTV || GameManager.instance.p2.turnOnTV)
        {
            if (boxingMinigame.instance.isboxing)
            {
                minigameUI.SetActive(false);
                GameManager.instance.p1.isFreeze = false;
                GameManager.instance.p2.isFreeze = false;
                boxingMinigame.instance.boxingCanvas.SetActive(true);
            }
            else
            {
                minigameUI.SetActive(true);
                GameManager.instance.p1.isFreeze = true;
                GameManager.instance.p2.isFreeze = true;
                boxingMinigame.instance.boxingCanvas.SetActive(false);
            }

        }
        else if (!GameManager.instance.p1.turnOnTV && !GameManager.instance.p2.turnOnTV)
        {
            minigameUI.SetActive(false);
            GameManager.instance.p1.isFreeze = false;
            GameManager.instance.p2.isFreeze = false;
            boxingMinigame.instance.boxingCanvas.SetActive(false);

        }

        if (GameManager.instance.showTVInstruction)
        {
            TVinstruction.SetActive(true);
        }
        else
        {
            TVinstruction.SetActive(false);
        }

        void CompleteLevel1()
        {
            if (GameManager.instance.LalahRequestWasCompleted)
            {
                phonePiece.SetActive(true);
                phoneRingText.SetActive(true);
            }
        }

        void CompleteLevel2()
        {
            if (GameManager.instance.WertherRequestWasCompleted)
            {
                phonePiece.SetActive(true);
                phoneRingText.SetActive(true);
            }
        }
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

    #region Lalah Event
    public void ShowLevel1Overview()
    {
        if (accept)
        {
            GameManager.instance.p1.isFreeze = false;
            GameManager.instance.p2.isFreeze = false;
        }

        if (accept && !LalahdialogueEnds)
        {
            werther.SetActive(true);
            SwitchCameraToNpc2();
            LalahOverviewUI.SetActive(false);
            
        }

        if (reject && !UITurnOff)
        {
            StartCoroutine(TurnOffUI());
        }


        if (level1Overview)
        {
            if (!LalahdialogueEnds && !wertherdialogueEnds)
            {
                if (!accept && !reject)
                {
                    werther.SetActive(false);
                    SwitchCameraToLalahCam();
                    //MoveCameraLalah(overviewCamLalah);
                    //yield return new WaitForSeconds(1f);
                    LalahOverviewUI.SetActive(true);
                    

                    LalahUI.SetActive(false);
                    lalahCollider.enabled = false;
                    UITurnOff = false;


                }

                if(delayTimer <= 0.2f)
                {
                    delayTimer += Time.deltaTime;
                }


                if (GameManager.instance.p1.ReadPushButton() || GameManager.instance.p2.ReadPushButton())
                {
                    if (!accept && delayTimer > 0.2f && !reject)
                    {
                        accept = true;
                        startLevel1 = true;
                        GameManager.instance.acceptLalahOrder = true;
                    }


                }

                if (GameManager.instance.p1.ReadActionButton() || GameManager.instance.p2.ReadActionButton())
                {
                    if (!reject && delayTimer > 0.2f && !accept)
                    {
                        print("Reject"); 
                        reject = true;
                    }
                }
            }
            //StartCoroutine(CameraSwitchLalah());


        }
        else
        {
            delayTimer = 0;
            accept = false;
            reject = false;
        
        }

    }

    IEnumerator TurnOffUI()
    {

        LalahOverviewUI.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        if (!GameManager.instance.WertherLeft)
        {
            werther.SetActive(true);
        }
        SwitchCameraToMain();

        GameManager.instance.p1.isFreeze = false;
        GameManager.instance.p2.isFreeze = false; 
        level1Overview = false;
        UITurnOff = true;

    }

    //IEnumerator CameraSwitchLalah()
    //{
    //    GameManager.instance.p1.isFreeze = true;
    //    GameManager.instance.p1.isFreeze = true;
    //    werther.SetActive(false);
    //    SwitchCameraToNpc2();
    //    yield return new WaitForSeconds(0.5f);
    //    //MoveCameraLalah(overviewCamLalah);
    //    //yield return new WaitForSeconds(1f);
    //    //LalahOverviewUI.SetActive(true);
    //    LalahOverviewDescriptionUI.SetActive(true);
    //    //level1Overview = true;
    //}

    #endregion

    #region Werther Event
    public void ShowLevel2Overview()
    {
        if (accept1)
        {
            GameManager.instance.p1.isFreeze = false;
            GameManager.instance.p2.isFreeze = false;
        }

        if (accept1 && !wertherdialogueEnds)
        {
            Lalah.SetActive(true);
            SwitchCameraToNpc();
            WertherOverviewUI.SetActive(false);

        }

        if (reject1 && !UI2turnOff)
        {
            StartCoroutine(TurnOffUIWerther());
        }


        if (level2Overview)
        {
            if (!LalahdialogueEnds && !wertherdialogueEnds)
            {
                if (!accept1 && !reject1)
                {
                    Lalah.SetActive(false);
                    SwitchCameraToWertherCam();
                    //MoveCameraLalah(overviewCamLalah);
                    //yield return new WaitForSeconds(1f);
                    WertherOverviewUI.SetActive(true);


                    WertherUI.SetActive(false);
                    wertherCollider.enabled = false;
                    UI2turnOff = false;


                }

                if (delayTimer1 <= 0.2f)
                {
                    delayTimer1 += Time.deltaTime;
                }


                if (GameManager.instance.p1.ReadPushButton() || GameManager.instance.p2.ReadPushButton())
                {
                    if (!accept1 && delayTimer1 > 0.2f && !reject1)
                    {
                        accept1 = true;
                        startLevel2 = true;
                        GameManager.instance.accepWertherOrder = true;
                    }


                }

                if (GameManager.instance.p1.ReadActionButton() || GameManager.instance.p2.ReadActionButton())
                {
                    if (!reject1 && delayTimer1 > 0.2f && !accept1)
                    {
                        print("Reject");
                        reject1 = true;
                    }
                }
            }
            //StartCoroutine(CameraSwitchLalah());


        }
        else
        {
            delayTimer1 = 0;
            accept1 = false;
            reject1 = false;
        }

    }

    IEnumerator TurnOffUIWerther()
    {
        if (!GameManager.instance.LalahLeft)
        {
            Lalah.SetActive(true);
        }
        WertherOverviewUI.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        SwitchCameraToMain();
        GameManager.instance.p1.isFreeze = false;
        GameManager.instance.p2.isFreeze = false;
        level2Overview = false;
        UI2turnOff = true;

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
        nameTag.SetActive(false);
        radialUI.SetActive(false);
        phoneRingText.SetActive(false);

        if (!isSkipped && !isntSkipped)
        {
            tutorialSkipUI.SetActive(true);
            tutorialUIisShowed = true;

        }

    }

    public void SkipChoice()
    {
        if (tutorialUIisShowed)
        {
            GameManager.instance.p1.isFreeze = true;
            GameManager.instance.p2.isFreeze = true;

            if (GameManager.instance.p1.ReadPushButton() || GameManager.instance.p2.ReadPushButton())
            {
                if (!skipTutorial)
                {
                    GameManager.instance.timesEnterHub += 1;
                    GameManager.instance.changeSceneTimes += 1;
                    skipTutorial = true;

                }


            }

            if (GameManager.instance.p1.ReadActionButton() || GameManager.instance.p2.ReadActionButton())
            {
                notSkipTutorial = true;
            }

            if (notSkipTutorial)
            {
                StartCoroutine(SwitchCamToTutorialLevel());
            }

            if (skipTutorial)
            {
                tutorialSkipUI.SetActive(false);
                GameManager.instance.p1.isFreeze = false;
                GameManager.instance.p2.isFreeze = false;
            }


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

    public void TurnOnJumpOverUI()
    {
        GameManager.instance.p1.JumpOverIntruction.SetActive(true);
    }
    public void TurnOffJumpOverUI()
    {
        GameManager.instance.p1.JumpOverIntruction.SetActive(false);
    }
    public void LalahLeave()
    {
        lalahTrigger.dialogueEnd = true;
        lalahIsGone = true;
        GameManager.instance.p1.isFreeze = false;
        GameManager.instance.p2.isFreeze = false;
        LalahConversationStart = false;
    }
    #endregion

    #region MVP Level

    public void wertherLeave()
    {
        NPCTrigger.dialogueEnd = true;
        wertherIsGone = true;
        SwitchCameraToMain();
        GameManager.instance.p1.isFreeze = false;
        GameManager.instance.p2.isFreeze = false;
        WertherConversationStart = false;
        //TurnOnCanvas();
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
