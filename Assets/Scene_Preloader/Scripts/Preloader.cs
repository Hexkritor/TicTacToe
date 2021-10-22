using System.Collections;
using System.Collections.Generic;
using TicTacToe.Commons;
using UnityEngine;

namespace TicTacToe.Scene_Preloader
{
    public class Preloader : MonoBehaviour
    {
        public string mainMenuSceneName;
        // Start is called before the first frame update
        void Start()
        {
            SceneLoader.instance.LoadLevel(mainMenuSceneName, 5);
            SceneLoader.instance.SetMessage("Загружаем игру");
        }
    }
}
