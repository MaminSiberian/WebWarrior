using System;
using UnityEngine;

public class UIDirector : MonoBehaviour
{
    [SerializeField] private GameObject _pauseButton;
    [SerializeField] private GameObject _pauseScreen;
    [SerializeField] private GameObject _gameOverScreen;
    [SerializeField] private GameObject _levelPassedScreen;

    private static GameObject pauseButton;
    private static GameObject pauseScreen;
    private static GameObject gameOverScreen;
    private static GameObject levelPassedScreen;

    public static event Action OnGamePaused;
    public static event Action OnGameUnpaused;

    private void Awake()
    {
        pauseButton = _pauseButton;
        pauseScreen = _pauseScreen;
        gameOverScreen = _gameOverScreen;
        levelPassedScreen = _levelPassedScreen;
    }
    private void OnEnable()
    {
        TestPlayer.OnPlayerDeath += OnPlayerDeath;
        LevelDirector.OnLevelFinished += OnLevelFinished;
    }
    private void OnDisable()
    {
        TestPlayer.OnPlayerDeath -= OnPlayerDeath;
        LevelDirector.OnLevelFinished -= OnLevelFinished;
    }
    public static void PauseGame()
    {
        Time.timeScale = 0f;
        pauseScreen.SetActive(true);
        pauseButton.SetActive(false);
    }
    public static void UnpauseGame()
    {
        pauseScreen.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1f;
    }
    private void OnPlayerDeath()
    {
        gameOverScreen.SetActive(true);
    }
    private void OnLevelFinished()
    {
        levelPassedScreen.SetActive(true);
    }
}
