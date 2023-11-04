
namespace UI
{
    public class OpenLevelsButton : ButtonBase
    {
        private MainMenuManager manager;

        protected override void Awake()
        {
            base.Awake();
            manager = FindAnyObjectByType<MainMenuManager>();
        }
        protected override void OnButtonClick()
        {
            manager.OpenLevelScreen();
        }
    }
}
