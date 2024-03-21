using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carmovement : MonoBehaviour
{

    public float speed;
    public int duration;
    float timer;
    public bool forward;
    public bool back;
    public bool left;
    public bool right;
    Vector3 direction;


    // Start is called before the first frame update
    void Start()
    {
        if (forward)
        {
            direction = Vector3.forward;
        } else 
        if (back)
        {
            direction = -Vector3.forward;
        }
        else 
        if(left)
        {
            direction = -Vector3.right;
        } else 
        if (right)
        {
            direction = Vector3.right;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed);
        timer += Time.deltaTime;
        if (timer >= duration)
        {
            Destroy(gameObject);
        }
    }
}
