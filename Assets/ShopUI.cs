using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.Windows;

public class ShopUI : MonoBehaviour
{
    enum ItemType { FlameBreath, CocktailMolotov};
    private Transform container;
    private Transform shopItemTemplate;
    public GameObject pointer;
    private bool activeShop = false;
    private ItemType selectedType = ItemType.FlameBreath;
    private int itemsSize;
    private List<ItemType> itemsBought = new List<ItemType>();

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
            if (checkIfItemBought(container.GetChild(i).name))
            {
                continue;
            }

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

    private void UpdateBoughtItemsCanvas(ItemType boughtItemType)
    {
        //LogicScript logicScript = GameObject.FindGameObjectWithTag("LogicManager").GetComponent<LogicScript>();

        for (int i = 0; i < container.childCount; i++)
        {
            if(container.GetChild(i).name == boughtItemType.ToString())
            {   
                Image itemBackgound = container.GetChild(i).Find("Canvas").Find("ItemBackground").GetComponent<Image>();
                itemBackgound.color = new Color32((byte)(itemBackgound.color.r*255.0f), (byte)(itemBackgound.color.g * 255.0f), (byte)(itemBackgound.color.b * 255.0f), 40);

                Debug.Log(itemBackgound.color.a);

                TextMeshProUGUI itemPriceText = container.GetChild(i).Find("Canvas").Find("ItemPrice").GetComponent<TextMeshProUGUI>();
                itemPriceText.color = new Color32((byte)(itemPriceText.color.r * 255.0f), (byte)(itemPriceText.color.g * 255.0f), (byte)(itemPriceText.color.b * 255.0f), 40);

                TextMeshProUGUI itemNameText = container.GetChild(i).Find("Canvas").Find("ItemName").GetComponent<TextMeshProUGUI>();
                itemNameText.color = new Color32((byte)(itemNameText.color.r * 255.0f), (byte)(itemNameText.color.g * 255.0), (byte)(itemNameText.color.b*255.0f), 40);

                Image CoinsImg = container.GetChild(i).Find("Canvas").Find("CoinsImg").GetComponent<Image>();
                CoinsImg.color = new Color32((byte)(CoinsImg.color.r * 255.0f), (byte)(CoinsImg.color.g*255.0), (byte)(CoinsImg.color.b*255.0), 40);

                Image ItemImg = container.GetChild(i).Find("Canvas").Find("ItemImg").GetComponent<Image>();
                ItemImg.color = new Color32((byte)(ItemImg.color.r * 255.0f), (byte)(ItemImg.color.g * 255.0), (byte)(ItemImg.color.b * 255), 40);

                return;
            }

        }
        return;

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
                BuyItem();
            }
        }
    }

    private bool checkIfItemBought(ItemType itemType)
    {
        Debug.Log(itemsBought);
        if(itemsBought.Count == 0)
        {
            return false;
        }

        for (int i = 0; i < itemsBought.Count; i++)
        {
            if (itemsBought[i] == itemType)
            {
                return true; 
            }
        }

        return false;
    }

    private bool checkIfItemBought(String itemType)
    {
        Debug.Log(itemsBought);
        if (itemsBought.Count == 0)
        {
            return false;
        }

        for (int i = 0; i < itemsBought.Count; i++)
        {
            if (itemsBought[i].ToString() == itemType)
            {
                return true;
            }
        }

        return false;
    }

    private void BuyItem()
    {
        GameObject logicManager = GameObject.FindGameObjectWithTag("LogicManager");

        bool success = false;

        switch (selectedType)
        {
            case ItemType.FlameBreath:
                if (checkIfItemBought(selectedType))
                {
                    break;
                }
                success = logicManager.GetComponent<LogicScript>().BuyWeapon(flameBreathPrice, (int)selectedType);
                break;

            case ItemType.CocktailMolotov:
                if (checkIfItemBought(selectedType))
                {
                    break;
                }
                success = logicManager.GetComponent<LogicScript>().BuyWeapon(molotovPrice, (int)selectedType);
                break;
            default:
                break;
        }

        if (success)
        {
            Debug.Log("Item bought = " + selectedType);
            UpdateBoughtItemsCanvas(selectedType);
            itemsBought.Add(selectedType);
        }
    }
}
