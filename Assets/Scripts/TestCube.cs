
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using UnityEngine.ProBuilder.Shapes;
using System.Runtime.CompilerServices;
using UnityEngine.SceneManagement;
using Yarn.Unity;
using Unity.VisualScripting;
using TMPro;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;
using Yarn;
using static UnityEngine.UI.Image;
using Image = UnityEngine.UI.Image;

public class TestCube : MonoBehaviour
{
    GameObject[] objectsInScene;
    [SerializeField]
    public DialogueRunner dR;
    [SerializeField]
    public LineView lineView;
    public bool sceneChange;
    [SerializeField]
    private InputActionReference continueControl;

    Vector2 i_movement;
    Vector3 movement;
    float moveSpeed = 10f;

    [SerializeField] private InputActionAsset inputAsset;
    [SerializeField] private InputActionMap player, dialogue, pause;
    [SerializeField] public InputAction move, dash, jump, parachute, cancelParachute, triggerButton, pull, close, push, skip, skipTrigger,pushRelease;
    [SerializeField] public bool isPicking;

    private bool isOnCircle;
    private GameObject activeCircle;

    [SerializeField] private bool useNewMovement = false;
    private CharacterControl charController;

    [SerializeField]
    private float movementForce = 1f;

    private Vector3 forceDirection = Vector3.zero;

    [SerializeField]
    public Animator playerAnimator;
    [SerializeField]
    public Animator playerAnimator2;

    private Rigidbody rb;
    [SerializeField]
    private float maxSpeed = 5f;
    [SerializeField]
    private float pullSpeed = 2f;
    [SerializeField]
    private float walkSpeed = 4f;
    [SerializeField]
    private float runSpeed = 9f;
    [SerializeField]
    private float gliderSpeed = 2.5f;
    [SerializeField]
    private float currentSpeed;

    private bool canParachute = false;


    private Vector3 faceDir;
    [SerializeField]
    private Transform playerDir;

    [SerializeField]
    private float timeToRun = 0.16f;
    [SerializeField]
    private float timeToWalk = 0.1f;
    [SerializeField]
    private float timeToGlide = 1f;
    [SerializeField]
    private float timeToPull = 1.5f;
    [SerializeField]
    private float timeToDash = 0.1f;
    [SerializeField]
    private float timeToZero = 0.083f;

    [SerializeField]
    private GameObject shadowRenderer;

    [SerializeField]
    private float jumpForce = 5f;
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    public bool isGrounded;
    [SerializeField]
    private float groundCheckRadius = 0.33f;
    [SerializeField]
    private float groundCheckDist = 0.75f;
    [SerializeField]
    LayerMask groundLayer;
    [SerializeField]
    private bool isRunning;


    [SerializeField]
    public Camera playerCamera;
    [SerializeField]
    Camera mainCam;
    [SerializeField]
    public CinemachineFreeLook thirdPersonCam;
    private Camera movementCamera;


    [SerializeField]
    private Transform playerPos;
    [SerializeField]
    public Transform itemContainer;
    [SerializeField]
    private Transform itemContainer1;
    [SerializeField]
    public ObjectGrabbable objectGrabbable;
    [SerializeField]
    public bool slotFull;

    [SerializeField]
    GameManager gM;
    [SerializeField]
    public string tagToFindCam = "Camera";
    Transform cam;
    [SerializeField]
    public bool camTurnoff;
    Camera cameraComponent;
    [SerializeField]
    string curSceneName;
    string scene1 = "HubStart";
    string scene2 = "PrototypeLevel";
    string scene3 = "HubEnd";
    string scene4 = "ParticleTesting";
    string scene5 = "MVPLevel";
    string scene6 = "TitleScene";
    string scene7 = "Tutorial";
    string scene8 = "ScoreCards";
    string scene9 = "Level1";
    string scene10 = "Level3";
    [SerializeField]
    bool withinDialogueRange;
    [SerializeField]
    bool conversationStart;
    bool hubStart, hubEnd;

    [Header("Freeze")]
    [SerializeField]
    public bool isFreeze;

    [SerializeField]
    UnityEngine.SceneManagement.Scene currentScene;
    [SerializeField]
    private GameObject playerObj;
    [SerializeField]
    private float jumpSpeed;
    [SerializeField]
    private bool isJumping;
    [SerializeField]
    private float jumpDeaccel = 12f;
    [SerializeField]
    private float minJumpForce = 6;
    [SerializeField]
    private float maxFall = -35;
    [SerializeField]
    bool isWalking;
    bool isInAir = false;
    private bool canJump;
    [SerializeField]
    private float jumpButtonGracePeriod;

    [SerializeField]
    private float? lastGroundedTime;
    [SerializeField]
    private float? jumpButtonPressedTime;


    [SerializeField]
    public bool isPlayer1;
    [SerializeField]
    public bool isPlayer2;


    [Header("Pick / Drop")]
    [SerializeField]
    private GameObject package;
    [SerializeField]
    private float pickDistance;
    [SerializeField]
    private RaycastHit raycastHit;
    [SerializeField]
    private bool isCast;
    [SerializeField]
    private float pickDistanceHeavy;
    [SerializeField]
    private float pickRadiusHeavy;
    [SerializeField]
    private LayerMask pickableMask;
    [SerializeField]
    public bool withinPackageRange;

    [SerializeField]
    public RespawnControl rC;
    [SerializeField]
    private float raycastDistance = 5.0f;
    [SerializeField]
    bool isCarrying;
    [SerializeField]
    private Trigger tG;
    [SerializeField]
    private GameObject trigger;
    [SerializeField]
    private RespawnControl p2rc, p1rc;

    [Header("Glide")]
    [SerializeField]
    private float parachuteSpeed = -5f;
    [SerializeField]
    private bool isGliding;
    [SerializeField]
    private int numOfButtonPressed;
    [SerializeField]
    private GameObject parachuteObj;
    [SerializeField]
    public bool inParachuteArea;
    [SerializeField]
    private GameObject parachuteInstruction;


    [Header("Push")]
    [SerializeField]
    public float pushCd;
    [SerializeField]
    public float pushTimer;
    [SerializeField]
    private float pushForce;
    [SerializeField]
    private float oriPushForce;
    [SerializeField]
    private LayerMask pushMask;
    [SerializeField]
    GameManager gameManager;
    [SerializeField]
    private Rigidbody otherRB;
    [SerializeField]
    private float pushDistance;
    [SerializeField]
    private float pushMultiply;
    [SerializeField]
    private float lerpSpeed;
    [SerializeField]
    public bool withinPushingRange;
    [SerializeField]
    private float slideTime;
    [SerializeField]
    public Animator p1Anim;
    [SerializeField]
    public Animator p2Anim;
    [SerializeField]
    public bool p1Steal;
    [SerializeField]
    public bool p2Steal;
    [SerializeField]
    public bool pushStartTimer;
    [SerializeField]
    private float pushDuration; 
    [SerializeField]
    private float pushHoldDuration;
    [SerializeField]
    private float pushHoldTime;
    [SerializeField]
    public bool holdPush;
    [SerializeField]
    private GameObject p1Particle;
    [SerializeField]
    private GameObject p2Particle;
    [SerializeField]
    public bool pushIsIntervinedP1;
    [SerializeField]
    public bool pushIsIntervinedP2;
    [SerializeField]
    private GameObject pushInstruction;

    [Header("Interact")]
    [SerializeField]
    private float interactDistance;
    [SerializeField]
    private float interactRadius;
    [SerializeField]
    private LayerMask interactableMask;
    [SerializeField]
    public bool withinTVRange;
    [SerializeField]
    public bool onTv;
    [SerializeField]
    public bool turnOnTV;

    [SerializeField]
    private LayerMask NPCLayer;
    [SerializeField]
    public bool withinNPCsRange;
    [SerializeField]
    public bool withinNPC2Range;
    [SerializeField]
    public bool withinNPC3Range;
    [SerializeField]
    public bool isTalking;
    [SerializeField]
    public bool interactWithNpc;
    [SerializeField]
    public float doorDistance;

    [SerializeField]
    private LayerMask postEnter;
    [SerializeField]
    public bool withinEntranceRange;
    [SerializeField]
    public bool isEntered;

    [SerializeField]
    private float pushButtonGracePeriod;


    private float? lastColliderTime;
    private float? pushButtonPressedTime;
    Vector3 forceDir;




    [SerializeField]
    public bool p1pushed;
    [SerializeField]
    public bool p2pushed;

    [SerializeField]
    GameObject noisy1;
    [SerializeField]
    GameObject noisy2;
    [SerializeField]
    GameObject circle1;
    [SerializeField]
    GameObject circle2;
    [SerializeField]
    private GameObject titleCanvas;
    [SerializeField]
    TMP_Text titleText;
    [SerializeField]
    bool titleDisplayed;

    float horizontalVelocity;

    [Header("Indicator")]
    [SerializeField]
    public GameObject p1Indicator;
    [SerializeField]
    public GameObject p2Indicator;

    [Header("Camera Control")]
    [SerializeField]
    private GameObject Cam1;
    [SerializeField]
    private GameObject Cam2;

    [Header("Player Model")]
    [SerializeField]
    private GameObject model1;
    [SerializeField]
    private float runThreshold;

    [SerializeField]
    private GameObject model2;

    [SerializeField]
    private bool NPCInteracting;
    [SerializeField]
    private bool NPC2Interacting;
    [SerializeField]
    private bool NPC3Interacting;
    [SerializeField]
    public bool Dialogue1;
    [SerializeField]
    public bool Dialogue1_2;
    [SerializeField]
    private bool Dialogue2;
    [SerializeField]
    public bool Dialogue3;
    [SerializeField]
    public bool Dialogue3_2;
    [SerializeField]
    private bool Dialogue4;

    [SerializeField]
    private GameObject selectNPC;
    [SerializeField]
    private bool isDropped;

    [Header("Camera Control")]
    [SerializeField]
    private bool switchPuzzleCam, switchPuzzleCamP2, switchPuzzle2CamL, switchPuzzle2CamR, switchPuzzle2CamLP2, switchPuzzle2CamRP2;


    //public enum CameraStyle
    //{
    //    Basic,
    //    Combat,
    //    Topdown
    //}

    [Space, Header("Wwise Stuff")]
    [SerializeField] private float footstepRate = 500;
    private bool shouldStep = true;
    private float lastStepTime = 0;

    [SerializeField] private string groundMaterial = "nothing";

    private PlayerSoundbank playerSounds;
    private AK.Wwise.Event footstepEvent;
    private AK.Wwise.Event landEvent;
    private AK.Wwise.Event jumpEvent;
    private AK.Wwise.Event dieEvent;
    private AK.Wwise.Event parachuteOpenEvent;
    private AK.Wwise.Event glideEvent;

    private bool shouldPlayGeiser = false;
    private bool isOnGeiser = false;

    [Header("Geiser")]
    public float geiserForce;
    
    
    [SerializeField]
    private bool wasFoundLV;

    [Header("Bounce")]
    [SerializeField]
    private float bounceForce;


    [Header("Pull/Push")]
    [SerializeField]
    private float pullItemForce, pushItemForce;
    [SerializeField]
    private float PRange;
    [SerializeField]
    public GameObject targetObject;
    [SerializeField]
    private Transform PPosition;
    [SerializeField]
    private LayerMask moveableLayer;
    [SerializeField]
    private Rigidbody targetRigid;
    [SerializeField]
    private bool isPulling;
    [SerializeField]
    Vector3 newPosition;
    [SerializeField]
    private bool isCameraLocked;
    [SerializeField]
    private float pullingSpeed;

    [Header("Detect Direction from player to object")]
    [SerializeField]
    bool isFront, isRight, isLeft, isBehind;
    [SerializeField]
    bool isFront2, isRight2, isLeft2, isBehind2;


    [Header("Heavy Package")]
    [SerializeField]
    public bool tooHeavy;
    [SerializeField]
    private float carryWeight;


    [Header("Phone")]
    [SerializeField]
    private float interactPhoneDistance;
    [SerializeField]
    public bool withinPhoneRange;
    [SerializeField]
    private LayerMask PhoneLayer;
    [SerializeField]
    public bool isAnswered;

    [Header("Dash")]
    [SerializeField]
    private float dashForce;
    [SerializeField]
    private float dashSpeed;
    [SerializeField]
    private float dashUpForce;
    [SerializeField]
    private float dashDuration;
    [SerializeField]
    private float dashCd;
    [SerializeField]
    private float dashCdTimer;
    [SerializeField]
    private bool isDashing;
    [SerializeField]
    private bool startTimer;

    [Header("Off-screen Indicator")]
    [SerializeField]
    private PlayerObject playerObject;

    [Header("Collectables")]
    public int mailCount;

    private bool lightsOn;

    [Header("Minigame")]
    [SerializeField]
    private Camera boxcam;
    [SerializeField]
    private GameObject boxcamHolder;
    private boxingMinigame bM;
    private GameObject minigame;
    [SerializeField]
    public bool damageApplied;
    [SerializeField]
    public float forceMagnitude1;
    [SerializeField]
    public float forceMagnitude2;

    [Header("Push UI")]
    [SerializeField]
    private float maxTimer;
    [SerializeField]
    private Slider pushSlider;
    [SerializeField]
    private float maxCDTimer;
    [SerializeField]
    private Slider pushCDSlider;
    [SerializeField]
    private float pushCDtimer;

    public float pushShaderTimer;

    [Header("Level1")]
    [SerializeField]
    public GameObject JumpOverIntruction;
    [SerializeField]
    public GameObject cantJump1;
    [SerializeField]
    public GameObject cantJump2;


    [Header("PauseSelection")]
    private bool isPaused = false;
    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private GameObject menuOptionsParent;
    [SerializeField]
    private GameObject quitOptionsParent;
    [SerializeField]
    private GameObject quitMenu;
    [SerializeField]
    private List<PauseMenuOption> menuOptions;
    private PauseMenuOption selectedOption;

    [SerializeField]
    private InputAction pauseGame, pauseJoystick, selectOption;
    private Vector2 joystickValue;
    private bool canToggle = false;
    private bool canMove = true;
    private bool canPress = true;
    private bool quitActive = false;

    [SerializeField] private int selectNum = 1;


    [SerializeField]
    private bool summoningAnimIsTriggered;
    [SerializeField]
    private bool summoningAnimIsTriggered2;
    //[SerializeField]
    //float dropValue;
    //[SerializeField]
    //float dropForce;

    [Header("Toy")]
    [SerializeField]
    public bool withinToyRange;
    [SerializeField]
    public Transform leftHand1, leftHand2;
    [SerializeField]
    public Transform rightHand1, rightHand2;
    [SerializeField]
    public bool leftHandisFull, rightHandisFull, handIsFull, itemIsFull;

    [Header("Facial Expression")]
    [SerializeField]
    private Animator facialAnim1;
    [SerializeField]
    private Animator facialAnim2;

    private void Awake()
    {
        isFreeze = false;
        inputAsset = this.GetComponent<PlayerInput>().actions;
        player = inputAsset.FindActionMap("Cube");
        pause = inputAsset.FindActionMap("PauseControls");
        dialogue = inputAsset.FindActionMap("Dialogue");

        rb = this.GetComponent<Rigidbody>();

        //playerPos = this.transform;
        maxSpeed = walkSpeed;
        mainCam = Camera.main;

        playerSounds = GetComponent<PlayerSoundbank>();
        footstepEvent = playerSounds.steps;
        landEvent = playerSounds.land;
        jumpEvent = playerSounds.jump;
        dieEvent = playerSounds.die;
        parachuteOpenEvent = playerSounds.parachuteOpen;
        glideEvent = playerSounds.parachuteClose;
        //gM = GetComponent<GameManager>();
    }

    private void OnEnable()
    {
        pauseGame = pause.FindAction("Pause/Unpause");
        pauseGame.Enable();
        pauseJoystick = pause.FindAction("MenuJoystick");
        selectOption = pause.FindAction("SelectOption");

        player.FindAction("Pick").started += DoDrop;
        move = player.FindAction("Move");
        pull = player.FindAction("Pull");
        dash = player.FindAction("Dash");
        close = player.FindAction("Close");
        push = player.FindAction("Push");
        skip = player.FindAction("Skip");
        skipTrigger = player.FindAction("SkipTrigger");
        pushRelease = player.FindAction("ReleasePush");

        //player.FindAction("Join").started += DoTalk;

        jump = player.FindAction("Jump");
        triggerButton = player.FindAction("Trigger");


        player.FindAction("Parachute").started += DoParachute;
        player.FindAction("Parachute").canceled += DoFall;
        //player.FindAction("Push").started += DoPush;

        continueControl.action.Enable();


        player.Enable();

    }

    private void OnDisable()
    {

        pauseGame.Disable();
        pauseJoystick.Disable();
        selectOption.Disable();

        player.FindAction("Pick").started -= DoDrop;

        player.Disable();
        //player.FindAction("Join").started -= DoTalk;
        player.FindAction("Parachute").started -= DoParachute;
        player.FindAction("Parachute").canceled -= DoFall;
        //player.FindAction("Push").started -= DoPush;
        continueControl.action.Disable();



        //dialogue.FindAction("ContinueDialogue").started -= DoContinue;
        //pickControl.action.Disable();

    }

    // Start is called before the first frame update
    void Start()
    {
        maxTimer = 1;
        maxCDTimer = 1;
        isCameraLocked = false;
        gameManager = Object.FindAnyObjectByType<GameManager>();
        //lineView = FindAnyObjectByType<LineView>();

        //objectGrabbable = Object.FindAnyObjectByType<ObjectGrabbable>();

        withinDialogueRange = false;

        //trigger = GameObject.FindGameObjectWithTag("Trigger");
        //tG = trigger.GetComponent<Trigger>();

        parachuteObj.SetActive(false);
        canJump = true;
        lastStepTime = Time.time;
        charController = GetComponent<CharacterControl>();
        objectsInScene = GameObject.FindObjectsByType<GameObject>(FindObjectsSortMode.None);


    }

    bool chargeToggle = true;

    // Update is called once per frame
    void Update()
    {
        P1Damage();
        P2Damage();
        DetectDirectionBetweenPlayerAndObject();
        //DetectPackageWeight();
        CastBlobShadow();
        CheckGrounded();
        //SpeedControl();

        //ContinueBottonControl();
        //MovementCalcs();
        //CheckCamera();
        ItemDetector();
        CameraSwitch();
        AnimationAndSound();
        //PauseMenu();
        if (curSceneName == "Level1" || curSceneName == "MVPLevel")
        {
            initBoxing();
        } else if(curSceneName == "HubStart")
        {
            initBoxing();
        }

        if (ReadPushButton())
        {
            OutlineActivate();
            if (chargeToggle == true)
            {
                chargeParticle.Play();
            }
            chargeToggle = false;
            
        } else if (ReadPushReleaseButton())
        {
            pushParticle.Play();
            if(chargeToggle == false)
            {
                chargeParticle.Stop();
            }
            chargeToggle = true;
            OutlineDeActivate();
        }


        playerPos = this.transform;

        if (!useNewMovement)
        {
            MovementCalcs();
            print("use old movementCal");
        }
        else
        {
            if (playerCamera != null || mainCam != null)
            {

                if (curSceneName == "TitleScene" || curSceneName == "HubStart")
                {
                    if(charController.rb != null)
                    {
                        if(bM != null)
                        {
                            if (bM.isboxing)
                            {
                                if (SelectMinigame.instance.chooseOne)
                                {
                                    charController.RunMovement(bM.boxingCamObject, canParachute, move.ReadValue<Vector2>(), jump, parachuteObj, tooHeavy, isOnCircle, isFreeze, isPlayer1, isPlayer2, holdPush);
                                }

                                if (SelectMinigame.instance.chooseTwo)
                                {
                                    charController.RunMovement(bM.boxingCamObject1, canParachute, move.ReadValue<Vector2>(), jump, parachuteObj, tooHeavy, isOnCircle, isFreeze, isPlayer1, isPlayer2, holdPush);
                                }

                            }
                            else
                            {
                                charController.RunMovement(mainCam, canParachute, move.ReadValue<Vector2>(), jump, parachuteObj, tooHeavy, isOnCircle, isFreeze, isPlayer1, isPlayer2, holdPush);

                            }
                        }
                        else
                        {
                            charController.RunMovement(mainCam, canParachute, move.ReadValue<Vector2>(), jump, parachuteObj, tooHeavy, isOnCircle, isFreeze, isPlayer1, isPlayer2, holdPush);
                        }

                    }

                    //print("use new movementCal");
                }else if (curSceneName == "Level1" && bM.isboxing || curSceneName == "MVPLevel" && bM.isboxing)
                {
                    if (charController.rb != null)
                        {
                            charController.RunMovement(boxcam, canParachute, move.ReadValue<Vector2>(), jump, parachuteObj, tooHeavy, isOnCircle, isFreeze, isPlayer1, isPlayer2, holdPush);
                        }
                    
                }
                else
                {
                    if (charController.rb != null)
                    {
                        charController.RunMovement(playerCamera, canParachute, move.ReadValue<Vector2>(), jump, parachuteObj, tooHeavy, isOnCircle, isFreeze, isPlayer1,isPlayer2, holdPush);
                    }

                }
                //charController.RunMovement(playerCamera, canParachute, move.ReadValue<Vector2>(), jump, parachuteObj, tooHeavy, isOnCircle, isFreeze);                    
                //print("use new movementCal");


                if (isPlayer1)
                {
                    if (!isFreeze)
                    {
                        playerAnimator.SetFloat("speed", rb.velocity.magnitude);
                    }
                    else
                    {
                        charController.ApplyGravity();
                        playerAnimator.SetFloat("speed", 0f);
                    }

                }
                if (isPlayer2)
                {
                    if (!isFreeze)
                    {
                        playerAnimator2.SetFloat("speed", rb.velocity.magnitude);
                    }
                    else
                    {
                        charController.ApplyGravity();
                        playerAnimator2.SetFloat("speed", 0f);
                    }

                }
            }

            if(curSceneName != "LeveL1")
            {
                JumpOverIntruction.SetActive(false);
            }


            Push();
            NewPush();
            DoPush();
            ShowParachuteInstruction();

            //pushShaderSetup();


         }

        DetectPushRange();

        if (curSceneName == scene3 || curSceneName == scene6)
        {
            package = null; 
        }

        if (curSceneName == scene5 || curSceneName == scene7 || curSceneName == scene9 || curSceneName == scene1 || curSceneName == "New CC" || curSceneName == scene10)
        {
            package = GameObject.FindGameObjectWithTag("Package");
            
        }

        if (curSceneName == "Level1" || curSceneName == "Tutorial" || curSceneName == "MVPLevel" || curSceneName == "Level3")
        {

            playerObject.GetComponent<PlayerObject>().enabled = true;
        }
        else
        {
            playerObject.GetComponent<PlayerObject>().enabled = false;
        }

        if (curSceneName == scene5 || curSceneName == "New CCa" || curSceneName == scene10)
        {
            if (!bM.isboxing)
            {
                canParachute = true;
            }
            
        }
        else
        {
            canParachute = false;
        }

            JoinGameTitle();
        //PlayerPosition();


        //CameraControl();
        //if (Input.GetKey(KeyCode.Space))
        //{
        //    Loader.Load(Loader.Scene.MVPLevel);
        //}

        GetPullObjects();

        if(startTimer)
        {
            dashCdTimer += Time.deltaTime;
        }

        if (pushStartTimer)
        {
            pushTimer += Time.deltaTime;
        }
        //OnDrawGizmos();
    }

    private void LateUpdate()
    {
        PlayerDetector();

        if (!useNewMovement)
        {
            CastBlobShadow();
        }
        else
        {
            charController.LateUpdateFunctions();
        }
       
    }

    private void FixedUpdate()
    {
        if (!isFreeze && !useNewMovement)
        {
            Move();
            Jump();
            //Dash();
        }
        else if (!isFreeze && useNewMovement)
        {
            //apply new movement functions
            charController.FixedUpdateFunctions();
        }

        if (!withinPushingRange & otherRB != null)
        {

            otherRB.useGravity = true;
            //otherRB.isKinematic = false;
        }


        TakePackage();
        if (curSceneName == scene1 || curSceneName == scene3)
        {
            Interacte();
            OnTV();
            Talk();
        }
        if (curSceneName == scene6)
        {
            EnterOffice();
            Interacte();
        }

        Pull();
        //PushShader();


        pushShaderTimer = pushCDtimer;
    }


    void JoinGameTitle()
    {
        if (isPlayer1 && !titleDisplayed)
        {
            titleText.text = "1P";

            StartCoroutine(StopShowTitle());
            model1.SetActive(true);
            Destroy(model2);

        }
        if (isPlayer2 && !titleDisplayed)
        {
            titleText.text = "2P";

            StartCoroutine(StopShowTitle());
            model2.SetActive(true);
            Destroy(model1);
        }


    }
    IEnumerator StopShowTitle()
    {
        yield return new WaitForSeconds(3f);
        titleDisplayed = true;
        titleCanvas.SetActive(false);
    }


    private void AnimationAndSound()
    {
        if (isPlayer1)
        {
            playerAnimator.SetBool("isGliding", isGliding);
            p1Indicator.SetActive(true);
        }

        if (isPlayer2)
        {
            p2Indicator.SetActive(true);
            playerAnimator2.SetBool("isGliding", isGliding);
        }

        //if is moving


        if (!useNewMovement)
        {
            if (isGliding)
            {
                parachuteObj.SetActive(true);
            }
            else
            {
                parachuteObj.SetActive(false);
            }
        }
     


    }
    private void MovementCalcs()
    {


        if (isFreeze)
        {
            if (isPlayer1)
            {
                playerAnimator.SetFloat("speed", 0);
            }
            if (isPlayer2)
            {
                playerAnimator2.SetFloat("speed", 0);
            }
        }
        else
        {
            if (isPlayer1)
            {
                playerAnimator.SetFloat("speed", currentSpeed);
            }
            if (isPlayer2)
            {
                playerAnimator2.SetFloat("speed", currentSpeed);
            }

        }

        if (move.ReadValue<Vector2>().x != 0 || move.ReadValue<Vector2>().y != 0)
        {
            Vector2 moveInput = move.ReadValue<Vector2>();
            if (moveInput.magnitude > 0)
            {
                //we are moving
                faceDir = new Vector3(move.ReadValue<Vector2>().x, 0, move.ReadValue<Vector2>().y);
                isWalking = true;

                if (moveInput.magnitude > runThreshold)
                {
                    isRunning = true;
                    //print("Is Runing");
                }
                else
                {
                    isRunning = false;
                }

                //print("isWalking" + isWalking);
            }


            if (isRunning)
            {
                float accel = (maxSpeed / timeToRun);
                currentSpeed += accel * Time.deltaTime;


            }
            else if (isPulling)
            {
                //float accel = (maxSpeed / timeToPull);
                currentSpeed = pullItemForce;
                print("isPulling" + currentSpeed);

            }
            else if (isGliding)
            {
                float accel = (maxSpeed / timeToGlide);
                currentSpeed += accel * Time.deltaTime;

            }
            else if (!isRunning && !isGliding && !isPulling && !isDashing) 
            {
                float accel = (maxSpeed / timeToWalk);
                currentSpeed += accel * Time.deltaTime;

            }

        }
        else
        {
            float deccel = (-maxSpeed / timeToZero);
            currentSpeed += deccel * Time.deltaTime;
            isWalking = false;
            shouldStep = false;
        }
        //Debug.Log(rb.velocity);
        currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
    }

    void CameraSwitch()
    {
        currentScene = SceneManager.GetActiveScene();
        curSceneName = currentScene.name;


        if (curSceneName == scene1 || curSceneName == scene3 || curSceneName == scene6 || curSceneName == scene8)
        {
            playerCamera.enabled = false;
            mainCam = Camera.main;
            //print("1");
            movementCamera = mainCam;

        }
        else if (curSceneName == scene2 || curSceneName == scene4 || curSceneName == scene5 || curSceneName == scene7 || curSceneName == scene9 || curSceneName == "New CC" || curSceneName == scene10)
        {
            playerCamera.enabled = true;
            mainCam = null;
            //print("PlayerCamara");
            //print("2");
            movementCamera = playerCamera;
            //if (exButton.instance.inBridge)
            //{
            //    StartCoroutine(exButton.instance.SwitchCamera());
            //}


        }
        //else
        //{
        //    playerCamera.enabled = true;
        //    mainCam = null;
        //}

        //Start the Devil phone Dialogue 
        //if(curSceneName == scene1 && !Dialogue2)
        //{
        //    SceneControl.instance.dR.StartDialogue("HubStart");
        //    Dialogue2 = true;
        //}

    }


    private void Move()
    {

        float forceAdd = timeToWalk;
        if (!isOnCircle)
        {
            if (isPlayer1)
            {
                if (!switchPuzzleCam || !switchPuzzle2CamL || !switchPuzzle2CamLP2 || !switchPuzzle2CamR || !switchPuzzle2CamRP2)
                {
                    if (curSceneName == scene1 || curSceneName == scene3 || curSceneName == scene6)
                    {
                        if (!isCameraLocked)
                        {

                            if (!isDashing)
                            {
                                if(mainCam != null)
                                {
                                    forceDirection += faceDir.z * GetCameraForward(mainCam) * currentSpeed;

                                    forceDirection += faceDir.x * GetCameraRight(mainCam) * currentSpeed;
                                }

                            }
                            else
                            {
                                if(mainCam != null)
                                {
                                    forceDirection += faceDir.z * GetCameraForward(mainCam) * dashSpeed;

                                    forceDirection += faceDir.x * GetCameraRight(mainCam) * dashSpeed;
                                }

                            }


                        }
                        else
                        {

                            float horizontalInput = move.ReadValue<Vector2>().x;
                            float verticalInput = move.ReadValue<Vector2>().y;

                            Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput).normalized;
                            //transform.Translate(Vector3.forward * Time.deltaTime * currentSpeed * verticalInput);
                            //transform.Translate(Vector3.right * Time.deltaTime * currentSpeed * horizontalInput);
                            transform.Translate(movement * currentSpeed * Time.deltaTime, Space.World);

                            //MoveTowardFacingDirection();
                        }

                    }
                    else
                    {
                        if (isGliding)
                        {
                            //currentSpeed = gliderSpeed;
                        }

                        if (!isCameraLocked)
                        {

                            if (!isDashing)
                            {
                                if(playerCamera != null)
                                {
                                    forceDirection += faceDir.x * GetCameraRight(playerCamera) * currentSpeed;
                                    forceDirection += faceDir.z * GetCameraForward(playerCamera) * currentSpeed;
                                }

                            }
                            else
                            {
                                if(playerCamera != null)
                                {
                                    forceDirection += faceDir.x * GetCameraRight(playerCamera) * dashSpeed;
                                    forceDirection += faceDir.z * GetCameraForward(playerCamera) * dashSpeed;
                                }

                            }


                        }
                        else
                        {
                            if(playerCamera != null)
                            {

                                forceDirection += faceDir.x * GetCameraRight(playerCamera) * pullingSpeed;
                                forceDirection += faceDir.z * GetCameraForward(playerCamera) * pullingSpeed;

                            }

                            //MoveTowardFacingDirection();
                            //print("pullingSpeed" + pullingSpeed);
                        }



                    }
                }
                else
                {
                    if (!isCameraLocked)
                    {
                        if (!isDashing)
                        {
                            if(camManager.instance.puzzle1Cam != null)
                            {
                                forceDirection += faceDir.x * GetCameraRight(camManager.instance.puzzle1Cam) * currentSpeed;
                                forceDirection += faceDir.z * GetCameraForward(camManager.instance.puzzle1Cam) * currentSpeed;
                            }
                            
                            if(camManager.instance.puzzle2Cam != null)
                            {
                                forceDirection += faceDir.x * GetCameraRight(camManager.instance.puzzle2Cam) * currentSpeed;
                                forceDirection += faceDir.z * GetCameraForward(camManager.instance.puzzle2Cam) * currentSpeed;
                            }
                            
                            if(camManager.instance.puzzle2CamP1 != null)
                            {
                                forceDirection += faceDir.x * GetCameraRight(camManager.instance.puzzle2CamP1) * currentSpeed;
                                forceDirection += faceDir.z * GetCameraForward(camManager.instance.puzzle2CamP1) * currentSpeed;
                            }

                        }
                        else
                        {
                            if (camManager.instance.puzzle1Cam != null)
                            {
                                forceDirection += faceDir.x * GetCameraRight(camManager.instance.puzzle1Cam) * dashSpeed;
                                forceDirection += faceDir.z * GetCameraForward(camManager.instance.puzzle1Cam) * dashSpeed;
                            }
                            else if (camManager.instance.puzzle2Cam != null)
                            {
                                forceDirection += faceDir.x * GetCameraRight(camManager.instance.puzzle2Cam) * dashSpeed;
                                forceDirection += faceDir.z * GetCameraForward(camManager.instance.puzzle2Cam) * dashSpeed;
                            }
                            else if (camManager.instance.puzzle2CamP1 != null)
                            {
                                forceDirection += faceDir.x * GetCameraRight(camManager.instance.puzzle2CamP1) * dashSpeed;
                                forceDirection += faceDir.z * GetCameraForward(camManager.instance.puzzle2CamP1) * dashSpeed;
                            }

                        }


                    }
                    else
                    {
                        float horizontalInput = move.ReadValue<Vector2>().x;
                        float verticalInput = move.ReadValue<Vector2>().y;

                        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput).normalized;
                        //transform.Translate(Vector3.forward * Time.deltaTime * currentSpeed * verticalInput);
                        //transform.Translate(Vector3.right * Time.deltaTime * currentSpeed * horizontalInput);

                        transform.Translate(movement * currentSpeed * Time.deltaTime, Space.World);

                        //MoveTowardFacingDirection();
                    }


                }
            }

            if (isPlayer2)
            {
                if (!switchPuzzleCamP2 || !switchPuzzle2CamL || !switchPuzzle2CamLP2 || !switchPuzzle2CamR || !switchPuzzle2CamRP2)
                {
                    if (curSceneName == scene1 || curSceneName == scene3 || curSceneName == scene6)
                    {
                        if (!isCameraLocked)
                        {
                            if (!isDashing)
                            {
                                if(mainCam != null)
                                {
                                    forceDirection += faceDir.x * GetCameraRight(mainCam) * currentSpeed;
                                    forceDirection += faceDir.z * GetCameraForward(mainCam) * currentSpeed;
                                }

                            }
                            else
                            {
                                if(mainCam != null)
                                {
                                    forceDirection += faceDir.x * GetCameraRight(mainCam) * dashSpeed;
                                    forceDirection += faceDir.z * GetCameraForward(mainCam) * dashSpeed;
                                }

                            }


                        }
                        else
                        {

                            float horizontalInput = move.ReadValue<Vector2>().x;
                            float verticalInput = move.ReadValue<Vector2>().y;

                            Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput).normalized;
                            //transform.Translate(Vector3.forward * Time.deltaTime * currentSpeed * verticalInput);
                            //transform.Translate(Vector3.right * Time.deltaTime * currentSpeed * horizontalInput);
                            transform.Translate(movement * currentSpeed * Time.deltaTime, Space.World);
                        }

                    }
                    else
                    {
                        if (isGliding)
                        {
                            //currentSpeed = gliderSpeed;
                        }

                        if (!isCameraLocked)
                        {
                            if (!isDashing)
                            {
                                if(playerCamera != null)
                                {
                                    forceDirection += faceDir.x * GetCameraRight(playerCamera) * currentSpeed;
                                    forceDirection += faceDir.z * GetCameraForward(playerCamera) * currentSpeed;
                                }

                            }
                            else
                            {
                                if(playerCamera != null)
                                {
                                    forceDirection += faceDir.x * GetCameraRight(playerCamera) * dashSpeed;
                                    forceDirection += faceDir.z * GetCameraForward(playerCamera) * dashSpeed;
                                }

                            }



                        }
                        else
                        {
                            if(playerCamera != null)
                            {
                                forceDirection += faceDir.x * GetCameraRight(playerCamera) * pullingSpeed;
                                forceDirection += faceDir.z * GetCameraForward(playerCamera) * pullingSpeed;
                            }

                        }



                    }
                }
                else
                {
                    if (!isCameraLocked)
                    {
                        if (!isDashing)
                        {
                            if(camManager.instance.puzzle1CamP2 != null)
                            {
                                forceDirection += faceDir.x * GetCameraRight(camManager.instance.puzzle1CamP2) * currentSpeed;
                                forceDirection += faceDir.z * GetCameraForward(camManager.instance.puzzle1CamP2) * currentSpeed;
                            }
                            
                            if (camManager.instance.puzzle2Cam2 != null)
                            {
                                forceDirection += faceDir.x * GetCameraRight(camManager.instance.puzzle2Cam2) * currentSpeed;
                                forceDirection += faceDir.z * GetCameraForward(camManager.instance.puzzle2Cam2) * currentSpeed;
                            }
                            
                            if (camManager.instance.puzzle2CamP2 != null)
                            {
                                forceDirection += faceDir.x * GetCameraRight(camManager.instance.puzzle2CamP2) * currentSpeed;
                                forceDirection += faceDir.z * GetCameraForward(camManager.instance.puzzle2CamP2) * currentSpeed;
                            }

                        }
                        else
                        {
                            if (camManager.instance.puzzle1CamP2)
                            {
                                forceDirection += faceDir.x * GetCameraRight(camManager.instance.puzzle1CamP2) * dashSpeed;
                                forceDirection += faceDir.z * GetCameraForward(camManager.instance.puzzle1CamP2) * dashSpeed;
                            }
                            else if (camManager.instance.puzzle2Cam2 != null)
                            {
                                forceDirection += faceDir.x * GetCameraRight(camManager.instance.puzzle2Cam2) * dashSpeed;
                                forceDirection += faceDir.z * GetCameraForward(camManager.instance.puzzle2Cam2) * dashSpeed;
                            }
                            else if (camManager.instance.puzzle2CamP2 != null)
                            {
                                forceDirection += faceDir.x * GetCameraRight(camManager.instance.puzzle2CamP2) * dashSpeed;
                                forceDirection += faceDir.z * GetCameraForward(camManager.instance.puzzle2CamP2) * dashSpeed;
                            }

                        }


                    }
                    else
                    {
                        float horizontalInput = move.ReadValue<Vector2>().x;
                        float verticalInput = move.ReadValue<Vector2>().y;

                        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput).normalized;
                        //transform.Translate(Vector3.forward * Time.deltaTime * currentSpeed * verticalInput);
                        //transform.Translate(Vector3.right * Time.deltaTime * currentSpeed * horizontalInput);

                        transform.Translate(movement * currentSpeed * Time.deltaTime, Space.World);
                    }

                }
            }
        }


        else
        {
            //rb.velocity = new Vector3(-(transform.position.x - activeCircle.transform.position.x) * 3 * Time.deltaTime, 0, -(transform.position.z - activeCircle.transform.position.z) * 3 *Time.deltaTime);
        }


        //forceDirection += move.ReadValue<Vector2>().x * GetCameraRight(mainCam) * movementForce;
        //forceDirection += move.ReadValue<Vector2>().y * GetCameraForward(mainCam) * movementForce;
        rb.AddForce(forceDirection, ForceMode.Impulse);
        forceDirection = Vector3.zero;

        //if (rb.velocity.y < 0f)
        //{
        //    rb.velocity -= Vector3.down * Physics.gravity.y * Time.fixedDeltaTime * 2;
        //}


        Vector3 horizontalVelocity = rb.velocity;
        horizontalVelocity.y = 0;
        if (horizontalVelocity.sqrMagnitude > maxSpeed * maxSpeed)
        {
            if (!isFreeze)
            {
                if (!tooHeavy)
                {
                    if (isRunning && !isGliding)
                    {
                        maxSpeed = runSpeed;
                    }
                    else if (isGliding)
                    {
                        maxSpeed = gliderSpeed;
                    }
                    else
                    {
                        maxSpeed = walkSpeed;
                    }
                }
                else
                {
                    if (isRunning && !isGliding)
                    {
                        maxSpeed = runSpeed / carryWeight;
                    }
                    else if (isGliding)
                    {
                        maxSpeed = gliderSpeed / carryWeight;
                    }
                    else
                    {
                        maxSpeed = walkSpeed / carryWeight;
                    }
                }

            }
            //else if (isPulling)
            //{
            //    maxSpeed = pullSpeed;
            //}


            rb.velocity = horizontalVelocity.normalized * currentSpeed + Vector3.up * rb.velocity.y;

            //rb.velocity = new Vector3((forceDirection.x * currentSpeed) * Time.deltaTime, rb.velocity.y, (forceDirection.z * currentSpeed) * Time.deltaTime);
            //Debug.Log("maxSpeed =" + maxSpeed);


            if (!isCameraLocked)
            {
                LookAt();
            }

        }

       


    }

    #region Interact with moveable obstacles
    private void DetectDirectionBetweenPlayerAndObject()
    {
        if (targetObject != null)
        {
            // Calculate the direction vector from the object to the player
            Vector3 toPlayer = this.transform.position - targetObject.transform.position;
            // Calculate the dot product between the forward vector of the object and the toPlayer vector
            float dotProduct = Vector3.Dot(targetObject.transform.forward, toPlayer.normalized);

            if (isPlayer1)
            {
                if (dotProduct > 0.5f)
                {
                    isRight = false;
                    isLeft = false;
                    isFront = false;
                    isBehind = true;

                    Debug.Log("Player is on the right side of the object");
                }
                else if (dotProduct < -0.5f)
                {
                    isRight = false;
                    isLeft = false;
                    isFront = true;
                    isBehind = false;
                    Debug.Log("Player is on the left side of the object");
                }
                else
                {
                    // You may need to adjust these thresholds based on your specific scenario
                    if (toPlayer.x > 0)
                    {
                        isRight = true;
                        isLeft = false;
                        isFront = false;
                        isBehind = false;
                        Debug.Log("Player is in front of the object");
                    }
                    else
                    {
                        isRight = false;
                        isLeft = true;
                        isFront = false;
                        isBehind = false;
                        Debug.Log("Player is behind the object");
                    }
                }
            }

            if (isPlayer2)
            {
                if (dotProduct > 0.5f)
                {
                    isRight2 = false;
                    isLeft2 = false;
                    isFront2 = false;
                    isBehind2 = true;

                    Debug.Log("Player is on the right side of the object");
                }
                else if (dotProduct < -0.5f)
                {
                    isRight2 = false;
                    isLeft2 = false;
                    isFront2 = true;
                    isBehind2 = false;
                    Debug.Log("Player is on the left side of the object");
                }
                else
                {
                    // You may need to adjust these thresholds based on your specific scenario
                    if (toPlayer.x > 0)
                    {
                        isRight2 = true;
                        isLeft2 = false;
                        isFront2 = false;
                        isBehind2 = false;
                        Debug.Log("Player is in front of the object");
                    }
                    else
                    {
                        isRight2 = false;
                        isLeft2 = true;
                        isFront2 = false;
                        isBehind2 = false;
                        Debug.Log("Player is behind the object");
                    }
                }

            }




        }


    }
    private void MoveTowardFacingDirection()
    {
        float horizontalInput = move.ReadValue<Vector2>().x;
        float verticalInput = move.ReadValue<Vector2>().y;

        if (isBehind)
        {
            transform.Translate(playerDir.right * Time.deltaTime * currentSpeed * horizontalInput);
            transform.Translate(playerDir.forward * Time.deltaTime * currentSpeed * verticalInput);
        }

        if (isLeft)
        {
            transform.Translate(-playerDir.right * Time.deltaTime * currentSpeed * horizontalInput);
            transform.Translate(-playerDir.forward * Time.deltaTime * currentSpeed * verticalInput);
        }

        if (isRight)
        {
            transform.Translate(-playerDir.right * Time.deltaTime * currentSpeed * horizontalInput);
            transform.Translate(-playerDir.forward * Time.deltaTime * currentSpeed * verticalInput);
        }

        if (isFront)
        {
            transform.Translate(playerDir.right * Time.deltaTime * currentSpeed * horizontalInput);
            transform.Translate(playerDir.forward * Time.deltaTime * currentSpeed * verticalInput);
        }



    }

    private void MoveTowardFacingDirection2()
    {
        float horizontalInput = move.ReadValue<Vector2>().x;
        float verticalInput = move.ReadValue<Vector2>().y;
        if (isBehind2)
        {
            transform.Translate(playerDir.right * Time.deltaTime * currentSpeed * horizontalInput);
            transform.Translate(playerDir.forward * Time.deltaTime * currentSpeed * verticalInput);
        }

        if (isLeft2)
        {
            transform.Translate(-playerDir.right * Time.deltaTime * currentSpeed * horizontalInput);
            transform.Translate(-playerDir.forward * Time.deltaTime * currentSpeed * verticalInput);
        }

        if (isRight2)
        {
            transform.Translate(-playerDir.right * Time.deltaTime * currentSpeed * horizontalInput);
            transform.Translate(-playerDir.forward * Time.deltaTime * currentSpeed * verticalInput);
        }

        if (isFront2)
        {
            transform.Translate(playerDir.right * Time.deltaTime * currentSpeed * horizontalInput);
            transform.Translate(playerDir.forward * Time.deltaTime * currentSpeed * verticalInput);
        }
    }

    #endregion

    #region Cameara Controls
    private void LookAt()
    {
        Vector3 direction = rb.velocity;
        direction.y = 0f;

        if (move.ReadValue<Vector2>().sqrMagnitude > 0.1f && direction.sqrMagnitude > 0.1f)
            this.rb.rotation = Quaternion.LookRotation(direction, Vector3.up);
        else
            rb.angularVelocity = Vector3.zero;
    }

    private Vector3 GetCameraForward(Camera playerCamera)
    {

        Vector3 forward = playerCamera.transform.forward;
        forward.y = 0;
        return forward.normalized;
    }

    private Vector3 GetCameraRight(Camera playerCamera)
    {
        Vector3 right = playerCamera.transform.right;
        right.y = 0;
        return right.normalized;
    }

    #endregion


    #region Package
    void TakePackage()
    {
        if (curSceneName != scene9)
        {
            if (objectGrabbable == null)
            {
                //if (Physics.SphereCast(playerPos.position, pickDistanceHeavy, playerPos.forward, out raycastHit, pickDistanceHeavy, pickableMask))
                if (Physics.Raycast(this.transform.position, this.transform.forward, out raycastHit, pickDistanceHeavy, pickableMask))
                {
                    if (targetObject == null)
                    {
                        if (isPlayer1)
                        {
                            objectGrabbable = package.GetComponent<ObjectGrabbable>();

                            objectGrabbable.Grab(itemContainer);
                            //if (tooHeavy)
                            //{
                            //    objectGrabbable.Grab(itemContainer1);
                            //}
                            //else
                            //{
                            //    objectGrabbable.Grab(itemContainer);
                            //}



                            playerSounds.packagePick.Post(this.gameObject);
                            
                            if(GameManager.instance.p2.objectGrabbable != null)
                            {
                                GameManager.instance.p2.objectGrabbable = null;
                            }


                        }

                        if (isPlayer2)
                        {
                            objectGrabbable = package.GetComponent<ObjectGrabbable>();
                            objectGrabbable.Grab(itemContainer);
                            //if (tooHeavy)
                            //{
                            //    objectGrabbable.Grab(itemContainer1);
                            //}
                            //else
                            //{
                            //    objectGrabbable.Grab(itemContainer);
                            //}
                            playerSounds.packagePick.Post(this.gameObject);
                            
                            if (GameManager.instance.p1.objectGrabbable != null)
                            {
                                GameManager.instance.p1.objectGrabbable = null;
                            }
                        }
                    }

                }
            }
        }
        else
        {
            if (objectGrabbable == null)
            {
                if(Physics.SphereCast(playerPos.position, pickRadiusHeavy, playerPos.forward, out raycastHit, pickDistanceHeavy, pickableMask))
                //if(Physics.Raycast(this.transform.position, this.transform.forward, out raycastHit, pickDistanceHeavy, pickableMask))
                {
                    //print("Package");
                    if (isPlayer1 && rC.Player2isCarrying)
                    {
                        //print("FoundPackage");
                        if (targetObject == null)
                        {
                            //print("TakePackage");
                            objectGrabbable = package.GetComponent<ObjectGrabbable>();
                            objectGrabbable.Grab(itemContainer);
                            //if (tooHeavy)
                            //{
                            //    objectGrabbable.Grab(itemContainer1);
                            //}
                            //else
                            //{
                            //    objectGrabbable.Grab(itemContainer);
                            //}
                            playerSounds.packagePick.Post(this.gameObject);
                            GameManager.instance.p2.objectGrabbable = null;
                        }

                    }

                    if (isPlayer2 && rC.Player1isCarrying)
                    {
                        //print("FoundPackage");
                        if (targetObject == null)
                        {
                            //print("TakePackage");
                            objectGrabbable = package.GetComponent<ObjectGrabbable>();
                            objectGrabbable.Grab(itemContainer);
                            //if (tooHeavy)
                            //{
                            //    objectGrabbable.Grab(itemContainer1);
                            //}
                            //else
                            //{
                            //    objectGrabbable.Grab(itemContainer);
                            //}
                            playerSounds.packagePick.Post(this.gameObject);
                            GameManager.instance.p1.objectGrabbable = null;
                        }
                    }
                }
            }
        }
    }
    private void DetectPackageWeight()
    {
        if (isPlayer1)
        {
            if (objectGrabbable != null) 
            {
                if (objectGrabbable.isHeavy && rC.Player1isCarrying)
                {
                    tooHeavy = true;
                }
                else if(!objectGrabbable.isHeavy || !rC.Player1isCarrying)
                {
                    tooHeavy = false;
                }
            }
            else 
            {
                tooHeavy = false;
            }
        }

        if (isPlayer2)
        {
            if (objectGrabbable != null)
            {
                if (objectGrabbable.isHeavy && rC.Player2isCarrying)
                {
                    tooHeavy = true;
                }
                else if(!objectGrabbable.isHeavy || !rC.Player2isCarrying)
                {
                    tooHeavy = false;
                }
            }
            else
            {
                tooHeavy = false;
            }
        }

        //if (objectGrabbable != null)
        //{
        //    print("Hello");
        //    if (isPlayer1 && objectGrabbable.isHeavy)
        //    {
        //        if (rC.Player1isCarrying)
        //        {
        //            print("HeavyPackage11");
        //            tooHeavy = true;
        //        }
        //        else
        //        {
        //            tooHeavy = false;
        //        }
        //    }

        //    if (isPlayer2 && objectGrabbable.isHeavy)
        //    {
        //        if (rC.Player2isCarrying)
        //        {
        //            print("HeavyPackage22");
        //            tooHeavy = true;
        //        }
        //        else
        //        {
        //            tooHeavy = false;

        //        }
        //    }
        //}
        //else if(!rC.Player1isCarrying && !rC.Player2isCarrying)
        //{
        //    print("HeavyPackage333");
        //    tooHeavy = false;
        //}
        //else if(objectGrabbable == null)
        //{
        //    tooHeavy = false;
        //}


        //if (objectGrabbable != null)
        //{
        //    if (isPlayer1)
        //    {
        //        if (rC.Player1isCarrying)
        //        {
        //            if (objectGrabbable.isHeavy)
        //            {
        //                tooHeavy = true;
        //            }
        //            else
        //            {
        //                tooHeavy = false;
        //            }
        //        }
        //    }

        //    if (isPlayer2)
        //    {
        //        if (rC.Player2isCarrying)
        //        {
        //            if (objectGrabbable.isHeavy)
        //            {
        //                tooHeavy = true;
        //            }
        //            else
        //            {
        //                tooHeavy = false;
        //            }
        //        }
        //    }
        //}
        //else
        //{
        //    tooHeavy = false;
        //}



    }

    private void DoDrop(InputAction.CallbackContext obj)
    {
        if (curSceneName == scene9)
        {
            if (objectGrabbable == null)
            {
                isDropped = false;

                //if (Physics.SphereCast(playerPos.position, pickRadiusHeavy, playerPos.forward, out raycastHit, pickDistance, pickableMask))
                //if (Physics.Raycast(this.transform.position, this.transform.forward, out raycastHit, pickDistance, pickableMask))
                if (withinPackageRange)
                {
                    if (isPlayer1)
                    {
                        if (targetObject == null)
                        {
                            print("TakePackage1");
                            objectGrabbable = package.GetComponent<ObjectGrabbable>();

                            objectGrabbable.Grab(itemContainer);
                            //if (tooHeavy)
                            //{
                            //    objectGrabbable.Grab(itemContainer1);
                            //}
                            //else
                            //{
                            //    objectGrabbable.Grab(itemContainer);
                            //}
                            playerSounds.packagePick.Post(this.gameObject);
                            //TakePackageFunction();
                        }
                    }

                    if (isPlayer2)
                    {
                        if (targetObject == null)
                        {
                            print("TakePackage2");
                            objectGrabbable = package.GetComponent<ObjectGrabbable>();
                            objectGrabbable.Grab(itemContainer);
                            //if (tooHeavy)
                            //{
                            //    objectGrabbable.Grab(itemContainer1);
                            //}
                            //else
                            //{
                            //    objectGrabbable.Grab(itemContainer);
                            //}
                            playerSounds.packagePick.Post(this.gameObject);
                            //TakePackageFunction();
                        }

                    }
                }
            }

            if (objectGrabbable != null)
            {
                if (isPlayer1 && rC.Player1isCarrying)
                {
                    if (targetObject == null)
                    {
                        objectGrabbable.P1Drop();
                        print("DoDrop1");
                        isDropped = true;
                        playerSounds.packageToss.Post(this.gameObject);
                    }
                    else
                    {
                        isDropped = false;
                    }


                }


                if (isPlayer2 && rC.Player2isCarrying)
                {
                    if (targetObject == null)
                    {
                        objectGrabbable.P2Drop();
                        //print("Drop");
                        print("DoDrop2");
                        isDropped = true;
                        playerSounds.packageToss.Post(this.gameObject);
                    }
                    else
                    {
                        isDropped = false;
                    }
                }

                if (isDropped)
                {
                    objectGrabbable = null;
                }
            }
        }
        else
        {
            if (objectGrabbable != null)
            {
                playerSounds.packageToss.Post(this.gameObject);
                if (isPlayer1 && rC.Player1isCarrying)
                {
                    if (targetObject == null)
                    {
                        objectGrabbable.P1Drop();
                        print("DoDrop1");
                        isDropped = true;
                    }
                    else
                    {
                        isDropped = false;
                    }


                }


                if (isPlayer2 && rC.Player2isCarrying)
                {
                    if (targetObject == null)
                    {
                        objectGrabbable.P2Drop();
                        //print("Drop");
                        print("DoDrop2");
                        isDropped = true;
                    }
                    else
                    {
                        isDropped = false;
                    }
                }

                if (isDropped)
                {
                    objectGrabbable = null;
                }
            }
        }

    }

    public void TakePackageFunction()
    {
        if(objectGrabbable == null)
        {
            isDropped = false;
        }
        if (isPlayer1)
        {
            if (rC.Player2isCarrying)
            {
                objectGrabbable = package.GetComponent<ObjectGrabbable>();
                if (tooHeavy)
                {
                    objectGrabbable.Grab(itemContainer1);
                }
                else
                {
                    objectGrabbable.Grab(itemContainer);
                }
                playerSounds.packagePick.Post(this.gameObject);
                GameManager.instance.p2.objectGrabbable = null;

            }
            else if (!rC.Player1isCarrying && !rC.Player2isCarrying)
            {
                objectGrabbable = package.GetComponent<ObjectGrabbable>();
                if (tooHeavy)
                {
                    objectGrabbable.Grab(itemContainer1);
                }
                else
                {
                    objectGrabbable.Grab(itemContainer);
                }
                playerSounds.packagePick.Post(this.gameObject);
            }

        }

        if (isPlayer2)
        {
            if (rC.Player1isCarrying)
            {

                objectGrabbable = package.GetComponent<ObjectGrabbable>();
                if (tooHeavy)
                {
                    objectGrabbable.Grab(itemContainer1);
                }
                else
                {
                    objectGrabbable.Grab(itemContainer);
                }
                playerSounds.packagePick.Post(this.gameObject);
                GameManager.instance.p1.objectGrabbable = null;

            }
            else if (!rC.Player1isCarrying && !rC.Player2isCarrying)
            {
                objectGrabbable = package.GetComponent<ObjectGrabbable>();
                if (tooHeavy)
                {
                    objectGrabbable.Grab(itemContainer1);
                }
                else
                {
                    objectGrabbable.Grab(itemContainer);
                }
                playerSounds.packagePick.Post(this.gameObject);
            }

        }
    }

      

    #endregion


    void Interacte()
    {
        if (withinTVRange && !onTv)
        {
            if (ReadActionButton())
            {
                gameManager.p1.turnOnTV = true;
                gameManager.p2.turnOnTV = true;
                //SceneControl.instance.LoadScene("MVPLevel");
                //change scene and enter tutorial level, set gameManger.sceneChanged to true               
            }
        }

        if (withinNPCsRange)
        {
            gameManager.sceneChanged = true;
            bool firstTime = false;
            if (!firstTime)
            {
                //SceneControl.instance.dR.Stop();
                firstTime = true;
            }


            if (ReadActionButton())
            {
                NPCInteracting = true;
                //SceneControl.LV.SetActive(false);
                //Start Dialogue
            }
            else
            {
                NPCInteracting = false;
            }

        }
        else
        {
            NPCInteracting = false;
        }

        if (withinNPC2Range)
        {
            gameManager.sceneChanged = true;
            bool firstTime = false;
            if (!firstTime)
            {
                //SceneControl.instance.dR.Stop();
                firstTime = true;
            }


            if (ReadActionButton())
            {
                NPC2Interacting = true;
                //SceneControl.LV.SetActive(false);
                //Start Dialogue
            }
            else
            {
                NPC2Interacting = false;
            }

        }
        else
        {
            NPC2Interacting = false;
        }

        if (withinNPC3Range)
        {
            gameManager.sceneChanged = true;
            bool firstTime = false;
            if (!firstTime)
            {
                //SceneControl.instance.dR.Stop();
                firstTime = true;
            }


            if (ReadActionButton())
            {
                NPC3Interacting = true;
                //SceneControl.LV.SetActive(false);
                //Start Dialogue
            }
            else
            {
                NPC3Interacting = false;
            }

        }
        else
        {
            NPC3Interacting = false;
        }

        if (withinEntranceRange && curSceneName == "TitleScene")
        {
            if(gameManager.player1 != null && gameManager.player2 != null)
            {
                if (ReadActionButton())
                {
                    //print("Enter");
                    isEntered = true;

                    //SceneControl.instance.LoadScene("MVPLevel");
                    //change scene and enter tutorial level, set gameManger.sceneChanged to true               
                }
            }
        }

        if (withinPhoneRange)
        {
            if (ReadActionButton() && !isAnswered)
            {
                //SceneControl.instance.nameTag.SetActive(true);
                SceneControl.instance.dR.StartDialogue("HubStart");
                isFreeze = true;
                SceneControl.instance.EnableUI();
                isAnswered = true;
            }
        }

    }

    void Talk()
    {
        if(SceneControl.instance.startLevel2 && !Dialogue1)
        {
            SceneControl.instance.WertherConversationStart = true;
            //print("interactiNPC1");
            //SceneControl.LV.SetActive(false);
            SceneControl.instance.dR.StopAllCoroutines();
            SceneControl.instance.phoneUI.SetActive(false);
            SceneControl.instance.dialogueBox.SetActive(true);
            SceneControl.instance.nameTag1.SetActive(true);
            SceneControl.instance.nameTag.SetActive(false);
            SceneControl.instance.nameTagNPC2.SetActive(false);
            SceneControl.instance.nameTagNPC3.SetActive(false);
            gameManager.p1.isFreeze = true;
            gameManager.p2.isFreeze = true;
            SceneControl.instance.dR.StartDialogue("BoomerQuest");

            NPCInteracting = false;
            if (isPlayer1)
            {
                gameManager.p2.Dialogue1 = true;
                Dialogue1 = true;
            }
            if (isPlayer2)
            {
                gameManager.p1.Dialogue1 = true;
                Dialogue1 = true;
            }
        }
        if (SceneControl.instance.startLevel1 && !Dialogue3)
        {
            //SceneControl.LV.SetActive(false);
            SceneControl.instance.LalahConversationStart = true;
            SceneControl.instance.dR.StopAllCoroutines();
            SceneControl.instance.phoneUI.SetActive(false);
            SceneControl.instance.dialogueBox.SetActive(true);
            SceneControl.instance.nameTag1.SetActive(false);
            SceneControl.instance.nameTag.SetActive(false);
            SceneControl.instance.nameTagNPC2.SetActive(true);
            SceneControl.instance.nameTagNPC3.SetActive(false);

            SceneControl.instance.dR.StartDialogue("LalahQuest");

            gameManager.p1.isFreeze = true;
            gameManager.p2.isFreeze = true;

            NPC2Interacting = false;

            if (isPlayer1)
            {
                gameManager.p2.Dialogue3 = true;
                Dialogue3 = true;
            }
            if (isPlayer2)
            {
                gameManager.p1.Dialogue3 = true;
                Dialogue3 = true;
            }
        }
        if (NPCInteracting)
        {
            if (!Dialogue1 && gameManager.timesEnterHub >= 1 && !Dialogue1_2)
            {
                if (!SceneControl.instance.startLevel2 && SceneControl.instance.UI2turnOff)
                {
                    if(gameManager.LalahRequestWasCompleted && gameManager.LalahLeft)
                    {
                        SceneControl.instance.level2Overview = true;
                        gameManager.p1.isFreeze = true;
                        gameManager.p2.isFreeze = true;
                    
                    }else if (!gameManager.LalahRequestWasCompleted)
                    {
                        SceneControl.instance.level2Overview = true;
                        gameManager.p1.isFreeze = true;
                        gameManager.p2.isFreeze = true;
                    }

                }               
            }
            if (!Dialogue1_2 && gameManager.timesEnterHub >= 2 && Dialogue1 && !SceneControl.instance.wertherdialogueEnds)
            {
                SceneControl.instance.WertherConversationStart = true;
                print("interactiNPC1");
                //SceneControl.LV.SetActive(false);
                SceneControl.instance.dR.StopAllCoroutines();
                SceneControl.instance.phoneUI.SetActive(false);
                SceneControl.instance.dialogueBox.SetActive(true);
                SceneControl.instance.nameTag1.SetActive(true);
                SceneControl.instance.nameTag.SetActive(false);
                SceneControl.instance.nameTagNPC2.SetActive(false);
                SceneControl.instance.nameTagNPC3.SetActive(false);
                SceneControl.instance.SwitchCameraToNpc();
                gameManager.p1.isFreeze = true;
                gameManager.p2.isFreeze = true;
                //SceneControl.instance.WertherTalkUI.SetActive(false);
                SceneControl.instance.dR.StartDialogue("HubEnd");


                NPCInteracting = false;
                if (isPlayer1)
                {
                    gameManager.p2.Dialogue1_2 = true;
                    Dialogue1_2 = true;
                }
                if (isPlayer2)
                {
                    gameManager.p1.Dialogue1_2 = true; 
                    Dialogue1_2 = true;;
                }
            }
        }

        if (NPC2Interacting)
        {
            if (!Dialogue3 && gameManager.timesEnterHub >= 1 && !Dialogue3_2)
            {               
                if (!SceneControl.instance.startLevel1 && SceneControl.instance.UITurnOff)
                {
                    if(gameManager.WertherRequestWasCompleted && gameManager.WertherLeft)
                    {
                        SceneControl.instance.level1Overview = true;
                        gameManager.p1.isFreeze = true;
                        gameManager.p2.isFreeze = true;
                    }
                    else if (!gameManager.WertherRequestWasCompleted)
                    {
                        SceneControl.instance.level1Overview = true;
                        gameManager.p1.isFreeze = true;
                        gameManager.p2.isFreeze = true;
                    }


                }
            }

            if(!Dialogue3_2 && gameManager.timesEnterHub >= 2 && Dialogue3 && !SceneControl.instance.LalahdialogueEnds)
            {
                SceneControl.instance.LalahConversationStart = true;
                print("interactiNPC23_2");
                SceneControl.instance.dR.StopAllCoroutines();
                SceneControl.instance.phoneUI.SetActive(false);
                SceneControl.instance.dialogueBox.SetActive(true);
                SceneControl.instance.nameTag1.SetActive(false);
                SceneControl.instance.nameTag.SetActive(false);
                SceneControl.instance.nameTagNPC2.SetActive(true);
                SceneControl.instance.nameTagNPC3.SetActive(false);
                //SceneControl.instance.LalahTalkUi.SetActive(false);
                SceneControl.instance.dR.StartDialogue("LalahEnd");
                withinNPC2Range = false;
                gameManager.showLalahInstruction = false;

                gameManager.p1.isFreeze = true;
                gameManager.p2.isFreeze = true;

                NPC2Interacting = false;
                if (isPlayer1)
                {
                    gameManager.p2.Dialogue3_2 = true;
                    Dialogue3_2 = true;
                }
                if (isPlayer2)
                {
                    gameManager.p1.Dialogue3_2 = true;
                    Dialogue3_2 = true;
                }
            }
        }

        if (NPC3Interacting)
        {
            if (!Dialogue4)
            {
                print("interactiNPC3");
                //SceneControl.LV.SetActive(false);
                SceneControl.instance.dR.StopAllCoroutines();
                SceneControl.instance.phoneUI.SetActive(false);
                SceneControl.instance.dialogueBox.SetActive(true);
                SceneControl.instance.nameTag1.SetActive(false);
                SceneControl.instance.nameTag.SetActive(false);
                SceneControl.instance.nameTagNPC2.SetActive(false);
                SceneControl.instance.nameTagNPC3.SetActive(true);
                SceneControl.instance.dR.StartDialogue("MichaelQuest");

                //StartCoroutine(MovingCameraNPC3());
                NPC3Interacting = false;
                Dialogue4 = true;


            }
        }
    }

    void OnTV()
    {
        if (turnOnTV)
        {
            //gameManager.sceneChanged = true;
            StartCoroutine(MovingCameraTV());

            if (onTv && !bM.isboxing)
            {

                if (jump.ReadValue<float>() == 1)
                {
                    SelectMinigame.instance.SelectItem();
                }
                if (ReadActionButton())
                {

                    gameManager.p1.turnOnTV = false;
                    gameManager.p2.turnOnTV = false;
                    bM.EndGameInHub();
                    
                }
                if(SelectMinigame.instance.chooseOne && ReadPushButton())
                {
                    bM.StartGameHub();
                }

                if (SelectMinigame.instance.chooseTwo && ReadPushButton())
                {
                    bM.StartGameHub();
                }
            }

        }
        else
        {
            StartCoroutine(MovingCameraTVBack());
        }
    }


    IEnumerator TurnOnLight()
    {
        gameManager.lighting1.SetActive(true);
        gameManager.lighting2.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        gameManager.lighting3.SetActive(true);
        gameManager.lighting1.SetActive(false);
        gameManager.lighting2.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        gameManager.lighting1.SetActive(true);
        gameManager.lighting2.SetActive(true);
        lightsOn = true;

    }
    void EnterOffice()
    {

        if (isEntered)
        {
            gameManager.enterOffice = true;
            //print("Enter Office");
            gameManager.sceneChanged = true;
            gameManager.firstTimeEnterHub = true;

            StartCoroutine(gameManager.MovingCamera1());

            if (!lightsOn)
            {
                StartCoroutine(TurnOnLight());
            }


            if (gameManager.camChanged1)
            {
                GameManager.instance.changeSceneTimes += 1;
                Loader.Load(Loader.Scene.HubStart);

                isEntered = false;
                gameManager.camChanged1 = false;

            }

        }
    }

    IEnumerator MovingCameraTV()
    {
        print("CAM Forward");
        SceneControl.instance.MoveCamera(SceneControl.instance.closeShootTV);
        yield return new WaitForSecondsRealtime(1f);
        onTv = true;
    }

    IEnumerator MovingCameraTVBack()
    {
        print("CAM BACK");
        SceneControl.instance.MoveCamera(SceneControl.instance.camPos);
        yield return new WaitForSecondsRealtime(2f);
        onTv = false;
    }


    //DrawGizons from Player to Entrance
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(playerPos.position + playerPos.forward * pickDistanceHeavy, pickRadiusHeavy);
    }
    #region Old DetectInteractRange
    //public void DetectInteractRange()
    //{
    //    if (Physics.SphereCast(playerPos.position, interactRadius, playerPos.forward, out raycastHit, interactDistance, interactableMask))
    //        if (Physics.Raycast(this.transform.position, this.transform.forward, out raycastHit, interactDistance, interactableMask))
    //        {
    //            withinTVRange = true;

    //        }
    //        else
    //        {
    //            withinTVRange = false;

    //        }

    //    Detect Phone Range
    //    if (Physics.Raycast(this.transform.position, this.transform.forward, out raycastHit, interactPhoneDistance, PhoneLayer))
    //    {
    //        withinPhoneRange = true;

    //    }
    //    else
    //    {
    //        withinPhoneRange = false;

    //    }
    //    if (Physics.Raycast(this.transform.position, this.transform.forward, out raycastHit, doorDistance, postEnter))
    //    //if (Physics.SphereCast(playerPos.position, doorDistance, playerPos.forward, out raycastHit, interactDistance, postEnter))
    //    {

    //        withinEntranceRange = true;
    //        //print("WithinDoorRange");

    //    }
    //    else
    //    {
    //        withinEntranceRange = false;
    //        //gameManager.StopShowDirection();

    //    }

    //    if (Physics.SphereCast(playerPos.position, interactRadius, playerPos.forward, out raycastHit, interactDistance, NPCLayer))
    //        if (Physics.Raycast(this.transform.position, this.transform.forward, out raycastHit, interactDistance, NPCLayer))
    //        {
    //            selectNPC = raycastHit.collider.gameObject;

    //            if (selectNPC.CompareTag("NPC1"))
    //            {
    //                withinNPCsRange = true;
    //            }
    //            else
    //            {
    //                withinNPCsRange = false;
    //            }

    //            if (selectNPC.CompareTag("NPC3"))
    //            {
    //                withinNPC2Range = true;
    //            }
    //            else
    //            {
    //                withinNPC2Range = false;
    //            }

    //            if (selectNPC.CompareTag("NPC4"))
    //            {
    //                withinNPC3Range = true;
    //            }
    //            else
    //            {
    //                withinNPC3Range = false;
    //            }

    //        }
    //        else
    //        {
    //            withinNPCsRange = false;
    //            withinNPC2Range = false;
    //            withinNPC3Range = false;
    //        }

    //}
    #endregion

    #region Push
    void DetectPushRange()
    {
        if (!withinPushingRange)
        {
            if (isPlayer1)
            {
                p1pushed = false;
            }

            if (isPlayer2)
            {
                p2pushed = false;
            }
            pushInstruction.SetActive(false);
        }

        if (withinPushingRange)
        {
            pushInstruction.SetActive(true);
        }
        
    }

    private void DoPush()
    {
        if(isPlayer1 && p1pushed && !pushIsIntervinedP1)
        {
            P1Push();

            //pushIsIntervinedP1 = true;
        }

        if (isPlayer2 && p2pushed && !pushIsIntervinedP2)
        {
            P2Push();
            //pushIsIntervinedP2 = true;
        }

    }
    IEnumerator P1PushCoroutine()
    {
        otherRB = gameManager.p2.charController.rb;
        p2Anim = GameManager.instance.p2Ani;

        otherRB.useGravity = false;

        // Calculate force direction and distance
        Vector3 forceDir = otherRB.transform.position - transform.position;

        float distance = forceDir.magnitude;

        // Normalize the force direction to get the unit vector
        forceDir.Normalize();

        // Calculate the force to be applied

        forceMagnitude1 = pushForce * pushHoldTime;

        if (forceMagnitude1 <= oriPushForce)
        {
            forceMagnitude1 = oriPushForce;
        }
        else
        {
            forceMagnitude1 = pushForce * pushHoldTime;
        }



        float elapsedTime = 0f;
        float duration = 0.3f; // Adjust this based on how long you want the force to be applied

        p2Anim.SetBool("beingPush", true);
        gameManager.p2.facialAnim2.SetBool("isShocked", true);



        StartCoroutine(StopBeingPushedP2());
        //noisy2 = gameManager.noisy2;

        if (rC.Player2isCarrying)
        {
            if (gameManager.curSceneName == "Level1" || gameManager.curSceneName == "MVPLevel")
            {
                if (!bM.isboxing)
                {
                    p1Steal = true;
                    gameManager.p2.objectGrabbable = null;
                }

            }
            else if(gameManager.curSceneName != "Level1" && gameManager.curSceneName != "MVPLevel")
            {
                p1Steal = true;
                gameManager.p2.objectGrabbable = null;
            }

        }

        while (elapsedTime < duration)
        {
            // Apply force based on linear interpolation
            float normalizedTime = elapsedTime / duration;
            float easedMagnitude = Mathf.Lerp(forceMagnitude1, 0f, normalizedTime * normalizedTime);
            otherRB.AddForce(forceDir * easedMagnitude, ForceMode.Force);

            elapsedTime += Time.deltaTime;
            distance = (otherRB.transform.position - transform.position).magnitude;

            // Check if the desired distance is reached (you may need to adjust the threshold)
            if (distance < 0.1f)
            {
                // Stop pushing when the desired distance is reached
                otherRB.useGravity = true;
                yield break; // Exit the coroutine
            }

            yield return null;
        }



    }



    void P1Push()
    {
        StartCoroutine(P1PushCoroutine());

    }

    void P1Damage()
    {
        if ((gameManager.curSceneName == "Level1" || gameManager.curSceneName == "HubStart") && bM != null)
        {
            if (bM.isboxing && isPlayer1 && p1pushed)
            {
                if (!damageApplied)
                {
                    if (forceMagnitude1 <= oriPushForce)
                    {
                        forceMagnitude1 = oriPushForce;
                    }
                    else
                    {
                        forceMagnitude1 = pushForce * pushHoldTime;
                    }


                    bM.p1pushedcount += forceMagnitude1;
                    bM.healthP1.fillAmount = (bM.maxDamage - bM.p1pushedcount) / bM.maxDamage;
                    //print("Damage1" + bM.p1pushedcount);
                    damageApplied = true;

                }

            }
        }
    }

    IEnumerator P2PushCoroutine()
    {
        otherRB = gameManager.p1.charController.rb;
        p1Anim = GameManager.instance.p1Ani;

        otherRB.useGravity = false;

        // Calculate force direction and distance
        Vector3 forceDir = otherRB.transform.position - transform.position;
        
        float distance = forceDir.magnitude;

        // Normalize the force direction to get the unit vector
        forceDir.Normalize();

        // Calculate the force to be applied

        forceMagnitude2 = pushForce * pushHoldTime;

        if (forceMagnitude2 <= oriPushForce)
        {
            forceMagnitude2 = oriPushForce;
        } 
        else
        {
            forceMagnitude2 = pushForce * pushHoldTime;
        }
        
        //print("ForceMagnitude2" + forceMagnitude2);

        float elapsedTime = 0f;
        float duration = 0.3f; // Adjust this based on how long you want the force to be applied

        p1Anim.SetBool("beingPush", true);
        gameManager.p1.facialAnim1.SetBool("isShocked", true);


        StartCoroutine(StopBeingPushedP1());
        //noisy2 = gameManager.noisy2;

        if (rC.Player1isCarrying)
        {
            if (gameManager.curSceneName == "Level1" || gameManager.curSceneName == "MVPLevel")
            {
                if (!bM.isboxing)
                {
                    p2Steal = true;
                    gameManager.p1.objectGrabbable = null;
                }

            } 
            else if (gameManager.curSceneName != "Level1" && gameManager.curSceneName != "MVPLevel")
            {
                p2Steal = true;
                gameManager.p1.objectGrabbable = null;
            }

            

        }

        while (elapsedTime < duration)
        {
            // Apply force based on linear interpolation
            float normalizedTime = elapsedTime / duration;
            float easedMagnitude = Mathf.Lerp(forceMagnitude2, 0f, normalizedTime * normalizedTime);
            otherRB.AddForce(forceDir * easedMagnitude, ForceMode.Force);

            elapsedTime += Time.deltaTime;
            distance = (otherRB.transform.position - transform.position).magnitude;

            // Check if the desired distance is reached (you may need to adjust the threshold)
            if (distance < 0.1f)
            {
                // Stop pushing when the desired distance is reached
                otherRB.useGravity = true;
                yield break; // Exit the coroutine
            }

            yield return null;
        }


    }

    void P2Push()
    {
        StartCoroutine(P2PushCoroutine());
        
    }

    void P2Damage()
    {
        
        if ((gameManager.curSceneName == "Level1" || gameManager.curSceneName == "HubStart") && bM != null)
        {
            if (bM.isboxing && isPlayer2 && p2pushed)
            {
                if (!damageApplied)
                {
                    if (forceMagnitude2 <= oriPushForce)
                    {
                        forceMagnitude2 = oriPushForce;
                    }
                    else
                    {
                        forceMagnitude2 = pushForce * pushHoldTime;
                    }


                    bM.p2pushedcount += forceMagnitude2;
                    bM.healthP2.fillAmount = (bM.maxDamage - bM.p2pushedcount) / bM.maxDamage;
                    print("Damage2" + bM.p2pushedcount);

                    damageApplied = true;
                }

            }

        }
    }

    IEnumerator StopBeingPushedP1()
    {
        OutlineActivate();
        pushCDSlider.gameObject.SetActive(true);
        pushCDtimer += Time.deltaTime;

        if (pushCDtimer < 1)
        {
            pushCDSlider.value = pushCDtimer;
        }
        else
        {
            pushCDSlider.value = 1;
        }
        yield return new WaitForSeconds(0.2f);
        gameManager.p2.p2Anim.SetBool("isPushing", false);

        p1Anim.SetBool("beingPush", false);
        p1Anim.SetBool("isFalling", true);
        p1Anim.SetFloat("speed", 0f);
        pushHoldTime = 0;
        //damageApplied = false;
        OutlineDeActivate();
        gameManager.p2.pushCDSlider.gameObject.SetActive(false);
        pushIsIntervinedP1 = false;

        yield return new WaitForSeconds(1.5f);
        p1Anim.SetBool("isFalling", false);
        gameManager.p1.facialAnim1.SetBool("isShocked", false);
    }

    IEnumerator StopBeingPushedP2()
    {
        OutlineActivate();
        pushCDSlider.gameObject.SetActive(true);
        pushCDtimer += Time.deltaTime;

        if (pushCDtimer < 1)
        {
            pushCDSlider.value = pushCDtimer;
        }
        else
        {
            pushCDSlider.value = maxCDTimer;
        }
        yield return new WaitForSeconds(0.2f);
        gameManager.p1.p1Anim.SetBool("isPushing", false);
        p2Anim.SetBool("beingPush", false);
        p2Anim.SetBool("isFalling", true);
        p2Anim.SetFloat("speed", 0f);
        pushHoldTime = 0;
        //damageApplied = false;
        OutlineDeActivate();
        gameManager.p1.pushCDSlider.gameObject.SetActive(false);
        pushIsIntervinedP2 = false;
        yield return new WaitForSeconds(1.5f);
        p2Anim.SetBool("isFalling", false);
        gameManager.p2.facialAnim2.SetBool("isShocked", true);
    }

    
    #endregion


    void ItemDetector()
    {
        if (isPlayer1 && p2rc != null)
        {
            if (p2rc.Player2Die && rC.Player2isCarrying)
            {
                objectGrabbable = package.GetComponent<ObjectGrabbable>();
                //p2rc.Player2Die = false;
                //GameManager.instance.p2.objectGrabbable = null;

            }
            
            if (rC.Player1Die && rC.Player2isCarrying)
            {
                //Debug.Log("Player1die" + rC.Player1Die);
                objectGrabbable = null;
                //rC.Player1Die = false;

            }
        }
       
        if (isPlayer2 && p1rc != null)
        {
            if (p1rc.Player1Die && rC.Player1isCarrying)
            {
                objectGrabbable = package.GetComponent<ObjectGrabbable>();
                //p1rc.Player1Die = false;
                GameManager.instance.p1.objectGrabbable = null;
            }
            
            if (rC.Player2Die && rC.Player1isCarrying)
            {
                //Debug.Log("Player2die" + rC.Player2Die);
                objectGrabbable = null;
                //rC.Player2Die = false;
            }
        }

    }
    void PlayerDetector()
    {
        string layerNameToFind1 = "P1Collider";
        string layerNameToFind2 = "P2Collider";
        string tagToFind = "FindScript";
        int layerToFind1 = LayerMask.NameToLayer(layerNameToFind1);
        int layerToFind2 = LayerMask.NameToLayer(layerNameToFind2);

        if (!isPlayer1 && !isPlayer2)
        {
            if (this.gameObject.layer == LayerMask.NameToLayer(layerNameToFind1) && !isPlayer1)
            {
                circle1.SetActive(true);
                circle2.SetActive(false);
                isPlayer1 = true;

            }
            if (this.gameObject.layer == LayerMask.NameToLayer(layerNameToFind2) && !isPlayer2)
            {
                circle1.SetActive(false);
                circle2.SetActive(true);
                isPlayer2 = true;
            }

        }

    }


    void Jump()
    {
        //if (isGrounded && jump.ReadValue<float>() == 1 && canJump)
        if (jump.ReadValue<float>() == 1 && canJump && !tooHeavy)
        {
            //jumpSpeed = jumpForce;
            //isJumping = true;
            //canJump = false;
            
            jumpButtonPressedTime = Time.time;

        }
        if (Time.time - lastGroundedTime <= jumpButtonGracePeriod)
        {
            if (Time.time - jumpButtonPressedTime <= jumpButtonGracePeriod)
            {
                if (curSceneName != scene9)
                {
                    jumpSpeed = jumpForce;
                    isJumping = true;
                    canJump = false;
                    jumpButtonPressedTime = null;
                    lastGroundedTime = null;
                }
                else if (isPlayer1 && !rC.Player1isCarrying)
                {
                    jumpSpeed = jumpForce;
                    isJumping = true;
                    canJump = false;
                    jumpButtonPressedTime = null;
                    lastGroundedTime = null;
                    //Debug.Log("1");
                }
                else if (isPlayer2 && !rC.Player2isCarrying)
                {
                    jumpSpeed = jumpForce;
                    isJumping = true;
                    canJump = false;
                    jumpButtonPressedTime = null;
                    lastGroundedTime = null;
                    //Debug.Log("2");
                }

            }
        }

        if (isJumping && jump.ReadValue<float>() == 0 && jumpSpeed <= minJumpForce)
        {
            //rb.velocity -= Vector3.down * Physics.gravity.y * Time.fixedDeltaTime;
            //Debug.Log(jumpSpeed);
            if (jumpSpeed > 0)
            {
                jumpSpeed = jumpSpeed / 2;
            }

            isJumping = false;
        }

        if (jump.ReadValue<float>() == 0)
        {
            canJump = true;

        }

        //print("jump" + jump.ReadValue<float>());
        //print("canJump" + canJump);

        //apply gravity
        if (jumpSpeed > maxFall)
        {
            jumpSpeed += -jumpDeaccel * Time.deltaTime;

        }
        else if (jumpSpeed <= maxFall)
        {
            jumpSpeed = maxFall;
        }

        //handle gliding
        //if(isInAir|| isJumping)
        //{
        //    if(parachute.triggered)
        //    {
        //        isGliding = true;

        //    }
        //    else if (cancelParachute.triggered)
        //    {
        //        isGliding = false;

        //    }

        //    print("isGliding = " + isGliding);
        //} 

        horizontalVelocity = new Vector3(rb.velocity.x, 0, rb.velocity.y).magnitude;

        //forceDirection += Vector3.up * jumpForce;

        //if we have started to move downwards we are not longer jumping
        if (jumpSpeed <= 0) isJumping = false;

        if (isInAir || isJumping)
        {
            if (!isGliding)
            {
                forceDirection += Vector3.up * jumpSpeed;

            }
            else

            if (horizontalVelocity < 9)
            {
                forceDirection += Vector3.up * parachuteSpeed;
            }
            else
            {
                forceDirection += Vector3.up * parachuteSpeed / 1.5f;
            }

        }


    }

    void DoParachute(InputAction.CallbackContext obj)
    {
        if (curSceneName == scene5 || curSceneName == "New CC" || curSceneName == scene10)
        {
            if (!bM.isboxing) { 
            }
            canParachute = true;
            if (isInAir || isJumping)
            {

                if (!isGliding)
                {
                    
                }

                if (!useNewMovement)
                {
                    forceDirection += Vector3.up * parachuteSpeed;
                }
           

                //forceDirection += Vector3.up * parachuteSpeed;

                print("Gliding");
                isGliding = true;
                canJump = false;
            }
        }


    }

    void DoFall(InputAction.CallbackContext obj)
    {

        if (isGliding)
        {
            playerSounds.parachuteClose.Post(this.gameObject);
        }

        isGliding = false;
        //print("Not Gliding");
        //currentStyle = CameraStyle.Basic;
    }

    void ShowParachuteInstruction()
    {
        if(curSceneName == "MVPLevel")
        {
            if (inParachuteArea)
            {
                parachuteInstruction.SetActive(true);
            } 
            else
            {
                parachuteInstruction.SetActive(false);
            }
        }
        else
        {
            parachuteInstruction.SetActive(false);
        }
    }

    private void CheckGrounded()
    {
        //send a spherecast downwards and check for ground, if theres ground we are grounded
        RaycastHit hit;
        if (Physics.SphereCast(groundCheck.position, groundCheckRadius, Vector3.down, out hit, groundCheckDist, groundLayer))
        {
            groundMaterial = hit.transform.tag;

            if (!isGrounded)
            {
                if (groundMaterial == "Metal") playerSounds.metalLand.Post(this.gameObject);
                else if (groundMaterial == "Wood") playerSounds.woodLand.Post(this.gameObject);
                else landEvent.Post(this.gameObject);
            }

            if (isGliding)
            {
                playerSounds.parachuteClose.Post(this.gameObject);
            }
            isGrounded = true;
            isInAir = false;
            isGliding = false;
            lastGroundedTime = Time.time;


            //isGliding = false;

            //Debug.Log("isGrounded" + isGrounded);

        }
        else
        {
            isInAir = true;
            isGrounded = false;
            groundMaterial = "nothing";
            //Debug.Log("isGrounded" + isGrounded);
        }
    }

    [YarnCommand("ChangeScene")]
    public static void GoToLevelScene()
    {
        GameManager.instance.changeSceneTimes += 1;
        GameManager.instance.sceneChanged = true;
        Loader.Load(Loader.Scene.MVPLevel);

    }

    [YarnCommand("SwitchToScoreCards")]
    public static void GoToScoreCards()
    {
        SceneManager.LoadScene("ScoreCards");


    }

    #region Read Button
    public bool ReadPushReleaseButton()
    {
        if (pushRelease.triggered) return true;
        else return false;

    }
    public bool ReadActionButton()
    {
        if (triggerButton.ReadValue<float>() == 1) return true;
        else return false;
    }

    public bool ReadPushButton()
    {
        if (push.ReadValue<float>() == 1) return true;
        else return false;
    }

    public bool ReadCloseTagButton()
    {
        if (close.triggered) return true;
        else return false;
    }

    public bool ReadSkipButton()
    {
        if (skip.triggered) return true;
        else return false;
    }

    public bool ReadSkipTriggerButton()
    {
        if (skipTrigger.ReadValue<float>() == 1) return true;
        else return false;
    }

    //public bool DetectDashButton()
    //{
    //    if (dash.ReadValue<float>() ==1) return true;
    //    else return false;

    //}

    #endregion

    public void OnSummoningEnter(GameObject circle)
    {
        //player can't move unless they let go of running
        //player is now in the summoning animation
        //the summoning circle is active
        //move player towards
        isOnCircle = true;
        activeCircle = circle;
        if (isPlayer1 && !summoningAnimIsTriggered && ReadActionButton())
        {
            //playerAnimator.SetBool("TriggerSummoningButton", true);
            summoningAnimIsTriggered = true;
        } else if (!ReadActionButton())
        {
            playerAnimator.SetBool("TriggerSummoningButton", false);
            summoningAnimIsTriggered = false;
        }

        if (isPlayer2 && !summoningAnimIsTriggered2 && ReadActionButton())
        {
            playerAnimator2.SetBool("TriggerSummoningButton", true);
            summoningAnimIsTriggered2 = true;
        }
        else if (!ReadActionButton())
        {
            playerAnimator.SetBool("TriggerSummoningButton", false);
            summoningAnimIsTriggered2 = false;
        }

        if (ScoreCount.instance != null)
        {
            ScoreCount.instance.AddBadgeValue(BadgeManager.BadgeValues.numButtons, 1, isPlayer1);
        }
        
    }

    IEnumerator TriggerSummoningCircleAnim()
    {
        yield return new WaitForSeconds(0.1f);
        playerAnimator.SetBool("TriggerSummoningButton", true);
    }

    public void OnSummoningExit()
    {
        //player can now move and summoning circle is not active
        //player is no longer in the summoning animation
        isOnCircle = false;
        activeCircle = null;
        if (isPlayer1)
        {
            playerAnimator.SetBool("TriggerSummoningButton", false);
            summoningAnimIsTriggered = false;
        }

        if (isPlayer2)
        {
            playerAnimator2.SetBool("TriggerSummoningButton", false);
            summoningAnimIsTriggered2 = false;
        }
    }

    private void CastBlobShadow()
    {
        RaycastHit hit;

        if (Physics.SphereCast(transform.position, groundCheckRadius, -Vector3.up, out hit, Mathf.Infinity, groundLayer))
        {
            shadowRenderer.SetActive(true);
            shadowRenderer.transform.position = new Vector3(transform.position.x, hit.point.y + 0.1f, transform.position.z);
        }
        else
        {
            shadowRenderer.SetActive(false);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PostOfficeDoor"))
        {
            withinEntranceRange = true;
        }

        if (other.CompareTag("WeatherNPC"))
        {
            if (!SceneControl.instance.wertherIsGone)
            {
                withinNPCsRange = true;
            }
            else
            {
                withinNPCsRange = false;
            }
        }

        if (other.CompareTag("NPC3"))
        {
            if (!SceneControl.instance.lalahIsGone)
            {
                withinNPC2Range = true;
            }
            else
            {
                withinNPC2Range = false;
            }

            
        }

        if (other.CompareTag("NPC4"))
        {
            withinNPC3Range = true;
        }

        if (other.CompareTag("Phone"))
        {
            withinPhoneRange = true;
        }

        if (other.CompareTag("Package"))
        {
            withinPackageRange = true;

        }

        if (other.CompareTag("TV"))
        {
            withinTVRange = true;
        }


    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == ("Geiser") && isGliding)
        {
            //we are now on a geiser
            if (shouldPlayGeiser) playerSounds.windCatch.Post(this.gameObject);
            shouldPlayGeiser = false;
            Vector3 direction = other.transform.position - transform.position;
            float distance = direction.magnitude;

            //rb.AddForce(transform.up * geiserForce / (distance * 100));
        }

        if (other.CompareTag("Puzzle1") && isPlayer1)
        {
            camManager.instance.switchPuzzle1Cam();
            switchPuzzleCam = true;
            //print("Cam1On");
        }

        if (other.CompareTag("Puzzle1") && isPlayer2)
        {
            camManager.instance.switchPuzzle1CamP2();
            switchPuzzleCamP2 = true;
            //print("Cam2On");
        }

        if (other.CompareTag("Puzzle2") && isPlayer1)
        {
            camManager.instance.switchPuzzle2CamL();
            switchPuzzle2CamL = true;
            //print("Cam1On");
        }

        if (other.CompareTag("Puzzle2") && isPlayer2)
        {
            camManager.instance.switchPuzzle2CamR();
            switchPuzzle2CamR = true;
            //print("Cam1On");
        }

        if (other.CompareTag("puzzle2_2") && isPlayer1)
        {
            camManager.instance.switchPuzzle2CamP2();
            switchPuzzle2CamLP2 = true;
            //print("Cam1On");
        }

        if (other.CompareTag("puzzle2_2") && isPlayer2)
        {
            camManager.instance.switchPuzzle2CamP2();
            switchPuzzle2CamRP2 = true;
            //print("Cam2On");
        }

  



    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == ("Geiser"))
        {
            if (!shouldPlayGeiser) playerSounds.windExit.Post(this.gameObject);
            shouldPlayGeiser = true;
        }

        if (other.CompareTag("Puzzle1") && isPlayer1)
        {

            camManager.instance.switchPuzzle1CamBack();
            switchPuzzleCam = false;
            //print("Cam1Off");

        }

        if (other.CompareTag("Puzzle1") && isPlayer2)
        {
            camManager.instance.switchPuzzle1CamBackP2();
            switchPuzzleCamP2 = false;
            //print("Cam2Off");
        }


        if (other.CompareTag("Puzzle2") && isPlayer1)
        {

            camManager.instance.switchPuzzle2CamBack();
            switchPuzzle2CamL = false;
            //print("Cam1Off");

        }

        if (other.CompareTag("Puzzle2") && isPlayer2)
        {
            camManager.instance.switchPuzzle2CamBackR();
            switchPuzzle2CamR = false;
            //print("Cam2Off");
        }

        if (other.CompareTag("puzzle2_2") && isPlayer1)
        {

            camManager.instance.switchPuzzle2CamBackP2L();
            switchPuzzle2CamLP2 = false;
            //print("Cam1Off");

        }

        if (other.CompareTag("puzzle2_2") && isPlayer2)
        {
            camManager.instance.switchPuzzle2CamBackP2();
            switchPuzzle2CamRP2 = false;
            //print("Cam2Off");
        }

        if (other.CompareTag("PostOfficeDoor"))
        {
            withinEntranceRange = false;
        }

        if (other.CompareTag("WeatherNPC"))
        {
            withinNPCsRange = false;
            //print("withinNPCsRange" + withinNPCsRange);
        }

        if (other.CompareTag("NPC3"))
        {
            withinNPC2Range = false;
            //print("withinNPC2Range" + withinNPC2Range);
        }

        if (other.CompareTag("NPC4"))
        {
            withinNPC3Range = false;
        }

        if (other.CompareTag("Phone"))
        {
            withinPhoneRange = false;
        }

        if (other.CompareTag("Package"))
        {
            withinPackageRange = false;

        }

        if (other.CompareTag("TV"))
        {
            withinTVRange = false;
        }
    }

    private void PlayGroundSound(string material)
    {
        if (material == "Metal")
        {
            playerSounds.metalStep.Post(this.gameObject);
        }
        else if (material == "Wood")
        {
            playerSounds.woodStep.Post(this.gameObject);
        }
        else
        {
            playerSounds.steps.Post(this.gameObject);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Wall")
        {
            rb.AddExplosionForce(bounceForce, collision.contacts[0].point, 5);
            //rb.AddForce(collision.contacts[0].normal * bounceForce);
            print("Bounce");
        }
    }
    #region Pull/Push 
    private void GetPullObjects()
    {
        targetObject = null;


        //if (Physics.SphereCast(playerPos.position, PRange, playerPos.forward, out raycastHit, interactDistance, moveableLayer))
        if (Physics.Raycast(this.transform.position, this.transform.forward, out raycastHit, interactDistance, moveableLayer))
        {
            //Gizmos.DrawWireSphere(playerPos.position + playerPos.forward * interactDistance, PRange);
            targetObject = raycastHit.collider.gameObject;
        }


    }


    public bool ReadPullButton()
    {
        if (pull.ReadValue<float>() == 1) return true;
        else return false;

    }

    private void Pull()
    {

        if (targetObject != null)
        {
            targetRigid = targetObject.GetComponent<Rigidbody>();

            if (ReadPullButton())
            {

                isPulling = true;
                targetRigid.useGravity = false;
                targetRigid.mass = 10;
                targetRigid.drag = 0;


                targetObject.transform.SetParent(this.transform);

                newPosition = new Vector3(PPosition.transform.position.x, targetObject.transform.position.y, PPosition.position.z);

                //targetObject.transform.rotation = PPosition.rotation;
                //targetObject.transform.position = newPosition;
                targetObject.transform.position = Vector3.Lerp(targetObject.transform.position, newPosition, currentSpeed * Time.deltaTime * 1.5f);
                isCameraLocked = true;
            }
            else
            {
                targetRigid.useGravity = true;
                isPulling = false;
                targetRigid.drag = 10;
                targetRigid.mass = 150;
                targetObject.transform.SetParent(null);
                newPosition = targetObject.transform.position;
                isCameraLocked = false;
            }

        }
        else
        {
            isPulling = false;
            if (targetRigid != null)
            {
                targetRigid.useGravity = true;
                targetRigid.gameObject.transform.SetParent(null);
            }
            //newPosition = targetObject.transform.position;
            //targetRigid = null;
            isCameraLocked = false;
        }


    }

    #endregion
    private void NewPush()
    {
        if (ReadPushButton())
        {

            holdPush = true;
            isCameraLocked = true;
            pushSlider.gameObject.SetActive(true);

            if (pushHoldDuration < 1)
            {
                pushHoldDuration += Time.deltaTime;
                pushSlider.value = pushHoldDuration;
                if (isPlayer1)
                {
                    powerUp1.SetFloat("_opacity", pushSlider.value + 0.5f);
                    //powerUp1.SetColor("_Glow", new Color(0, 5, 191, 25));
                }
                if (isPlayer2)
                {
                    powerUp2.SetFloat("_opacity", pushSlider.value + 0.5f);
                    //powerUp1.SetColor("_Glow", new Color(0, 91, 191, 25));
                }
                
                //print("fillImage" + pushHoldDuration);

            }
            else
            {
                pushHoldDuration = 1;
                pushSlider.value = maxTimer;

                if (isPlayer1)
                {
                    powerUp1.SetFloat("_opacity", 3);
                    //powerUp1.SetColor("_Glow", new Color(46, 0, 61));
                }
                if (isPlayer2)
                {
                    powerUp2.SetFloat("_opacity", 3);
                    //powerUp1.SetColor("_Glow", new Color(46, 0, 61));
                }

            }

            if (isPlayer1)
            {
                p1Particle.SetActive(true);
                p1Anim.SetBool("isHolding", true);
            }

            if (isPlayer2)
            {
                p2Particle.SetActive(true);
                p2Anim.SetBool("isHolding", true);
            }

            pushHoldTime = pushHoldDuration;
        }
        else
        {
            if (isPlayer1)
            {
                p1Anim.SetBool("isHolding", false);
            }

            if (isPlayer2)
            {
                p2Anim.SetBool("isHolding", false);
            }

            StartCoroutine(RestoreHoldPushForce());

            if (!isPulling)
            {
                isCameraLocked = false;
            }
            if (isPlayer1)
            {
                p1Particle.SetActive(false);
            }

            if (isPlayer2)
            {
                p2Particle.SetActive(false);
            }
            pushSlider.gameObject.SetActive(false);

        }

        //print("PushHoldDuration" + pushHoldDuration);
    }

    IEnumerator RestoreHoldPushForce()
    {
        yield return new WaitForSeconds(0.3f);
        pushHoldDuration = 0;
        holdPush = false;
    }

    public ParticleSystem pushParticle;
    public ParticleSystem chargeParticle;

    private void Push()
    {
        //if (ReadPushButton())
        if(ReadPushReleaseButton())
        {
            if(pushTimer >= pushCd)
            {
                if(isPlayer1)
                {

                    if (withinPushingRange)
                    {
                        p1pushed = true;
                        pushStartTimer = false;
                        p1Anim.SetBool("isPushing", true);
                        

                        if (ScoreCount.instance != null)
                        {
                            ScoreCount.instance.AddBadgeValue(BadgeManager.BadgeValues.numPushes, 1, true);
                            ScoreCount.instance.AddBadgeValue(BadgeManager.BadgeValues.numPushed, 1, false);
                        }

                        pushCDtimer = 0;

                        Invoke(nameof(ResetPush), pushDuration);
                    }

                }

                if (isPlayer2)
                {
                    pushIsIntervinedP2 = false;
                    if (withinPushingRange)
                    {
                        p2pushed = true;
                        pushStartTimer = false;
                        p2Anim.SetBool("isPushing", true);
                        

                        if (ScoreCount.instance != null)
                        {
                            ScoreCount.instance.AddBadgeValue(BadgeManager.BadgeValues.numPushes, 1, false);
                            ScoreCount.instance.AddBadgeValue(BadgeManager.BadgeValues.numPushed, 1, true);
                        }

                        pushCDtimer = 0;
                        Invoke(nameof(ResetPush), pushDuration);
                    }
                    
                }
            }

            pushHoldDuration = 0;
            holdPush = false;
            StopCoroutine(RestoreHoldPushForce());
        }

    }

    private void initBoxing()
    {
        minigame = GameObject.FindGameObjectWithTag("boxing");
        bM = minigame.GetComponent<boxingMinigame>();

        if(curSceneName == "Level1" || curSceneName == "MVPLevel")
        {
            boxcamHolder = GameObject.FindGameObjectWithTag("boxcam");
            if (boxcamHolder != null)
            {
                boxcam = boxcamHolder.GetComponent<Camera>();
            }
        }
    }



    private void ResetPush()
    {

        if (isPlayer1)
        {
            p1pushed = false;
            pushTimer = 0;
            pushStartTimer = true;
            //pushIsIntervinedP1 = false;
            damageApplied = false; 
        }

        if (isPlayer2)
        {
            p2pushed = false;
            pushTimer = 0;
            pushStartTimer = true;
            //pushIsIntervinedP2 = false;
            damageApplied = false;
        }

    }

    public TestCube tc;
    private List<GameObject> platforms = new List<GameObject>();
    private List<Renderer> renderers = new List<Renderer>();
    public Material powerUp1;
    public Material powerUp2;
    bool toggle;

    void OutlineActivate()
    {

        // Get all Renderer components in the GameObject
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        if (toggle == false)
        {
            foreach (Renderer renderer in renderers)
            {
                if (renderer.gameObject.tag != "pushParticle")
                    {
                    // Get the existing materials
                    Material[] materials = renderer.materials;

                    // Add the additional material to the array
                    if (materials.Length <= 1)
                    {
                        Material[] newMaterials = new Material[materials.Length + 1];
                        for (int i = 0; i < materials.Length; i++)
                        {
                            newMaterials[i] = materials[i];
                        }
                        if (isPlayer1)
                        {
                            newMaterials[materials.Length] = powerUp1;
                        } else if (isPlayer2)
                        {
                            newMaterials[materials.Length] = powerUp2;
                        }


                        // Update the materials on the Renderer
                        renderer.materials = newMaterials;
                    }
                }

                

            }
            toggle = true;
        }
    }

    void OutlineDeActivate()
    {
        
        // Get all Renderer components in the GameObject
        Renderer[] renderers = GetComponentsInChildren<Renderer>();

        foreach (Renderer renderer in renderers)
        {
            if (renderer.gameObject.tag != "pushParticle") {
                // Get the existing materials
                Material[] materials = renderer.materials;

            // Remove the last material if there are more than one materials
            if (materials.Length > 1)
            {
                Material[] newMaterials = new Material[materials.Length - 1];
                for (int i = 0; i < newMaterials.Length; i++)
                {
                    newMaterials[i] = materials[i];
                }

                // Update the materials on the Renderer
                renderer.materials = newMaterials;
            }
        }
        }


        toggle = false;
    }

    /*
    float shaderOpacity;
    //public TestCube tc;
    private List<GameObject> platforms = new List<GameObject>();
    private List<Renderer> renderers = new List<Renderer>();
    public Material powerUp;


    bool true1 = true;
    private void PushShader()
    {
        

            // Calculate the progress (0 to 1)
            float progress = Mathf.Clamp01(pushHoldTime / 3);

            Debug.Log(progress);

            // Calculate the new value of _Cutoff_height
            float newCutoffHeight = Mathf.Lerp(0f, 1f, progress);

            Debug.Log(newCutoffHeight);

            // Update the material property
            powerUp.SetFloat("_opacity", newCutoffHeight);

            foreach (Renderer renderer in renderers)
            {

            renderer.material = powerUp;


                if (true1 == true)
                {
                // Get the existing materials
                if (renderer.material != null)
                {



                    Material[] materials = renderer.materials;


                    /*
                    // Add the additional material to the array
                    if (materials.Length <= 1)
                    {
                        Material[] newMaterials = new Material[materials.Length + 1];
                        for (int i = 0; i < materials.Length; i++)
                        {
                            newMaterials[i] = materials[i];
                        }
                        newMaterials[materials.Length] = powerUp;

                        // Update the materials on the Renderer
                        renderer.materials = newMaterials;
                    }
                    
                }


                }



            



        }




    }

    private void pushShaderSetup()
    {
        foreach (Transform child in transform)
        {
            platforms.Add(child.gameObject);


            if (child.gameObject.GetComponent<Renderer>() != null)
            {
                Renderer renderer = child.gameObject.GetComponent<Renderer>();
                renderers.Add(renderer);
            }
            

        }

    }
    */


    #region Dash
    //private void Dash()
    //{
    //    if (DetectDashButton())
    //    {
    //        if(dashCdTimer >= dashCd)
    //        {
    //            isDashing = true;
    //            startTimer = false;

    //            Invoke(nameof(ResetDash), dashDuration);
    //        }  

    //    }
    //}

    //private void ResetDash()
    //{
    //    isDashing = false;
    //    dashCdTimer = 0;
    //    startTimer = true;

    //}

    #endregion

    #region Pause

    private void PauseMenu()
    {

        if (pauseGame.IsPressed() && canToggle)
        {
            print("Pause");
            if (isPaused)
            {
                Resume();
                canToggle = false;
            }
            else
            {
                Pause();
                canToggle = false;
            }
        }

        if (!pauseGame.IsPressed())
        {
            print("!Pause");
            canToggle = true;
        }

        if (isPaused && canMove)
        {
            //joystickValue = pauseJoystick.ReadValue<Vector2>();
            if (joystickValue.y > 0 && selectNum > 0)
            {
                selectNum -= 1;
                canMove = false;
            }
            else if (joystickValue.y < 0 && selectNum < menuOptions.Count - 1)
            {
                selectNum += 1;
                canMove = false;
            }

            for (int i = 0; i < menuOptions.Count; i++)
            {
                if (selectNum == i)
                {
                    menuOptions[i].selected = true;
                    selectedOption = menuOptions[i];
                }
                else
                {
                    menuOptions[i].selected = false;
                }
            }

            if (selectOption.IsPressed() && canPress)
            {
                selectedOption.OnSelect();
                canPress = false;
                if (selectNum == 0)
                {
                    Resume();
                }
            }

            if (!selectOption.IsPressed())
            {
                canPress = true;
            }

            selectNum = Mathf.Clamp(selectNum, 0, menuOptions.Count - 1);

        }

        if (joystickValue.y == 0)
        {
            canMove = true;
        }




    }

    public void Pause()
    {
        //quitMenu.SetActive(false);
        pauseMenu.SetActive(true);
        isPaused = true;
        GameManager.instance.p1.isFreeze = true;
        GameManager.instance.p2.isFreeze = true;
        Time.timeScale = 0;

        menuOptions.Clear();
        foreach (var obj in menuOptionsParent.GetComponentsInChildren<PauseMenuOption>())
        {
            menuOptions.Add(obj.GetComponent<PauseMenuOption>());
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        isPaused = false;
        GameManager.instance.p1.isFreeze = false;
        GameManager.instance.p2.isFreeze = false;
        Time.timeScale = 1;
    }



    #endregion
}



