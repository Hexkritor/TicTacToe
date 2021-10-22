using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TicTacToe.Commons
{
    public class SceneLoader : MonoBehaviour
    {

        public static SceneLoader instance { get; private set; }

        [SerializeField]
        private string m_loadingScene;
        [SerializeField]
        private bool m_useAsyncLoadProgress;

        private string m_currentScene;
        private string m_nextScene;
        private bool m_isLoading = false;

        public string loadingMessage { get; private set; }
        public float loadingProgress { get; private set; }
        public float maxLoadingProgress { get; private set; }

        private void Awake()
        {
            if (instance)
            {
                DestroyImmediate(instance.gameObject);
            }
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void OnDestroy()
        {
            if (instance == this)
            {
                instance = null;
            }
        }

        public void SetMessage(string message)
        {
            loadingMessage = message;
        }

        public void AddProgress(float ammount)
        {
            ammount = ammount <= 0 ? 0 : ammount;

            loadingProgress += ammount;
        }

        private void LoadLevel()
        {
            m_isLoading = true;

            SceneManager.LoadSceneAsync(m_loadingScene, LoadSceneMode.Single);
            
            loadingProgress = 0;

            if (m_useAsyncLoadProgress)
            {
                StartCoroutine(NormalLoading());
            }
            else
            {
                StartCoroutine(VarriableLoading());
            }
        }

        /// <summary>
        /// ассинхронная загрузка уровня стандартным методом юнити
        /// </summary>
        private IEnumerator NormalLoading()
        {
            var loadingOperation = SceneManager.LoadSceneAsync(m_nextScene, LoadSceneMode.Additive);
            loadingOperation.allowSceneActivation = false;

            maxLoadingProgress = 0.9f;

            while (loadingOperation.progress < 0.9f)
            {
                loadingProgress = loadingOperation.progress;
                yield return null;
            }

            loadingOperation.allowSceneActivation = true;

            Scene nextScene = SceneManager.GetSceneByName(m_nextScene);

            while (!nextScene.isLoaded)
            {
                yield return null;
            }

            SceneManager.SetActiveScene(nextScene);
            SceneManager.UnloadSceneAsync(m_loadingScene);

            m_isLoading = false;
        }

        /// <summary>
        /// ассинхронная загрузка уровня пока переменная loadingProgress не станет больше или равной maxLoadingProgress
        /// </summary>
        private IEnumerator VarriableLoading()
        {
            var loadingOperation = SceneManager.LoadSceneAsync(m_nextScene, LoadSceneMode.Additive);
            loadingOperation.allowSceneActivation = false;

            while (loadingProgress < maxLoadingProgress)
            {
                yield return null;
            }

            while (loadingOperation.progress < 0.9f)
            {
                yield return null;
            }

            loadingOperation.allowSceneActivation = true;

            Scene nextScene = SceneManager.GetSceneByName(m_nextScene);

            while (!nextScene.isLoaded)
            {
                yield return null;
            }

            SceneManager.SetActiveScene(nextScene);
            SceneManager.UnloadSceneAsync(m_loadingScene);

            m_isLoading = false;
        }

        public void LoadLevel(string sceneName, float maxProgress = 0)
        {
            if (m_isLoading)
            {
                return;
            }

            m_currentScene = SceneManager.GetActiveScene().name;
            m_nextScene = sceneName;

            m_useAsyncLoadProgress = maxProgress <= 0;
            maxLoadingProgress = maxProgress <= 0 ? 0 : maxProgress;

            LoadLevel();
        }
    }
}
