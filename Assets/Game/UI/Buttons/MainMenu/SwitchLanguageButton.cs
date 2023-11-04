using UnityEngine;
using YG;

namespace UI 
{
    public class SwitchLanguageButton : ButtonBase
    {
        [SerializeField] private string language;

        protected override void OnButtonClick()
        {
            YandexGame.SwitchLanguage(language);
        }
    }
}