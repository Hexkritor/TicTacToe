using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace TicTacToe.Commons
{
    public class BasicScene : MonoBehaviour
    {
        public AudioMixer audioMixer;
        public SettingsPopup settingsPopup;
        public ExitPopup exitPopup;

        protected virtual void Start()
        {
            SetupSounds();

            Input.backButtonLeavesApp = false;
        }

        protected void SetupSounds()
        {
            if (!PlayerPrefs.HasKey(GameConstants.Sound))
            { 
                PlayerPrefs.SetInt(GameConstants.Sound, 1);
            }
            if (!PlayerPrefs.HasKey(GameConstants.Music))
            { 
                PlayerPrefs.SetInt(GameConstants.Music, 1);
            }
            audioMixer.SetFloat(GameConstants.Sound, -80 * (1 - PlayerPrefs.GetInt(GameConstants.Sound)));
            audioMixer.SetFloat(GameConstants.Music, -80 * (1 - PlayerPrefs.GetInt(GameConstants.Music)));
        }

        public void ShowSettings()
        {
            Instantiate(settingsPopup);
        }

        public void ExitGame()
        {
            Instantiate(exitPopup);
        }

        private void Update()
        {
#if UNITY_ANDROID || UNITY_IOS
            if (Input.GetKeyDown(KeyCode.Menu))
            {
                ExitGame();
            }
#else
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ExitGame();
            }
#endif
        }
    }
}
