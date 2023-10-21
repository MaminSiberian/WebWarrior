using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelDirector : MonoBehaviour
{
    public static event Action OnLevelFinished;

    private void Awake()
    {
        Time.timeScale = 1.0f;
    }
    public static void FinishLevel()
    {
        //Save game
        OnLevelFinished?.Invoke();
    }
    public static void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public static void PlayNextLevel()
    {

    }
    public static void ReturnToMainMenu()
    {

    }
}
