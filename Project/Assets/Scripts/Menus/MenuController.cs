using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuController : MonoBehaviour
{
    public static MenuController instance;

    public Canvas menuCanvas;

    public TextMeshProUGUI coinsTXT;

    private void Awake()
    {
        instance = this;
        
    }

    private void Start()
    {
        RefreshCoins();
        //GameManager.instance.onCoinsChange += ChangeCoins;
    }

    public void RefreshCoins()
    {
        coinsTXT.text = PlayerPrefs.GetInt("Coins").ToString();
        //gemsTXT.text = PlayerPrefs.GetInt("Gems").ToString();
    }
}
