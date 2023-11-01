using UI;
using UnityEngine;
using YG;

[RequireComponent(typeof(UIDirector))]
public class AdvManager : MonoBehaviour
{
    private int revivesCounter = 1;
    private UIDirector director;

    private void Awake()
    {
        director = GetComponent<UIDirector>();
    }
    private void OnEnable()
    {
        YandexGame.RewardVideoEvent += OnAddEnded;
        EventSystem.OnPlayerDeath.AddListener(OnPlayerDeath);
        EventSystem.OnPlayerRevive.AddListener(OnPlayerRevive);
    }
    private void OnDisable()
    {
        YandexGame.RewardVideoEvent -= OnAddEnded;
        EventSystem.OnPlayerDeath.RemoveListener(OnPlayerDeath);
        EventSystem.OnPlayerRevive.RemoveListener(OnPlayerRevive);
    }
    private void Start()
    {
        revivesCounter = 1;
    }

    public static void WatchAddToRevive()
    {
        YandexGame.RewVideoShow(0);
    }
    public void OnAddEnded(int id)
    {
        director.ShowReviveButton();
    }
    private void OnPlayerDeath()
    {
        if (revivesCounter > 0)
            director.ShowAddButton();
    }
    private void OnPlayerRevive()
    {
        revivesCounter--;
    }
}
