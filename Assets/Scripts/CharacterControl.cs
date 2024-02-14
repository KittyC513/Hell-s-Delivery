using System.Collections;
using System.Collections.Generic;
using System.Drawing.Text;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterControl : MonoBehaviour
{
   
    private enum playerStates { idle, run, jump, fall, land, parachute, thrown, dead }

    [Header("General")]
    [SerializeField] private playerStates pState = playerStates.idle;


    private bool isThrown = false;
    private bool isDead = false;
    [SerializeField] private GameObject shadowRenderer;


    [Header("Ground Movement")]
    [SerializeField] private float peakSpeed = 500;
    [SerializeField] private AnimationCurve velocityCurve;
    [SerializeField] private AnimationCurve decelerationCurve;
    [SerializeField] private AnimationCurve airQuickTurnCurve;
    //velocity curve goes from 0.0 - 1.0, multiply our peak speed by that to get our current speed
    private Vector2 forwardDirection;
    [SerializeField] private float currentSpeed;
    private Vector3 directionSpeed;
    [SerializeField] private float rotationSpeed = 300;
    [SerializeField] private float velocityTime = 0;
    public float decelerationTime = 0;
    [SerializeField] private Vector3 faceDir;
    private Vector3 lookDir;

    public bool isGrounded = false;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.33f;
    [SerializeField] private float groundCheckDist = 0.75f;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody rb;
    private Vector2 lastInput = Vector2.zero;
    [HideInInspector] public Vector2 stickValue = Vector2.zero;
    private Vector2 inputValue;
    private Vector2 rawInput;

    [SerializeField] private float minQuickTurn = 0.8f;
    private float quickTurnTime = 0;
    private bool quickTurn = false;

    [SerializeField] private float runMagnitude = 0.6f;
    [SerializeField] private float slowRotationSpeed = 300f;
    [SerializeField] private float requiredQTVelocity = 8;
    private float lastSpeedValue;

    [Space, Header("Jumping Variables")]
    [SerializeField] private float airQuickTurn = 0.2f;
    [SerializeField] private float peakJumpSpeed = 800;
    [SerializeField] private float maxFallSpeed = 15;
    [SerializeField] private AnimationCurve jumpCurve;
    [SerializeField] private AnimationCurve fallAccelCurve;
    //air velocity curve will build more slowly than a normal grounded acceleration to give the player a more controlled turn around
    [SerializeField] private AnimationCurve airVelocityCurve;
    [SerializeField] private AnimationCurve airDecelerationCurve;
    [SerializeField] private float ySpeed = 0;
    private float fTime = 0;
    public float jTime = 0;
    private float airSpeed = 0;
    private Vector3 lastStandingVector;

    private bool isParachuting = false;
    [SerializeField] private bool isJumping = false;
    [SerializeField] private float minJumpTime = 0.5f;
    private bool reachedMinJump = false;

    private bool hasLeftGround = true;

    [SerializeField] private bool canJump = true;
    [SerializeField] private bool readJumpValue = false;
    [SerializeField] private float jumpButtonGracePeriod = 0.3f;
    private float graceTimer = 0;

    [SerializeField] private bool snappedToGround = false;
    [SerializeField] private float coyoteTime = 0.12f;
    public bool coyote = false;
    private float coyoteTimer = 0;

    private bool shouldJump = false;
    [Space, Header("Misc Movement Variables")]
    [SerializeField] private float maxGlideSpeed = 900;
    [SerializeField] private float glideAccelerationSpeed = 200;
    [SerializeField] private AnimationCurve glideAcceleration;
    private float lastAirSpeed;
    private float parachutingSpeed = 0;
    private float parachuteTime = 0;
    [SerializeField] private float glideFallMax = 800;
    [SerializeField] private float slowDownMultiplier = 0.6f;
    private bool isSlow = false;

    [Space, Header("Input Asset Variables")]
    public Test inputAsset;
    private InputActionMap player, dialogue, pause;
    private InputAction move, run, parachute, cancelParachute, triggerButton;

 
    private Camera camera;

    #region Initialization
    private void Awake()
    {
        //initialize a new instance of our input asset
        inputAsset = new Test();

        //set our actions from our input asset
        move = inputAsset.Cube.Move;

    }

    #endregion

    #region Essential Functions
    private void Start()
    {
    
        rb = GetComponent<Rigidbody>();
        currentSpeed = 0;
        faceDir = Vector3.zero;

    }

    private void Update()
    {
     
        CheckGrounded();
       
        //Debug.Log(rb.velocity.x.ToString() + " " + rb.velocity.z.ToString());
    }

    public void RunMovement(Camera cam, bool canParachute, Vector2 input, InputAction jump, GameObject parachuteObj, bool bigPackage)
    {
        camera = cam;
        MovementCalculations(camera);
        StateMachineUpdate();
        GetStickInputs(camera, input);
        ApplyGravity();
        //if there is some stick input lets rotate, this means that weird inputs right before letting go of the stick wont have time to rotate
        if (stickValue.x != 0 || stickValue.y != 0) RotateTowards(lookDir.normalized);

        JumpCalculations(jump);
        //Debug.Log(directionSpeed);
        CheckParachute(jump, canParachute);

        if (isParachuting) parachuteObj.SetActive(true);
        else parachuteObj.SetActive(false);
        isSlow = bigPackage;
    }

    public void FixedUpdateFunctions()
    {
      

        if (isGrounded)
        {
            
            rb.velocity = new Vector3(directionSpeed.x * Time.fixedDeltaTime, ySpeed * Time.fixedDeltaTime, directionSpeed.z * Time.fixedDeltaTime);
        }

        else if (!isGrounded)
        {
            rb.velocity = new Vector3(directionSpeed.x * Time.fixedDeltaTime, ySpeed * Time.fixedDeltaTime, directionSpeed.z * Time.fixedDeltaTime);
          
        }
    }

    public void LateUpdateFunctions()
    {
        CastBlobShadow();
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
                //can transition to idle, jump, thrown or deadr

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
                if (isJumping == false && !isGrounded)
                {
                    pState = playerStates.fall;
                }
                if (isGrounded) pState = playerStates.land;
                if (isParachuting) pState = playerStates.parachute;
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
                if (isGrounded) pState = playerStates.idle;
                if (isJumping) pState = playerStates.jump;

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

        camForward.y = 0;
        camRight.y = 0;

        camForward = camForward.normalized;
        camRight = camRight.normalized;

        //get our stick input
        Vector3 stickInput = inputValue;
        stickInput = stickInput.normalized;
        //multiply our stick value by our cam right and forward to get a camera relative input
        Vector3 horizontal = stickInput.x * camRight;
        Vector3 vertical = stickInput.y * camForward;

        //Debug.Log(horizontal.ToString() + " " + vertical.ToString());

        Vector3 input = horizontal + vertical;
        input = input.normalized;
        
        return input.normalized;
    }

    public void GetStickInputs(Camera cam, Vector2 input)
    {

        rawInput = input;
        stickValue = rawInput.normalized;
       

        Vector3 relativeStick = GetRelativeInputDirection(cam, stickValue);

        //when the player makes a quick turn we should stop/cut their momentum to give them more control over a quick turn around
        if (relativeStick.x != 0 || relativeStick.y != 0)
        {
            if (isGrounded)
            {
                //get the player's facing direction and check if our new input is far enough away from our facing direction
                if (lookDir.x - relativeStick.x > minQuickTurn || lookDir.x - relativeStick.x < -minQuickTurn)
                {
                    
                    if (!quickTurn) quickTurn = true;
                    //Debug.Log(quickTurn);
                }

                if (lookDir.z - relativeStick.z > minQuickTurn || lookDir.z - relativeStick.z < -minQuickTurn)
                {
                    if (!quickTurn) quickTurn = true;
                    //Debug.Log(quickTurn);
                }

            }
            else
            {
                //get the player's facing direction and check if our new input is far enough away from our facing direction
                if (lookDir.x - relativeStick.x > airQuickTurn || lookDir.x - relativeStick.x < -airQuickTurn)
                {
                    if (!quickTurn) quickTurn = true;
                    //Debug.Log(quickTurn);
                }

                if (lookDir.z - relativeStick.z > airQuickTurn || lookDir.z - relativeStick.z < -airQuickTurn)
                {
                    if (!quickTurn) quickTurn = true;
                    //Debug.Log(quickTurn);
                }

            }

            //if we have a new input
            if (relativeStick.x != lastInput.x || relativeStick.y != lastInput.y)
            {
                
                //we want to update our input value to the new stick direction
                inputValue = stickValue;
              
            }
        }
        else
        {
            inputValue = Vector2.zero;
        }

        lookDir = GetRelativeInputDirection(cam, inputValue);
    }
    private void MovementCalculations(Camera cam)
    {
        //we get our camera relative inputs from this function to be used later
        Vector3 inputDir = GetRelativeInputDirection(cam, inputValue);
        
        //if we are grounded use our grounded speed curve otherwise use the airspeed curve
        if (isGrounded)
        {
            //if the player is inputting anything at all we should multiply the speed by their stick input to get a variable push rate
            if (!isSlow)
            {
                if (rawInput.magnitude > 0)
                {
                    directionSpeed = new Vector3((faceDir.x * currentSpeed) * rawInput.magnitude, rb.velocity.y, (faceDir.z * currentSpeed) * rawInput.magnitude);
                }
                else
                {
                    //otherwise if they let go of the stick we want then to slide just a tiny bit to slow down
                    directionSpeed = new Vector3((faceDir.x * currentSpeed), rb.velocity.y, (faceDir.z * currentSpeed));
                }
            }
            else
            {
                if (rawInput.magnitude > 0)
                {
                    directionSpeed = new Vector3((faceDir.x * currentSpeed) * rawInput.magnitude * slowDownMultiplier, rb.velocity.y, (faceDir.z * currentSpeed) * rawInput.magnitude * slowDownMultiplier);
                }
                else
                {
                    //otherwise if they let go of the stick we want then to slide just a tiny bit to slow down
                    directionSpeed = new Vector3((faceDir.x * currentSpeed) * slowDownMultiplier, rb.velocity.y, (faceDir.z * currentSpeed) * slowDownMultiplier);
                }
            }
           
            
        }
        else if (pState == playerStates.fall || pState == playerStates.jump)
        {
            //the air speed is unaffected by the stick magnitude
            directionSpeed = new Vector3(faceDir.x * airSpeed, rb.velocity.y, faceDir.z * airSpeed);
           
        }
        else if (pState == playerStates.parachute)
        {
            directionSpeed = new Vector3(faceDir.x * airSpeed, rb.velocity.y, faceDir.z * airSpeed);
        }

        //if there is some input
        if (inputValue.x != 0 || inputValue.y != 0)
        {
            //and our magnitude isn't an accidental stick flick
            //and the player is quick turning
            if (rawInput.magnitude > 0.05 && !quickTurn)
            {
                //set our facing direction
                faceDir = GetRelativeInputDirection(cam, inputValue);
            }
            
        }

        //if we are inputting on the control stick we want to apply our velocity curve and start speeding the character up
        if (!quickTurn)
        {
            if (inputDir.x != 0 || inputDir.y != 0)
            {
                //this facing direction means even if we are not inputting anything we are still facing somewhere
                //this is used to keep applying speed for a short time after we input to get a deceleration
               
                decelerationTime = 0;
                velocityTime += Time.deltaTime;

                //if we are on the ground we want to use our grounded velocity curve, otherwise we use the aerial one
                //multiply our peak speed by our place on the velocity curve
                //if we are at the peak our velocity will be our peak speed times 1, so our peak speed
                currentSpeed = (peakSpeed * velocityCurve.Evaluate(velocityTime));

                //if we aren't grounded we evaluate the air movement curve
                airSpeed = (peakSpeed * airVelocityCurve.Evaluate(velocityTime));

                if (pState == playerStates.parachute)
                {
                    parachuteTime += Time.deltaTime;
                    //add to our last speed by evaluating the curve
                    if (parachutingSpeed < maxGlideSpeed)
                    {
                        parachutingSpeed += (glideAccelerationSpeed * glideAcceleration.Evaluate(parachuteTime));
                    }
                }
                else
                {
                    lastAirSpeed = airSpeed;
                    parachutingSpeed = lastAirSpeed;
                    parachuteTime = 0;

                    
                }

                //what was the player's speed the last time they were inputting
                lastSpeedValue = currentSpeed * rawInput.magnitude;

            }
            else
            {
                velocityTime = 0;
                decelerationTime += Time.deltaTime;


                if (currentSpeed > 0 || airSpeed > 0 || parachutingSpeed > 0)
                {
                    //apply a curve in the same way we applied the velocity but for when we want to slow down
                    currentSpeed = (lastSpeedValue * decelerationCurve.Evaluate(decelerationTime));
                    //apply a curve in the same way we applied the velocity but for when we want to slow down
                    airSpeed = (lastSpeedValue * airDecelerationCurve.Evaluate(decelerationTime));

                    parachutingSpeed = (lastSpeedValue * airDecelerationCurve.Evaluate(decelerationTime));
                }
                else
                {
                    lastSpeedValue = 0;
                }

                //since we are multiplying we might get some weird values, just incase if we are at a decimal point below 1 just set to zero
                if (currentSpeed < 1)
                {
                    currentSpeed = 0;
                }
            }
        }
        else
        {
            
            quickTurnTime += Time.deltaTime;

            if (isGrounded)
            {
                if (currentSpeed > peakSpeed / 5)
                {
                    //apply a curve in the same way we applied the velocity but for when we want to slow down
                    currentSpeed = (lastSpeedValue * decelerationCurve.Evaluate(quickTurnTime));

                }
                else
                {
                    quickTurn = false;
                    velocityTime = 0;
                    quickTurnTime = 0;
                }

            }
            else if (!isGrounded)
            {
                if (airSpeed > peakSpeed / 5)
                {
                    //apply a curve in the same way we applied the velocity but for when we want to slow down
                    airSpeed = (lastSpeedValue * airQuickTurnCurve.Evaluate(quickTurnTime));
                }
                else
                {
                    quickTurn = false;
                    velocityTime = 0;
                    quickTurnTime = 0;
                }


            }

        }


    }

    private void JumpCalculations(InputAction jump)
    {

        //if the jump button is pressed
        if (readJumpValue && shouldJump && !isJumping && canJump && !isSlow)
        {
            canJump = false;
            if (!isJumping && shouldJump)
            {
                //lets get our starting y vector
                lastStandingVector = transform.position;
            }
            //reset the reached minimum jump variable
            reachedMinJump = false;

            //we are not jumping
            isJumping = true;
            jTime = 0;
            
        }
        else if (jump.ReadValue<float>() == 0 && !isGrounded && isJumping && reachedMinJump)
        {
            //if we let go of jump while not in the air, while jumping and if we have reached the minimum jump requirements
            isJumping = false;

            //cut our jump speed
            ySpeed /= 2;

            //reset our jump time
            jTime += 0.2f;

            

        }

        //if (isGrounded && jump.ReadValue<float>() == 1 && canJump)
        //{
        //    readJumpValue = true;
        //    graceTimer = 0;
        //}

        if (jump.ReadValue<float>() == 0)
        {
            canJump = true;
        }

        //when the player presses the jump button, read that input for a few extra frames until it no longer works
        //player presses jump, the jump value is set to true for x seconds, jump is now set to false
        if (jump.ReadValue<float>() == 1 && !readJumpValue && canJump)
        {
            //set jump to true for a few seconds
            //read jump for 0.3 frames
            readJumpValue = true;
            graceTimer = 0;

        }
   
        //we want the player's jump input to stay there for a little longer than they pressed
        //it makes jumping as soon as you reach the ground a lot easier
        if (readJumpValue)
        {
            graceTimer += Time.deltaTime;

            if (graceTimer <= jumpButtonGracePeriod)
            {
                //read jump input
                readJumpValue = true;
                
            }
            else
            {
                //stop reading jump input and reset
                readJumpValue = false;
                canJump = false;
            }
        }

        if (isJumping)
        {
            jTime += Time.deltaTime;
            ySpeed = peakJumpSpeed * jumpCurve.Evaluate(jTime);
        }
        else
        {
            //jTime = 0;
            reachedMinJump = false;
        }

        if (jTime >= minJumpTime && isJumping && hasLeftGround) reachedMinJump = true;

        //if we have stopped gaining height, and have reached a minimum jump speed, start falling
        if (ySpeed <= 0 && isJumping && reachedMinJump && hasLeftGround) isJumping = false;

        if (isJumping && isGrounded && reachedMinJump && hasLeftGround)
        {
            isJumping = false;
        }

        //if we aren't grounded we have left the ground
        if (!isGrounded)
        {
            hasLeftGround = true;
        }//if we're not mid jump and grounded we have reached the ground and are not rising upwards
        else if (!isJumping && isGrounded)
        {
            hasLeftGround = false;
        }
    }

    private void ApplyGravity()
    {
        //build downwards velocity until you reach peak speed
        if (pState != playerStates.jump && pState != playerStates.parachute && !isJumping && !snappedToGround)
        {
            //add time to the fall timer
            fTime += Time.deltaTime;
            //add to our fall speed by using the animation curve
            ySpeed = (-maxFallSpeed) * fallAccelCurve.Evaluate(fTime);
        }
        else if (pState == playerStates.parachute)
        {
            //add time to the fall timer
            fTime += Time.deltaTime;
            //add to our fall speed by using the animation curve
            ySpeed = (-glideFallMax) * fallAccelCurve.Evaluate(fTime);
        }
        else 
        {
            if (!isJumping) ySpeed = 0;
            //reset the fall timer

            fTime = 0;
            
        }
    }

    private void CheckParachute(InputAction button, bool canParachute)
    {
        if (!shouldJump && !isGrounded && canParachute && !isJumping && !isSlow)
        {
            if (button.ReadValue<float>() == 1)
            {
                //parachute
                isParachuting = true;
            }
            else
            {
                isParachuting = false;
            }
        }
        else
        {
            isParachuting = false;
        }
    }

    private void RotateTowards(Vector3 direction)
    {
        direction = new Vector3(direction.x, 0, direction.z);
        Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
    
        if (rawInput.magnitude < runMagnitude)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.fixedDeltaTime);
        }
        else
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, slowRotationSpeed * Time.fixedDeltaTime);
        }
       

        
    }

    private void CheckGrounded()
    {
        RaycastHit hit;
        if (Physics.SphereCast(groundCheck.position, groundCheckRadius, Vector3.down, out hit, groundCheckDist, groundLayer))
        {
            isGrounded = true;
            //if the player isn't jumping and the distance between them and the ground is smaller than some number, snap them to the floor
            if (!isJumping && !YDistanceGreaterThan(0.5f, groundCheck.transform.position, hit.point))
            {
                transform.position = new Vector3(transform.position.x, (hit.point.y + 1.05f), transform.position.z);
                snappedToGround = true;
            }

            if (!isJumping)
            {
                shouldJump = true;
            }
        }
        else
        {
            //if we were just grounded, lets factor in coyote time
            //if we are grounded but we shouldn't be anymore
            if (isGrounded && !coyote)
            {
                //activate coyote time
                coyote = true;
                coyoteTimer = Time.time;
            }

            //if we are in coyote time
            if (coyote && !isJumping)
            {
                shouldJump = true;

                if (Time.time - coyoteTimer >= coyoteTime)
                {
                    //we are no longer grounded
                    coyote = false;
                }
            }
            else
            {
                shouldJump = false;
                coyote = false;
            }    

            //Debug.Log(Time.deltaTime - coyoteTimer);
            isGrounded = false;
            snappedToGround = false;
        }

 
    }

    private bool YDistanceGreaterThan(float dist, Vector3 pos1, Vector3 pos2)
    {
        if (Mathf.Abs(pos1.y - pos2.y) > dist)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(groundCheck.position, groundCheckRadius);
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDist, groundCheck.position.z));
    }

    private void CastBlobShadow()
    {
        RaycastHit hit;

        if (Physics.SphereCast(groundCheck.position, groundCheckRadius, -Vector3.up, out hit, Mathf.Infinity, groundLayer))
        {
            shadowRenderer.SetActive(true);
            shadowRenderer.transform.position = new Vector3(transform.position.x, hit.point.y + 0.1f, transform.position.z);
        }
        else
        {
            shadowRenderer.SetActive(false);
        }

    }

    #endregion
}
