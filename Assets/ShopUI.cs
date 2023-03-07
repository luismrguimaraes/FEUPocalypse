using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ShopUI : MonoBehaviour
{
    enum ItemType { FlameBreath, Molotov};
    private Transform container;
    private Transform shopItemTemplate;
    public GameObject pointer;
    private bool activeShop = false;
    private ItemType selectedType = ItemType.FlameBreath;
    private int itemsSize;

    private void Awake()
    {
        container = transform.Find("container");
        shopItemTemplate = container.Find("shopItemTemplate");
        itemsSize = Enum.GetNames(typeof(ItemType)).Length;
        HideShop();

    }

    private void ChangePrice(int newPrice)  
    {
        shopItemTemplate.Find("Canvas").Find("ItemPrice").GetComponent<TextMeshProUGUI>().SetText(""+ newPrice);
    }

    public void HideShop()
    {
        activeShop = false;
        gameObject.SetActive(false);
    }

    public void ShowShop()
    {
        activeShop = true;
        gameObject.SetActive(true);
    }

    private void Update()
    {
        if (activeShop)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                if ((int)selectedType + 1 >= itemsSize)
                {
                    return;
                }
                selectedType = selectedType + 1;
                pointer.GetComponent<PointerScript>().Next();
            }
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                
                if ((int)selectedType <= 0)
                {
                    return;
                }
                selectedType = selectedType - 1;
                pointer.GetComponent<PointerScript>().Previous();
            }
            if (Input.GetKeyDown(KeyCode.B) )
            {
                Debug.Log("Buying " + selectedType);
            }

        }

    }
}
