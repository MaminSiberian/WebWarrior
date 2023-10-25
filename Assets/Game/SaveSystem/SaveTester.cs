using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class SaveTester : MonoBehaviour
{
    [SerializeField] private int levelNumber;

    private void Awake()
    {
        
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
    private void SaveLevelPassed(bool isPassed = true)
    {
        SaveManager.SaveLevelPassed(levelNumber, isPassed);
    }
    [Button]
    private void SetAllLevelsPassed(bool isPassed = true)
    {
        List<LevelData> levels = SaveManager.LoadAllLevelData();

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
        SaveManager.ResetData();
    }
    [Button]
    private bool LevelIsPassed()
    {
        bool result = SaveManager.LevelIsPassed(levelNumber);
        Debug.Log(result);
        return result;
    }
}
