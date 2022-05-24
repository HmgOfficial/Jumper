using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jumper.Data
{
    public enum ItemType
    {
        Shield,
        Impulse,
        Coins
    }
    public enum ItemRarity
    {
        Common,
        Rare,
        Epic,
        Legendary
    }
    [System.Serializable]
    public class ItemData
    {
        [Header("General")]
        public string ItemName;
        public ItemType itemType;
        public ItemRarity itemRarity;
        public int Amount;
        public string Info;
        public Sprite sprite;

        [Header("Shop")]
        public int Price;
        //public bool Discount;
        //public int DiscountAmount;        

        public void SetAmount(ItemData item, int amount)
        {
            item.Amount += amount;
            PlayerPrefs.SetInt(item.ItemName.ToString(), amount);
        }

        public int GetPriceWithDiscount(int discount)
        {
            return Price - (Price * discount / 100);
        }
    }
}

