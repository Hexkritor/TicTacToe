using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace TicTacToe.Commons
{
    public class SettingsPopup : Popup
    {
        public AudioMixer audioMixer;
        public Toggle soundToggle;
        public Toggle musicToggle;


        // Start is called before the first frame update
        void Awake()
        {
            if (!PlayerPrefs.HasKey(GameConstants.Sound))
            {
                PlayerPrefs.SetInt(GameConstants.Sound, 1);
            }
            if (!PlayerPrefs.HasKey(GameConstants.Music))
            {
                PlayerPrefs.SetInt(GameConstants.Music, 1);
            }
            soundToggle.isOn = PlayerPrefs.GetInt(GameConstants.Sound) > 0;
            musicToggle.isOn = PlayerPrefs.GetInt(GameConstants.Music) > 0;
            soundToggle.onValueChanged.AddListener(delegate { UpdateSound(); });
            musicToggle.onValueChanged.AddListener(delegate { UpdateMusic(); });
        }

        // Update is called once per frame
        public void UpdateSound()
        {
            PlayerPrefs.SetInt(GameConstants.Sound, 1 - PlayerPrefs.GetInt(GameConstants.Sound));
            audioMixer.SetFloat(GameConstants.Sound, -80 * (1 - PlayerPrefs.GetInt(GameConstants.Sound)));
        }

        public void UpdateMusic()
        {
            PlayerPrefs.SetInt(GameConstants.Music, 1 - PlayerPrefs.GetInt(GameConstants.Music));
            audioMixer.SetFloat(GameConstants.Music, -80 * (1 - PlayerPrefs.GetInt(GameConstants.Music)));
        }

    }
}
