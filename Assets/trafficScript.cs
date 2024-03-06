using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trafficScript : MonoBehaviour
{

    public Transform startPoint;    // Starting point of the movement
    public GameObject prefab;

    // Start is called before the first frame update
    void Start()
    {
        

        StartCoroutine(trafficloop());

    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator trafficloop()
    {

        while (true)
        {
            Instantiate(prefab, startPoint.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(5f);
        }
        
            
    }

        

    
}
