using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using Runtime.Gameplay.Game.Levels;

public class LevelConfigGenerator : EditorWindow
{
    private const int TotalLevels = 20;
    private const float MinRaceLength = 100f;
    private const float MaxRaceLength = 1000f;
    private const float MinBotSpeed = 0.6f;
    private const float MaxBotSpeed = 1.1f;
    private static readonly string OutputPath = "Assets/Resources/Levels";

    [MenuItem("Tools/Generate Level Configs")]
    public static void Generate()
    {
        if (!AssetDatabase.IsValidFolder(OutputPath))
        {
            AssetDatabase.CreateFolder("Assets/Resources", "Levels");
        }

        for (int i = 0; i < TotalLevels; i++)
        {
            LevelConfig config = ScriptableObject.CreateInstance<LevelConfig>();

            float t = i / (float)(TotalLevels - 1);
            float raceLength = Mathf.Lerp(MinRaceLength, MaxRaceLength, t);
            float botSpeed = Mathf.Lerp(MinBotSpeed, MaxBotSpeed, t);

            int botsCount = 3 + i / 2; // Example increase

            List<SpawnData> spawnDatas = GenerateSpawnData(i, raceLength);
            
            // Use reflection to set private fields
            SetField(config, "_raceLength", raceLength);
            SetField(config, "_botsSpeedMultiplier", botSpeed);
            SetField(config, "_botsCount", botsCount);
            SetField(config, "_spawnDatas", spawnDatas);
            SetField(config, "LevelDifficulty", i + 1);

            string assetPath = $"{OutputPath}/LevelConfig_{i + 1}.asset";
            AssetDatabase.CreateAsset(config, assetPath);
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log("LevelConfigs generated.");
    }

    private static List<SpawnData> GenerateSpawnData(int levelIndex, float raceLength)
    {
        List<SpawnData> list = new List<SpawnData>();
        float currentY = 0f;

        int maxUnlockedItems = Mathf.Min((int)GameItemType.Windmill + 1, 2 + levelIndex / 4);

        while (currentY < raceLength)
        {
            float nextStep = Random.Range(15f, 30f);
            currentY += nextStep;
            if (currentY > raceLength) break;

            Vector3 pos = new Vector3(Random.Range(-15f, 15f), currentY, 0f);
            GameItemType type = (GameItemType)Random.Range(0, maxUnlockedItems);
            list.Add(new SpawnData { SpawnPosition = pos, ItemType = type });
        }

        return list;
    }

    private static void SetField<T>(object target, string fieldName, T value)
    {
        var field = target.GetType().GetField(fieldName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
        if (field != null)
        {
            field.SetValue(target, value);
        }
    }
}
