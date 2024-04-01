using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionAnimation : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        StartDot();
    }

    void StartDot()
    {
        anim.SetTrigger("DotDotDot");
    }
}
