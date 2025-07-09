using Core;
using Core.Factory;
using UnityEngine;
using Zenject;

namespace Runtime.Gameplay.Game.Factories
{
    public abstract class BaseFactory : IInitializable
    {
        private readonly IAssetProvider _assetProvider;
        private readonly GameObjectFactory _gameObjectFactory;
    
        private GameObject _prefab;
        private string _prefabName;

        public BaseFactory(IAssetProvider assetProvider, GameObjectFactory gameObjectFactory, string prefabName)
        {
            _assetProvider = assetProvider;
            _gameObjectFactory = gameObjectFactory;
            _prefabName = prefabName;
        }
    
        public async void Initialize()
        {
            _prefab = await _assetProvider.Load<GameObject>(_prefabName);
        }

        public GameObject CreateItem()
        {
            return _gameObjectFactory.Create(_prefab);
        }
    }
}
