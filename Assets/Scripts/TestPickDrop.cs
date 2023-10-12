using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestPickDrop : MonoBehaviour
{
    [SerializeField]
    private LayerMask pickableMask;
    [SerializeField]
    private Transform player;
    [SerializeField]
    private Transform itemContainer;

    private ObjectGrabbable objectGrabbable;
    [SerializeField]
    public bool slotFull;
    [SerializeField]
    private InputActionReference pickControl;


    [Header("Camera Control")]
    public CameraStyle currentStyle;
    public GameObject thirdPersonCam;
    public GameObject combatCam;
    public GameObject topDownCam;
    public GameObject aimCursor;
    TestCube testCube;
    bool withinRange;


    public enum CameraStyle
    {
        Basic,
        Combat,
        Topdown
    }

    private void OnEnable()
    {
        //pickControl.action.Enable();
    }

    private void OnDisable()
    {
        //pickControl.action.Disable();
    }
    private void Awake()
    {
        testCube = GetComponent<TestCube>();
        player = this.transform;
    }

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        /*
        //press "E" to pick the item when player facing the pickable items
        if (pickControl.action.triggered)
        {
            Debug.Log("pick");
            if(objectGrabbable == null)
            {
                float pickDistance = 10f;
                if (withinRange = Physics.Raycast(player.position, player.forward, out RaycastHit raycastHit, pickDistance, pickableMask))
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
                objectGrabbable.Drop();
                objectGrabbable = null;

            }
            
        }
        */
    }


    void SwitchCameraStyle(CameraStyle newStyle)
    {
        combatCam.SetActive(false);
        thirdPersonCam.SetActive(false);
        topDownCam.SetActive(false);
        aimCursor.SetActive(false);

        if (newStyle == CameraStyle.Basic)
        {
            thirdPersonCam.SetActive(true);
        }

        if (newStyle == CameraStyle.Combat)
        {
            combatCam.SetActive(true);
            aimCursor.SetActive(true);
        }

        if (newStyle == CameraStyle.Topdown)
        {
            topDownCam.SetActive(true);
        }

        currentStyle = newStyle;
    }

    private void OnTriggerStay(Collider other)
    {
        if (testCube.isPicking)
        {
            if (other.tag == ("pickable"))
            {
                if (objectGrabbable == null)
                {
                    //transform the item
                    objectGrabbable.Grab(itemContainer);
                    slotFull = true;
                }
                else
                {
                    objectGrabbable.Drop();
                    objectGrabbable = null;
                    slotFull = false;
                    testCube.isPicking = false;
                }
            }
        }

    }

}
