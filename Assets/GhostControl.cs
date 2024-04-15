using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostControl : MonoBehaviour
{
    [SerializeField]
    private Transform[] respawnPoints;
    [SerializeField]
    private bool isOnLeft;
    [SerializeField]
    private bool isOnRight;
    [SerializeField]
    private float speed;

    // Start is called before the first frame update
    private void Awake()
    {

    }
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(this.transform.position, new Vector3(-33.69f, -5.11f, 2.9f)) < 0.5f)
        {
            isOnLeft = true;
        }

        if (Vector3.Distance(this.transform.position, new Vector3(33.72f, -5.11f, 2.9f)) < 0.5f)
        {
            isOnRight = true;
        }

        if (isOnLeft)
        {
            Vector3 targetPosition = new Vector3(33.72f, -5.11f, 2.9f);
            Vector3 direction = targetPosition - transform.position;
            direction.Normalize();
            transform.Translate(direction * speed * Time.deltaTime);

            if (Vector3.Distance(this.transform.position, targetPosition) < 0.1f)
            {
                Destroy(this.gameObject);
            }
        }

        if (isOnRight)
        {
            Vector3 targetPosition = new Vector3(-33.69f, -5.11f, 2.9f);
            Vector3 direction = targetPosition - transform.position;
            direction.Normalize();
            transform.Translate(direction * speed * Time.deltaTime);

            if (Vector3.Distance(this.transform.position, targetPosition) < 0.1f)
            {
                Destroy(this.gameObject);
            }
        }

    }
}
