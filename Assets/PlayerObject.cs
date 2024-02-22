using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : MonoBehaviour
{
    [SerializeField]
    private TestCube testCube;
    [SerializeField]
    private bool isFound;
    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        if (Level1CamControl.instance.endCutScene && !isFound)
        {
            UIController ui = FindAnyObjectByType<UIController>();


            if (ui == null && Level1CamControl.instance.endCutScene)
            {
                ui = GameObject.Find("World").GetComponent<UIController>();
            }

            if (ui == null) Debug.LogError("No UIController component found");

            if (ui != null)
            {
                if (testCube.isPlayer1)
                {
                    ui.AddPlayerIndicator(null, this.gameObject);
                }

                if (testCube.isPlayer2)
                {
                    ui.AddTargetIndicator(null, this.gameObject);
                }

                isFound = true;
            }
        }


    }
}
