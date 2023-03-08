using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.Windows;

public class ShopUI : MonoBehaviour
{
    enum ItemType { FlameBreath, Molotov};
    private Transform container;
    private Transform shopItemTemplate;
    public GameObject pointer;
    private bool activeShop = false;
    private ItemType selectedType = ItemType.FlameBreath;
    private int itemsSize;


    // Prices of the Items
    public int flameBreathPrice = 100;
    public int molotovPrice = 100;

    private void Awake()
    {
        container = transform.Find("container");
        shopItemTemplate = container.Find("shopItemTemplate");
        itemsSize = Enum.GetNames(typeof(ItemType)).Length;
        HideShop();

    }

   private void UpdatePriceTextColor()
    {
        LogicScript logicScript = GameObject.FindGameObjectWithTag("LogicManager").GetComponent<LogicScript>();

        int availbleMoney = logicScript.coins;

        for (int i = 0; i < container.childCount; i++)
        {
            TextMeshProUGUI itemPriceText = container.GetChild(i).Find("Canvas").Find("ItemPrice").GetComponent<TextMeshProUGUI>();
            int itemCost = Int32.Parse(itemPriceText.text);
            if (itemCost > availbleMoney)
            {
                itemPriceText.color = new Color32(241, 0, 0, 255);
                continue;
            }

            itemPriceText.color = new Color32(241, 219, 0, 255);
        }


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
        UpdatePriceTextColor();
        activeShop = true;
        gameObject.SetActive(true);
    }

    private void Update()
    {
        if (activeShop)
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.DownArrow) || UnityEngine.Input.GetKeyDown(KeyCode.S))
            {
                if ((int)selectedType + 1 >= itemsSize)
                {
                    return;
                }
                selectedType = selectedType + 1;
                pointer.GetComponent<PointerScript>().Next();
            }
            if (UnityEngine.Input.GetKeyDown(KeyCode.UpArrow) || UnityEngine.Input.GetKeyDown(KeyCode.W))
            {
                
                if ((int)selectedType <= 0)
                {
                    return;
                }
                selectedType = selectedType - 1;
                pointer.GetComponent<PointerScript>().Previous();
            }
            if (UnityEngine.Input.GetKeyDown(KeyCode.B) )
            {
                Debug.Log("Buying " + selectedType);
                BuyItem();
            }
        }
    }

    private void BuyItem()
    {
        GameObject logicManager = GameObject.FindGameObjectWithTag("LogicManager");

        switch (selectedType)
        {
            case ItemType.FlameBreath:
                logicManager.GetComponent<LogicScript>().BuyWeapon(flameBreathPrice, (int)selectedType);
                return;

            case ItemType.Molotov:
                logicManager.GetComponent<LogicScript>().BuyWeapon(molotovPrice, (int)selectedType);
                return;
            default:
                return;
        }
    }
}
