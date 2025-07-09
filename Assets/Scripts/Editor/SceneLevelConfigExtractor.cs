using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Runtime.Gameplay.Game.Items;
using Runtime.Gameplay.Game.Levels;
using UnityEditor;
using UnityEngine;

public static class SceneLevelConfigExtractor
{
    [MenuItem("Tools/Extract LevelConfig From Scene")]
    public static void Extract()
    {
        var spawnDatas = new List<SpawnData>();
        var highestY = 0f;

        foreach (var go in GameObject.FindObjectsOfType<GameObject>())
        {
            if (!go.activeInHierarchy) continue;

            var type = GetItemTypeFromComponents(go);
            if (type == null) continue;

            var pos = go.transform.position;
            spawnDatas.Add(new SpawnData
            {
                SpawnPosition = pos,
                ItemType = type.Value
            });

            if (pos.y > highestY)
                highestY = pos.y;
        }

        var config = ScriptableObject.CreateInstance<LevelConfig>();

        SetField(config, "_raceLength", highestY);
        SetField(config, "_botsCount", 5); // Customize as needed
        SetField(config, "_botsSpeedMultiplier", 1.0f); // Customize as needed
        SetField(config, "_spawnDatas", spawnDatas);
        SetField(config, "LevelDifficulty", 999); // Special marker for Time Attack

        const string outputPath = "Assets/Resources/Levels/TimeAttack";
        if (!Directory.Exists(outputPath)) Directory.CreateDirectory(outputPath);

        var assetPath = $"{outputPath}/TimeAttack_Level.asset";
        AssetDatabase.CreateAsset(config, assetPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log($"Time attack level generated with {spawnDatas.Count} items. Max height: {highestY:F1}");
    }

    private static GameItemType? GetItemTypeFromComponents(GameObject go)
    {
        if (go.GetComponent<BrickWallItem>()) return GameItemType.BrickWall;
        if (go.GetComponent<CactusItem>()) return GameItemType.Cactus;
        if (go.GetComponent<KnifeItem>()) return GameItemType.Knife;
        if (go.GetComponent<TeleportItem>()) return GameItemType.Teleport;
        if (go.GetComponent<WindmillItem>()) return GameItemType.Windmill;

        return null;
    }

    private static void SetField<T>(object target, string fieldName, T value)
    {
        var field = target.GetType()
            .GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
        if (field != null) field.SetValue(target, value);
    }
}