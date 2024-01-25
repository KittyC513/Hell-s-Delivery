using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;

public class exButton : MonoBehaviour
{

    public static exButton instance;

    [SerializeField]
    //private TestCube[] players;

    private TestCube activePlayer;

    [SerializeField]
    private float radius = 1f;
    [SerializeField]
    private LayerMask playerMask;

    public LayerMask wrongPlayer;

    [SerializeField]
    private UnityEvent onSummon;
    [SerializeField]
    private UnityEvent onExit;
    [SerializeField]
    private bool hasPlayer;
    [SerializeField]
    private bool summoningActive = false;
    [SerializeField]
    private Collider[] playerCollider;
    [SerializeField]
    private Collider[] wrongCollider;
    [SerializeField]
    private int numOfPlayer;
    public Material OnPush;
    public Material Default;
    Renderer matChange1;
    public bool debug;
    [SerializeField]
    private int num;
    //public Animator Square;
    public bool blueActive;
    public bool orangeActive;
    public GameObject b1Cyl;
    public Animator Anim;
    public Animator x;
    bool switch1;
    [SerializeField]
    private Camera cam;
    [SerializeField]
    public bool inBridge;

    private bool shouldPlay = false;

    [SerializeField] private AK.Wwise.Event summonStart;
    [SerializeField] private AK.Wwise.Event summonEnd;
    

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        

        matChange1 = b1Cyl.GetComponent<Renderer>();
        switch1 = true;


        //players = new TestCube[2];
        //matChange.material = Default;
        onExit.Invoke();
        num = 0;
    }

    private void Update()
    {

        DetectPlayer();


        if (summoningActive)
        {
            onSummon.Invoke();

            if (shouldPlay)
            {
                summonStart.Post(this.gameObject);
                shouldPlay = false; 
            }
          
        }
        else
        {
            onExit.Invoke();

            if (!shouldPlay)
            {
                shouldPlay = true;
                summonEnd.Post(this.gameObject);
            }
            
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    private void DetectPlayer()
    {
        playerCollider = Physics.OverlapSphere(transform.position, radius, playerMask);
        wrongCollider = Physics.OverlapSphere(transform.position, radius, wrongPlayer);

        //numOfPlayer = num;



        if (playerCollider.Length > 0)
        {
            if (switch1)
            {
                summoningActive = true;
                matChange1.material = OnPush;
                blueActive = true;
                Anim.SetBool("Activate", true);
                switch1 = false;
                if(this.gameObject.layer == LayerMask.NameToLayer("BridgeButton"))
                {
                    inBridge = true;

                }
                else
                {
                    inBridge = false;
                }
            }
            

        }
        else
        {
            if (!switch1)
            {
                summoningActive = false;
                matChange1.material = Default;
                Anim.SetBool("Activate", false);
                switch1 = true;

                inBridge = false;
            }
            
        }

        if (wrongCollider.Length > 0 && playerCollider.Length <= 0)
        {
            x.SetBool("Activate", true);
        }
        else
        {
            x.SetBool("Activate", false);
        }



    }

    public IEnumerator SwitchCamera()
    {
        
        activePlayer.playerCamera.enabled = false;
        AdjustCameraViewport(cam, 0.5f, 1f);

        yield return new WaitForSeconds(3);
        activePlayer.playerCamera.enabled = true;
        
    }

    void AdjustCameraViewport(Camera camera, float width, float height)
    {
        // Make sure the camera component exists
        if (camera != null)
        {
            Rect cameraRect = camera.rect;
            cameraRect.width = width;
            cameraRect.height = height;
            camera.rect = cameraRect;
        }
        else
        {
            Debug.LogError("Camera reference is null!");
        }
    }
}




