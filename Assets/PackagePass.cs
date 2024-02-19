using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackagePass : MonoBehaviour
{

    [SerializeField]
    public bool packageIsDetected;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Package")
        {
            packageIsDetected = true;
        }
    }
}
