using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class SaveManager
{
    private const string DATA_KEY = "Data.json";

    private static void SetLevelsData(List<LevelData> levels)
    {
        SaveSystem.SaveToFile(levels, DATA_KEY);
    }
    public static void SaveLevelPassed(int levelNumber)
    {
        List<LevelData> levels = LoadAllLevelData();        

        if (levels.Any(l => l.levelNumber == levelNumber))
        {
            var level = levels.FirstOrDefault(l => l.levelNumber == levelNumber);
            level.isPassed = true;
        }
        else
        {
            LevelData newLevel = new LevelData() { levelNumber = levelNumber, isPassed = true };
            levels.Add(newLevel);
        }

        SetLevelsData(levels);
    }
    public static List<LevelData> LoadAllLevelData()
    {
        return SaveSystem.LoadFromFile<List<LevelData>>(DATA_KEY);
    }
    public static bool LevelIsPassed(int levelNumber)
    {
        List<LevelData> levels = LoadAllLevelData();

        if (!levels.Any(l => l.levelNumber == levelNumber))
            return false;

        var level = levels.FirstOrDefault(l => l.levelNumber == levelNumber);
        return level.isPassed;
    }
    public static void ResetData()
    {
        SetLevelsData(null);
    }
}
