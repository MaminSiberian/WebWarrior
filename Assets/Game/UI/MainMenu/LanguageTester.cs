using UnityEngine;
using YG;
using NaughtyAttributes;

public class LanguageTester : MonoBehaviour
{
    private void Start()
    {
        Debug.Log(YandexGame.EnvironmentData.language);
    }
    [Button]
    private void GoRu()
    {
        YandexGame.SwitchLanguage("ru");
    }
    [Button]
    private void GoEn()
    {
        YandexGame.SwitchLanguage("en");
    }
    [Button]
    private void GoTr()
    {
        YandexGame.SwitchLanguage("tr");
    }
}
