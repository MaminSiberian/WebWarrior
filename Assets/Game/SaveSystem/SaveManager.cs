using System;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class SaveManager : MonoBehaviour
{
    [SerializeField] private SaveSystem saveSystem;

    public List<int> passelLevels { get; private set; }
    public static event Action<List<int>> OnLevelsDataChangedEvent;
    private SaveLoader saveManager;

    private void Awake()
    {
        passelLevels = new List<int>();

        switch (saveSystem)
        {
            case SaveSystem.Json:
                saveManager = new JsonSaveLoader();
                break;
            case SaveSystem.YG:
                saveManager = new YGSaveLoader();
                break;
            default:
                break;
        }
    }
    private void OnEnable()
    {
        if (saveSystem == SaveSystem.YG) YandexGame.GetDataEvent += LoadLevelsData;
    }
    private void OnDisable()
    {
        if (saveSystem == SaveSystem.YG) YandexGame.GetDataEvent -= LoadLevelsData;
    }
    private void Start()
    {
        if (YandexGame.SDKEnabled)
        {
            LoadLevelsData();
        }
    }
    public void LoadLevelsData()
    {
        List<LevelData> levels = saveManager.LoadAllLevelData();
        foreach (var level in levels)
        {
            if (level.isPassed)
            {
                passelLevels.Add(level.levelNumber);
            }
        }
        passelLevels.ForEach(l => Debug.Log(l));
        OnLevelsDataChangedEvent?.Invoke(passelLevels);
    }
    public void SaveLevelPassed(int levelNumber)
    {
        saveManager.SaveLevelPassed(levelNumber);
    }
}
