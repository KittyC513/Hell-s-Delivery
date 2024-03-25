using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;

public class RisingPlatforms : MonoBehaviour
{
    private List <GameObject> platforms = new List <GameObject>();
    private List<PlatformChild> platformChildren = new List<PlatformChild>();

    [SerializeField]
    private GameObject platformParent;
    public bool active = false;

    [SerializeField]
    private float riseDuration = 3;
    [SerializeField]
    private Transform endPosition;
    [SerializeField]
    private Transform startPosition;
    public Material onActive;
    Material Default;
    [SerializeField]
    private List<Renderer> renderers = new List<Renderer>();
    public Material outline;

    public bool toggle;

    private float time;
    private float animationTime;

    public bool isRunning;

    [SerializeField] private AK.Wwise.Event platformsMove;
    private bool shouldSound = false;


    [Header ("Animation Variables")]
    [SerializeField] public bool animated = false;
    [SerializeField] private float fallDelay = 0.65f;
    [SerializeField] private float fallMultiplier = 0.85f;
    [SerializeField] private float moveOffset = 0.6f;
  
    private void Awake()
    {

    }
    private void Start()
    {
        //put all the platforms (child objects to the parent) into an array
        //currently unused but could be used in the future to move each platform on its own for a better visual effect

        
        foreach (Transform child in platformParent.transform)
        {
            if (animated)
            {
                platforms.Add(child.gameObject);

                PlatformChild plat = new PlatformChild();
                plat.child = child.gameObject;
                plat.offset = Random.Range(-moveOffset * riseDuration, moveOffset * riseDuration);
                plat.preShakePos = plat.child.transform.position;
                platformChildren.Add(plat);
               
            }
         

            Renderer renderer = child.gameObject.GetComponent<Renderer>();
            renderers.Add(renderer);

        }

        toggle = false;
      //  foreach (Transform child in platformParent.transform)
       // {
         //   Renderer renderer = child.GetComponent<Renderer>();

          //  if (renderer != null)
          //  {
           //     Default = renderer.material;
          //  }
      //  }

        platformParent.transform.position = startPosition.transform.position;
    }

    private void FixedUpdate()
    {
        MovePlatforms();

        
    }

    private void MovePlatforms()
    {
        //send platforms from starting position to target position over a certain speed
        if (active)
        {
            //move platforms to target

            //move the entire parent object giving the level designer more freedom at loss of the visual upgrade of moving each piece individually
            //could implement later
           

            if (!animated)
            { 
                platformParent.transform.position = Vector3.Lerp(platformParent.transform.position, endPosition.transform.position, time / (riseDuration + Random.Range(0, 2)));
            }
            else
            {
                foreach (PlatformChild platform in platformChildren)
                {
                    Vector3 childPos = new Vector3(platform.child.transform.position.x, startPosition.transform.position.y, platform.child.transform.position.z);
                    Vector3 endPos = new Vector3(platform.child.transform.position.x, endPosition.transform.position.y, platform.child.transform.position.z);

                    platform.child.transform.position = Vector3.Lerp(childPos, endPos, time / (riseDuration + platform.offset));
                }
            }

            if (shouldSound)
            {
                shouldSound = false;
                platformsMove.Post(this.gameObject);
            }
            

        }
        else
        {
            //move platforms back to starting
            if (!animated)
            {
                platformParent.transform.position = Vector3.Lerp(platformParent.transform.position, startPosition.transform.position, time / (riseDuration + Random.Range(0, 2)));
            }
            else
            {
                
                if (Mathf.Abs(Time.deltaTime - time) > fallDelay)
                {
                    //fall
                    foreach (PlatformChild platform in platformChildren)
                    {
                        Vector3 childPos = new Vector3(platform.preShakePos.x, startPosition.transform.position.y, platform.preShakePos.z);
                        Vector3 endPos = new Vector3(platform.preShakePos.x, endPosition.transform.position.y, platform.preShakePos.z);

                        platform.child.transform.position = Vector3.Lerp(endPos, childPos, animationTime / ((riseDuration + platform.offset) * fallMultiplier));
                    }
                }
                else
                {
                    //stall and shake platforms
                    animationTime = Time.deltaTime;
                    foreach (PlatformChild platform in platformChildren)
                    {
                        Vector3 randomizedPos = platform.preShakePos + Random.insideUnitSphere * 0.35f;
                        platform.child.transform.position = new Vector3 (randomizedPos.x, platform.preShakePos.y, randomizedPos.z);
                    }
                }
            }
            

            if (!shouldSound)
            {
                shouldSound = true;
                platformsMove.Post(this.gameObject);
            }


        }

        //used for our lerp functions
        time += Time.deltaTime;
        animationTime += Time.deltaTime;
    }


    public void ActivatePlatforms()
    {
        //when on the button activate platforms

        if (!active)
        {
            time = Time.deltaTime;
            
        }
        active = true;
        
    }

    public void DeactivatePlatforms()
    {
        //platforms are no longer active
        if (active)
        {
            time = Time.deltaTime;
            toggle = false;
        }
        active = false;

        foreach (PlatformChild platform in platformChildren)
        {
           
            platform.offset = Random.Range(-moveOffset * riseDuration, moveOffset * riseDuration);
        }
        //Debug.Log("Deactivate");
    }

    public void OutlineDeActivate()
    {
        // Get all Renderer components in the GameObject
        Renderer[] renderers = GetComponentsInChildren<Renderer>();

        foreach (Renderer renderer in renderers)
        {
            // Get the existing materials
            Material[] materials = renderer.materials;

            // Remove the last material if there are more than one materials
            if (materials.Length > 1)
            {
                Material[] newMaterials = new Material[materials.Length - 1];
                for (int i = 0; i < newMaterials.Length; i++)
                {
                    newMaterials[i] = materials[i];
                }

                // Update the materials on the Renderer
                renderer.materials = newMaterials;
            }
        }
    }

    public void OutlineActivate()
    {
        // Get all Renderer components in the GameObject
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        if (toggle == false)
        {
            foreach (Renderer renderer in renderers)
            {
                if (renderer.gameObject.tag != "noOutline" && renderer.gameObject.tag != "Character")
                {
                    // Get the existing materials
                    Material[] materials = renderer.materials;

                    // Add the additional material to the array
                    if (materials.Length <= 1)
                    {
                        Material[] newMaterials = new Material[materials.Length + 1];
                        for (int i = 0; i < materials.Length; i++)
                        {
                            newMaterials[i] = materials[i];
                        }
                        newMaterials[materials.Length] = outline;

                        // Update the materials on the Renderer
                        renderer.materials = newMaterials;
                    }

                
                }

            }
            toggle = true;
        }
    }

}

class PlatformChild
{
    public GameObject child;
    public float offset;
    public Vector3 preShakePos;
}