using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class SaveTester : MonoBehaviour
{
    [SerializeField] private int levelNumber;

    private void Awake()
    {
        ShowInfo();
    }
    [Button]
    private void SaveLevelPassed()
    {
        SaveManager.SaveLevelPassed(levelNumber);
    }
    [Button]
    private void SaveLevelNotPassed()
    {
        SaveManager.SaveLevelPassed(levelNumber, false);
    }
    [Button]
    private void ShowInfo()
    {
        List<LevelData> levels = SaveManager.LoadAllLevelData();

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
    private void SetAllLevelsNotPassed()
    {
        List<LevelData> levels = SaveManager.LoadAllLevelData();

        foreach (LevelData level in levels)
        {
            if (level.isPassed)
            {
                levelNumber = level.levelNumber;
                SaveLevelNotPassed();
            }
        }
        levelNumber = 0;
    }
    [Button]
    private void SetAllLevelsPassed()
    {
        List<LevelData> levels = SaveManager.LoadAllLevelData();

        foreach (LevelData level in levels)
        {
            if (!level.isPassed)
            {
                levelNumber = level.levelNumber;
                SaveLevelPassed();
            }
        }
        levelNumber = 0;
    }
    [Button]
    private void ResetData()
    {
        SaveManager.ResetData();
    }
}
