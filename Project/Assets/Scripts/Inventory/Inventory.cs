using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jumper.Data;

public enum Items
{
    Impulse,
    ExtraLifes
}
public class Inventory : MonoBehaviour
{
    public static Inventory Instance;



    public List<Item> inventoryList = new List<Item>();

    public List<GameObject> inventoryGameObjects = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        RefreshInventory();
    }

    public void RefreshInventory()
    {
        for (int i = 0; i < MainMenu.Instance.ItemList.Count; i++)
        {
            MainMenu.Instance.ItemList[i].Amount = PlayerPrefs.GetInt(MainMenu.Instance.ItemList[i].ItemName);
            if (MainMenu.Instance.ItemList[i].Amount != 0)
            {
                inventoryList[i].gameObject.SetActive(true);
                inventoryList[i].UpdateInventory(MainMenu.Instance.ItemList[i]);
            }
            else
            {
                inventoryList[i].gameObject.SetActive(false);
            }
            
        }
    }
}
