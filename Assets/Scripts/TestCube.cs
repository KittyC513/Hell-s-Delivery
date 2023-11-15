
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
    [SerializeField] private InputActionMap player, dialogue;
    [SerializeField] private InputAction move, run, jump, parachute, cancelParachute, triggerButton;
    [SerializeField] public bool isPicking;

    private bool isOnCircle;
    private GameObject activeCircle;

    [SerializeField]
    private float movementForce = 1f;

    private Vector3 forceDirection = Vector3.zero;

    [SerializeField]
    private Animator playerAnimator;

    private Rigidbody rb;
    [SerializeField]
    private float maxSpeed = 5f;
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
    private float timeToRun = 0.16f;
    [SerializeField]
    private float timeToWalk = 0.1f;
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
    private float jumpButtonGracePeriod;

    private float? lastGroundedTime;
    private float? jumpButtonPressedTime;

    [SerializeField]
    private Camera playerCamera;
    [SerializeField]
    Camera mainCam;
    [SerializeField]
    TestPickDrop testPickDrop;



    [SerializeField]
    private LayerMask pickableMask;
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
    bool isPlayer1;
    [SerializeField]
    bool isPlayer2;
    [SerializeField]
    private GameObject package;
    [SerializeField]
    private float pickDistance;
    [SerializeField]
    private RaycastHit raycastHit;
    [SerializeField]
    private bool isCast;
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
    private float parachuteSpeed = -2f;
    [SerializeField]
    private bool isGliding;
    [SerializeField]
    private int numOfButtonPressed;
    [SerializeField]
    private GameObject parachuteObj;




    [Header("Camera Control")]
    //public CameraStyle currentStyle;
    public GameObject thirdPersonCam;
    public GameObject combatCam;
    public GameObject topDownCam;
    public GameObject aimCursor;
    public bool isAiming;

    //public enum CameraStyle
    //{
    //    Basic,
    //    Combat,
    //    Topdown
    //}

    [Space, Header("Wwise Stuff")]
    [SerializeField] private AK.Wwise.Event footstepEvent;
    [SerializeField] private float footstepRate = 500;
    private bool shouldStep = true;
    private float lastStepTime = 0;

    private void Awake()
    {
        inputAsset = this.GetComponent<PlayerInput>().actions;
        player = inputAsset.FindActionMap("Cube");
        dialogue = inputAsset.FindActionMap("Dialogue");
        rb = this.GetComponent<Rigidbody>();
        testPickDrop = GetComponent<TestPickDrop>();
        //playerPos = this.transform;
        maxSpeed = walkSpeed;
        mainCam = Camera.main;
        


        //gM = GetComponent<GameManager>();
    }

    private void OnEnable()
    {

        player.FindAction("Pick").started += DoPick;
        move = player.FindAction("Move");

        run = player.FindAction("Run");
        player.FindAction("Join").started += DoTalk;

        jump = player.FindAction("Jump");
        triggerButton = player.FindAction("Trigger");

        player.FindAction("Parachute").started += DoParachute;
        player.FindAction("Parachute").canceled += DoFall;


        continueControl.action.Enable();


        player.Enable();

    }

    private void OnDisable()
    {

        player.FindAction("Pick").started -= DoPick;

        player.Disable();
        player.FindAction("Join").started -= DoTalk;
        player.FindAction("Parachute").started -= DoParachute;
        player.FindAction("Parachute").canceled -= DoFall;

        continueControl.action.Disable();



        //dialogue.FindAction("ContinueDialogue").started -= DoContinue;
        //pickControl.action.Disable();

    }

    // Start is called before the first frame update
    void Start()
    {
        // Get the currently active scene
        currentScene = SceneManager.GetActiveScene();
        curSceneName = currentScene.name;
        Debug.Log("Current scene" + curSceneName);
        dR = Object.FindAnyObjectByType<DialogueRunner>();
        //lineView = FindAnyObjectByType<LineView>();

        //objectGrabbable = Object.FindAnyObjectByType<ObjectGrabbable>();

        withinDialogueRange = false;
        package = GameObject.FindGameObjectWithTag("Package");
        //trigger = GameObject.FindGameObjectWithTag("Trigger");
        //tG = trigger.GetComponent<Trigger>();

        parachuteObj.SetActive(false);
        canJump = true;
        lastStepTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        CastBlobShadow();
        CheckGrounded();
        SpeedControl();

        ContinueBottonControl();
        //MovementCalcs();
        //CheckCamera();
        ItemDetector();
        CameraSwitch();

        playerPos = this.transform;

        MovementCalcs();

        //CameraControl();


    }

    private void LateUpdate()
    {
        PlayerDetector();
        CastBlobShadow();
    }

    private void FixedUpdate()
    {
        if (!isFreeze)
        {
            Move();
            Jump();
        }



    }

    void ContinueBottonControl()
    {
        if (continueControl.action.triggered)
        {
            Debug.Log("Hello");
            lineView = FindObjectOfType<LineView>();
            lineView.OnContinueClicked();
        }
    }


    private void MovementCalcs()
    {
        if (isFreeze)
        {
            playerAnimator.SetFloat("speed", 0);
        }
        else
        {
            playerAnimator.SetFloat("speed", currentSpeed);
        }

        if (move.ReadValue<Vector2>().x != 0 || move.ReadValue<Vector2>().y != 0)
        {
            //we are moving
            faceDir = new Vector3(move.ReadValue<Vector2>().x, 0, move.ReadValue<Vector2>().y);
            isWalking = true;

            
            if (isRunning)
            {
                float accel = (maxSpeed / timeToRun);
                currentSpeed += accel * Time.deltaTime;

            }
            else
            {
                float accel = (maxSpeed / timeToWalk);
                currentSpeed += accel * Time.deltaTime;
            }


            if (shouldStep)
            {
                footstepEvent.Post(this.gameObject);
                lastStepTime = Time.time;
                shouldStep = false;
            }
            else if (currentSpeed > 0)
            {
                if (Time.time - lastStepTime > (footstepRate / currentSpeed) * Time.deltaTime)
                {
                    shouldStep = true;
                }
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
        if (curSceneName == scene1 || curSceneName == scene3 && curSceneName != scene2)
        {
            playerCamera.enabled = false;

        }
        else if (curSceneName == scene2 && curSceneName != scene1 || curSceneName != scene3)
        {
            playerCamera.enabled = true;

        }
    }
    private void Move()
    {
        float forceAdd = timeToWalk;
        if (!isOnCircle)
        {
            if (curSceneName == scene1 || curSceneName == scene3)
            {
                forceDirection += faceDir.x * GetCameraRight(mainCam) * currentSpeed;
                forceDirection += faceDir.z * GetCameraForward(mainCam) * currentSpeed;
            }
            else if (curSceneName == scene2 || curSceneName == scene4 || curSceneName == scene5)
            {
                if (isGliding) 
                {
                    currentSpeed = gliderSpeed;
                }


                forceDirection += faceDir.x * GetCameraRight(playerCamera) * currentSpeed;
                forceDirection += faceDir.z * GetCameraForward(playerCamera) * currentSpeed;
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
                if (isRunning)
                {
                    maxSpeed = runSpeed;
                }
                else
                {
                    maxSpeed = walkSpeed;
                }
            }

            rb.velocity = horizontalVelocity.normalized * currentSpeed + Vector3.up * rb.velocity.y;

            //rb.velocity = new Vector3((forceDirection.x * currentSpeed) * Time.deltaTime, rb.velocity.y, (forceDirection.z * currentSpeed) * Time.deltaTime);
            //Debug.Log("maxSpeed =" + maxSpeed);

        }

        LookAt();
    }

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

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(playerPos.position, pickDistance);
    }

    private void DoPick(InputAction.CallbackContext obj)
    {
        //Set up Pick up condition: 1. player is facing the item within the pickup range 2. "Pick" button is pressed
        if (objectGrabbable == null)
        {
            Debug.Log("Object in range:" + Physics.SphereCast(playerPos.position, pickDistance, playerPos.forward, out raycastHit, pickDistance, pickableMask));
            if (Physics.SphereCast(playerPos.position, pickDistance, playerPos.forward, out raycastHit, pickDistance, pickableMask))
            {

                if (isPlayer1)
                {
                    objectGrabbable = package.GetComponent<ObjectGrabbable>();
                    objectGrabbable.Grab(itemContainer);
                }

                if (isPlayer2)
                {
                    objectGrabbable = package.GetComponent<ObjectGrabbable>();
                    objectGrabbable.Grab(itemContainer);
                }

                //if (raycastHit.transform.TryGetComponent(out objectGrabbable) && isPlayer1)
                //{
                //    objectGrabbable.Grab(itemContainer);

                //}

                //if (raycastHit.transform.TryGetComponent(out objectGrabbable) && isPlayer2)
                //{

                //    objectGrabbable.Grab(itemContainer);
                //}

            }

        }
        else
        {
            if (isPlayer1 && rC.Player1isCarrying)
            {
                objectGrabbable.P1Drop();

            }

            if (isPlayer2 && rC.Player2isCarrying)
            {
                objectGrabbable.P2Drop();
            }
            objectGrabbable = null;

        }

    }




    void ItemDetector()
    {
        if (isPlayer1 && p2rc != null)
        {
            if (p2rc.Player2Die && rC.Player2isCarrying)
            {
                objectGrabbable = package.GetComponent<ObjectGrabbable>();
                p2rc.Player2Die = false;

            }
            else if (rC.Player1Die && rC.Player2isCarrying)
            {
                Debug.Log("Player1die" + rC.Player1Die);
                objectGrabbable = null;
                rC.Player1Die = false;

            }
        }
        else if (isPlayer2 && p1rc != null)
        {
            if (p1rc.Player1Die && rC.Player1isCarrying)
            {
                objectGrabbable = package.GetComponent<ObjectGrabbable>();
                p1rc.Player1Die = false;
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
            isPlayer1 = true;
        }
        if (this.gameObject.layer == LayerMask.NameToLayer(layerNameToFind2) && !isPlayer2)
        {
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

    //When player is on the ground and button is pressed, the Jump is triggered
    //void DoJump(InputAction.CallbackContext obj)
    //{
    //    if (isGrounded && !isFreeze)
    //    {
    //        forceDirection += Vector3.up * jumpForce;
    //    }
    //}

    void Jump()
    {
        //if (isGrounded && jump.ReadValue<float>() == 1 && canJump)
        if (jump.ReadValue<float>() == 1 && canJump)
        {
            //jumpSpeed = jumpForce;
            //isJumping = true;
            //canJump = false;
            jumpButtonPressedTime = Time.time;

        }
        if(Time.time - lastGroundedTime <= jumpButtonGracePeriod)
        {
            if(Time.time - jumpButtonPressedTime <= jumpButtonGracePeriod)
            {
                jumpSpeed = jumpForce;
                isJumping = true;
                canJump = false;
                jumpButtonPressedTime = null;
                lastGroundedTime = null;
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

        print("jump" + jump.ReadValue<float>());
        print("canJump" + canJump);
             
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


        //forceDirection += Vector3.up * jumpForce;

        //if we have started to move downwards we are not longer jumping
        if (jumpSpeed <= 0) isJumping = false;

        if (isInAir || isJumping)
        {
            if (!isGliding)
            {
                forceDirection += Vector3.up * jumpSpeed;
                
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

        if (isInAir || isJumping)
        {
            forceDirection += Vector3.up * parachuteSpeed;
            print("Gliding");
            isGliding = true;
            canJump = false;
        }
    }

    void DoFall(InputAction.CallbackContext obj)
    {
        isGliding = false;
        print("Not Gliding");
        //currentStyle = CameraStyle.Basic;
    }




    void SpeedControl()
    {
        if (run.ReadValue<float>() == 1)
        {
            isRunning = true;
        }
        else if (run.ReadValue<float>() == 0)
        {
            isRunning = false;
        }

    }

    private void CheckGrounded()
    {
        //send a spherecast downwards and check for ground, if theres ground we are grounded
        RaycastHit hit;
        if (Physics.SphereCast(groundCheck.position, groundCheckRadius, Vector3.down, out hit, groundCheckDist, groundLayer))
        {
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

            //Debug.Log("isGrounded" + isGrounded);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "NPC1")
        {
            withinDialogueRange = true;
            hubStart = true;

        }
        else if (other.gameObject.tag == "NPC2")
        {
            withinDialogueRange = true;
            hubEnd = true;
        }
    }


    //void DoContinue(InputAction.CallbackContext obj)
    //{

    //    ContinueBotton.instance.PressContinue();

    //}

    void DoTalk(InputAction.CallbackContext obj)
    {
        if (withinDialogueRange)
        {
            if (!conversationStart && hubStart == true)
            {
                dR.StartDialogue("BoomerQuest");
                conversationStart = true;

                lineView = FindObjectOfType<LineView>();
                withinDialogueRange = false;
            }

            if (!conversationStart && hubEnd == true)
            {
                dR.StartDialogue("HubEnd");
                conversationStart = true;

                lineView = FindObjectOfType<LineView>();
                withinDialogueRange = false;
            }

        }
    }


    //void DoContinue(InputAction.CallbackContext obj)
    //{
    //    lineView.OnContinueClicked();
    //}

    //void ContinueBottonControl()
    //{
    //    if (continueControl.action.triggered)
    //    {
    //        Debug.Log("Hello");
    //        lineView = FindObjectOfType<LineView>();
    //        lineView.OnContinueClicked();
    //    }
    //}

    [YarnCommand("ChangeScene")]
    public static void GoToLevelScene()
    {
        SceneManager.LoadScene("PrototypeLevel");

    }

    [YarnCommand("SwitchToScoreCards")]
    public static void GoToScoreCards()
    {
        SceneManager.LoadScene("ScoreCards");

    }

    public bool ReadActionButton()
    {
        if (triggerButton.ReadValue<float>() == 1) return true;
        else return false;
    }

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



    //void SwitchCameraStyle(CameraStyle newStyle)
    //{
    //    combatCam.SetActive(false);
    //    thirdPersonCam.SetActive(false);
    //    topDownCam.SetActive(false);
    //    //aimCursor.SetActive(false);

    //    if (newStyle == CameraStyle.Basic)
    //    {
    //        thirdPersonCam.SetActive(true);
    //    }

    //    if (newStyle == CameraStyle.Combat)
    //    {
    //        combatCam.SetActive(true);
    //        aimCursor.SetActive(true);
    //    }

    //    if (newStyle == CameraStyle.Topdown)
    //    {
    //        topDownCam.SetActive(true);
    //    }

    //    currentStyle = newStyle;
    //}

    //void DoAiming(InputAction.CallbackContext obj)
    //{
    //    isAiming = true;
    //    print("isAiming" + isAiming);
    //}

    //void CancelAiming(InputAction.CallbackContext obj)
    //{
    //    isAiming = false;
    //    print("isAiming" + isAiming);
    //}

    //void CameraControl()
    //{
    //    if (isAiming)
    //    {
    //        currentStyle = CameraStyle.Combat;
    //        SwitchCameraStyle(currentStyle);
    //    }
    //    else
    //    {
    //        currentStyle = CameraStyle.Basic;
    //        SwitchCameraStyle(currentStyle);
    //    }
    //}

}

    


    /*
    private void Move()
    {
        Vector3 movement = new Vector3(i_movement.x, 0, i_movement.y) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);
    }

    private void Move(InputValue value)
    {
        i_movement = value.Get<Vector2> ();
        Debug.Log("Moving");
    }
    private void MoveDown(InputAction.CallbackContext obj)
    {
        transform.Translate(transform.up);
        Debug.Log("Moving");
    }
    private void MoveUp(InputAction.CallbackContext obj)
    {
        transform.Translate(-transform.up);
        Debug.Log("Moving");
    }
    */


