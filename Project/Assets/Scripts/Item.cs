using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Jumper.Data;

public enum ItemType
{
    Type1,
    Type2,
    Coins
}

public class Item : MonoBehaviour
{
    private ItemData itemData;

    private bool haveDiscount;
    private int discount;

    [Header("General")]
    public TextMeshProUGUI NameTxt;
    public Image itemImage;
    [Header("Shop")]
    public TextMeshProUGUI PriceTxt;
    public GameObject DiscountPanel;
    public TextMeshProUGUI DiscountAmountTxt;
    [Header("Inventory")]
    public TextMeshProUGUI InfoTxt;
    public TextMeshProUGUI AmountTxt;

    public ItemData GetItemData()
    {
        return itemData;
    }

    public void UpdateInventory(ItemData itemData)
    {
        AmountTxt.text = "x" + PlayerPrefs.GetInt(itemData.ItemName);
        itemData.Amount = PlayerPrefs.GetInt(itemData.ItemName);
    }

    public void UpdateStore()
    {
        itemData = MainMenu.Instance.ItemList[Random.Range(0, MainMenu.Instance.ItemList.Count)];
        if (DiscountPanel != null)
        {
            if (!DiscountPanel.activeInHierarchy)
            {
                DiscountPanel.SetActive(true);
            }
            discount = GetDiscountAmount();
            PriceTxt.text = itemData.GetPriceWithDiscount(discount).ToString();
            DiscountAmountTxt.text = discount + "%";
        }
        else
        {
            PriceTxt.text = itemData.Price.ToString();
        }
        NameTxt.text = itemData.ItemName.ToString();
        itemImage.sprite = itemData.sprite;
    }

    int GetDiscountAmount()
    {
        int a = Random.Range(0, 100);
        //20%
        if (a < 50)
        {
            return 20;
        }
        //30%
        else if (a < 75)
        {
            return 30;
        }
        //40%
        else if (a < 95)
        {
            return 40;
        }
        //50%
        else
        {
            return 50;
        }
    }
}
