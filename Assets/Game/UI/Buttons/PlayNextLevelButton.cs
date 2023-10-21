
public class PlayNextLevelButton : ButtonBase
{
    protected override void OnButtonClick()
    {
        LevelDirector.PlayNextLevel();
    }
}
