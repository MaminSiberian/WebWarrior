
namespace UI
{
    public class PlayNextLevelButton : ButtonBase
    {
        protected override void OnEnable()
        {
            base.OnEnable();
            if (LevelDirector.currentSceneNumber == LevelDirector.numberOfLevels)
            {
                gameObject.SetActive(false);
            }
        }
        protected override void OnButtonClick()
        {
            LevelDirector.PlayNextLevel();
        }
    }
}
