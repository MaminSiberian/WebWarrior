
namespace UI
{
    public class ReviveButton : ButtonBase
    {
        protected override void OnButtonClick()
        {
            EventSystem.SendPlayerRevive();
            gameObject.SetActive(false);
        }
    }
}
