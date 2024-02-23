using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Geiser : MonoBehaviour
{

    public bool active;
    ParticleSystem ps;
    BoxCollider bc;
    bool activeSwitch = true;
    [SerializeField] private AK.Wwise.Event playFan;
    [SerializeField] private AK.Wwise.Event stopFan;
    Renderer r;
    bool toggle;
    public Material outline;
    public GameObject cylinder;

    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        bc = GetComponent<BoxCollider>();

        ps.Stop();
        bc.enabled = false;

        r = cylinder.gameObject.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            
        } else
        {
            
        }
    }

    public void ActivateGeiser()
    {
        if (activeSwitch)
        {
            //Debug.Log("FanActive");
            active = true;
            ps.Play();
            bc.enabled = true;
            activeSwitch = false;
            playFan.Post(this.gameObject);
        }
        
    }

    public void DeactivateGeiser()
    {
        if (activeSwitch == false)
        {
            //Debug.Log("FanDeactive");
            active = false;
            ps.Stop();
            bc.enabled = false;
            activeSwitch = true;
            stopFan.Post(this.gameObject);
        }
        

    }

    public void OutlineDeActivate()
    {
        if (toggle == true)
        {
            // Get the existing materials
            Material[] materials = r.materials;

            // Remove the last material if there are more than one materials
            if (materials.Length > 1)
            {
                Material[] newMaterials = new Material[materials.Length - 1];
                for (int i = 0; i < newMaterials.Length; i++)
                {
                    newMaterials[i] = materials[i];
                }

                // Update the materials on the Renderer
                r.materials = newMaterials;
            }
            toggle = false;
        }
    }

    public void OutlineActivate()
    {

        if (toggle == false)
        {
                // Get the existing materials
                Material[] materials = r.materials;

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
                    r.materials = newMaterials;
                }
            toggle = true;
        }
    }
}
