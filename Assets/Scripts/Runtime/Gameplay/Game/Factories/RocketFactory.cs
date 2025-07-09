using Core;
using Core.Factory;
using Runtime.Gameplay.Game.Items;
using Runtime.Gameplay.Game.Systems;
using Runtime.Gameplay.HelperServices.SettingsProvider;
using UnityEngine;

namespace Runtime.Gameplay.Game.Factories
{
    public class RocketFactory : BaseFactory, ICleanupSystem
    {
        public RocketFactory(IAssetProvider assetProvider, GameObjectFactory gameObjectFactory, SystemsManager systemsManager) : base(assetProvider, gameObjectFactory, ConstPrefabs.RocketPrefab)
        {
            systemsManager.RegisterSystem(this);
        }

        public void Cleanup()
        {
            RocketItem[] rockets = Object.FindObjectsOfType<RocketItem>(true);

            foreach (var rocket in rockets)
                Object.Destroy(rocket.gameObject);
        }
    }
}
