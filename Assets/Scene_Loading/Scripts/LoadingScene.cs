using System.Collections;
using System.Collections.Generic;
using TicTacToe.Commons;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TicTacToe.Scene_Loading
{
    public class LoadingScene : MonoBehaviour
    {

        public Image loadingBar;
        public TextMeshProUGUI loadingMessage;

        private SceneLoader m_loader = SceneLoader.instance;

        private void Update()
        {
            m_loader.AddProgress(Time.deltaTime);
        }

        private void OnGUI()
        {
            loadingBar.fillAmount = m_loader.loadingProgress / m_loader.maxLoadingProgress;
            loadingMessage.text = m_loader.loadingMessage;
        }
    }
}
