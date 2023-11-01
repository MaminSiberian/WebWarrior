using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using YG;

public class SaveTester : MonoBehaviour
{
    [SerializeField] private SaveSystem saveSystem;
    [SerializeField] private int levelNumber;
    private SaveManager saveManager;

    private void Awake()
    {
        switch (saveSystem)
        {
            case SaveSystem.Json:
                saveManager = new JsonSaveManager();
                break;
            case SaveSystem.YG:
                saveManager = new YGSaveManager();
                break;
            default:
                break;
        }
    }
    private void OnEnable()
    {
        if (saveSystem == SaveSystem.YG) YandexGame.GetDataEvent += ShowInfo;
    }
    private void OnDisable()
    {
        if (saveSystem == SaveSystem.YG) YandexGame.GetDataEvent -= ShowInfo;
    }
    private void Start()
    {
        if (YandexGame.SDKEnabled)
        {
            ShowInfo();
        }
    }

    [Button]
    private void ShowInfo()
    {
        List<LevelData> levels = saveManager.LoadAllLevelData();

        if (levels == null)
        {
            Debug.Log("Save file is empty");
            return;
        }

        foreach (LevelData level in levels)
        {
            Debug.Log($"Level: {level.levelNumber}, is Passed: {level.isPassed}");
        }
    }
    [Button]
    private void SaveLevelPassed(bool isPassed = true)
    {
        saveManager.SaveLevelPassed(levelNumber, isPassed);
    }
    [Button]
    private void SetAllLevelsPassed(bool isPassed = true)
    {
        List<LevelData> levels = saveManager.LoadAllLevelData();

        foreach (LevelData level in levels)
        {
            if (level.isPassed != isPassed)
            {
                levelNumber = level.levelNumber;
                SaveLevelPassed(isPassed);
            }
        }
        levelNumber = 0;
    }
    [Button]
    private void SaveLevelNotPassed()
    {
        SaveLevelPassed(false);
    }
    [Button]
    private void SetAllLevelsNotPassed()
    {
        SetAllLevelsPassed(false);
    }
    [Button]
    private void ResetData()
    {
        saveManager.ResetData();
    }
    [Button]
    private bool LevelIsPassed()
    {
        bool result = saveManager.LevelIsPassed(levelNumber);
        Debug.Log(result);
        return result;
    }
}
