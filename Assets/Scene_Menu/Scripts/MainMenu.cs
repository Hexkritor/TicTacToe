using System.Collections;
using System.Collections.Generic;
using TicTacToe.Commons;
using TMPro;
using UnityEngine;

namespace TicTacToe.Scene_Menu
{
    public class MainMenu : BasicScene
    {

        public TextMeshProUGUI ratingText;

        protected override void Start()
        {
            base.Start();
            if (!PlayerPrefs.HasKey(GameConstants.Rating))
            {
                PlayerPrefs.SetInt(GameConstants.Rating, 1000);
            }

            ratingText.text = PlayerPrefs.GetInt(GameConstants.Rating, 1000).ToString();
        }

        public void StartGame(string sceneName)
        {
            SceneLoader.instance.LoadLevel(sceneName, 5);
            SceneLoader.instance.SetMessage("Готовимся к битве");
        }
    }
}
