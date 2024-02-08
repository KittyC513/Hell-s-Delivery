using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class LostMail : MonoBehaviour
{
    //need to collect on collision with player
    //destroy this object once moved and add to the player's count
    //move from the screen to the top left ui
    //can get the camera to world point position of the UI
    private bool collected = false;
    private RectTransform p1MailSlot;
    private RectTransform p2MailSlot;
    private float time = 0;
    private bool p1 = false;
    private PlayerCollector collector;
    private Vector3 position;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CollectHitbox"))
        {
            if (!collected)
            {
                collector = other.GetComponent<PlayerCollector>();

                AddScoreToPlayer(collector);

                p1MailSlot = collector.scoreCount.p1MailSlot;
                p2MailSlot = collector.scoreCount.p2MailSlot;

                if (other.GetComponent<PlayerCollector>().isPlayer1)
                {
                    p1 = true;
                }
                else
                {
                    p1 = false;
                }
                time = Time.time;
                anim.SetBool("Collected", true);
                collected = true;
                Destroy(this.gameObject, 0.5f);
            }
            
        }
    }

    private void Update()
    {
        MoveToUI();
        if (collected)
        {
            if (p1) GetPositionToUI(p1MailSlot);
            else GetPositionToUI(p1MailSlot);
        }
        
    }

    private void AddScoreToPlayer(PlayerCollector player)
    {
        if (!collected) player.player.mailCount++;
       
    }

    private void GetPositionToUI(RectTransform rect)
    {
        Vector3 mailPoint = rect.TransformPoint(rect.rect.center);

        mailPoint.z = 3;

        position = (collector.cam.ScreenToWorldPoint(mailPoint));
    }

    private void MoveToUI()
    {
        if (!collected)
        {
           
        }
        else
        {
            float percent = (Time.time - time);
       

            //collector.cam.WorldToScreenPoint(position);
            
            transform.position = Vector3.Lerp(transform.position, position, percent);
        }

       
    }
}
