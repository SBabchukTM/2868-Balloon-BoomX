using System.Collections.Generic;
using Core;
using Core.Factory;
using Runtime.Gameplay.Game.Levels;
using Runtime.Gameplay.Game.Racing;
using Runtime.Gameplay.Game.Systems;
using Runtime.Gameplay.HelperServices.SettingsProvider;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Runtime.Gameplay.Game
{
    public class CheckpointSystem : IInitializable, ICleanupSystem
    {
        private readonly CameraSizeProvider _cameraSizeProvider;
        private readonly IAssetProvider _assetProvider;
        private readonly GameObjectFactory _gameObjectFactory;

        private GameObject _checkpointPrefab;

        private List<GameObject> _checkpointObjects = new();
        
        public CheckpointSystem(CameraSizeProvider cameraSizeProvider, IAssetProvider assetProvider,
            GameObjectFactory gameObjectFactory, SystemsManager systemsManager)
        {
            _cameraSizeProvider = cameraSizeProvider;
            _assetProvider = assetProvider;
            _gameObjectFactory = gameObjectFactory;
            
            systemsManager.RegisterSystem(this);
        }

        public async void Initialize()
        {
            _checkpointPrefab = await _assetProvider.Load<GameObject>(ConstPrefabs.CheckpointPrefab);
        }

        public void SpawnCheckpoints(LevelConfig levelConfig)
        {
            if(levelConfig.Checkpoints <= 0)
                return;
        
            float spawnInterval = levelConfig.RaceLength / (levelConfig.Checkpoints + 1);

            float xSpawnPos = _cameraSizeProvider.HalfSize.x;
        
            for (int i = 0; i < levelConfig.Checkpoints; i++)
            {
                var checkpoint = _gameObjectFactory.Create(_checkpointPrefab);
                checkpoint.transform.position = new Vector3(xSpawnPos, (i + 1) * spawnInterval, 0);

                _checkpointObjects.Add(checkpoint.gameObject);
            }
        }

        public void ResetBalloonToCheckpoint(BaseBalloonController balloon)
        {
            Vector3 lastCheckpointPos = new Vector3(0, RaceSpawner.SpawnYPos, 0);

            Vector3 balloonPos = balloon.transform.position;
            
            for (int i = 0; i < _checkpointObjects.Count; i++)
            {
                Vector3 checkpointPos = _checkpointObjects[i].transform.position;
                if (balloonPos.y > checkpointPos.y)
                    lastCheckpointPos = checkpointPos;
                else
                    break;
            }
            
            balloon.Teleport(lastCheckpointPos);
        }
        
        public void Cleanup()
        {
            foreach (var checkpointObject in _checkpointObjects)
                Object.Destroy(checkpointObject.gameObject);
            
            _checkpointObjects.Clear();
        }
    }
}