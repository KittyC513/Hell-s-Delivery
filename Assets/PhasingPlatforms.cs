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

    private float time;

    [SerializeField] private AK.Wwise.Event phaseIn;
    [SerializeField] private AK.Wwise.Event phaseOut;

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

        //platformParent.transform.position = startPosition.transform.position;
    }

    private void Update()
    {
        MovePlatforms();
    }

    private void MovePlatforms()
    {

        if (active)
        {

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
                renderer.material = Solid;
            }

            if (shouldPlaySound)
            {
                shouldPlaySound = false;
                phaseIn.Post(this.gameObject);
            }

        }
        else
        {
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
                renderer.material = Phased;
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
