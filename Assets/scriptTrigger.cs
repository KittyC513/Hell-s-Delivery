using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class scriptTrigger : MonoBehaviour
{

    [SerializeField]
    private UnityEvent onSummon;
    [SerializeField]
    private UnityEvent onExit;
    public bool Activate;
    bool switchbool;

    // Start is called before the first frame update
    void Start()
    {
        switchbool = true;
        onExit.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        if (Activate && switchbool)
        {
            onSummon.Invoke();
            switchbool = false;

        }
        if (!Activate && !switchbool)
        {
            onExit.Invoke();
            switchbool = true;
        }
    }
}
