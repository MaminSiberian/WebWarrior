using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;
using YG;

public class AdvTester : MonoBehaviour
{
    private YandexGame sdk;
    [SerializeField] private Button advButton;
    [SerializeField] private Button resetAdv;

    private void Awake()
    {
        sdk = FindAnyObjectByType<YandexGame>();
        resetAdv.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        advButton?.onClick.AddListener(ShowAdv);
        resetAdv?.onClick.AddListener(ResetAdv);
    }
    private void OnDisable()
    {
        advButton?.onClick.RemoveListener(ShowAdv);
        resetAdv?.onClick.RemoveListener(ResetAdv);
    }

    [Button]
    public void ShowAdv()
    {
        Debug.Log("Showing");
        advButton.gameObject.SetActive(false);
        sdk._RewardedShow(1);
    }
    [Button]
    public void OnAdvEnded()
    {
        Debug.Log("Ended");
        resetAdv.gameObject.SetActive(true);
    }
    public void ResetAdv()
    {
        resetAdv.gameObject.SetActive(false);
        advButton.gameObject.SetActive(true);
    }
}
