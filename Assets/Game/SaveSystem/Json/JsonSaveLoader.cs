using System.Collections.Generic;

public class JsonSaveLoader : SaveLoader
{
    private const string DATA_KEY = "Data.json";

    public override void SetLevelsData(List<LevelData> levels)
    {
        JsonSaveSystem.SaveToFile(levels, DATA_KEY);
    }
    public override List<LevelData> LoadAllLevelData()
    {
        return JsonSaveSystem.LoadFromFile<List<LevelData>>(DATA_KEY);
    }
}
