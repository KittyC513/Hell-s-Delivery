using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakaPackage : MonoBehaviour
{
    public static TakaPackage instance;

    [SerializeField]
    private TestCube testCube;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if(GameManager.instance.curSceneName == "Level1")
        {
            if (other.CompareTag("Player"))
            {
                testCube = other.GetComponent<TestCube>();
                testCube.TakePackageFunction();
            }
        }

    }
}
