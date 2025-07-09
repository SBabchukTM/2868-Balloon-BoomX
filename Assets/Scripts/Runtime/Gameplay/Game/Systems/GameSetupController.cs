using System.Threading;
using Core;
using Cysharp.Threading.Tasks;
using Runtime.Gameplay.Game.Levels;
using Runtime.Gameplay.Game.Racing;

namespace Runtime.Gameplay.Game.Systems
{
    public class GameSetupController 
    {
        private readonly SystemsManager _systemsManager;
        private readonly GameData _gameData;
        private readonly RaceSpawner _raceSpawner;
        private readonly ISettingProvider _settingProvider;
        private readonly RaceManager _raceManager;
        private readonly RaceObjectsSpawner _raceObjectsSpawner;
        private readonly CheckpointSystem _checkpointSystem;
        private readonly GameTimer _gameTimer;
    
        private CancellationTokenSource _cancellationTokenSource;

        public GameSetupController(SystemsManager manager, GameData gameData, 
            RaceSpawner raceSpawner, ISettingProvider settingProvider, RaceManager raceManager,
            RaceObjectsSpawner raceObjectsSpawner, CheckpointSystem checkpointSystem, GameTimer gameTimer)
        {
            _systemsManager = manager;
            _gameData = gameData;
            _raceSpawner = raceSpawner;
            _settingProvider = settingProvider;
            _raceManager = raceManager;
            _raceObjectsSpawner = raceObjectsSpawner;
            _checkpointSystem = checkpointSystem;
            _gameTimer = gameTimer;
        
            _gameTimer.OnSecondPassed += UpdateGameDataTime;
        }

        public void StartGame()
        {
            ResetGameData();
        
            LevelConfig levelConfig = null;
        
            if(_gameData.GameplayMode == GameplayMode.Race)
                levelConfig =_settingProvider.Get<GameConfig>().LevelConfigs[_gameData.LevelID];
            else
                levelConfig = _settingProvider.Get<GameConfig>().TimeAttackConfig;
        
            _systemsManager.ResetAllSystems();
            _systemsManager.EnableAllSystem(true);
            _raceSpawner.SpawnRace(levelConfig);
            _raceObjectsSpawner.SpawnRaceObjects(levelConfig);
            _checkpointSystem.SpawnCheckpoints(levelConfig);
            _raceManager.SetRaceLength(levelConfig.RaceLength);
        
            _systemsManager.InitializeAllSystems();

            _cancellationTokenSource = new();

            if (_gameData.GameplayMode == GameplayMode.TimeAttack)
                _gameTimer.StartTime(_cancellationTokenSource.Token).Forget();
        }

        public void EndGame()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
        
            _systemsManager.EnableAllSystem(false);
            _systemsManager.CleanupAllSystems();
        }

        private void UpdateGameDataTime(int seconds)
        {
            _gameData.TimeAttackTime = seconds;
        }

        private void ResetGameData()
        {
            _gameData.Coins = 0;
            _gameData.TimeAttackTime = 0;
            _gameData.PlayerFinishPlace = 0;
        }
    }
}
