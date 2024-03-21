using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;

public class PhasingPlatforms : MonoBehaviour
{
    private List<GameObject> platforms = new List<GameObject>();
    private List<MeshCollider> col = new List<MeshCollider>();
    private List<Renderer> renderers = new List<Renderer>();
    [SerializeField]
    private GameObject platformParent;
    public bool active = false;

    public Material Phased;
    public Material Solid;

    public Material onActive;
    Material Default;
    private float cutoffHeight = -10f; // Initial value of _Cutoff_height
    public float duration = 1f; // Duration over which to increase _Cutoff_height
    private float timer = 0f; // Timer to track the progress

    private float time;

    [SerializeField] private AK.Wwise.Event phaseIn;
    [SerializeField] private AK.Wwise.Event phaseOut;

    bool animSwitch;
    bool animSwitch2;
    bool animSwitch3;
    bool animSwitch4;

    public bool startDeActive;

    private bool shouldPlaySound = false;

    private void Awake()
    {

        

    }
    private void Start()
    {
        //put all the platforms (child objects to the parent) into an array
        //currently unused but could be used in the future to move each platform on its own for a better visual effect
        foreach (Transform child in platformParent.transform)
        {
            platforms.Add(child.gameObject);

            MeshCollider meshcol = child.gameObject.GetComponent<MeshCollider>();
            col.Add(meshcol);

            Renderer renderer = child.gameObject.GetComponent<Renderer>();
            renderers.Add(renderer);

        }

        foreach (Renderer renderer in renderers)
        {
            renderer.material = onActive;
        }

        

            //platformParent.transform.position = startPosition.transform.position;
        }

    private void Update()
    {
        MovePlatforms();
        if (startDeActive && animSwitch4)
        {
            // Increment the timer
            timer += Time.deltaTime;

            // Calculate the progress (0 to 1)
            float progress = Mathf.Clamp01(timer / duration);

            // Calculate the new value of _Cutoff_height
            float newCutoffHeight = Mathf.Lerp(2f, -2f, progress);

            // Update the material property
            onActive.SetFloat("_Cutoff_height", newCutoffHeight);

            // Reset the timer when duration is reached
            if (timer >= duration)
            {
                timer = 0f;
                animSwitch4 = false;
            }
        }
    }

    private void MovePlatforms()
    {
        
        if (active)
        {
            if (animSwitch)
            {
                timer = 0;
                animSwitch2 = true;
                animSwitch = false;
            }
            if (animSwitch2)
            {
                // Increment the timer
                timer += Time.deltaTime;

                // Calculate the progress (0 to 1)
                float progress = Mathf.Clamp01(timer / duration);

                // Calculate the new value of _Cutoff_height
                float newCutoffHeight = Mathf.Lerp(-2f, 2f, progress);

                // Update the material property
                onActive.SetFloat("_Cutoff_height", newCutoffHeight);

                // Reset the timer when duration is reached
                if (timer >= duration)
                {
                    timer = 0f;
                    animSwitch2 = false;
                }
            }
                
            //move the entire parent object giving the level designer more freedom at loss of the visual upgrade of moving each piece individually
            //could implement later
            //platformParent.transform.position = Vector3.Lerp(platformParent.transform.position, endPosition.transform.position, time / (riseDuration + Random.Range(0, 2)));

            foreach (MeshCollider meshcol in col)
            {
                if (meshcol != null)
                {
                    meshcol.enabled = true;
                }
                

            }


            foreach (Renderer renderer in renderers)
            {
                
                
            }

            if (shouldPlaySound)
            {
                shouldPlaySound = false;
                phaseIn.Post(this.gameObject);
            }

        }
        else
        {

            if (!animSwitch)
            {
                timer = 0;
                animSwitch = true;
                animSwitch3 = true;
            }

            if (animSwitch3)
            {
                // Increment the timer
                timer += Time.deltaTime;

                // Calculate the progress (0 to 1)
                float progress = Mathf.Clamp01(timer / duration);

                // Calculate the new value of _Cutoff_height
                float newCutoffHeight = Mathf.Lerp(2f, -2f, progress);

                // Update the material property
                onActive.SetFloat("_Cutoff_height", newCutoffHeight);

                // Reset the timer when duration is reached
                if (timer >= duration)
                {
                    timer = 0f;
                    animSwitch3 = false;
                }
            }

            //move platforms back to starting

            //platformParent.transform.position = Vector3.Lerp(platformParent.transform.position, startPosition.transform.position, time / (riseDuration + Random.Range(0, 2)));

            foreach (MeshCollider meshcol in col)
            {
                if (meshcol != null)
                {
                    meshcol.enabled = false;
                }
                

            }
            foreach (Renderer renderer in renderers)
            {
                
            }

            if (!shouldPlaySound)
            {
                shouldPlaySound = true;
                phaseOut.Post(this.gameObject);
            }

        }

        //used for our lerp functions
//time += Time.deltaTime;
    }


    public void ActivatePlatforms()
    {
        //when on the button activate platforms

        //if (!active) time = Time.deltaTime;
        active = true;
    }

    public void DeactivatePlatforms()
    {
        //platforms are no longer active
        active = false;
       // time = Time.deltaTime;
    }


}
