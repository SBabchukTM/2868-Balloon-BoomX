using System.Collections.Generic;
using Core;
using Core.Factory;
using Runtime.Gameplay.Game.Levels;
using Runtime.Gameplay.Game.Movement;
using Runtime.Gameplay.Game.Systems;
using Runtime.Gameplay.GameplayStates.Game;
using Runtime.Gameplay.HelperServices.SettingsProvider;
using UnityEngine;
using Zenject;

namespace Runtime.Gameplay.Game.Racing
{
    public class RaceSpawner : IInitializable, ICleanupSystem
    {
        public const float SpawnYPos = -22f;
        private const float LeftStartSpawnPos = -12f;
        private const float HorizontalSpawnOffset = 8;
    
        private readonly IAssetProvider _assetProvider;
        private readonly GameObjectFactory _gameObjectFactory;
        private readonly GameInstaller.BotInputProviderFactory _botInputProviderFactory;
        private readonly BalloonPositionClamper _positionClamper;
        private readonly SkinProvider _skinProvider;
    
        private GameObject _finishLinePrefab;
        private GameObject _playerPrefab;
        private GameObject _botPrefab;

        private readonly List<GameObject> _spawnedObjects = new();
    
        public async void Initialize()
        {
            _finishLinePrefab = await _assetProvider.Load<GameObject>(ConstPrefabs.FinishLinePrefab);
            _playerPrefab = await _assetProvider.Load<GameObject>(ConstPrefabs.PlayerPrefab);
            _botPrefab = await _assetProvider.Load<GameObject>(ConstPrefabs.BotPrefab);
        }

        public RaceSpawner(IAssetProvider assetProvider, GameObjectFactory gameObjectFactory, SystemsManager systemsManager, 
            GameInstaller.BotInputProviderFactory botInputProviderFactory, BalloonPositionClamper positionClamper, SkinProvider skinProvider)
        {
            _assetProvider = assetProvider;
            _gameObjectFactory = gameObjectFactory;
            _botInputProviderFactory = botInputProviderFactory;
            _positionClamper = positionClamper;
            _skinProvider = skinProvider;
        
            systemsManager.RegisterSystem(this);
        }

        public void SpawnRace(LevelConfig levelConfig)
        {
            SpawnFinishLines(levelConfig.RaceLength);
            SpawnBalloons(levelConfig.BotsCount, levelConfig.BotsSpeedMultiplier);
        }

        private void SpawnFinishLines(float yPos)
        {
            SpawnFinishLine(new Vector3(0, SpawnYPos, 0));
            SpawnFinishLine(new Vector3(0, yPos, 0));
        }

        private void SpawnFinishLine(Vector3 position)
        {
            var finishLine = _gameObjectFactory.Create(_finishLinePrefab);
            finishLine.transform.position = position;
            _spawnedObjects.Add(finishLine);
        }
    
        private void SpawnBalloons(int bots, float botsDifficulty)
        {
            float leftPos = LeftStartSpawnPos;
        
            SpawnPlayer(leftPos);

            for (int i = 0; i < bots; i++)
            {
                leftPos += HorizontalSpawnOffset;
                SpawnBot(new Vector3(leftPos, SpawnYPos, 0), botsDifficulty);
            }
        }

        private void SpawnPlayer(float leftPos)
        {
            var player = _gameObjectFactory.Create<PlayerBalloonController>(_playerPrefab);
            player.transform.position = new Vector3(leftPos, SpawnYPos, 0);
            player.SetSprite(_skinProvider.GetPlayerSkin());
        
            _spawnedObjects.Add(player.gameObject);
        }

        public void SpawnBot(Vector3 position, float botsDifficulty)
        {
            var bot = _gameObjectFactory.Create<BaseBalloonController>(_botPrefab, position, Quaternion.identity, null);

            var movement = bot.GetComponent<BotBalloonMovement>();
            movement.SetSpeedMultiplier(botsDifficulty);
        
            var inputProvider = _botInputProviderFactory.Create();
            inputProvider.SetTransform(bot.transform);
            movement.Construct(inputProvider, _positionClamper);
        
            bot.SetSprite(_skinProvider.GetRandomSkin());
        
            _spawnedObjects.Add(bot.gameObject);
        }
    
        public void Cleanup()
        {
            foreach (var spawnedObject in _spawnedObjects)
                Object.Destroy(spawnedObject.gameObject);
        
            _spawnedObjects.Clear();
        }
    }
}
