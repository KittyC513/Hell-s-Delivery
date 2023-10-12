
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class TestCube : MonoBehaviour
{
    Vector2 i_movement;
    Vector3 movement;
    float moveSpeed = 10f;

    [SerializeField] private InputActionAsset inputAsset;
    [SerializeField] private InputActionMap player;
    [SerializeField] private InputAction move,cameraLook;
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
    [SerializeField]
    TestPickDrop testPickDrop;
    private void Awake()
    {
        inputAsset = this.GetComponent<PlayerInput>().actions;
        player = inputAsset.FindActionMap("Cube");
        rb = this.GetComponent<Rigidbody>();
        testPickDrop = GetComponent<TestPickDrop>();
    }

    private void OnEnable()
    {
        //player.FindAction("Move").started += DoMove;
        move = player.FindAction("Move");
        //player.FindAction("Pick").started += DoPick;
        cameraLook= player.FindAction("CameraLook");
        pickControl.action.Enable();

        player.Enable();

    }

    private void OnDisable()
    {
        //player.FindAction("Move").started -= DoMove;
        // player.FindAction("MoveUp").started -= MoveUp;
        //player.FindAction("MoveDown").started -= MoveDown;
        //player.FindAction("Pick").started -= DoPick;
        player.Disable();
        pickControl.action.Disable();
    }
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Move();


    }
    private void FixedUpdate()
    {
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

    private void DoPick()
    {
        if (!testPickDrop.slotFull)
        {
            if (pickControl.action.triggered)
            {
                isPicking = true;
                Debug.Log("is picking");
            }
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

}
