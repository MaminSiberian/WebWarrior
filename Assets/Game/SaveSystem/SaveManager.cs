using System;
using System.Collections.Generic;
using UnityEngine;
using YG;
using NaughtyAttributes;
using System.Linq;

public class SaveManager : MonoBehaviour
{
    [SerializeField] private SaveSystem saveSystem;

    public static List<int> passelLevels { get; private set; }
    public static SaveLoader saveLoader { get; private set; }
    public static event Action<List<int>> OnLevelsDataChangedEvent;

    private void Awake()
    {
        passelLevels = new List<int>();

        switch (saveSystem)
        {
            case SaveSystem.Json:
                saveLoader = new JsonSaveLoader();
                break;
            case SaveSystem.YG:
                saveLoader = new YGSaveLoader();
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
    [Button]
    public static void LoadLevelsData()
    {
        passelLevels.Clear();
        List<LevelData> levels = saveLoader.LoadAllLevelData();
        if (levels == null)
            passelLevels.Add(0);
        else
        {
            foreach (var level in levels)
            {
                if (level.isPassed)
                    passelLevels.Add(level.levelNumber);
            }
        }
        if (!passelLevels.Any(l => l == 0)) passelLevels.Add(0);
        passelLevels.ForEach(l => Debug.Log(l));
        OnLevelsDataChangedEvent?.Invoke(passelLevels);
    }
    public static void SaveLevelPassed(int levelNumber, bool isPassed = true)
    {
        saveLoader.SaveLevelPassed(levelNumber, isPassed);
    }
}
