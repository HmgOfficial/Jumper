using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CanvasMenu : MonoBehaviour
{
    public static CanvasMenu instance;

    [Header("Panels")]
    public GameObject p_HUD;
    public GameObject p_GameOver;
    public GameObject p_Pause;
    public GameObject p_StartGame;
    public GameObject p_GameInputButtons;

    [Header("Text")]
    public TextMeshProUGUI timerTXT;
    public TextMeshProUGUI scoreTXT;
    public TextMeshProUGUI finalScoreTXT;

    [Header("Buttons")]
    public Button rightButton;
    public Button leftButton;
    public GameObject impulseButton;

    private float timer = 3f;
    private float score;

    private void Awake()
    {
        instance = this;
        GameManager.instance.onGameStart += GameStart;
        GameManager.instance.onGameEnd += GameEnd;
    }

    private void Start()
    {
        score = 0;
    }

    private void Update()
    {
        SetScore();
    }

    public void GameStart()
    {
        p_StartGame.SetActive(true);
        StartCoroutine(GameTimer());
        if (!p_HUD.activeInHierarchy)
        {
            p_HUD.SetActive(true);
        }
        p_GameOver.SetActive(false);
        p_Pause.SetActive(false);
        ButtonsGameMode();
    }

    /// <summary>
    /// Acive or deactive the buttons for the game input mode "Buttons"
    /// </summary>
    public void ButtonsGameMode()
    {
        if (PlayerPrefs.GetInt("GameInput") == 1)
        {
            p_GameInputButtons.SetActive(true);
        }
        else
        {
            p_GameInputButtons.SetActive(false);
        }
    }

    public void DeactivateImpulseButton()
    {
        impulseButton.SetActive(false);
    }

    /// <summary>
    /// Active or deactive the Pasue Panel
    /// </summary>
    /// <param name="active"></param>
    public void PauseGame(bool active)
    {
        p_Pause.SetActive(active);
        if (active)
        {
            GameManager.instance.GamePaused();
        }
        else
        {
            GameManager.instance.GameUnpaused();
        }
    }

    /// <summary>
    /// Active or deactive the GameOver Panel
    /// </summary>
    /// <param name="active"></param>
    private void GameEnd()
    {
        p_GameOver.SetActive(true);
        FinalScore();
    }

    /// <summary>
    /// Updates the scoreTxt values
    /// </summary>
    private void FinalScore()
    {
        finalScoreTXT.text = "GAME OVER! You scored " + float.Parse(scoreTXT.text) + " points";
    }
    private void SetScore()
    {
        score = Player.Instance.transform.position.y;
        if (score > float.Parse(scoreTXT.text))
        {
            scoreTXT.text = score.ToString("0.0");
        }
    }

    private IEnumerator GameTimer()
    {
        while (timer >= 0)
        {
            timerTXT.text = timer.ToString("0");
            timer -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        timer = 3;
        p_StartGame.SetActive(false);
        GameManager.instance.ChangeGameState(GameState.inGame);
        Invoke("DeactivateImpulseButton", 3f);
    }
}
