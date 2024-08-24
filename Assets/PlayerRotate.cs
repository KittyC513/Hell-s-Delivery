using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    [SerializeField]
    private TestCube test;
    [SerializeField]
    private CharacterControl cC;
    [SerializeField]
    public float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
    }

    void Rotate()
    {
        if (test.isPlayer1)
        {
            if(GameManager.instance.curSceneName != "HubStart" && GameManager.instance.curSceneName != "TitleScene")
            {
                cC.movementDirection = cC.movementDirection.normalized;
                print(cC.movementDirection);
                Quaternion desiredRotation = Quaternion.LookRotation(cC.movementDirection);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRotation, rotationSpeed * Time.deltaTime);
            }

        }

        if (test.isPlayer2)
        {
            if (GameManager.instance.curSceneName != "HubStart" && GameManager.instance.curSceneName != "TitleScene")
            {
                cC.movementDirection = cC.movementDirection.normalized;
                print(cC.movementDirection);
                Quaternion desiredRotation = Quaternion.LookRotation(cC.movementDirection);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }
}
