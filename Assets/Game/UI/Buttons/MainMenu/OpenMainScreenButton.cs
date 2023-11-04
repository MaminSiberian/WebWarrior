
namespace UI
{
    public class OpenMainScreenButton : ButtonBase
    {
        private MainMenuManager manager;

        protected override void Awake()
        {
            base.Awake();
            manager = FindAnyObjectByType<MainMenuManager>();
        }
        protected override void OnButtonClick()
        {
            manager.OpenMainScreen();
        }
    }
}
