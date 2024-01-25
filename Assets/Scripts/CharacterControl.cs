using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterControl : MonoBehaviour
{
   
    private enum playerStates { idle, run, jump, fall, land, parachute, thrown, dead }

    [Header("General")]
    [SerializeField] private playerStates pState = playerStates.idle;

    private Camera cam;
    [SerializeField] private Camera thirdPersonCam;
    [SerializeField] private Camera hubCam;
    [SerializeField] private Camera topDownCam;
    [SerializeField] private Camera virtualCam;

    private bool isThrown = false;
    private bool isDead = false;

    [Header("Ground Movement")]
    [SerializeField] private float peakSpeed = 500;
    [SerializeField] private AnimationCurve velocityCurve;
    [SerializeField] private AnimationCurve decelerationCurve;
    //velocity curve goes from 0.0 - 1.0, multiply our peak speed by that to get our current speed
    private Vector2 forwardDirection;
    private float currentSpeed;
    private Vector3 directionSpeed;
    [SerializeField]private float rotationSpeed = 300;
    private float velocityTime = 0;
    private float decelerationTime = 0;
    private Vector3 faceDir;

    public bool isGrounded = false;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.33f;
    [SerializeField] private float groundCheckDist = 0.75f;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody rb;
    private Vector2 lastInput = Vector2.zero;
    private Vector2 stickValue = Vector2.zero;
    private Vector2 inputValue;

    [SerializeField] private float minQuickTurn = 0.8f;

    [Space, Header("Jumping Variables")]
    [SerializeField] private float peakJumpSpeed = 800;
    [SerializeField] private float maxFallSpeed = 15;
    [SerializeField] private AnimationCurve jumpCurve;
    [SerializeField] private AnimationCurve fallAccelCurve;
    //air velocity curve will build more slowly than a normal grounded acceleration to give the player a more controlled turn around
    [SerializeField] private AnimationCurve airVelocityCurve;
    [SerializeField] private float ySpeed = 0;
    private float fTime = 0;
    private float jTime = 0;
    private float airSpeed = 0;
    private Vector3 lastStandingVector;

    private bool isParachuting = false;
    [SerializeField] private bool isJumping = false;
    [SerializeField] private float minJump = 100f;
    private bool reachedMinJump = false;

    [Space, Header("Input Asset Variables")]
    public Test inputAsset;
    private InputActionMap player, dialogue, pause;
    private InputAction move, run, jump, parachute, cancelParachute, triggerButton;

    #region Initialization
    private void Awake()
    {
        //initialize a new instance of our input asset
        inputAsset = new Test();

        //set our actions from our input asset
        move = inputAsset.Cube.Move;
        jump = inputAsset.Cube.Jump;

        //on jump start we jump and when the button is let go of we exit the jump
        jump.started += ctx => OnJump();
        jump.canceled += ctx => OnJumpExit();
    }

    private void OnEnable()
    {
        
        //enable our action maps
        inputAsset.Enable();

    }

    private void OnDisable()
    {
        //disable our action maps
        inputAsset.Disable();

        jump.performed -= ctx => OnJump();
        jump.canceled -= ctx => OnJumpExit();
    }
    #endregion

    #region Essential Functions
    private void Start()
    {
        cam = thirdPersonCam;
        rb = GetComponent<Rigidbody>();
        currentSpeed = 0;
        faceDir = Vector3.zero;

    }

    private void Update()
    {
        MovementCalculations();
        StateMachineUpdate();
        GetStickInputs();
        
        ApplyGravity();
        CheckGrounded();

        RotateTowards(faceDir.normalized);

        JumpCalculations();
    }

    private void FixedUpdate()
    {
        //not used currently but if we need to use different physics based on grounded state we can change that here
        if (isGrounded)
        {
            rb.velocity = new Vector3(directionSpeed.x * Time.fixedDeltaTime, ySpeed * Time.fixedDeltaTime, directionSpeed.z * Time.fixedDeltaTime);
        }
            
        else if (!isGrounded)
        {
            rb.velocity = new Vector3(directionSpeed.x * Time.fixedDeltaTime, ySpeed * Time.fixedDeltaTime, directionSpeed.z * Time.fixedDeltaTime);
        }
            

    }

    private void StateMachineUpdate()
    {
        //all needed variables so far
        //movement inputs, isJumping, isGrounded, isParachute, isThrown and isDead
        switch (pState)
        {
            case playerStates.idle:
                //can transition to run, jump, thrown, dead

                //if we move, jump, are thrown or die we can transition states

                //if we are grounded and start moving we should transition to a run state
                if (isGrounded && currentSpeed > 0) pState = playerStates.run;
                if (!isGrounded && !isJumping) pState = playerStates.fall;
                if (isJumping) pState = playerStates.jump;
                break;
            case playerStates.run:
                //can transition to idle, jump, thrown or dead

                //if we are grounded and have no speed we are now idle
                if (isGrounded && currentSpeed <= 0) pState = playerStates.idle;
                if (!isGrounded && !isJumping)
                {
                    //transition state AND apply our grounded speed to our air movement speed
                    //this keeps our grounded speed while jumping because the air velocity will be different 
                    airSpeed = currentSpeed;
                    pState = playerStates.fall;
                }

                if (isJumping)
                {
                    airSpeed = currentSpeed;
                    pState = playerStates.jump;
                }
                break;
            case playerStates.jump:
                //can transition to fall, land, parachute, dead, thrown
                if (isJumping == false && !isGrounded && !isParachuting)
                {
                    pState = playerStates.fall;
                }
                if (isGrounded) pState = playerStates.land;
               
                break;
            case playerStates.fall:
                //can transition to land, parachute, thrown, dead
                if (isParachuting) pState = playerStates.parachute;
                if (isGrounded) pState = playerStates.land;
                break;
            case playerStates.land:
                //can transition to idle, run, jump, thrown, dead

                if (isGrounded && currentSpeed > 0) pState = playerStates.run;
                if (!isGrounded && !isJumping) pState = playerStates.fall;
                if (isJumping) pState = playerStates.jump;
                break;
            case playerStates.parachute:
                //can transition to fall, thrown, dead, land
                if (!isGrounded && !isJumping && !isParachuting) pState = playerStates.fall;
                break;
            case playerStates.thrown:
                //can transition to idle, run, fall, land, thrown, dead, parachute
                break;
            case playerStates.dead:
                //can transition to fall or idle
                break;
            default:
                break;
        }
    }

    #endregion

    #region Physics Calculations
    private void OnJump()
    {
        //start a jump and hold the input for higher jumps
        //jump is pressed
        //Debug.Log("Jump is pressed");
    }

    private void OnJumpExit()
    {
        //if mid jump, shorten the jump and start losing height
    }

    private Vector3 GetRelativeInputDirection(Camera camera, Vector2 inputValue)
    {
        //get camera forward and right
        Vector3 camForward = camera.transform.forward;
        Vector3 camRight = camera.transform.right;

        //get our stick input
        Vector3 stickInput = inputValue;
        //multiply our stick value by our cam right and forward to get a camera relative input
        Vector3 horizontal = stickInput.x * camRight;
        Vector3 vertical = stickInput.y * camForward;

        Vector3 input = horizontal + vertical;
        return input.normalized;
    }
    private void GetStickInputs()
    {
        stickValue = inputAsset.Cube.Move.ReadValue<Vector2>();
        stickValue = stickValue.normalized;

        //when the player makes a quick turn we should stop/cut their momentum to give them more control over a quick turn around
        if (stickValue.x != 0 || stickValue.y != 0)
        {
            //if we have a new input
            if (stickValue.x != lastInput.x || stickValue.y != lastInput.y)
            {
                //if our new input is a big enough difference from our last input we do a quick turn
                if (stickValue.x - lastInput.x > minQuickTurn || stickValue.x - lastInput.x < -minQuickTurn)
                {
                    //quick turn here
                    velocityTime = 0;
                    currentSpeed = 0;
                }

                if (stickValue.y - lastInput.y > minQuickTurn || stickValue.y - lastInput.y < -minQuickTurn)
                {
                    //quick turn here
                    velocityTime = 0;
                    currentSpeed = 0;
                }

                //we want to update our input value to the new stick direction
                inputValue = stickValue;
                lastInput = stickValue;
            }
        }
        else
        {
            inputValue = Vector2.zero;
        }
    }
    private void MovementCalculations()
    {
        //we get our camera relative inputs from this function to be used later
        Vector3 inputDir = GetRelativeInputDirection(cam, inputValue);

        //if we are grounded use our grounded speed curve otherwise use the airspeed curve
        if (isGrounded)
        {
            directionSpeed = new Vector3(faceDir.x * currentSpeed, rb.velocity.y, faceDir.z * currentSpeed);
        }
        else
        {
            directionSpeed = new Vector3(faceDir.x * airSpeed, rb.velocity.y, faceDir.z * airSpeed);
        }


        //if we are inputting on the control stick we want to apply our velocity curve and start speeding the character up
        if (inputDir.x != 0 || inputDir.y != 0)
        {
            //this facing direction means even if we are not inputting anything we are still facing somewhere
            //this is used to keep applying speed for a short time after we input to get a deceleration
            faceDir = GetRelativeInputDirection(cam, inputValue);


            decelerationTime = 0;
            velocityTime += Time.deltaTime;
            //if we are on the ground we want to use our grounded velocity curve, otherwise we use the aerial one
            if (isGrounded)
            {
                //multiply our peak speed by our place on the velocity curve
                //if we are at the peak our velocity will be our peak speed times 1, so our peak speed
                currentSpeed = (peakSpeed * velocityCurve.Evaluate(velocityTime));
            }
            else
            {
                //if we aren't grounded we evaluate the air movement curve
                airSpeed = (peakSpeed * airVelocityCurve.Evaluate(velocityTime));
            }

        }
        else
        {
            velocityTime = 0;
            decelerationTime += Time.deltaTime;

            if (currentSpeed > 0)
            {
                //apply a curve in the same way we applied the velocity but for when we want to slow down
                currentSpeed = (currentSpeed * decelerationCurve.Evaluate(decelerationTime));
            }
           
            //since we are multiplying we might get some weird values, just incase if we are at a decimal point below 1 just set to zero
            if (currentSpeed < 1)
            {
                currentSpeed = 0;
            }
        }

    
    }

    private void JumpCalculations()
    {

        //if the jump button is pressed
        if (jump.ReadValue<float>() == 1 && isGrounded && !isJumping)
        {
            if (!isJumping && isGrounded)
            {
                //lets get our starting y vector
                lastStandingVector = transform.position;
            }
            //reset the reached minimum jump variable
            reachedMinJump = false;

            //we are not jumping
            isJumping = true;
           
            
        }

        if (jump.ReadValue<float>() == 0 && !isGrounded && isJumping && reachedMinJump)
        {
            //if we let go of jump while not in the air, while jumping and if we have reached the minimum jump requirements
            isJumping = false;

            //cut our jump speed
            ySpeed /= 2;

            //reset our jump time
            jTime = 0.8f;

            Debug.Log("stop jump");

        }

        if (isJumping)
        {
            jTime += Time.deltaTime;
            ySpeed = peakJumpSpeed * jumpCurve.Evaluate(jTime);
        }
        else
        {
            jTime = 0;
            reachedMinJump = false;
        }

        if (ySpeed >= minJump && isJumping) reachedMinJump = true;

        if (isGrounded && reachedMinJump) isJumping = false;

            //if we have stopped gaining height, and have reached a minimum jump speed, start falling
            if (ySpeed <= 1 && isJumping && reachedMinJump) isJumping = false;
    }

    private void ApplyGravity()
    {
        //build downwards velocity until you reach peak speed
        if (!isGrounded && pState == playerStates.fall)
        {
            //add time to the fall timer
            fTime += Time.deltaTime;
            //add to our fall speed by using the animation curve
            ySpeed = (-maxFallSpeed) * fallAccelCurve.Evaluate(fTime);
        }
        else if (pState == playerStates.idle || pState == playerStates.run || pState == playerStates.land)
        {
            if (!isJumping) ySpeed = 0;
            //reset the fall timer

            fTime = 0;
            
        }
    }

    private void RotateTowards(Vector3 direction)
    {
        //make sure we have no y value so that we don't rotate on the wrong axis
        direction = new Vector3(direction.x, 0, direction.z);
        Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.fixedDeltaTime);
    }

    private void CheckGrounded()
    {
        RaycastHit hit;
        if (Physics.SphereCast(groundCheck.position, groundCheckRadius, Vector3.down, out hit, groundCheckDist, groundLayer))
        {
            isGrounded = true;
            transform.position = new Vector3(transform.position.x, (hit.point.y + 1.05f), transform.position.z);
        }
        else
        {
            isGrounded = false;
        }

    }

    #endregion
}
