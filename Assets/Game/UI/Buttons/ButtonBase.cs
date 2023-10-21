using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public abstract class ButtonBase : MonoBehaviour
    {
        private Button button;

        private void Awake()
        {
            button = GetComponent<Button>();
        }
        private void OnEnable()
        {
            //button.onClick.AddListener(PlayClickSound);
            button.onClick.AddListener(OnButtonClick);
        }
        private void OnDisable()
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
