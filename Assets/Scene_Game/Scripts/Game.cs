using System.Linq;
using System.Collections.Generic;
using TicTacToe.Commons;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TicTacToe.Scene_Game
{
    public class Game : BasicScene
    {

        public enum Turn
        {
            Player1 = 1,
            Player2 = -1
        }

        public FieldButton fieldButton;

        public Transform gameField;

        public GameEndPopup gameEndPopup;

        public TextMeshProUGUI playerRating;
        public TextMeshProUGUI opponentRating;

        [SerializeField, Min(3)]
        private int m_fieldSize = 3;

        private int[,] m_fieldValues;
        private FieldButton[,] m_fieldButtons;
        private Turn m_turn = Turn.Player1;
        private int m_turnsDone = 0;

        private int opponentRatingValue;

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();

            CreateField();

            ShowRatings();
        }

        public void CreateField()
        {
            m_fieldValues = new int[m_fieldSize, m_fieldSize];
            m_fieldButtons = new FieldButton[m_fieldSize, m_fieldSize];

            Vector2 fieldButtonSize = fieldButton.GetComponent<RectTransform>().sizeDelta;
            Vector2 fieldOffset = new Vector2(-fieldButtonSize.x * (m_fieldSize - 1) / 2, -fieldButtonSize.y * (m_fieldSize - 1) / 2);

            for (int i = 0; i < m_fieldSize; ++i)
            {
                for (int j = 0; j < m_fieldSize; ++j)
                {
                    m_fieldValues[i,j] = 0;
                    var newFieldButton = Instantiate(fieldButton, gameField);
                    newFieldButton.GetComponent<RectTransform>().anchoredPosition = fieldOffset;
                    newFieldButton.SetPosition(new Vector2Int(i, j));
                    newFieldButton.onClick += Click;
                    m_fieldButtons[i, j] = newFieldButton;
                    fieldOffset.x += fieldButtonSize.x;
                }
                fieldOffset.x = -fieldButtonSize.x * (m_fieldSize - 1) / 2;
                fieldOffset.y += fieldButtonSize.y;
            }
        }

        public void Click(Vector2Int position)
        {
            if (m_fieldValues[position.x, position.y] != 0)
            {
                return;
            }

            ++m_turnsDone;

            m_fieldValues[position.x, position.y] = (int)m_turn;
            m_fieldButtons[position.x, position.y].ChangeColor(m_turn);

            if (!CheckVictory())
            {
                ChangeTurn();
            }
        }

        private bool CheckVictory()
        {
            if (m_turnsDone == m_fieldSize * m_fieldSize)
            {
                Draw();
                return true;
            }

            int[] verticalScore = new int[m_fieldSize];
            int[] horizontalScore = new int[m_fieldSize];
            int diagonalScore = 0;
            int reversedDiagonalScore = 0;
            
            for (int i = 0; i < m_fieldSize; ++i)
            {
                for (int j = 0; j < m_fieldSize; ++j)
                {
                    verticalScore[j] += m_fieldValues[i, j];
                    horizontalScore[i] += m_fieldValues[i, j];
                    diagonalScore += i == j ? m_fieldValues[i, j] : 0;
                    reversedDiagonalScore += i + j == m_fieldSize - 1 ? m_fieldValues[i, j] : 0;
                }
            }

            for (int i = 0; i < m_fieldSize; ++i)
            {
                if (verticalScore[i] == m_fieldSize)
                {
                    Victory();
                    return true;
                }
                else if (verticalScore[i] == -m_fieldSize)
                {
                    Lose();
                    return true;
                }

                if (horizontalScore[i] == m_fieldSize)
                {
                    Victory();
                    return true;
                }
                else if (horizontalScore[i] == -m_fieldSize)
                {
                    Lose();
                    return true;
                }
            }

            if (diagonalScore == m_fieldSize)
            {
                Victory();
                return true;
            }
            else if (diagonalScore == -m_fieldSize)
            {
                Lose();
                return true;
            }

            if (reversedDiagonalScore == m_fieldSize)
            {
                Victory();
                return true;
            }
            else if (reversedDiagonalScore == -m_fieldSize)
            {
                Lose();
                return true;
            }
            return false;
        }

        private void Victory()
        {
            var endPopup = Instantiate(gameEndPopup);
            endPopup.SetWindowState(GameEndPopup.State.Victory);
            endPopup.CountRating(PlayerPrefs.GetInt(GameConstants.Rating, 1000), opponentRatingValue);
        }

        private void Lose()
        {
            var endPopup = Instantiate(gameEndPopup);
            endPopup.SetWindowState(GameEndPopup.State.Lose);
            endPopup.CountRating(PlayerPrefs.GetInt(GameConstants.Rating, 1000), opponentRatingValue);
        }

        private void Draw()
        {
            var endPopup = Instantiate(gameEndPopup);
            endPopup.SetWindowState(GameEndPopup.State.Draw);
            endPopup.CountRating(PlayerPrefs.GetInt(GameConstants.Rating, 1000), opponentRatingValue);
        }

        private void ChangeTurn()
        {
            m_turn = (Turn)(-(int)m_turn);

            if (m_turn == Turn.Player2)
            {
                DoAIMove();
            }
        }

        private void DoAIMove()
        {
            List<FieldButton> fieldButtons = new List<FieldButton>();

            for (int i = 0; i < m_fieldSize; ++i)
            {
                for (int j = 0; j < m_fieldSize; ++j)
                {
                    if (m_fieldValues[i, j] == 0)
                    {
                        fieldButtons.Add(m_fieldButtons[i, j]);
                    }
                }
            }

            fieldButtons[Random.Range(0, fieldButtons.Count)].ClickAction();
        }
        private void ShowRatings()
        {
            playerRating.text = PlayerPrefs.GetInt(GameConstants.Rating, 1000).ToString();
            opponentRatingValue = Random.Range(1000, 1500);
            opponentRating.text = opponentRatingValue.ToString();
        }
    }
}
