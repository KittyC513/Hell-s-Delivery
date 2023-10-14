
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using UnityEngine.ProBuilder.Shapes;
using System.Runtime.CompilerServices;
using UnityEngine.SceneManagement;

public class TestCube : MonoBehaviour
{
    Vector2 i_movement;
    Vector3 movement;
    float moveSpeed = 10f;

    [SerializeField] private InputActionAsset inputAsset;
    [SerializeField] private InputActionMap player;
    [SerializeField] private InputAction move, run;
    [SerializeField] public bool isPicking;




    [SerializeField]
    private float movementForce = 1f;

    private Vector3 forceDirection = Vector3.zero;


    private Rigidbody rb;
    [SerializeField]
    private float maxSpeed = 5f;
    [SerializeField]
    private float walkSpeed = 5f;
    [SerializeField]
    private float runSpeed = 12f;
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




    private void Awake()
    {
        inputAsset = this.GetComponent<PlayerInput>().actions;
        player = inputAsset.FindActionMap("Cube");
        rb = this.GetComponent<Rigidbody>();
        testPickDrop = GetComponent<TestPickDrop>();
        playerPos = this.transform;
        maxSpeed = walkSpeed;
        mainCam = Camera.main;

        //gM = GetComponent<GameManager>();
    }

    private void OnEnable()
    {
        //player.FindAction("Move").started += DoMove;
        player.FindAction("Pick").started += DoPick;
        move = player.FindAction("Move");
        player.FindAction("Jump").started += DoJump;
        run = player.FindAction("Run");
        //cameraLook= player.FindAction("CameraLook");
        //pickControl.action.Enable();

        player.Enable();

    }

    private void OnDisable()
    {
        //player.FindAction("Move").started -= DoMove;
        // player.FindAction("MoveUp").started -= MoveUp;
        //player.FindAction("MoveDown").started -= MoveDown;
        //player.FindAction("Pick").started -= DoPick;
        player.FindAction("Pick").started -= DoPick;
        player.FindAction("Jump").started -= DoJump;
        player.Disable();
        //pickControl.action.Disable();

    }

    // Start is called before the first frame update
    void Start()
    {
        // Get the currently active scene
        Scene currentScene = SceneManager.GetActiveScene();
        curSceneName = currentScene.name;
        Debug.Log("Current scene" + curSceneName);

    }

    // Update is called once per frame
    void Update()
    {
        CheckGrounded();
        SpeedControl();
        CheckCamera();


    }
    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if(camTurnoff == true)
        {
            forceDirection += move.ReadValue<Vector2>().x * GetCameraRight(mainCam) * movementForce;
            forceDirection += move.ReadValue<Vector2>().y * GetCameraForward(mainCam) * movementForce;
        }
        else
        {
            forceDirection += move.ReadValue<Vector2>().x * GetCameraRight(cameraComponent) * movementForce;
            forceDirection += move.ReadValue<Vector2>().y * GetCameraForward(cameraComponent) * movementForce;
        }

        forceDirection += move.ReadValue<Vector2>().x * GetCameraRight(mainCam) * movementForce;
        forceDirection += move.ReadValue<Vector2>().y * GetCameraForward(mainCam) * movementForce;
        rb.AddForce(forceDirection, ForceMode.Impulse);
        forceDirection = Vector3.zero;

        if (rb.velocity.y < 0f)
        {
            rb.velocity -= Vector3.down * Physics.gravity.y * Time.fixedDeltaTime * 2;
        }


        Vector3 horizontalVelocity = rb.velocity;
        horizontalVelocity.y = 0;
        if (horizontalVelocity.sqrMagnitude > maxSpeed * maxSpeed)
        {
            if (isRunning)
            {
                maxSpeed = runSpeed;
            }
            else
            {
                maxSpeed = walkSpeed;
            }

            rb.velocity = horizontalVelocity.normalized * maxSpeed + Vector3.up * rb.velocity.y;
            Debug.Log("maxSpeed =" + maxSpeed);
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

    private void DoPick(InputAction.CallbackContext obj)
    {
        //Set up Pick up condition: 1. player is facing the item within the pickup range 2. "Pick" button is pressed
        if (objectGrabbable == null)
        {
            float pickDistance = 10f;
            if (Physics.Raycast(playerPos.position, playerPos.forward, out RaycastHit raycastHit, pickDistance, pickableMask))
            {

                if (raycastHit.transform.TryGetComponent(out objectGrabbable))
                {
                    //transform the item
                    objectGrabbable.Grab(itemContainer);


                }

            }
        }
        else
        {
            if (this.gameObject.layer == LayerMask.NameToLayer("P1Collider"))
            {
                objectGrabbable.P1Drop();
            }

            if (this.gameObject.layer == LayerMask.NameToLayer("P2Collider"))
            {
                objectGrabbable.P2Drop();
            }
            objectGrabbable = null;

        }

    }

    //When player is on the ground and button is pressed, the Jump is triggered
    void DoJump(InputAction.CallbackContext obj)
    {
        if (isGrounded)
        {
            forceDirection += Vector3.up * jumpForce;
        }
    }

    void SpeedControl()
    {
        if (run.ReadValue<float>() == 1)
        {
            isRunning = true;
        }
        else if(run.ReadValue<float>() == 0)
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

            //Debug.Log("isGrounded" + isGrounded);

        }
        else
        {
            isGrounded = false;
            //Debug.Log("isGrounded" + isGrounded);

        }
    }


    void CheckCamera()
    {
        Transform parentTransform = this.transform;

        foreach (Transform child in parentTransform)
        {
            if (child.CompareTag(tagToFindCam) && child == null)
            {
                cam = child;
                Debug.Log("Found GameObject on Tag: " + child.gameObject.name);
            }

            cameraComponent = cam.GetComponent<Camera>();

            if (cameraComponent != null)
            {
                if (curSceneName == scene1)
                {
                    cameraComponent.enabled = false;
                    camTurnoff = true;
                }
                else if (curSceneName == scene2)
                {
                    cameraComponent.enabled = true;
                    camTurnoff = false;
                }
            }

        }





    }


    public void Respawn(Vector3 respawnPos)
    {
        transform.position = respawnPos;
    }

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


