using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace AIAI
{
    /// <summary>
    /// 
    /// </summary>
    public enum GameState
    {
        inGame,
        pause,
        gameOver
    }

    /// <summary>
    /// 
    /// </summary>
    public class GameManager : MonoBehaviour
    {

        public static GameManager instance;
        public GameState currentGameState = GameState.gameOver;

        public int collectedCoins = 0;

        public delegate void OnGameStart();
        public event OnGameStart onGameStart;
        public delegate void OnGameEnd();
        public event OnGameEnd onGameEnd;
        public delegate void OnGamePaused();
        public event OnGameEnd onGamePaused;
        public delegate void OnGameUnpaused();
        public event OnGameEnd onGameUnpaused;
        public delegate void OnCoinsChange();
        public event OnCoinsChange onCoinsChange;

        void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            GameStart();
        }

        public void Restart()
        {
            SceneManager.LoadScene(1);

        }

        public void ChangePlayerCoins(int value)
        {
            int a = PlayerPrefs.GetInt("Coins");
            a += value;
            PlayerPrefs.SetInt("Coins", a);
            //onCoinsChange();
        }

        public void GameStart()
        {
            onGameStart();
        }

        public void GameEnd()
        {
            onGameEnd();
            ChangeGameState(GameState.gameOver);
        }

        public void GamePaused()
        {
            onGamePaused();
            ChangeGameState(GameState.pause);
        }

        public void GameUnpaused()
        {
            onGameUnpaused();
            ChangeGameState(GameState.inGame);
        }


        public void ChangeGameState(GameState gs)
        {
            currentGameState = gs;
        }
    }
}




