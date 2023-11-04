using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject mainScreen;
    [SerializeField] private GameObject levelsScreen;

    private const string levelStr = "UILevel";

    private void Start()
    {
        OpenMainScreen();
    }
    public void OpenLevelScreen()
    {
        TurnOffEverything();
        levelsScreen.SetActive(true);
    }
    public void OpenMainScreen()
    {
        TurnOffEverything();
        mainScreen.SetActive(true);
    }
    public void LoadLevel(int levelNumber)
    {
        SceneManager.LoadScene(levelStr + levelNumber.ToString());
    }
    private void TurnOffEverything()
    {
        mainScreen.SetActive(false);
        levelsScreen.SetActive(false);
    }
}
