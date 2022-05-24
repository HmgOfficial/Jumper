using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    public static Store instance;
    public List<Item> allItems;
    public GameObject confirmPursachePanel;

    private System.DateTime lastTimeConnected;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;

        CheckDate();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            UpdataItemStore();
        }
    }

    public void UpdataItemStore()
    {
        for (int i = 0; i < allItems.Count; i++)
        {
            allItems[i].UpdateStore();
        }
    }

    public void Buy(Item item)
    {
        confirmPursachePanel.SetActive(true);
        confirmPursachePanel.GetComponent<ConfirmPursache>().SetInfo(item);
    }

    private void CheckDate()
    {
        System.DateTime currentTime = System.DateTime.Today;
        if (currentTime.Hour >= 0 && (lastTimeConnected.Year != currentTime.Year || lastTimeConnected.Month != currentTime.Month || lastTimeConnected.Day != currentTime.Day))
        {
            UpdataItemStore();
            lastTimeConnected = currentTime;
        }
    }
}
