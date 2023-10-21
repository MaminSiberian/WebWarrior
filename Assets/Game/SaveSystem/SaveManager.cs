using System.Collections.Generic;
using System.Linq;

public static class SaveManager
{
    private const string DATA_KEY = "Data.json";

    public static void SetLevelsData(List<LevelData> levels)
    {
        SaveSystem.SaveToFile(levels, DATA_KEY);
    }
    public static void SaveLevelPassed(int levelNumber, bool isPassed = true)
    {
        List<LevelData> levels = LoadAllLevelData();        

        if (levels == null)
        {
            levels = new List<LevelData>();
        }

        if (levels.Any(l => l.levelNumber == levelNumber))
        {
            int index = levels.IndexOf(levels.FirstOrDefault(l => l.levelNumber == levelNumber));
            levels[index] = new LevelData() { levelNumber = levelNumber, isPassed = isPassed };
        }
        else
        {
            LevelData newLevel = new LevelData() { levelNumber = levelNumber, isPassed = isPassed };
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
