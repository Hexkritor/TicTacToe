using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TicTacToe.Commons
{
    public class ExitPopup : Popup
    {
        public void CloseGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
			Application.Quit();
#endif
        }
    }
}
