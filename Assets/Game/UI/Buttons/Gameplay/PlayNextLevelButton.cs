using UnityEngine.SceneManagement;

namespace UI
{
    public class PlayNextLevelButton : ButtonBase
    {
        protected override void OnEnable()
        {
            base.OnEnable();
            if (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 1)
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
