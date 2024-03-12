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
    public bool collected = false;
    private RectTransform p1MailSlot;
    private RectTransform p2MailSlot;
    private float time = 0;
    private bool p1 = false;
    private PlayerCollector collector;
    private Vector3 position;
    public bool goldenMail = false;
    [HideInInspector] public int goldMailValue = 5;
    public bool cosmeticOnly = false;

    private Animator anim;
    [SerializeField] private GameObject shadow;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private GameObject mailObj;
    [SerializeField] private Material goldMaterial;
    [SerializeField] private GameObject mailObjPrefab;

    [SerializeField] private AK.Wwise.Event collectSound;

    private List<LostMail> extraMail;
    private bool spawned = false;
    private Vector3 startPos;

    private void Start()
    {
        anim = GetComponent<Animator>();
        extraMail = new List<LostMail>();
        startPos = transform.position;
        CastBlobShadow(shadow);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (this.GetComponent<SphereCollider>() != null)
        {
            if (other.gameObject.CompareTag("CollectHitbox"))
            {
                if (!collected)
                {
                    if (!cosmeticOnly)
                    {
                        collector = other.GetComponent<PlayerCollector>();

                        AddScoreToPlayer(collector, 1);

                        collectSound.Post(this.gameObject);
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

                        if (goldenMail) Destroy(this.gameObject, 0.3f);
                        else Destroy(this.gameObject, 0.3f);


                    }

                }

            }
        }
        
    }

    public void SpawnCosmetic(float timeOffSet, PlayerCollector pCollector)
    {
        StartCoroutine(CosmeticAnimation(timeOffSet));
        collector = pCollector;
        p1MailSlot = collector.scoreCount.p1MailSlot;
        p2MailSlot = collector.scoreCount.p2MailSlot;

        if (pCollector.isPlayer1)
        {
            p1 = true;
        }
        else
        {
            p1 = false;
        }

        anim = GetComponent<Animator>();
        anim.SetBool("Collected", true);

    }

    private IEnumerator CosmeticAnimation(float offset)
    {
        yield return new WaitForSeconds(offset);
        time = Time.time;
  
        AddScoreToPlayer(collector, 1);
        collectSound.Post(this.gameObject);
        collected = true;
        Destroy(this.gameObject, 0.3f);
       
    }

    private void Update()
    {
        MoveToUI();

        if (collected)
        {
            if (p1) GetPositionToUI(p1MailSlot);
            else GetPositionToUI(p1MailSlot);
            LookAtCamera(collector.cam);
        }

   
        if (goldenMail)
        {
            mailObj.GetComponent<MeshRenderer>().material = goldMaterial;
        }

        
    }
    private void LookAtCamera(Camera cam)
    {
        this.transform.LookAt(cam.transform, cam.transform.up);
    }

    private void AddScoreToPlayer(PlayerCollector player, int value)
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
        else if (!goldenMail && collected)
        {
            float percent = (Time.time - time);
       

            //collector.cam.WorldToScreenPoint(position);
            
            transform.position = Vector3.Lerp(transform.position, position, percent);
        }
        else if (collected)
        {
            float percent = (Time.time - time);

            if (!spawned)
            {
                
                //collector.cam.WorldToScreenPoint(position);
                for (int i = 0; i < goldMailValue; i++)
                {
                    LostMail tempMail = Instantiate(mailObjPrefab, collector.transform.position, Quaternion.identity).GetComponent<LostMail>();
                    tempMail.cosmeticOnly = true;
                    extraMail.Add(tempMail);
                    tempMail.SpawnCosmetic((i + 1) * 0.06f, collector);
                    
                }

                spawned = true;
            }

            transform.position = Vector3.Lerp(transform.position, position, percent);
        }

       
    }

    private void CastBlobShadow(GameObject shadowRenderer)
    {
        RaycastHit hit;
        Vector3 position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);

        if (Physics.SphereCast(position, 1, -Vector3.up, out hit, Mathf.Infinity, groundLayer))
        {
            shadowRenderer.SetActive(true);
            shadowRenderer.transform.position = new Vector3(transform.position.x, hit.point.y + 0.1f, transform.position.z);
        }
        else
        {
            shadowRenderer.SetActive(false);
        }

    }
}
