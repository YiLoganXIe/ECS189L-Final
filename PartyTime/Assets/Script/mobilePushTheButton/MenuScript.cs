using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// used to load new scene
using UnityEngine.SceneManagement;

// tutorial: https://www.youtube.com/watch?v=N9fF0AHhBLw&list=PLNgJHRje5SoVlK2dAYaz7bj73K13H0-pi&index=3

namespace MobileBallGame
{
    public class MenuScript : MonoBehaviour
    {
        // Menu States
        public enum MenuStates
        {
            Main,
            InGame,
            GameOver
        };
        public MenuStates currentState;

        public GameObject mainMenu;
        public GameObject inGameControl;
        public GameObject gameOverMenu;

        public BallController bc;

        // when script first starts
        void Awake()
        {
            // always sets first menu to main menu
            this.currentState = MenuStates.Main;
        }

        // Update is called once per frame
        void Update()
        {
            // checks current menu state
            switch (this.currentState)
            {
                case MenuStates.Main:
                    // sets active gameobject for main menu
                    mainMenu.SetActive(true);
                    inGameControl.SetActive(false);
                    gameOverMenu.SetActive(false);
                    break;
                case MenuStates.InGame:
                    // sets active gameobject for InGame so player buttons pop up
                    mainMenu.SetActive(false);
                    inGameControl.SetActive(true);
                    gameOverMenu.SetActive(false);
                    break;
                case MenuStates.GameOver:
                    // sets active gameobject to GameOver state
                    mainMenu.SetActive(false);
                    inGameControl.SetActive(false);
                    gameOverMenu.SetActive(true);
                    break;
                default:
                    Debug.Log("Unexpected GameState!");
                    break;
            }
        }

        public void setCurrentState(MenuStates ms)
        {
            this.currentState = ms;
        }

        public void OnStartGame()
        {
            Debug.Log("You pressed start game!");
            
            // toggle from Menu state to InGame state
            this.currentState = MenuStates.InGame;
            this.bc.ResetGame();
        }

        public void OnMainMenu()
        {
            Debug.Log("Going to main menu!");

            // toggle to Main Menu
            this.currentState = MenuStates.Main;
        }

        public void OnQuitGame()
        {
            Debug.Log("You have left the game 'Push the Button!'");

            // switch to lobby scene
            // SceneManager.LoadScene("Room for 2");
        }

        public void OnRestartGame()
        {
            Debug.Log("Pushed Restart!");

            // reset game and switch back to InGame state
            this.currentState = MenuStates.InGame;
            this.bc.ResetGame();
        }
    }
}
