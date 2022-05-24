using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameInput
{
    accelerometer,
    buttons,
    swipe,
    keyboard
}
public class GameMode : MonoBehaviour
{
    public Button[] GameModeButtons;

    /// <summary>
    /// Set the game input 
    /// </summary>
    /// <param name="index"> 0 = Accelerometer, 1 = Buttons, 2 = Swipe, 3 or other = keyboard </param>
    public void SetGameInput(int index)
    {
#if UNITY_ANDROID
        switch (index)
        {
            case 0:
                GameModeButtons[0].interactable = false;
                GameModeButtons[1].interactable = true;
                GameModeButtons[2].interactable = true;
                break;
            case 1:
                GameModeButtons[0].interactable = true;
                GameModeButtons[1].interactable = false;
                GameModeButtons[2].interactable = true;
                break;
            case 2:
                GameModeButtons[0].interactable = true;
                GameModeButtons[1].interactable = true;
                GameModeButtons[2].interactable = false;
                break;
            default:

                GameModeButtons[0].interactable = true;
                GameModeButtons[1].interactable = false;
                GameModeButtons[2].interactable = true;
                index = 1;
                break;
        }
#else
        index = 3;
#endif
        PlayerPrefs.SetInt("GameInput", index);

    }
}
