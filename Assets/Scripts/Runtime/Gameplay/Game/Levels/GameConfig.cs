using System.Collections.Generic;
using Core;
using UnityEngine;

namespace Runtime.Gameplay.Game.Levels
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Config/GameConfig")]
    public class GameConfig : BaseSettings
    {
        [SerializeField] private List<LevelConfig> _levelConfigs;
        [SerializeField] private LevelConfig _timeAttackConfig;
    
        public List<LevelConfig> LevelConfigs => _levelConfigs;
        public LevelConfig TimeAttackConfig => _timeAttackConfig;
    }
}
