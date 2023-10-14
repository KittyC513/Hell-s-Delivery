
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class TestCube : MonoBehaviour
{
    [Header("Movement")]
    Vector2 i_movement;
    Vector3 movement;
    float moveSpeed = 10f;

    [SerializeField]
    private float playerWalkSpeed = 8.5f;
    [SerializeField]
    private float playerRunSpeed = 12;
    [SerializeField]
    private float timeToWalkSpeed = 0.15f;
    [SerializeField]
    private float timeToZero = 0.05f;
    [SerializeField]
    private float timeToRunSpeed = 0.2f;

    [SerializeField] private InputActionAsset inputAsset;
    [SerializeField] private InputActionMap player;
    [SerializeField] private InputAction move,pick,jump,run;
    [SerializeField] public bool isPicking;
    [SerializeField] InputActionReference pickControl;
    [SerializeField]
    private float movementForce = 1f;
    private Vector3 forceDirection = Vector3.zero;
    private Rigidbody rb;
    [SerializeField]
    private float maxSpeed = 5f;
    [SerializeField]
    private Camera playerCamera;

    [Header("Pick&Drop")]
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

    [Header("GroundCheck")]
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    LayerMask groundLayer;
    [SerializeField]
    private bool isGrounded = false;
    [SerializeField]
    private float groundCheckRadius = 0.33f;
    [SerializeField]
    private float groundCheckDist = 0.75f;

    [SerializeField]
    private float currentSpeed; 
    private bool running = false;

    [Header("Jump Variables")]
    [SerializeField]
    private float jumpSpeed;
    [SerializeField]
    private float jumpForce = 15;
    [SerializeField]
    private float jumpDeaccel = 12f;
    [SerializeField]
    private float minJumpForce = 6;
    [SerializeField]
    private float maxFall = -35;
    private bool isJumping = false;

    [Header("Other")]
    [SerializeField]
    private Animator playerAnim;
    private bool isOnCircle = false;
    private GameObject activeCircle;
    [SerializeField]
    private Sprite shadowSprite;
    [SerializeField]
    private GameObject shadowRenderer;

    [Header("Player Status")]
    public playerState pState;
    public enum playerState { idle, walking, running, jumping, airborne, summoning }




    private void Awake()
    {
        inputAsset = this.GetComponent<PlayerInput>().actions;
        player = inputAsset.FindActionMap("Cube");
        rb = this.GetComponent<Rigidbody>();
        testPickDrop = GetComponent<TestPickDrop>();
        playerPos = this.transform;
    }

    private void OnEnable()
    {
        jump = player.FindAction("Jump");
        player.FindAction("Pick").started += DoPick;
        move = player.FindAction("Move");
        run = player.FindAction("Run");
        //player.FindAction("Pick").started += DoPick;
        //cameraLook= player.FindAction("CameraLook");
        //pickControl.action.Enable();

        player.Enable();

    }

    private void OnDisable()
    {
        // player.FindAction("MoveUp").started -= MoveUp;
        //player.FindAction("MoveDown").started -= MoveDown;
        //player.FindAction("Pick").started -= DoPick;
        player.Disable();
        //pickControl.action.Disable();
        player.FindAction("Pick").started -= DoPick;
    }
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckGrounded();
        CastBlobShadow();




    }
    private void FixedUpdate()
    {
        MovePlayer();
    }

    #region Player Movement & Orientation
    private void MovePlayer()
    {
     /*
        if(forceDirection.x != 0 && forceDirection.z != 0)
        {
            currentSpeed = maxSpeed;
            if (run.ReadValue<float>() == 1)
            {
                currentSpeed += ((playerRunSpeed - 0) / timeToRunSpeed) * Time.deltaTime;
                if (currentSpeed < playerWalkSpeed / 2)
                {
                    currentSpeed = playerWalkSpeed;
                }
            }
            else if (currentSpeed < playerWalkSpeed)
            {
                currentSpeed += ((playerWalkSpeed - 0) / timeToWalkSpeed) * Time.deltaTime;
            }
        } else if(currentSpeed > 0)
        {
            currentSpeed += ((0 - playerWalkSpeed) / timeToZero) * Time.deltaTime;
        }

        if (running)
        {
            currentSpeed = Mathf.Clamp(currentSpeed, 0, playerRunSpeed);
        }
        else
        {
            //if our current speed is more than our walk speed and we let go of running
            //slow down a little gradually back to walk for weight
            if (currentSpeed > playerWalkSpeed + 5)
            {
                currentSpeed += ((0 - playerWalkSpeed) / 0.6f) * Time.deltaTime;

            }
            else
            {
                currentSpeed = Mathf.Clamp(currentSpeed, 0, playerWalkSpeed);
            }
        }
     */
        forceDirection += move.ReadValue<Vector2>().x * GetCameraRight(playerCamera) * movementForce;
        forceDirection += move.ReadValue<Vector2>().y * GetCameraForward(playerCamera) * movementForce;

        rb.AddForce(forceDirection, ForceMode.Impulse);
        forceDirection = Vector3.zero;

        if (rb.velocity.y < 0f)
            rb.velocity -= Vector3.down * Physics.gravity.y * Time.fixedDeltaTime;

        Vector3 horizontalVelocity = rb.velocity;
        horizontalVelocity.y = 0;
        if (horizontalVelocity.sqrMagnitude > maxSpeed * maxSpeed)
            rb.velocity = horizontalVelocity.normalized * maxSpeed + Vector3.up * rb.velocity.y;
            //rb.velocity = horizontalVelocity.normalized * currentSpeed + Vector3.up * rb.velocity.y;

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

    #endregion

    #region Player Abilities: Pick&drop, Jump
    //Pick & Drop
    private void DoPick(InputAction.CallbackContext obj)
    {
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

    //Jump
    void Jump()
    {
        if (jump.ReadValue<float>() == 1 && isGrounded)
        {
            //apply the initial jump force
            jumpSpeed = jumpForce;
            isJumping = true;
        }

        //if we let go of the button while jumping cut off our jump speed by half
        if (isJumping && jump.ReadValue<float>() == 0 && jumpSpeed <= minJumpForce)
        {
            Debug.Log(jumpSpeed);
            if (jumpSpeed > 0)
            {
                jumpSpeed = jumpSpeed / 2;
            }

            isJumping = false;
        }

        //apply gravity
        if (jumpSpeed > maxFall)
        {
            jumpSpeed += -jumpDeaccel * Time.deltaTime;
        }
        else
        {
            jumpSpeed = maxFall;
        }


        //if we have started to move downwards we are not longer jumping
        if (jumpSpeed <= 0) isJumping = false;

    }

    //Ground Check
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

    private void CastBlobShadow()
    {
        RaycastHit hit;

        if (Physics.SphereCast(transform.position, groundCheckRadius, -Vector3.up, out hit, Mathf.Infinity, groundLayer))
        {
            shadowRenderer.transform.position = new Vector3(transform.position.x, hit.point.y + 0.1f, transform.position.z);
        }

    }
    #endregion


    #region State Machine
    private void UpdateStates()
    {
        //A player state machine
        //changes state based on what the player is doing
        //this can be used to do certain things only when the player is doing certain actions
        //keeps everything in one place rather than a ton of bools
        if (isOnCircle)
        {
            pState = playerState.summoning;
        }
        else if (currentSpeed <= 0 && isGrounded)
        {
            pState = playerState.idle;
        }
        else if (!isGrounded && isJumping)
        {
            pState = playerState.jumping;
        }
        else if (!isGrounded)
        {
            //if we were just grounded and now we're airborne and we aren't jumping set our speed to 0
            //so we dont fall like a brick
            if (pState != playerState.airborne && pState != playerState.jumping)
            {
                jumpSpeed = 0;
            }
            pState = playerState.airborne;
        }
        else if (currentSpeed > playerWalkSpeed)
        {
            pState = playerState.running;
        }
        else if (currentSpeed <= playerWalkSpeed)
        {
            pState = playerState.walking;
        }

        //a switch function to make things run based on our current state
        switch (pState)
        {
            case playerState.idle:
                break;
            case playerState.walking:
                break;
            case playerState.running:
                break;
            case playerState.jumping:
                break;
            case playerState.airborne:
                break;
            case playerState.summoning:
                //set our direction towards the summoning circle and slowly move the player to it
                if (activeCircle != null)
                {
                    forceDirection = activeCircle.transform.position - transform.position;
                    forceDirection = forceDirection.normalized;
                    currentSpeed = playerWalkSpeed;

                    rb.velocity = new Vector3(forceDirection.x * currentSpeed, 0, forceDirection.z * currentSpeed);
                }
                break;
        }
    }

    #endregion
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


