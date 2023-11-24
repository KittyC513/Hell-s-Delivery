using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialKeywords : MonoBehaviour
{

    public Material mat1;
    public Material mat2;
    public Material mat3;
    public Material mat4;
    public Material mat5;
    public Material mat6;


    // Start is called before the first frame update
    void Start()
    {

        mat1.EnableKeyword("_ALPHABLEND_ON");
        mat2.EnableKeyword("_ALPHABLEND_ON");
        mat3.EnableKeyword("_ALPHABLEND_ON");


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
