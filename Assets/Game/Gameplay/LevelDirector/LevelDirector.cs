using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelDirector : MonoBehaviour
{
    public static int currentSceneNumber {  get; private set; }
    public static int numberOfLevels => 3;

    private const string mainMenuStr = "MainMenu";
    private const string levelStr = "UILevel";

    private void Awake()
    {
        Time.timeScale = 1.0f;
    }
    private void Start()
    {
        currentSceneNumber = int.Parse(SceneManager.GetActiveScene().name.Substring(levelStr.Length));
    }
    
    [Button]
    public static void FinishLevel()
    {
        SaveManager.SaveLevelPassed(currentSceneNumber);
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
