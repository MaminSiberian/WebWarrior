using System;
using UnityEngine;
using NaughtyAttributes;

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
        TurnOffAll();
        pauseScreen.SetActive(true);
    }
    public static void UnpauseGame()
    {
        TurnOffAll();
        pauseButton.SetActive(true);
        Time.timeScale = 1f;
    }
    [Button]
    private void OnPlayerDeath()
    {
        TurnOffAll();
        gameOverScreen.SetActive(true);
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
}
