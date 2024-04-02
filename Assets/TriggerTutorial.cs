using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTutorial : MonoBehaviour
{



    // Start is called before the first frame update
    void Start()
    {



    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(this.gameObject.tag == "Package_Tutorial")
            {
                TutorialCamControl.instance.inPackageArea = true;
                SceneControl.instance.canRespawn = true;
            }

            if(this.gameObject.tag == "Push_Tutorial")
            {
                TutorialCamControl.instance.inPushArea = true;
            }

            if (this.gameObject.tag == "Pressure_Tutorial")
            {
                TutorialCamControl.instance.inPressurePlateArea = true;
            }

            if (this.gameObject.tag == "Gold_Tutorial")
            {
                TutorialCamControl.instance.inGoldSummningArea = true;
            }

            if (this.gameObject.tag == "Checkpoint_Tutorial")
            {
                TutorialCamControl.instance.inCheckpointArea = true;
            }

            if (this.gameObject.tag == "SummoningCircle_Tutorial")
            {
                TutorialCamControl.instance.inSummoningCircleArea = true;
            }

            if (this.gameObject.tag == "Sabotage_Tutorial")
            {
                TutorialCamControl.instance.inSabptageArea = true;
            }

            if (this.gameObject.tag == "Dual_Tutorial")
            {
                TutorialCamControl.instance.inDualSummoningArea = true;
            }

            if (this.gameObject.tag == "Specific_Tutorial")
            {
                TutorialCamControl.instance.inPlayerSpecificArea = true;
            }

        }

    }


}
