using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TicTacToe.Commons
{
    public abstract class Popup : MonoBehaviour
    {
        protected float timeScale;

        public virtual void ShowPopup()
        {
            timeScale = Time.timeScale;
            Time.timeScale = 0;
        }

        public virtual void ClosePopup()
        {
            Time.timeScale = timeScale;
            Destroy(gameObject);
        }
    }
}
