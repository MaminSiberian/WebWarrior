using UnityEngine;

namespace UI
{
    public class WatchAddToReviveButton : ButtonBase
    {
        protected override void OnButtonClick()
        {
            AdvManager.WatchAddToRevive();
            gameObject.SetActive(false);
        }
    }
}