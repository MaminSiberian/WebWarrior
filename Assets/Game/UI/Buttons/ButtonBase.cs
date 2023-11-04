using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public abstract class ButtonBase : MonoBehaviour
    {
        protected Button button;

        protected virtual void Awake()
        {
            button = GetComponent<Button>();
        }
        protected virtual void OnEnable()
        {
            //button.onClick.AddListener(PlayClickSound);
            button.onClick.AddListener(OnButtonClick);
        }
        protected virtual void OnDisable()
        {
            //button?.onClick.RemoveListener(PlayClickSound);
            button?.onClick.RemoveListener(OnButtonClick);
        }
        /*private void PlayClickSound()
        {
            SoundsManager.PlayClickSound();
        }*/
        protected abstract void OnButtonClick();
    }
}
