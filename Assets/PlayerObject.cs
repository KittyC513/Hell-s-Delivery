using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : MonoBehaviour
{
    [SerializeField]
    private TestCube testCube; 
    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        UIController ui = FindAnyObjectByType<UIController>();

        if (ui == null)
        {
            ui = GameObject.Find("World").GetComponent<UIController>();
        }

        if (ui == null) Debug.LogError("No UIController component found");

        if (testCube.isPlayer1)
        {
            ui.AddPlayerIndicator(this.gameObject);
        }

        if (testCube.isPlayer2)
        {
            ui.AddTargetIndicator(null, this.gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
