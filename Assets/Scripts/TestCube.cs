
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

public class TestCube : MonoBehaviour
{

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
    [SerializeField] private InputAction move, dash, jump, parachute, cancelParachute, triggerButton, pull, close;
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




    [SerializeField]
    private Transform playerPos;
    [SerializeField]
    private Transform itemContainer;
    [SerializeField]
    private ObjectGrabbable objectGrabbable;
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
    [SerializeField]
    bool withinDialogueRange;
    [SerializeField]
    bool conversationStart;
    bool hubStart, hubEnd;
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
    private RespawnControl rC;
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

    [SerializeField]
    private float parachuteSpeed = -5f;
    [SerializeField]
    private bool isGliding;
    [SerializeField]
    private int numOfButtonPressed;
    [SerializeField]
    private GameObject parachuteObj;


    [SerializeField]
    private float pushForce;
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
    private bool Dialogue1;
    [SerializeField]
    private bool Dialogue2;
    [SerializeField]
    private bool Dialogue3;
    [SerializeField]
    private bool Dialogue4;

    [SerializeField]
    private GameObject selectNPC;
    [SerializeField]
    private bool isDropped;

    [Header("Camera Control")]
    [SerializeField]
    private bool switchPuzzleCam, switchPuzzleCamP2;


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
    private bool tooHeavy;
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


    //[SerializeField]
    //float dropValue;
    //[SerializeField]
    //float dropForce;
    private void Awake()
    {
        inputAsset = this.GetComponent<PlayerInput>().actions;
        player = inputAsset.FindActionMap("Cube");
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

        player.FindAction("Pick").started += DoDrop;

        move = player.FindAction("Move");
        pull = player.FindAction("Pull");
        dash = player.FindAction("Dash");
        close = player.FindAction("Close");

        //player.FindAction("Join").started += DoTalk;

        jump = player.FindAction("Jump");
        triggerButton = player.FindAction("Trigger");


        player.FindAction("Parachute").started += DoParachute;
        player.FindAction("Parachute").canceled += DoFall;
        player.FindAction("Push").started += DoPush;

        continueControl.action.Enable();


        player.Enable();

    }

    private void OnDisable()
    {

        player.FindAction("Pick").started -= DoDrop;

        player.Disable();
        //player.FindAction("Join").started -= DoTalk;
        player.FindAction("Parachute").started -= DoParachute;
        player.FindAction("Parachute").canceled -= DoFall;
        player.FindAction("Push").started -= DoPush;
        continueControl.action.Disable();



        //dialogue.FindAction("ContinueDialogue").started -= DoContinue;
        //pickControl.action.Disable();

    }

    // Start is called before the first frame update
    void Start()
    {

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



    }

    // Update is called once per frame
    void Update()
    {

        DetectDirectionBetweenPlayerAndObject();
        DetectPackageWight();
        CastBlobShadow();
        CheckGrounded();
        //SpeedControl();

        //ContinueBottonControl();
        //MovementCalcs();
        //CheckCamera();
        ItemDetector();
        CameraSwitch();
        AnimationAndSound();

        playerPos = this.transform;
        if (!useNewMovement)
        {
            MovementCalcs();
        }
        else
        {
            charController.RunMovement(playerCamera, isGliding);
        }

        DetectPushRange();

        if (curSceneName == scene1 || curSceneName == scene3 || curSceneName == scene6)
        {
            //DetectInteractRange();
        }
        if (curSceneName == scene5 || curSceneName == scene7 || curSceneName == scene9)
        {

            package = GameObject.FindGameObjectWithTag("Package");
            //package = GameObject.FindGameObjectWithTag("HeavyPackage");

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
            //OnTV();
            Talk();
        }
        if (curSceneName == scene6)
        {
            EnterOffice();
            Interacte();
        }

        Pull();
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
        //if (shouldStep && isGrounded)
        //{
        //    PlayGroundSound(groundMaterial);
        //    lastStepTime = Time.time;
        //    shouldStep = false;
        //}
        //else if (currentSpeed > 0)
        //{
        //    if (Time.time - lastStepTime > (footstepRate / currentSpeed) * Time.deltaTime)
        //    {
        //        shouldStep = true;
        //    }
        //}

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


        }
        else if (curSceneName == scene2 || curSceneName == scene4 || curSceneName == scene5 || curSceneName == scene7 || curSceneName == scene9)
        {
            playerCamera.enabled = true;
            mainCam = null;
            //print("2");

            //if (exButton.instance.inBridge)
            //{
            //    StartCoroutine(exButton.instance.SwitchCamera());
            //}


        }
        else
        {
            playerCamera.enabled = true;
            mainCam = null;
        }

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
                if (!switchPuzzleCam)
                {
                    if (curSceneName == scene1 || curSceneName == scene3 || curSceneName == scene6)
                    {
                        if (!isCameraLocked)
                        {
                            if (!isDashing)
                            {
                                forceDirection += faceDir.z * GetCameraForward(mainCam) * currentSpeed;

                                forceDirection += faceDir.x * GetCameraRight(mainCam) * currentSpeed;
                            }
                            else
                            {
                                forceDirection += faceDir.z * GetCameraForward(mainCam) * dashSpeed;

                                forceDirection += faceDir.x * GetCameraRight(mainCam) * dashSpeed;
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
                                forceDirection += faceDir.x * GetCameraRight(playerCamera) * currentSpeed;
                                forceDirection += faceDir.z * GetCameraForward(playerCamera) * currentSpeed;
                            }
                            else
                            {
                                forceDirection += faceDir.x * GetCameraRight(playerCamera) * dashSpeed;
                                forceDirection += faceDir.z * GetCameraForward(playerCamera) * dashSpeed;
                            }


                        }
                        else
                        {

                            forceDirection += faceDir.x * GetCameraRight(playerCamera) * pullingSpeed;
                            forceDirection += faceDir.z * GetCameraForward(playerCamera) * pullingSpeed;
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
                            forceDirection += faceDir.x * GetCameraRight(camManager.instance.puzzle1Cam) * currentSpeed;
                            forceDirection += faceDir.z * GetCameraForward(camManager.instance.puzzle1Cam) * currentSpeed;
                        }
                        else
                        {
                            forceDirection += faceDir.x * GetCameraRight(camManager.instance.puzzle1Cam) * dashSpeed;
                            forceDirection += faceDir.z * GetCameraForward(camManager.instance.puzzle1Cam) * dashSpeed;
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
                if (!switchPuzzleCamP2)
                {
                    if (curSceneName == scene1 || curSceneName == scene3 || curSceneName == scene6)
                    {
                        if (!isCameraLocked)
                        {
                            if (!isDashing)
                            {
                                forceDirection += faceDir.x * GetCameraRight(mainCam) * currentSpeed;
                                forceDirection += faceDir.z * GetCameraForward(mainCam) * currentSpeed;
                            }
                            else
                            {
                                forceDirection += faceDir.x * GetCameraRight(mainCam) * dashSpeed;
                                forceDirection += faceDir.z * GetCameraForward(mainCam) * dashSpeed;
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
                                forceDirection += faceDir.x * GetCameraRight(playerCamera) * currentSpeed;
                                forceDirection += faceDir.z * GetCameraForward(playerCamera) * currentSpeed;
                            }
                            else
                            {
                                forceDirection += faceDir.x * GetCameraRight(playerCamera) * dashSpeed;
                                forceDirection += faceDir.z * GetCameraForward(playerCamera) * dashSpeed;

                            }



                        }
                        else
                        {
                            forceDirection += faceDir.x * GetCameraRight(playerCamera) * pullingSpeed;
                            forceDirection += faceDir.z * GetCameraForward(playerCamera) * pullingSpeed;
                        }



                    }
                }
                else
                {
                    if (!isCameraLocked)
                    {
                        if (!isDashing)
                        {
                            forceDirection += faceDir.x * GetCameraRight(camManager.instance.puzzle1CamP2) * currentSpeed;
                            forceDirection += faceDir.z * GetCameraForward(camManager.instance.puzzle1CamP2) * currentSpeed;
                        }
                        else
                        {
                            forceDirection += faceDir.x * GetCameraRight(camManager.instance.puzzle1CamP2) * dashSpeed;
                            forceDirection += faceDir.z * GetCameraForward(camManager.instance.puzzle1CamP2) * dashSpeed;
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

        }

        if (!isCameraLocked)
        {
            LookAt();
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
                //if(Physics.SphereCast(playerPos.position, pickDistance, playerPos.forward, out raycastHit, pickDistance, pickableMask))
                if(Physics.Raycast(this.transform.position, this.transform.forward, out raycastHit, pickDistanceHeavy, pickableMask))
                {
                    print("Package");
                    if (isPlayer1 && rC.Player2isCarrying)
                    {
                        print("FoundPackage");
                        if (targetObject == null)
                        {
                            print("TakePackage");
                            objectGrabbable = package.GetComponent<ObjectGrabbable>();
                            objectGrabbable.Grab(itemContainer);
                            playerSounds.packagePick.Post(this.gameObject);
                            GameManager.instance.p2.objectGrabbable = null;
                        }

                    }

                    if (isPlayer2 && rC.Player1isCarrying)
                    {
                        print("FoundPackage");
                        if (targetObject == null)
                        {
                            print("TakePackage");
                            objectGrabbable = package.GetComponent<ObjectGrabbable>();
                            objectGrabbable.Grab(itemContainer);
                            playerSounds.packagePick.Post(this.gameObject);
                            GameManager.instance.p1.objectGrabbable = null;
                        }
                    }
                }
            }
        }
    }
    private void DetectPackageWight()
    {
        if (objectGrabbable != null)
        {
            if (isPlayer1)
            {
                if (rC.Player1isCarrying)
                {
                    if (objectGrabbable.isHeavy)
                    {
                        tooHeavy = true;
                    }
                    else
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
                if (rC.Player2isCarrying)
                {
                    if (objectGrabbable.isHeavy)
                    {
                        tooHeavy = true;
                    }
                    else
                    {
                        tooHeavy = false;
                    }
                }
                else
                {
                    tooHeavy = false;
                }

            }
        }
        else
        {
            tooHeavy = false;
        }



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
                            objectGrabbable = package.GetComponent<ObjectGrabbable>();
                            objectGrabbable.Grab(itemContainer);
                            playerSounds.packagePick.Post(this.gameObject);
                            //TakePackageFunction();
                        }
                    }

                    if (isPlayer2)
                    {
                        if (targetObject == null)
                        {
                            objectGrabbable = package.GetComponent<ObjectGrabbable>();
                            objectGrabbable.Grab(itemContainer);
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
        if (isPlayer1)
        {
            if (rC.Player2isCarrying)
            {

                objectGrabbable = package.GetComponent<ObjectGrabbable>();
                objectGrabbable.Grab(itemContainer);
                playerSounds.packagePick.Post(this.gameObject);
                GameManager.instance.p2.objectGrabbable = null;

            }
            else if (!rC.Player1isCarrying && !rC.Player2isCarrying)
            {
                objectGrabbable = package.GetComponent<ObjectGrabbable>();
                objectGrabbable.Grab(itemContainer);
                playerSounds.packagePick.Post(this.gameObject);
            }

        }

        if (isPlayer2)
        {
            if (rC.Player1isCarrying)
            {

                objectGrabbable = package.GetComponent<ObjectGrabbable>();
                objectGrabbable.Grab(itemContainer);
                playerSounds.packagePick.Post(this.gameObject);
                GameManager.instance.p1.objectGrabbable = null;

            }
            else if (!rC.Player1isCarrying && !rC.Player2isCarrying)
            {
                objectGrabbable = package.GetComponent<ObjectGrabbable>();
                objectGrabbable.Grab(itemContainer);
                playerSounds.packagePick.Post(this.gameObject);
            }

        }
    }

      

    #endregion


    void Interacte()
    {
        if (withinTVRange)
        {
            if (ReadActionButton())
            {
                turnOnTV = true;
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

        if (withinEntranceRange)
        {

            if (ReadActionButton())
            {
                print("Enter");
                isEntered = true;

                //SceneControl.instance.LoadScene("MVPLevel");
                //change scene and enter tutorial level, set gameManger.sceneChanged to true               
            }


        }

        if (withinPhoneRange)
        {
            if (ReadActionButton() && !isAnswered)
            {
                SceneControl.instance.dR.StartDialogue("HubStart");
                isAnswered = true;
            }
        }

    }

    void Talk()
    {
        if (NPCInteracting)
        {
            if (!Dialogue1)
            {
                print("interactiNPC1");
                //SceneControl.LV.SetActive(false);
                SceneControl.instance.dR.StopAllCoroutines();
                SceneControl.instance.phoneUI.SetActive(false);
                SceneControl.instance.dialogueBox.SetActive(true);
                SceneControl.instance.nameTag1.SetActive(true);
                SceneControl.instance.nameTag.SetActive(false);
                SceneControl.instance.dR.StartDialogue("BoomerQuest");

                NPCInteracting = false;
                Dialogue1 = true;
                //StartCoroutine(MovingCameraWerther());

            }
        }

        if (NPC2Interacting)
        {
            if (!Dialogue3)
            {
                print("interactiNPC2");
                //SceneControl.LV.SetActive(false);
                SceneControl.instance.dR.StopAllCoroutines();
                SceneControl.instance.phoneUI.SetActive(false);
                SceneControl.instance.dialogueBox.SetActive(true);
                SceneControl.instance.nameTag1.SetActive(true);
                SceneControl.instance.nameTag.SetActive(false);
                SceneControl.instance.dR.StartDialogue("LalahQuest");

                NPCInteracting = false;
                Dialogue3 = true;
                //StartCoroutine(MovingCameraNPC2());

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
                SceneControl.instance.nameTag1.SetActive(true);
                SceneControl.instance.nameTag.SetActive(false);
                SceneControl.instance.dR.StartDialogue("MichaelQuest");

                //StartCoroutine(MovingCameraNPC3());
                NPCInteracting = false;
                Dialogue4 = true;


            }
        }
    }

    void OnTV()
    {
        if (turnOnTV)
        {
            gameManager.sceneChanged = true;
            StartCoroutine(MovingCameraTV());
            if (onTv)
            {
                Loader.Load(Loader.Scene.MVPLevel);
                onTv = false;
                turnOnTV = false;
            }

        }
    }

    void EnterOffice()
    {

        if (isEntered)
        {
            gameManager.enterOffice = true;
            print("Enter Office");
            gameManager.sceneChanged = true;
            gameManager.firstTimeEnterHub = true;

            StartCoroutine(gameManager.MovingCamera1());
            if (gameManager.camChanged1)
            {
                Loader.Load(Loader.Scene.HubStart);

                isEntered = false;
                gameManager.camChanged1 = false;

            }

        }
    }

    IEnumerator MovingCameraTV()
    {
        SceneControl.instance.MoveCamera(SceneControl.instance.closeShootTV);
        yield return new WaitForSecondsRealtime(2f);
        onTv = true;

    }

    //DrawGizons from Player to Entrance
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawWireSphere(playerPos.position + playerPos.forward * interactDistance, interactRadius);
    //}
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

        //if (Physics.SphereCast(playerPos.position, pushDistance, playerPos.forward, out raycastHit, pushDistance, pushMask))
        if (Physics.Raycast(this.transform.position, this.transform.forward, out raycastHit, pushDistance, pushMask))
        {
            withinPushingRange = true;
            //lastColliderTime = Time.time;
        }
        else
        {
            withinPushingRange = false;
            //if (isPlayer1 && p1Anim != null)
            //{
            //    p2Anim.SetBool("beingPushed", false);
            //    p1pushed = false;

            //}

            //if (isPlayer2 && p2Anim != null)
            //{
            //    p1Anim.SetBool("beingPushed", false);
            //    p2pushed = false;
            //}
        }
    }
    private void DoPush(InputAction.CallbackContext obj)
    {

        if (withinPushingRange)
        {
            if (isPlayer1)
            {
                P1Push();
                //objectGrabbable = null;
                p1pushed = true;


            }

            if (isPlayer2)
            {
                P2Push();
                //objectGrabbable = null;
                p2pushed = true;
            }


        }

    }



    void P1Push()
    {

        otherRB = gameManager.player2.GetComponent<Rigidbody>();
        //p2Anim = gameManager.p2Character.GetComponent<Animator>();
        p2Anim = GameManager.instance.p2Ani;

        otherRB.useGravity = true;

        Vector3 forceDir = otherRB.transform.position - transform.position;

        gameManager.p2.forceDirection += playerDir.forward * pushForce;
        
        //if (curSceneName == scene1 || curSceneName == scene3 || curSceneName == scene6)
        //{
        //    gameManager.p2.forceDirection += playerDir.forward * pushForce;

        //}
        //else
        //{        
        //    gameManager.p2.forceDirection += playerDir.forward * pushForce;

        //    //otherRB.AddForce(playerDir.up * pushForce, ForceMode.Impulse);


        //    //transform.Translate(Vector3.forward * Time.deltaTime * currentSpeed * verticalInput);
        //    //transform.Translate(Vector3.right * Time.deltaTime * currentSpeed * horizontalInput);

        //    //otherRB.AddForce(playerDir.up * pushForce, ForceMode.Impulse);
        //    //gameManager.p2.forceDirection += forceDir.z * gameManager.p2.GetCameraForward(playerCamera) * pushForce;
        //    //gameManager.p2.forceDirection += forceDir.x * gameManager.p2.GetCameraRight(playerCamera) * pushForce;
        //}


        //otherRB.AddForce(forcePosition.normalized * pushForce, ForceMode.Force);

        //float randomTorque = Random.Range(-5f, 5f);
        //otherRB.AddTorque(new Vector3(randomTorque, randomTorque, randomTorque));
        //Vector3 newPosition = Vector3.Lerp(otherRB.transform.position, forcePosition, Time.deltaTime * lerpSpeed);
        //otherRB.MovePosition(newPosition);
        //StartCoroutine(SlideToPosition(forcePosition));

        p2Anim.SetBool("beingPush", true);
        StartCoroutine(StopBeingPushedP2());
        //noisy2 = gameManager.noisy2;

        if (rC.Player2isCarrying)
        {
            p1Steal = true;
            gameManager.p2.objectGrabbable = null;
        }

    }

    void P2Push()
    {

        otherRB = gameManager.player1.GetComponent<Rigidbody>();
        p1Anim = GameManager.instance.p1Ani;

        otherRB.useGravity = false;
        //otherRB.isKinematic = true;
        forceDir = otherRB.transform.position - transform.position;

        Vector3 forcePosition = gameManager.player1.transform.position + forceDir * pushForce;

        gameManager.p1.forceDirection += playerDir.forward * pushForce;

        //gameManager.p1.forceDirection += gameManager.player1.transform.position * forceDir.z * pushForce;
        //gameManager.p1.forceDirection += gameManager.player1.transform.position * forceDir.x * pushForce;

        //if (curSceneName == scene1 || curSceneName == scene3 || curSceneName == scene6)
        //{

        //    gameManager.p1.forceDirection += forceDir.z * GetCameraForward(mainCam) * pushForce;
        //    gameManager.p1.forceDirection += forceDir.x * GetCameraRight(mainCam) * pushForce;
        //}
        //else
        //{
        //    gameManager.p1.forceDirection += forceDir.z * gameManager.p1.GetCameraForward(playerCamera) * pushForce;
        //    gameManager.p1.forceDirection += forceDir.x * gameManager.p1.GetCameraRight(playerCamera) * pushForce;
        //}

        //otherRB.AddForce(forcePosition.normalized * pushForce, ForceMode.Force);

        //float randomTorque = Random.Range(-5f, 5f);
        //otherRB.AddTorque(new Vector3(randomTorque, randomTorque, randomTorque));
        //Vector3 newPosition = Vector3.Lerp(otherRB.transform.position, forcePosition, Time.deltaTime * lerpSpeed);
        //otherRB.MovePosition(newPosition);
        //StartCoroutine(SlideToPosition(forcePosition));

        p1Anim.SetBool("beingPush", true);
        StartCoroutine(StopBeingPushedP1());
        //noisy1 = gameManager.noisy1;

        if (rC.Player1isCarrying)
        {
            p2Steal = true;
            gameManager.p1.objectGrabbable = null;

        }
    }

    IEnumerator StopBeingPushedP1()
    {
        yield return new WaitForSeconds(1f);
        p1Anim.SetBool("beingPush", false); ;
    }

    IEnumerator StopBeingPushedP2()
    {
        yield return new WaitForSeconds(1f);
        p2Anim.SetBool("beingPush", false); ;
    }

    //IEnumerator SlideToPosition(Vector3 targetPosition)
    //{
    //    float elapsedTime = 0f;

    //    while (elapsedTime < slideTime)
    //    {
    //        otherRB.MovePosition(Vector3.Lerp(startingPosition, targetPosition, elapsedTime / slideTime));
    //        elapsedTime += Time.deltaTime;
    //        yield return null;
    //    }

    //    // Ensure the object reaches the exact target position at the end
    //    otherRB.MovePosition(targetPosition);
    //    yield return null;

    //}

    #endregion


    void ItemDetector()
    {
        if (isPlayer1 && p2rc != null)
        {
            if (p2rc.Player2Die && rC.Player2isCarrying)
            {
                objectGrabbable = package.GetComponent<ObjectGrabbable>();
                p2rc.Player2Die = false;
                GameManager.instance.p2.objectGrabbable = null;

            }
            else if (rC.Player1Die && rC.Player2isCarrying)
            {
                Debug.Log("Player1die" + rC.Player1Die);
                objectGrabbable = null;
                rC.Player1Die = false;

            }
        }
       
        if (isPlayer2 && p1rc != null)
        {
            if (p1rc.Player1Die && rC.Player1isCarrying)
            {
                objectGrabbable = package.GetComponent<ObjectGrabbable>();
                p1rc.Player1Die = false;
                GameManager.instance.p1.objectGrabbable = null;
            }
            else if (rC.Player2Die && rC.Player1isCarrying)
            {
                Debug.Log("Player2die" + rC.Player2Die);
                objectGrabbable = null;
                rC.Player2Die = false;
            }
        }

        //if (isPlayer2)
        //{
        //    if (p1rc.Player1Die)
        //    {
        //        objectGrabbable = package.GetComponent<ObjectGrabbable>();
        //        p1rc.Player1Die = false;
        //    }
        //    else if (rC.Player2Die)
        //    {
        //        Debug.Log("Player2die" + rC.Player2Die);
        //        objectGrabbable = null;
        //        rC.Player2Die = false;
        //    }
        //}



    }
    void PlayerDetector()
    {
        string layerNameToFind1 = "P1Collider";
        string layerNameToFind2 = "P2Collider";
        string tagToFind = "FindScript";
        int layerToFind1 = LayerMask.NameToLayer(layerNameToFind1);
        int layerToFind2 = LayerMask.NameToLayer(layerNameToFind2);

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

        GameObject[] objectsInScene = GameObject.FindObjectsOfType<GameObject>();
        if (isPlayer1 && p2rc == null)
        {
            // Debug.Log("Trigger1");
            foreach (GameObject obj in objectsInScene)
            {
                if (obj.layer == layerToFind2)
                {
                    Debug.Log("Found GameObject on layer: " + obj.name);
                    Transform parentTransform = obj.transform;

                    foreach (Transform child in parentTransform)
                    {
                        if (child.CompareTag(tagToFind))
                        {
                            p2rc = child.gameObject.GetComponent<RespawnControl>();
                            Debug.Log("Found GameObject on Tag: " + child.gameObject.name);
                        }
                    }
                }

            }

        }

        if (isPlayer2 && p1rc == null)
        {
            Debug.Log("Trigger1");
            foreach (GameObject obj in objectsInScene)
            {

                if (obj.layer == layerToFind1)
                {
                    Debug.Log("Found GameObject on layer: " + obj.name);
                    Transform parentTransform = obj.transform;

                    foreach (Transform child in parentTransform)
                    {
                        if (child.CompareTag(tagToFind))
                        {
                            p1rc = child.gameObject.GetComponent<RespawnControl>();
                            Debug.Log("Found GameObject on Tag: " + child.gameObject.name);
                        }
                    }
                }

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

        if (isGliding)
        {
            parachuteObj.SetActive(true);
        }
        else
        {
            parachuteObj.SetActive(false);
        }


    }

    void DoParachute(InputAction.CallbackContext obj)
    {
        if (curSceneName == scene5)
        {
            if (isInAir || isJumping)
            {

                if (!isGliding)
                {
                    playerSounds.parachuteOpen.Post(this.gameObject);
                }

                forceDirection += Vector3.up * parachuteSpeed;

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

        Loader.Load(Loader.Scene.MVPLevel);

    }

    [YarnCommand("SwitchToScoreCards")]
    public static void GoToScoreCards()
    {
        SceneManager.LoadScene("ScoreCards");


    }

    #region Read Button
    public bool ReadActionButton()
    {
        if (triggerButton.ReadValue<float>() == 1) return true;
        else return false;
    }

    public bool ReadCloseTagButton()
    {
        if (close.triggered) return true;
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
    }

    public void OnSummoningExit()
    {
        //player can now move and summoning circle is not active
        //player is no longer in the summoning animation
        isOnCircle = false;
        activeCircle = null;
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


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == ("Geiser") && isGliding)
        {
            //we are now on a geiser
            if (shouldPlayGeiser) playerSounds.windCatch.Post(this.gameObject);
            shouldPlayGeiser = false;
            rb.AddForce(Vector3.up * geiserForce);
        }

        if (other.CompareTag("Puzzle1") && isPlayer1)
        {
            camManager.instance.switchPuzzle1Cam();
            switchPuzzleCam = true;
            print("Cam1On");
        }

        if (other.CompareTag("Puzzle1") && isPlayer2)
        {
            camManager.instance.switchPuzzle1CamP2();
            switchPuzzleCamP2 = true;
            print("Cam2On");
        }

        if (other.CompareTag("PostOfficeDoor"))
        {
            withinEntranceRange = true;
        }

        if (other.CompareTag("NPC1"))
        {
            withinNPCsRange = true;
            print("withinNPCsRange" + withinNPCsRange);
        }

        if (other.CompareTag("NPC3"))
        {
            withinNPC2Range = true;
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
            print("Cam1Off");

        }

        if (other.CompareTag("Puzzle1") && isPlayer2)
        {
            camManager.instance.switchPuzzle1CamBackP2();
            switchPuzzleCamP2 = false;
            print("Cam2Off");
        }

        if (other.CompareTag("PostOfficeDoor"))
        {
            withinEntranceRange = false;
        }

        if (other.CompareTag("NPC1"))
        {
            withinNPCsRange = false;
            print("withinNPCsRange" + withinNPCsRange);
        }

        if (other.CompareTag("NPC3"))
        {
            withinNPC2Range = false;
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

}



