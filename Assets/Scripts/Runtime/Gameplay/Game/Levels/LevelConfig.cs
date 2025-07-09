using System;
using System.Collections.Generic;
using UnityEngine;

namespace Runtime.Gameplay.Game.Levels
{
    [CreateAssetMenu(fileName = "Level Config 0", menuName = "Config/Level Config")]
    public class LevelConfig : ScriptableObject
    {
        [SerializeField] private float _raceLength;
        [SerializeField] private int _botsCount;
        [SerializeField] private List<SpawnData> _spawnDatas;
        [SerializeField] private float _botsSpeedMultiplier;
        [SerializeField] private int _checkpoints;
        [SerializeField] private int _minCoins;
        [SerializeField] private int _maxCoins;
    
        public float RaceLength => _raceLength;
        public int BotsCount => _botsCount;
        public List<SpawnData> SpawnDatas => _spawnDatas;
        public float BotsSpeedMultiplier => _botsSpeedMultiplier;
        public int Checkpoints => _checkpoints;
        public int MinCoins => _minCoins;
        public int MaxCoins => _maxCoins;
    }

    [Serializable]
    public class SpawnData
    {
        public Vector3 SpawnPosition;
        public GameItemType ItemType;
    }

    public enum GameItemType
    {
        BrickWall,
        Cactus,
        Knife,
        Teleport,
        Windmill
    }
}