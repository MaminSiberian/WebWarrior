using Audio;

namespace UI
{
    public class SoundController : SliderBase
    {
        private void Start()
        {
            AudioManager.mixer.audioMixer.GetFloat(AudioManager.soundsVolumeParam, out float value);
            slider.value = value;
        }
        protected override void OnValueChanged(float value)
        {
            AudioManager.ChangeSoundsVolume(value);
        }
    }
}
