
public class PauseButton : ButtonBase
{
    protected override void OnButtonClick()
    {
        UIDirector.PauseGame();
    }
}
