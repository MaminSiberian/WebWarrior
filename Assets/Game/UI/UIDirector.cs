using UnityEngine;
using NaughtyAttributes;

namespace UI
{
    [RequireComponent(typeof(AdvManager))]
    public class UIDirector : MonoBehaviour
    {
        #region FIELDS
        [SerializeField] private GameObject pauseButton;
        [SerializeField] private GameObject pauseScreen;
        [SerializeField] private GameObject gameOverScreen;
        [SerializeField] private GameObject levelPassedScreen;
        [SerializeField] private GameObject watchAddToReviveButton;
        [SerializeField] private GameObject reviveButton;
        #endregion

        #region MONOBEHS

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

        public void ShowReviveButton()
        {
            reviveButton.SetActive(true);
        }
        public void ShowAddButton()
        {
            watchAddToReviveButton.SetActive(true);
        }

        [Button]
        private void OnPlayerDeath()
        {
            TurnOffAll();
            gameOverScreen.SetActive(true);
            reviveButton.SetActive(false);
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
        }
    }
}
