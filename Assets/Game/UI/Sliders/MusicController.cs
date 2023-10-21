using Audio;

namespace UI
{
    public class MusicController : SliderBase
    {
        private void Start()
        {
            AudioManager.mixer.audioMixer.GetFloat(AudioManager.musicVolumeParam, out float value);
            slider.value = value;
        }
        protected override void OnValueChanged(float value)
        {
            AudioManager.ChangeMusicVolume(value);
        }
    }
}
