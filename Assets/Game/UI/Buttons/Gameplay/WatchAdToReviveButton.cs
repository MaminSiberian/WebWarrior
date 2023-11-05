using UnityEngine;

namespace UI
{
    public class WatchAdToReviveButton : ButtonBase
    {
        protected override void OnButtonClick()
        {
            AdvManager.WatchAddToRevive();
            gameObject.SetActive(false);
            Time.timeScale = 0f;
        }
    }
}