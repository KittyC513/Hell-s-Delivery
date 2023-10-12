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

    private InputActionAsset inputAsset;
    InputActionMap player;
    InputAction move;


    private void Awake()
    {
        inputAsset = this.GetComponent<PlayerInput>().actions;
        player = inputAsset.FindActionMap("Test");
    }

    private void OnEnable()
    {
        //player.FindAction("Move").started += DoMove;
        player.FindAction("MoveUp").started += DoMoveUp;
        player.FindAction("MoveDown").started += DoMoveDown;
        player.Enable();

    }

    private void OnDisable()
    {
        //player.FindAction("Move").started -= DoMove;
        player.FindAction("MoveUp").started -= DoMoveUp;
        player.FindAction("MoveDown").started -= DoMoveDown;
        player.Disable();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void Move()
    {

    }

    private void DoMove(InputAction.CallbackContext obj)
    {
        movement = new Vector3(i_movement.x, 0, i_movement.y) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);

        Vector3 doMove = new Vector3(movement.x, 0, movement.y);

        Debug.Log("Moving");
    }
    private void DoMoveDown(InputAction.CallbackContext obj)
    {
        transform.Translate(transform.up);
        Debug.Log("Moving");
    }
    private void DoMoveUp(InputAction.CallbackContext obj)
    {
        transform.Translate(-transform.up);
        Debug.Log("Moving");
    }

}
