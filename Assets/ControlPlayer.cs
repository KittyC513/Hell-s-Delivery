using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPlayer : MonoBehaviour
{



    // Start is called before the first frame update
    public void FreezePlayer()
    {
        if (GameManager.instance.p1 != null && GameManager.instance.p2 != null)
        {
            GameManager.instance.p1.isFreeze = true;
            GameManager.instance.p2.isFreeze = true;
        }


    }

    public void UnfreezePlayer()
    {
        if (GameManager.instance.p1 != null && GameManager.instance.p2 != null)
        {
            GameManager.instance.p1.isFreeze = false;
            GameManager.instance.p2.isFreeze = false;

        }

    }





}
