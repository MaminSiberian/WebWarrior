
namespace UI
{
    public class UnpauseButton : ButtonBase
    {
        protected override void OnButtonClick()
        {
            UIDirector.UnpauseGame();
        }
    }
}
