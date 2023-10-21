using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public abstract class SliderBase : MonoBehaviour
    {
        protected Slider slider;

        private void Awake()
        {
            slider = GetComponent<Slider>();
        }
        private void OnEnable()
        {
            slider.onValueChanged.AddListener(OnValueChanged);
        }
        private void OnDisable()
        {
            slider?.onValueChanged.RemoveListener(OnValueChanged);
        }
        protected abstract void OnValueChanged(float value);
    }
}
