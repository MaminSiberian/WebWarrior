using System.Collections.Generic;
using System.Linq;

public abstract class SaveManager
{
    public void SaveLevelPassed(int levelNumber, bool isPassed = true)
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
    public LevelData LoadLevelData(int levelNumber)
    {
        List<LevelData> levels = LoadAllLevelData();

        if (levels == null) levels = new List<LevelData>();

        if (!levels.Any(l => l.levelNumber == levelNumber))
        {
            SaveLevelPassed(levelNumber, false);
        }
        var level = levels.FirstOrDefault(l => l.levelNumber == levelNumber);
        return level;
    }
    public bool LevelIsPassed(int levelNumber)
    {
        return LoadLevelData(levelNumber).isPassed;
    }

    public abstract void SetLevelsData(List<LevelData> levels); //protected
    public abstract List<LevelData> LoadAllLevelData(); //protected
    public void ResetData() // delete
    {
        SetLevelsData(null);
    }
}
