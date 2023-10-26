using UnityEngine;

namespace UI
{
    public class WatchAddToReviveButton : ButtonBase
    {
        protected override void OnButtonClick()
        {
            UIDirector.WatchAddToRevive();
            gameObject.SetActive(false);
        }
    }
}
