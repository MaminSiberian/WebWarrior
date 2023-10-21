using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class SaveTester : MonoBehaviour
{
    [SerializeField] private int levelNumber;
    [SerializeField] private bool isPassed;

    private void Awake()
    {
        ShowInfo();
    }
    [Button]
    private void SaveLevel()
    {
        SaveManager.SaveLevelPassed(levelNumber);
    }
    [Button]
    private void ShowInfo()
    {
        List<LevelData> levels = SaveManager.LoadAllLevelData();

        Debug.Log(levels);
    }
    private void ResetData()
    {
        SaveManager.ResetData();
    }
}
