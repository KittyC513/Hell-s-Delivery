using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectMinigame : MonoBehaviour
{
    public static SelectMinigame instance;
    [SerializeField]
    public bool oriShop;
    [SerializeField]
    public int selectedItem;
    [SerializeField]
    public int pressingTimes;
    [SerializeField]
    public GameObject[] shopItem;
    [SerializeField]
    public bool firstEnter;
    [SerializeField]
    public bool chooseOne;
    [SerializeField]
    public bool chooseTwo;
    private float timer;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        firstEnter = true;
        pressingTimes = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //SelectItem();
    }

    public void SelectItem()
    {
        if (oriShop)
        {
            if(timer >= 0.17)
            {
                selectedItem += 1;

                if (selectedItem == pressingTimes * 2)
                {
                    chooseTwo = true;
                    chooseOne = false;
                    timer = 0;
                }
                else
                {
                    chooseOne = true;
                    chooseTwo = false;
                    timer = 0;
                }

                if (chooseOne)
                {
                    shopItem[0].SetActive(true);
                    shopItem[1].SetActive(false);
                }

                if (chooseTwo)
                {
                    shopItem[0].SetActive(false);
                    shopItem[1].SetActive(true);
                }

                if (selectedItem >= pressingTimes * 2)
                {
                    pressingTimes += 1;
                }
            }
            else
            {
                timer += Time.deltaTime;
            }
            

        }

        if (firstEnter && !oriShop)
        {
            selectedItem = 0;
            shopItem[0].SetActive(false);
            shopItem[1].SetActive(false);
            chooseOne = false;
            chooseTwo = false;
            oriShop = true;
           
        }

    }


}
