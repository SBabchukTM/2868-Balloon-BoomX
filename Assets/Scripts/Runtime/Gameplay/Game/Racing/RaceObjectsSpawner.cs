using System.Collections.Generic;
using Core;
using Core.Factory;
using Runtime.Gameplay.Game.Levels;
using Runtime.Gameplay.Game.Systems;
using Runtime.Gameplay.HelperServices.SettingsProvider;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Runtime.Gameplay.Game.Racing
{
    public class RaceObjectsSpawner : IInitializable, ICleanupSystem
    {
        private readonly IAssetProvider _assetProvider;
        private readonly GameObjectFactory _gameObjectFactory;
    
        private GameObject _knifePrefab;
        private GameObject _brickWallPrefab;
        private GameObject _cactusPrefab;
        private GameObject _windmillPrefab;
        private GameObject _teleportPrefab;
        private GameObject _coinPrefab;
    
        private readonly List<GameObject> _spawnedObjects = new ();
        private readonly List<Bounds> _occupiedBounds = new ();

        public async void Initialize()
        {
            _knifePrefab = await _assetProvider.Load<GameObject>(ConstPrefabs.KnifePrefab);
            _brickWallPrefab = await _assetProvider.Load<GameObject>(ConstPrefabs.BrickWallPrefab);
            _cactusPrefab = await _assetProvider.Load<GameObject>(ConstPrefabs.CactusPrefab);
            _windmillPrefab = await _assetProvider.Load<GameObject>(ConstPrefabs.WindMillPrefab);
            _teleportPrefab = await _assetProvider.Load<GameObject>(ConstPrefabs.TeleportPrefab);
            _coinPrefab = await _assetProvider.Load<GameObject>(ConstPrefabs.CoinPrefab);
        }

        public RaceObjectsSpawner(IAssetProvider assetProvider, GameObjectFactory gameObjectFactory, 
            SystemsManager systemsManager)
        {
            _assetProvider = assetProvider;
            _gameObjectFactory = gameObjectFactory;
        
            systemsManager.RegisterSystem(this);
        }

        public void SpawnRaceObjects(LevelConfig levelConfig)
        {
            foreach (var item in levelConfig.SpawnDatas)
                SpawnItem(item);

            SpawnCoins(levelConfig);
        }

        private void SpawnItem(SpawnData spawnData)
        {
            _occupiedBounds.Clear();
        
            GameObject spawnedItem = null;

            switch (spawnData.ItemType)
            {
                case GameItemType.BrickWall:
                    spawnedItem = _gameObjectFactory.Create(_brickWallPrefab);
                    break;
                case GameItemType.Cactus:
                    spawnedItem = _gameObjectFactory.Create(_cactusPrefab);
                    break;
                case GameItemType.Windmill:
                    spawnedItem = _gameObjectFactory.Create(_windmillPrefab);
                    break;
                case GameItemType.Teleport:
                    spawnedItem = _gameObjectFactory.Create(_teleportPrefab);
                    break;
                case GameItemType.Knife:
                    spawnedItem = _gameObjectFactory.Create(_knifePrefab);
                    break;
                default:
                    spawnedItem = _gameObjectFactory.Create(_brickWallPrefab);
                    break;
            }

            spawnedItem.transform.position = spawnData.SpawnPosition;
        
            _spawnedObjects.Add(spawnedItem);
        
            _occupiedBounds.Add(spawnedItem.GetComponent<Collider2D>().bounds);
        }
    
        private void SpawnCoins(LevelConfig config)
        {
            int coinsToSpawn = Random.Range(config.MinCoins, config.MaxCoins + 1);
            int attempts = 0;

            int spawned = 0;

            Vector3 coinSize = _coinPrefab.GetComponent<Collider2D>().bounds.size;
        
            while (coinsToSpawn > 0)
            {
                float x = Random.Range(-15f, 15f);
                float y = Random.Range(5f, config.RaceLength - 5f);
                Vector3 pos = new Vector3(x, y, 0f);
            
                Bounds testBounds = new Bounds(pos, coinSize);
            
                bool overlaps = false;
                foreach (var existing in _occupiedBounds)
                {
                    if (existing.Intersects(testBounds))
                    {
                        overlaps = true;
                        break;
                    }
                }

                if (attempts > 1000)
                {
                    break;
                }
            
                if (overlaps)
                {
                    attempts++;
                    continue;
                }

                var coin = _gameObjectFactory.Create(_coinPrefab);
                coin.transform.position = pos;
            
                _occupiedBounds.Add(testBounds);

                _spawnedObjects.Add(coin);
                spawned++;
                coinsToSpawn--;
            }
        }

        public void Cleanup()
        {
            foreach (var spawnedObject in _spawnedObjects)
                Object.Destroy(spawnedObject.gameObject);
        
            _spawnedObjects.Clear();
        }
    }
}
