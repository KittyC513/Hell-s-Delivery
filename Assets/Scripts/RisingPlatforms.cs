using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;

public class RisingPlatforms : MonoBehaviour
{
    private List <GameObject> platforms = new List <GameObject>();
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

    private float time;

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

            
            
        }

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

    private void Update()
    {
        MovePlatforms();
    }

    private void MovePlatforms()
    {
        //send platforms from starting position to target position over a certain speed
        if (active)
        {
            //move platforms to target
           // for (int i = 0; i < platforms.Count; i++)
            //{
            //    Vector3 targetPos = new Vector3(platforms[i].transform.position.x, endPosition.transform.position.y, platforms[i].transform.position.z);
           //    platforms[i].transform.position = Vector3.Lerp(platforms[i].transform.position, targetPos, time / (riseDuration + Random.Range(0, 20)));
           // }

           //move the entire parent object giving the level designer more freedom at loss of the visual upgrade of moving each piece individually
           //could implement later
           platformParent.transform.position = Vector3.Lerp(platformParent.transform.position, endPosition.transform.position, time / (riseDuration + Random.Range(0, 2)));

           // foreach (Transform child in platformParent.transform)
           // {
               // Renderer renderer = child.GetComponent<Renderer>();

             //   if (renderer != null)
             //   {
              //      renderer.material = onActive;
              //  }
         //   }

        }
        else
        {
            //move platforms back to starting
            //for (int i = 0; i < platforms.Count; i++)
            // {
            //    Vector3 targetPos = new Vector3(platforms[i].transform.position.x, startPosition.transform.position.y, platforms[i].transform.position.z);
            //    platforms[i].transform.position = Vector3.Lerp(platforms[i].transform.position, targetPos, time / (riseDuration + Random.Range(0, 20)));
            //}

            platformParent.transform.position = Vector3.Lerp(platformParent.transform.position, startPosition.transform.position, time / (riseDuration + Random.Range(0, 2)));

           // foreach (Transform child in platformParent.transform)
           // {
               //// Renderer renderer = child.GetComponent<Renderer>();

              //  if (renderer != null)
               // {
              //      renderer.material = Default;
              //  }
           // }

        }

        //used for our lerp functions
        time += Time.deltaTime;
    }


    public void ActivatePlatforms()
    {
        //when on the button activate platforms
        
        if (!active) time = Time.deltaTime;
        active = true;
    }

    public void DeactivatePlatforms()
    {
        //platforms are no longer active
        active = false;
        time = Time.deltaTime;
    }


}
