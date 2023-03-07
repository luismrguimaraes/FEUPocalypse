using System;
using UnityEngine;

public class ShopItem
{
    //public class ShopItem()
    //{

    //}
    public enum ItemType
    {
        FlameBreath
    }

    public static int GetCost(ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.FlameBreath:
                return 100;
        }
    }

    public static int GetSprite(ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.FlameBreath:
                return 100;
        }
    }
}

