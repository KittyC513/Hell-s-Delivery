//using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterControl : MonoBehaviour
{
   
    private enum playerStates { idle, run, jump, fall, land, parachute, thrown, dead }

    [Header("General")]
    [SerializeField] private playerStates pState = playerStates.idle;
    private LevelData lvlData;
    private ScoreCount scoreCount;

    private bool isThrown = false;
    private bool isDead = false;
    [SerializeField] private GameObject shadowRenderer;
    private bool isPlayer1;
    private bool isPlayer2;
    private PlayerSoundbank soundBank;
    private string groundMaterial = "nothing";
    private bool shouldStep;
    private float lastStepTime;
    [SerializeField] private float footStepRate = 1;
    [SerializeField] public PlayerCollector playerCollector;


    [Header("Ground Movement")]
    [SerializeField] private float peakSpeed = 500;
    [SerializeField] private float walkSpeed = 300;
    [SerializeField] private float runSpeed = 1000;
    [SerializeField] private AnimationCurve velocityCurve;
    [SerializeField] private AnimationCurve decelerationCurve;
    [SerializeField] private AnimationCurve airQuickTurnCurve;
    //velocity curve goes from 0.0 - 1.0, multiply our peak speed by that to get our current speed
    private Vector2 forwardDirection;
    [SerializeField] private float currentSpeed;
    public Vector3 directionSpeed;
    [SerializeField] private float rotationSpeed = 300;
    [SerializeField] private float velocityTime = 0;
    public float decelerationTime = 0;
    [SerializeField] public Vector3 faceDir;
    private Vector3 lookDir;

    public bool isGrounded = false;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.33f;
    [SerializeField] private float groundCheckDist = 0.75f;
    [SerializeField] private LayerMask groundLayer;

    public Rigidbody rb;
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
    [SerializeField] private AnimationCurve parachuteDecelerationCurve;
    [SerializeField] public float ySpeed = 0;
    private float fTime = 0;
    public float jTime = 0;
    private float airSpeed = 0;
    private Vector3 lastStandingVector;

    private bool isParachuting = false;
    [SerializeField] private Animator parachuteAnim;
    [SerializeField] private bool isJumping = false;
    [SerializeField] private float minJumpTime = 0.5f;
    [SerializeField] private bool reachedMinJump = false;

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
    [SerializeField] private bool isSlow = false;
    [SerializeField] private bool buttonHold = false;
    [SerializeField] private bool freezeState = false;
    [SerializeField] private ParticleSystem dustGen;
    [SerializeField] private ParticleSystem sweatGen;
    private bool reachedMaxSpeed = false;
    private float runTime = 1;
    private float runTemp = 0;
    private bool running = false;

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
        soundBank = this.GetComponent<PlayerSoundbank>();

        sweatGen.Pause();
    }

    public void OnLevelStart()
    {
        lvlData = ScoreCount.instance.lvlData;
        scoreCount = ScoreCount.instance;
    }

    private void Update()
    {
     
        CheckGrounded();
        
        //Debug.Log(rb.velocity.x.ToString() + " " + rb.velocity.z.ToString());
    }

    public void RunMovement(Camera cam, bool canParachute, Vector2 input, InputAction jump, GameObject parachuteObj, bool bigPackage, bool isOnCircle, bool isFreeze, bool player1, bool player2, bool holdPushButton)
    {
        isPlayer1 = player1;
        camera = cam;

        StateMachineUpdate();
        GetStickInputs(camera, input);
        ApplyGravity();
        //if there is some stick input lets rotate, this means that weird inputs right before letting go of the stick wont have time to rotate

        if (!freezeState)
        {
            if (stickValue.x != 0 || stickValue.y != 0) RotateTowards(lookDir.normalized);
        }


        //if we're on a summoning circle freeze movement

        if(GameManager.instance.curSceneName == "Level1" || GameManager.instance.curSceneName == "MVPLevel")
        {
            if (!boxingMinigame.instance.isboxing)
            {
                if (!isOnCircle && !isFreeze)
                {
                    MovementCalculations(camera);

                    if (!isSlow)
                    {
                        JumpCalculations(jump);
                    }
                    else
                    {
                        isJumping = false;
                    }

                }
                else
                {
                    directionSpeed = Vector3.zero;
                }

            }
            else
            {
                MovementCalculations(camera);
                JumpCalculations(jump);
            }
        }
        else
        {
            if (!isOnCircle && !isFreeze)
            {
                MovementCalculations(camera);

                if (!isSlow)
                {
                    JumpCalculations(jump);
                }
                else
                {
                    isJumping = false;
                }

            }
            else
            {
                directionSpeed = Vector3.zero;
            }
        }
        
       
        //Debug.Log(directionSpeed);
        CheckParachute(jump, canParachute);

        if (isParachuting) parachuteObj.SetActive(true);
        else parachuteObj.SetActive(false);
        isSlow = bigPackage;
        buttonHold = holdPushButton;
        freezeState = isFreeze;

        if(GameManager.instance.curSceneName == "Level1" || GameManager.instance.curSceneName == "MVPLevel")
        {
            if (bigPackage && !boxingMinigame.instance.isboxing)
            {
                if (!sweatGen.isPlaying)
                {
                    sweatGen.Play();
                }

            }
            else if (!bigPackage || boxingMinigame.instance.isboxing)
            {
                if (sweatGen.isPlaying)
                {
                    sweatGen.Stop(true);
                }

            }
        }
        else
        {
            if (bigPackage)
            {
                if (!sweatGen.isPlaying)
                {
                    sweatGen.Play();
                }

            }
            else
            {
                if (sweatGen.isPlaying)
                {
                    sweatGen.Stop(true);
                }

            }
        }
        
    }

    public void FixedUpdateFunctions()
    {
      

        if (isGrounded)
        {
            if (GameManager.instance.p1.p2pushed)
            {

            }
            rb.velocity = new Vector3(directionSpeed.x * Time.fixedDeltaTime, ySpeed * Time.fixedDeltaTime, directionSpeed.z * Time.fixedDeltaTime);
            
        }

        else if (!isGrounded)
        {
            rb.velocity = new Vector3(directionSpeed.x * Time.fixedDeltaTime, ySpeed * Time.fixedDeltaTime, directionSpeed.z * Time.fixedDeltaTime);

        
            if (pState == playerStates.parachute)
            {
                
            }
          
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
                if(scoreCount != null)
                {
                    ScoreCount.instance.AddBadgeValue(BadgeManager.BadgeValues.walkDist, Mathf.RoundToInt(directionSpeed.magnitude * 0.05f * Time.fixedDeltaTime), isPlayer1);
                }

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

                if(scoreCount != null)
                {
                    ScoreCount.instance.AddBadgeValue(BadgeManager.BadgeValues.fallDist, Mathf.RoundToInt(Mathf.Abs(ySpeed * 0.05f * Time.fixedDeltaTime)), isPlayer1);
                }

                break;
            case playerStates.land:
                //can transition to idle, run, jump, thrown, dead
                if (hasLeftGround)
                {
                    isJumping = false;
                    
                }
               
                if (!isJumping)
                {
                    GenerateDust();
                    GenerateDust();
                }
               
                if (isGrounded && currentSpeed == 0) pState = playerStates.idle;

                if (isGrounded && currentSpeed > 0) pState = playerStates.run;
                if (!isGrounded && !isJumping) pState = playerStates.fall;
                if (isJumping) pState = playerStates.jump;
                break;
            case playerStates.parachute:

                if (stickValue.magnitude> 0)
                {
                    if(scoreCount != null)
                    {
                        ScoreCount.instance.AddBadgeValue(BadgeManager.BadgeValues.glideDist, Mathf.RoundToInt(parachutingSpeed * 0.05f * Time.fixedDeltaTime), isPlayer1);
                    }
                    
                }
               
                //can transition to fall, thrown, dead, land
                if (!isGrounded && !isJumping && !isParachuting)
                {
                    pState = playerStates.fall;
                    fTime = 0.15f;
                }
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

    private void GenerateDust()
    {
        dustGen.Play();
    }
    private Vector3 GetRelativeInputDirection(Camera camera, Vector2 inputValue)
    {
        //get camera forward and right

        if(camera != null)
        {
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
        else
        {
            return new Vector3(0, 0, 0);
        }
        
    }

    public void GetStickInputs(Camera cam, Vector2 input)
    {

        rawInput = input;
        stickValue = rawInput.normalized;
       
        if(cam != null)
        {
            Vector3 relativeStick = GetRelativeInputDirection(cam, stickValue);

            if (stickValue.magnitude > 0.5f)
            {
                //when the player makes a quick turn we should stop/cut their momentum to give them more control over a quick turn around
                if (relativeStick.x != 0 || relativeStick.y != 0)
                {
                    if (isGrounded)
                    {
                        //get the player's facing direction and check if our new input is far enough away from our facing direction
                        if (lookDir.x - relativeStick.x > minQuickTurn || lookDir.x - relativeStick.x < -minQuickTurn)
                        {
                            if (currentSpeed >= peakSpeed / 2)
                            {
                                GenerateDust();
                            }
                            if (!quickTurn) quickTurn = true;
                            //Debug.Log(quickTurn);


                        }

                        if (lookDir.z - relativeStick.z > minQuickTurn || lookDir.z - relativeStick.z < -minQuickTurn)
                        {

                            if (currentSpeed >= peakSpeed / 2)
                            {
                                GenerateDust();
                            }
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

    }
    private void MovementCalculations(Camera cam)
    {
        //we get our camera relative inputs from this function to be used later
        if(cam != null)
        {
            Vector3 inputDir = GetRelativeInputDirection(cam, inputValue);

            //if we are grounded use our grounded speed curve otherwise use the airspeed curve
            if (isGrounded)
            {
                if (GameManager.instance.curSceneName == "Level1" || GameManager.instance.curSceneName == "MVPLevel")
                {
                    if (!boxingMinigame.instance.isboxing)
                    {
                        //if the player is inputting anything at all we should multiply the speed by their stick input to get a variable push rate
                        if (!isSlow && !buttonHold)
                        {
                            if (rawInput.magnitude > 0)
                            {
                                directionSpeed = new Vector3((faceDir.x * currentSpeed), rb.velocity.y, (faceDir.z * currentSpeed));
                            }
                            else
                            {
                                //otherwise if they let go of the stick we want then to slide just a tiny bit to slow down
                                directionSpeed = new Vector3((faceDir.x * currentSpeed), rb.velocity.y, (faceDir.z * currentSpeed));
                            }
                        }
                        else if (isSlow || buttonHold)
                        {
                            if (rawInput.magnitude > 0)
                            {
                                directionSpeed = new Vector3((faceDir.x * currentSpeed) * slowDownMultiplier, rb.velocity.y, (faceDir.z * currentSpeed) * rawInput.magnitude * slowDownMultiplier);
                            }
                            else
                            {
                                //otherwise if they let go of the stick we want then to slide just a tiny bit to slow down
                                directionSpeed = new Vector3((faceDir.x * currentSpeed) * slowDownMultiplier, rb.velocity.y, (faceDir.z * currentSpeed) * slowDownMultiplier);
                            }
                        }
                    }
                    else
                    {
                        if (!buttonHold)
                        {
                            if (rawInput.magnitude > 0)
                            {
                                directionSpeed = new Vector3((faceDir.x * currentSpeed), rb.velocity.y, (faceDir.z * currentSpeed));
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
                                directionSpeed = new Vector3((faceDir.x * currentSpeed) * slowDownMultiplier, rb.velocity.y, (faceDir.z * currentSpeed) * rawInput.magnitude * slowDownMultiplier);
                            }
                            else
                            {
                                //otherwise if they let go of the stick we want then to slide just a tiny bit to slow down
                                directionSpeed = new Vector3((faceDir.x * currentSpeed) * slowDownMultiplier, rb.velocity.y, (faceDir.z * currentSpeed) * slowDownMultiplier);
                            }
                        }
                    }
                }
                else
                {
                    if (!isSlow && !buttonHold)
                    {
                        if (rawInput.magnitude > 0)
                        {
                            directionSpeed = new Vector3((faceDir.x * currentSpeed), rb.velocity.y, (faceDir.z * currentSpeed));
                        }
                        else
                        {
                            //otherwise if they let go of the stick we want then to slide just a tiny bit to slow down
                            directionSpeed = new Vector3((faceDir.x * currentSpeed), rb.velocity.y, (faceDir.z * currentSpeed));
                        }
                    }
                    else if (isSlow || buttonHold)
                    {
                        if (rawInput.magnitude > 0)
                        {
                            directionSpeed = new Vector3((faceDir.x * currentSpeed) * slowDownMultiplier, rb.velocity.y, (faceDir.z * currentSpeed) * rawInput.magnitude * slowDownMultiplier);
                        }
                        else
                        {
                            //otherwise if they let go of the stick we want then to slide just a tiny bit to slow down
                            directionSpeed = new Vector3((faceDir.x * currentSpeed) * slowDownMultiplier, rb.velocity.y, (faceDir.z * currentSpeed) * slowDownMultiplier);
                        }
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
                directionSpeed = new Vector3(faceDir.x * parachutingSpeed, rb.velocity.y, faceDir.z * parachutingSpeed);
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
                    PlayWalkSound();
                    decelerationTime = 0;
                    velocityTime += Time.deltaTime;

                    //if we are on the ground we want to use our grounded velocity curve, otherwise we use the aerial one
                    //multiply our peak speed by our place on the velocity curve
                    //if we are at the peak our velocity will be our peak speed times 1, so our peak speed
                    if (rawInput.magnitude > 0.5f)
                    {
                        if (running)
                        {
                            currentSpeed = (runSpeed * velocityCurve.Evaluate(velocityTime));
                            airSpeed = (runSpeed * airVelocityCurve.Evaluate(velocityTime));
                        }
                        else
                        {
                            currentSpeed = (peakSpeed * velocityCurve.Evaluate(velocityTime));
                            airSpeed = (peakSpeed * airVelocityCurve.Evaluate(velocityTime));
                        }

                    }
                    else
                    {
                        currentSpeed = (walkSpeed * velocityCurve.Evaluate(velocityTime));
                        airSpeed = (walkSpeed * airVelocityCurve.Evaluate(velocityTime));
                    }

                    //if we aren't grounded we evaluate the air movement curve
                    if (currentSpeed >= peakSpeed)
                    {
                        if (!reachedMaxSpeed)
                        {
                            reachedMaxSpeed = true;

                        }

                    }
                    else
                    {
                        reachedMaxSpeed = false;
                    }




                    if (runTemp >= runTime)
                    {
                        if (!running)
                        {
                            GenerateDust();
                            running = true;
                        }
                    }
                    else if (rawInput.magnitude > 0.5f && isGrounded)
                    {
                        runTemp += 1 * Time.deltaTime;
                    }



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
                    lastSpeedValue = currentSpeed; //* rawInput.magnitude;

                }
                else
                {
                    if (running)
                    {
                        GenerateDust();
                    }

                    running = false;
                    runTemp = 0;

                    velocityTime = 0;
                    decelerationTime += Time.deltaTime;


                    if (currentSpeed > 0 || airSpeed > 0 || parachutingSpeed > 0)
                    {
                        //apply a curve in the same way we applied the velocity but for when we want to slow down
                        currentSpeed = (lastSpeedValue * decelerationCurve.Evaluate(decelerationTime));
                        //apply a curve in the same way we applied the velocity but for when we want to slow down
                        airSpeed = (lastSpeedValue * airDecelerationCurve.Evaluate(decelerationTime));

                        parachutingSpeed = (lastSpeedValue * parachuteDecelerationCurve.Evaluate(decelerationTime));
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
                running = false;
                runTemp = 0;
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
                        parachutingSpeed = (lastSpeedValue * airQuickTurnCurve.Evaluate(decelerationTime));
                    }
                    else
                    {
                        quickTurn = false;
                        velocityTime = 0;
                        quickTurnTime = 0;
                    }


                }

                if (stickValue.magnitude == 0)
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
        if (readJumpValue && shouldJump && !isJumping && canJump)
        {


            canJump = false;
            if (!isJumping && shouldJump)
            {
                //lets get our starting y vector
                lastStandingVector = transform.position;
                GenerateDust();
            }
            //reset the reached minimum jump variable
            reachedMinJump = false;

            //we are not jumping
            isJumping = true;
            jTime = 0;

            //we jumped
            if(scoreCount != null)
            {
                scoreCount.AddBadgeValue(BadgeManager.BadgeValues.numJumps, 1, isPlayer1);
            }

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

        if (isJumping)
        {
            if (isPlayer1)
            {
                GameManager.instance.p1.p1Anim.SetBool("Jump", true);
            }

            if (!isPlayer1)
            {
                GameManager.instance.p2.p1Anim.SetBool("Jump", true);
            }
        }
        else
        {
            if (isPlayer1)
            {
                GameManager.instance.p1.p1Anim.SetBool("Jump", false);
            }

            if (!isPlayer1)
            {
                GameManager.instance.p2.p1Anim.SetBool("Jump", false);
            }
        }



        //if (canJump)
        //{
        //    isJumping = false;
        //}

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

    public void ApplyGravity()
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
                if (!isParachuting)
                {
                    soundBank.parachuteOpen.Post(this.gameObject);
                }
                //parachute
                isParachuting = true;
                parachuteAnim.SetBool("isUsed", true);
            }
            else
            {
                if (isParachuting)
                {
                    soundBank.parachuteClose.Post(this.gameObject);
                }
                StartCoroutine(DedaultParachute());                            
            }
        }
        else
        {
            //isParachuting = false;
            StartCoroutine(DedaultParachute());
        }
    }

    IEnumerator CloseParachute()
    {
        yield return new WaitForSeconds(0.2f);
        parachuteAnim.SetBool("isUsed", false);
        parachuteAnim.SetTrigger("Close");
        isParachuting = false;
    }

    IEnumerator DedaultParachute()
    {
        yield return new WaitForSeconds(0.2f);
        parachuteAnim.SetBool("isUsed", false);
        //parachuteAnim.SetTrigger("Close");
        isParachuting = false;
    }

    private void RotateTowards(Vector3 direction)
    {
        direction = new Vector3(direction.x, 0, direction.z);
        Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
    
        if (rawInput.magnitude < runMagnitude)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, (rotationSpeed *  rawInput.magnitude) * Time.fixedDeltaTime);
        }
        else
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, (slowRotationSpeed * rawInput.magnitude) * Time.fixedDeltaTime);
        }
       

        
    }

    private void CheckGrounded()
    {
        RaycastHit hit;
        if (Physics.SphereCast(groundCheck.position, groundCheckRadius, Vector3.down, out hit, groundCheckDist, groundLayer))
        {
            if (!isGrounded)
            {
                if (groundMaterial == "Metal") soundBank.metalLand.Post(this.gameObject);
                else if (groundMaterial == "Wood") soundBank.woodLand.Post(this.gameObject);
                else soundBank.land.Post(this.gameObject);
                isJumping = false;
            }

            groundMaterial = hit.transform.tag;
            isGrounded = true;
            //isJumping = false;
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

    private void PlayGroundSound(string material)
    {
        if (material == "Metal")
        {
            soundBank.metalStep.Post(this.gameObject);
        }
        else if (material == "Wood")
        {
            soundBank.woodStep.Post(this.gameObject);
        }
        else
        {
            soundBank.steps.Post(this.gameObject);
        }

    }

    private void PlayWalkSound()
    {
        if (shouldStep && isGrounded)
        {
            PlayGroundSound(groundMaterial);
            lastStepTime = Time.time;
            shouldStep = false;
        }
        else if (currentSpeed > 0)
        {
            if (Time.time - lastStepTime > (footStepRate / (currentSpeed / 100)) * Time.deltaTime)
            {
                shouldStep = true;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {

        //adding back geiser functionality
        if (other.gameObject.tag == ("Geiser") && isParachuting)
        {

            //old sound code
            //if (shouldPlayGeiser) playerSounds.windCatch.Post(this.gameObject);
            //shouldPlayGeiser = false;
            Vector3 direction = other.transform.position - transform.position;
            float distance = direction.magnitude;

            Debug.Log(distance + "Distance");
            Debug.Log(transform.up * 200 / (distance) + "force");
            if (distance <= 15)
            {
                rb.AddForce(transform.up * 200);
            } else if (distance > 15 && distance <= 20)
            {
                rb.AddForce(transform.up * 150);
            }
            else if (distance > 20 && distance <= 25)
            {
                rb.AddForce(transform.up * 100);
            }
            else if (distance > 25)
            {
                rb.AddForce(transform.up * 50);
            }


        }
    }

        #endregion
    }
