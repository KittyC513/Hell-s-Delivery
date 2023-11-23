using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterMVPLevel : MonoBehaviour
{
    [SerializeField]
    private Collider[] playerCollider;
    [SerializeField]
    private float radius;
    [SerializeField]
    private Transform origin;
    [SerializeField]
    private LayerMask playerMask;

    // Start is called before the first frame update
    private void Update()
    {
        DetectPlayer();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(origin.position, radius);
    }
    private void DetectPlayer()
    {
        playerCollider = Physics.OverlapSphere(origin.position, radius, playerMask);
        if (playerCollider.Length == 2)
        {
            SceneManager.LoadScene("MVPLevel");
            print("SceneLoad");
        }
    }
}
