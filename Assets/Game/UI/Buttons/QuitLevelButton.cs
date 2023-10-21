
public class QuitLevelButton : ButtonBase
{
    protected override void OnButtonClick()
    {
        LevelDirector.ReturnToMainMenu();
    }
}
