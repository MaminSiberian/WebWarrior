using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelDirector : MonoBehaviour
{
    private void Awake()
    {
        Time.timeScale = 1.0f;
    }
    public static void FinishLevel()
    {
        Debug.Log("save game: no method");
        EventSystem.OnLevelFinished?.Invoke();
    }
    public static void RestartLevel()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public static void PlayNextLevel()
    {
        Debug.Log("next level: no method");
    }
    public static void ReturnToMainMenu()
    {
        Debug.Log("quit game: no method");
    }
}
