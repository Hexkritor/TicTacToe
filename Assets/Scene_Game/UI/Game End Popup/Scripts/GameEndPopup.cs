using System.Collections;
using System.Collections.Generic;
using TicTacToe.Commons;
using TMPro;
using UnityEngine;

namespace TicTacToe.Scene_Game
{
    public class GameEndPopup : Popup
    {
        public enum State
        {
            Victory,
            Draw,
            Lose
        }

        public TextMeshProUGUI caption;
        public TextMeshProUGUI playerRatingText;
        public TextMeshProUGUI playerRatingIncreaceText;

        public Color ratingIncreacePositive;
        public Color ratingIncreaceNegative;

        private const float ratingMultiplier = 100;

        [SerializeField]
        private string[] captionTexts;
        [SerializeField, Range(0, 1)]
        private float[] ratingCoefficients;

        private State m_state;
        private float m_ratingCoefficient;

        public void SetWindowState(State state)
        {
            m_state = state;
            m_ratingCoefficient = ratingCoefficients[(int)m_state];
            caption.text = captionTexts[(int)m_state];
        }

        public void CountRating(int playerRating, int opponentRating)
        {
            float estimated = 1 / (1 + Mathf.Pow(10, (opponentRating - playerRating) / 400));
            int ratingIncreace = Mathf.FloorToInt(ratingMultiplier * (m_ratingCoefficient - estimated));
            PlayerPrefs.SetInt(GameConstants.Rating, playerRating + ratingIncreace);
            StartCoroutine(AnimateRating(playerRating, ratingIncreace));
        }

        IEnumerator AnimateRating(int rating, int ratingIncreace)
        {
            while (Mathf.Abs(ratingIncreace) > 0)
            {
                
                rating += (int)Mathf.Sign(ratingIncreace) * Mathf.Max(1, Mathf.Abs(ratingIncreace) / 4);
                ratingIncreace -= (int)Mathf.Sign(ratingIncreace) * Mathf.Max(1, Mathf.Abs(ratingIncreace) / 4);
                playerRatingText.text = rating.ToString();
                playerRatingIncreaceText.text = (ratingIncreace > 0 ? "+" : "") + ratingIncreace.ToString();
                playerRatingIncreaceText.color = ratingIncreace < 0 ? ratingIncreaceNegative : ratingIncreacePositive;
                yield return new WaitForSecondsRealtime(0.1f);
            }
            playerRatingText.text = rating.ToString();
            playerRatingIncreaceText.text = (ratingIncreace > 0 ? "+" : "") + ratingIncreace.ToString();
            playerRatingIncreaceText.color = ratingIncreace < 0 ? ratingIncreaceNegative : ratingIncreacePositive;
        }

        public void GoToMenu(string sceneName)
        {
            SceneLoader.instance.LoadLevel(sceneName, 5);
            SceneLoader.instance.SetMessage("Возвращаемся домой");
        }
    }
}
