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
    public GameObject phonePiece;
    [SerializeField]
    public GameObject deliveryText;

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
        }

        if (Input.GetKey(KeyCode.E) && GameManager.instance.timesEnterHub < 1)
        {
            StopCoroutine(StartComicIntro());
            Comic1.SetActive(false);
            GameManager.instance.UnfreezePlayer();

            phonePiece.SetActive(true);
            phoneRingText.SetActive(true);
        }

        if (GameManager.instance.p1.ReadSkipButton()|| GameManager.instance.p2.ReadSkipButton())
        {
            if(GameManager.instance.timesEnterHub < 1)
            {
                StopCoroutine(StartComicIntro());
                Comic1.SetActive(false);
                GameManager.instance.UnfreezePlayer();

                phonePiece.SetActive(true);
                phoneRingText.SetActive(true);
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
        yield return new WaitForSeconds(28);
        Comic1.SetActive(false);
        GameManager.instance.UnfreezePlayer();
        phonePiece.SetActive(true);
        phoneRingText.SetActive(true);

    }



    void HubStart()
    {

        if (GameManager.instance.firstTimeEnterHub == true)
        {
            StartComic();
            GameManager.instance.firstTimeEnterHub = false;
        }

        if (GameManager.instance.timesEnterHub == 1)
        {
            GameManager.instance.p1.isFreeze = false;
            GameManager.instance.p2.isFreeze = false;
            Lalah.SetActive(true);
            firstCustomer = true;
            phonePiece.SetActive(false);
        }
        else
        {
            Lalah.SetActive(false);
            firstCustomer = false;
        }

        if (GameManager.instance.showWertherInstruction && !WeatherdialogueEnds)
        {
            WertherUI.SetActive(true);
        }
        else if(WeatherdialogueEnds && !GameManager.instance.showWertherInstruction)
        {
            WertherUI.SetActive(false);
        }

        if (GameManager.instance.showLalahInstruction && !LalahdialogueEnds)
        {
            LalahUI.SetActive(true);
            print("showLalahInstruction" + GameManager.instance.showLalahInstruction);
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
            SwitchCameraToTV();
            //dialogueFin = false;
        }

        if (WeatherdialogueEnds && !showPackage)
        {
            normalPackage.SetActive(true);
            showPackage = true;
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
    #endregion




}
