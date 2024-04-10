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
    public int previousItem;
    [SerializeField]
    public GameObject[] shopItem;
    [SerializeField]
    public bool firstEnter;
    [SerializeField]
    public bool chooseOne;
    [SerializeField]
    public bool chooseTwo;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        SelectItem();
    }

    public void SelectItem()
    {
        if (oriShop)
        {
            previousItem = 0;
            selectedItem += 1;

            if(selectedItem == 1)
            {
                chooseOne = true;
            }

            if(selectedItem == 2)
            {
                chooseTwo = true;
                chooseOne = false;

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
