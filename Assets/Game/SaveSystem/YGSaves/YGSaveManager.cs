using System.Collections.Generic;
using YG;

public class YGSaveManager : SaveManager
{
    public override void SetLevelsData(List<LevelData> levels)
    {
        YandexGame.savesData.levelData = levels;
        YandexGame.SaveProgress();
    }
    public override List<LevelData> LoadAllLevelData()
    {
        return YandexGame.savesData.levelData;
    }
}
