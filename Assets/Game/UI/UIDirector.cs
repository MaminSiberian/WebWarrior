using System;
using UnityEngine;
using NaughtyAttributes;
using YG;

namespace UI
{
    public class UIDirector : MonoBehaviour
    {
        #region FIELDS
        [SerializeField] private GameObject pauseButton;
        [SerializeField] private GameObject pauseScreen;
        [SerializeField] private GameObject gameOverScreen;
        [SerializeField] private GameObject levelPassedScreen;
        [SerializeField] private GameObject watchAddToReviveButton;
        [SerializeField] private GameObject reviveButton;
        //[SerializeField] private AdvCooldownTimer advTimer;

        private static YandexGame yandexSDK;
        private static int revivesCounter = 1;
        #endregion

        #region MONOBEHS
        private void Awake()
        {
            //advTimer = Instantiate(advTimer);
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
            yandexSDK.infoYG.AdWhenLoadingScene = false;
        }
        #endregion

        #region PAUSE
        private void PauseGame()
        {
            Time.timeScale = 0f;
            TurnOffAll();
            pauseScreen.SetActive(true);
        }
        private void UnpauseGame()
        {
            TurnOffAll();
            pauseButton.SetActive(true);
            Time.timeScale = 1f;
        }
        #endregion

        public static void WatchAddToRevive()
        {
            yandexSDK._RewardedShow(0);
        }
        public void OnAddEnded()
        {
            Debug.Log(revivesCounter);
            reviveButton.SetActive(true);
            //advTimer.StartTimer();
        }

        [Button]
        private void OnPlayerDeath()
        {
            TurnOffAll();
            gameOverScreen.SetActive(true);
            reviveButton.SetActive(false);

            if (revivesCounter > 0 /*&& advTimer.addIsReady*/)
                watchAddToReviveButton.SetActive(true);
        }
        [Button]
        private void OnLevelFinished()
        {
            TurnOffAll();
            levelPassedScreen.SetActive(true);
        }
        private void TurnOffAll()
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
