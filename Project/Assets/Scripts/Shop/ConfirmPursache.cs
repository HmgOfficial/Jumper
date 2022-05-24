using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Jumper.Data;

public class ConfirmPursache : MonoBehaviour
{
    public TextMeshProUGUI itemName_1;
    public TextMeshProUGUI itemName_2;
    public TextMeshProUGUI priceTxt;
    public Image objectImg;
    public Button pursacheButton;
    public Image priceTypeImg;

    private Item itemToPursache;
    private ItemData itemData;
    private bool areGems;

    public void SetInfo(Item desiredItem)
    {
        itemToPursache = desiredItem;
        itemData = itemToPursache.GetItemData();
        pursacheButton.interactable = true;

        itemName_1.text = itemToPursache.NameTxt.text;
        itemName_2.text = itemToPursache.NameTxt.text;
        priceTxt.text = itemToPursache.PriceTxt.text;
        objectImg.sprite = itemToPursache.itemImage.sprite;
        CanBuyIt(false);
    }

    public void CanBuyIt(bool gems)
    {
        areGems = gems;
        pursacheButton.interactable = ((areGems && PlayerPrefs.GetInt("Gems") < itemData.Price) || (!areGems && PlayerPrefs.GetInt("Coins") < itemData.Price)) ? false : true;
    }

    public void PursachedBought()
    {
        if (!areGems)
        {
            int currentCoins = PlayerPrefs.GetInt("Coins");
            PlayerPrefs.SetInt("Coins", currentCoins - itemData.Price);
        }
        else if (areGems)
        {
            int currentGems = PlayerPrefs.GetInt("Gems");
            PlayerPrefs.SetInt("Gems", currentGems - itemData.Price);
        }
        PlayerPrefs.SetInt(itemData.ItemName, PlayerPrefs.GetInt(itemData.ItemName) + 1 );
        MenuController.instance.RefreshCoins();
        Inventory.Instance.RefreshInventory();
    }
}
