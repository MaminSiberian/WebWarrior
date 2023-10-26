using System;
using UnityEngine;
using NaughtyAttributes;
using YG;

namespace UI
{
    public class UIDirector : MonoBehaviour
    {
        #region FIELDS
        [SerializeField] private GameObject _pauseButton;
        [SerializeField] private GameObject _pauseScreen;
        [SerializeField] private GameObject _gameOverScreen;
        [SerializeField] private GameObject _levelPassedScreen;
        [SerializeField] private GameObject _watchAddToReviveButton;
        [SerializeField] private GameObject _reviveButton;

        private static GameObject pauseButton;
        private static GameObject pauseScreen;
        private static GameObject gameOverScreen;
        private static GameObject levelPassedScreen;
        private static GameObject watchAddToReviveButton;
        private static GameObject reviveButton;

        private static YandexGame yandexSDK;
        private static int revivesCounter = 1;
        #endregion

        #region MONOBEHS
        private void Awake()
        {
            pauseButton = _pauseButton;
            pauseScreen = _pauseScreen;
            gameOverScreen = _gameOverScreen;
            levelPassedScreen = _levelPassedScreen;
            watchAddToReviveButton = _watchAddToReviveButton;
            reviveButton = _reviveButton;
        }
        private void OnEnable()
        {
            EventSystem.OnPlayerDeath.AddListener(OnPlayerDeath);
            EventSystem.OnLevelFinished.AddListener(OnLevelFinished);
            EventSystem.OnPauseEnable.AddListener(PauseGame);
            EventSystem.OnPauseDisable.AddListener(UnpauseGame);
            EventSystem.OnPlayerRevive.AddListener(OnPlayerRevive);
        }
        private void OnDisable()
        {
            EventSystem.OnPlayerDeath.RemoveListener(OnPlayerDeath);
            EventSystem.OnLevelFinished.RemoveListener(OnLevelFinished);
            EventSystem.OnPauseEnable.RemoveListener(PauseGame);
            EventSystem.OnPauseDisable.RemoveListener(UnpauseGame);
            EventSystem.OnPlayerRevive.RemoveListener(OnPlayerRevive);
            yandexSDK.RewardVideoAd.RemoveListener(OnAddEnded);
        }
        private void Start()
        {
            revivesCounter = 1;
            yandexSDK = FindAnyObjectByType<YandexGame>();
            yandexSDK.RewardVideoAd.AddListener(OnAddEnded);
        }
        #endregion

        #region PAUSE
        private static void PauseGame()
        {
            Time.timeScale = 0f;
            TurnOffAll();
            pauseScreen.SetActive(true);
        }
        private static void UnpauseGame()
        {
            TurnOffAll();
            pauseButton.SetActive(true);
            Time.timeScale = 1f;
        }
        #endregion

        public static void WatchAddToRevive()
        {
            yandexSDK._RewardedShow(1);
        }
        public void OnAddEnded()
        {
            Debug.Log(revivesCounter);
            reviveButton.SetActive(true);
        }

        [Button]
        private void OnPlayerDeath()
        {
            TurnOffAll();
            gameOverScreen.SetActive(true);
            reviveButton.SetActive(false);

            if (revivesCounter > 0)
                watchAddToReviveButton.SetActive(true);
        }
        [Button]
        private void OnLevelFinished()
        {
            TurnOffAll();
            levelPassedScreen.SetActive(true);
        }
        private static void TurnOffAll()
        {
            pauseButton.SetActive(false);
            pauseScreen.SetActive(false);
            gameOverScreen.SetActive(false);
            levelPassedScreen.SetActive(false);
        }
        private void OnPlayerRevive()
        {
            TurnOffAll();
            pauseButton.SetActive(true);
            revivesCounter--;
        }
    }
}
