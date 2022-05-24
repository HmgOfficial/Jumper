using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Jumper.Data;

public class MainMenu : MonoBehaviour
{
    public static MainMenu Instance;
    public List<ItemData> ItemList;

    private void Awake()
    {
        Instance = this;
#if UNITY_STANDALONE
        PlayerPrefs.SetInt("GameInput", 3);

#elif UNITY_ANDROID
        if (PlayerPrefs.GetInt("GameInput") == 3)
	{
        FindObjectOfType<GameMode>().SetGameInput(1);
	}       
#endif
    }

    // Start is called before the first frame update
    void Start()
    {
        SetSartMenuPanel(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public enum MenuPanel
    {
        Home,
        Store,
        Inventory
    }
    public Button[] StartPanelButtons;
    public GameObject[] StartPanelPanels;

    public void SetSartMenuPanel(int index)
    {
        for (int i = 0; i < StartPanelButtons.Length; i++)
        {
            if (index == i)
            {
                StartPanelButtons[i].interactable = false;
                StartPanelPanels[i].SetActive(true);
            }
            else
            {
                StartPanelButtons[i].interactable = true;
                StartPanelPanels[i].SetActive(false);
            }
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
