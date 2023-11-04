using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelDirector : MonoBehaviour
{
    private const string mainMenuStr = "MainMenu";
    private const string levelStr = "UILevel";

    private void Awake()
    {
        Time.timeScale = 1.0f;
        Debug.Log(SceneManager.sceneCountInBuildSettings);
    }
    [Button]
    public static void FinishLevel()
    {
        int currentLevel = int.Parse(SceneManager.GetActiveScene().name.Substring(levelStr.Length));
        SaveManager.SaveLevelPassed(currentLevel);
        EventSystem.OnLevelFinished?.Invoke();
    }
    public static void RestartLevel()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public static void PlayNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public static void ReturnToMainMenu()
    {
        SceneManager.LoadScene(mainMenuStr);
    }
}
