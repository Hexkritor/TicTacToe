using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TicTacToe.Scene_Game
{
    public class FieldButton : MonoBehaviour
    {
        public delegate void FieldButtonDelegate(Vector2Int position);
        public FieldButtonDelegate onClick;

        public Sprite player1Sprite;
        public Sprite player2Sprite;

        public Image fieldIcon;

        private Vector2Int m_position;

        public void SetPosition(Vector2Int position)
        {
            m_position = position;
        }

        public void ClickAction()
        {
            onClick?.Invoke(m_position);
        }

        public void ChangeColor(Game.Turn turn)
        {
            print(turn);
            switch (turn)
            {
                case Game.Turn.Player1:
                    fieldIcon.sprite = player1Sprite;
                    break;
                case Game.Turn.Player2:
                    fieldIcon.sprite = player2Sprite;
                    break;
            }

            gameObject.GetComponent<Button>().interactable = false;
        }
    }
}
