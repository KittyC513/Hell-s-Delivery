using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CordControl : MonoBehaviour
{

    public Material deActive;
    public Material Active;
    //public Renderer r;
    bool isactive;

    public GameObject objParent;

    private List<GameObject> objects = new List<GameObject>();
    private List<Renderer> renderers = new List<Renderer>();

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in objParent.transform)
        {
            objects.Add(child.gameObject);

            Renderer renderer = child.gameObject.GetComponent<Renderer>();
            renderers.Add(renderer);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate()
    {
        foreach (Renderer renderer in renderers)
        {
            renderer.material = Active;
        }
        
        isactive = true;
    }

    public void DeActivate()
    {
        foreach (Renderer renderer in renderers)
        {
            renderer.material = deActive;
        }
        isactive = false;
    }
}
