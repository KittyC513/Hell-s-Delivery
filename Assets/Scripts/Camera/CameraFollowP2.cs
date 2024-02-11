using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowP2 : MonoBehaviour
{
    [SerializeField]
    private float followSpeed = 5f;
    [SerializeField]
    private Transform target;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        target = GameManager.instance.player2.transform;
        Vector3 newPos = new Vector3(target.position.x - 40f, this.transform.position.y, this.transform.position.z);
        transform.position = Vector3.Slerp(transform.position, newPos, followSpeed * Time.deltaTime);
    }
}
