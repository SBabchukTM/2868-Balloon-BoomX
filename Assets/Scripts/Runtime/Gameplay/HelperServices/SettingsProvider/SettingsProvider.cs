using System;
using System.Collections.Generic;
using Core;
using Core.Services.Audio;
using Cysharp.Threading.Tasks;
using Runtime.Gameplay.BalloonSkinsShop;
using Runtime.Gameplay.Game.Levels;
using Runtime.Gameplay.HelperServices.ScreenOrientation;
using Runtime.Gameplay.UserProfile;

namespace Runtime.Gameplay.HelperServices.SettingsProvider
{
    public class SettingsProvider : ISettingProvider
    {
        private readonly IAssetProvider _assetProvider;

        private Dictionary<Type, BaseSettings> _settings = new Dictionary<Type, BaseSettings>();

        public SettingsProvider(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public async UniTask Initialize()
        {
            var screenOrientationConfig = await _assetProvider.Load<ScreenOrientationConfig>(ConstConfigs.ScreenOrientationConfig);
            var audioConfig = await _assetProvider.Load<AudioConfig>(ConstConfigs.AudioConfig);
            var shopSetup = await _assetProvider.Load<ShopConfig>(ConstConfigs.ShopSetup);
            var gameConfig = await _assetProvider.Load<GameConfig>(ConstConfigs.GameConfig);        
            var validationConfig = await _assetProvider.Load<UserDataValidationConfig>(ConstConfigs.UserDataValidationConfig);

            Set(screenOrientationConfig);
            Set(audioConfig);
            Set(validationConfig);
            Set(gameConfig);
            Set(shopSetup);
        }

        public T Get<T>() where T : BaseSettings
        {
            if (_settings.ContainsKey(typeof(T)))
            {
                var setting = _settings[typeof(T)];
                return setting as T;
            }

            throw new Exception("No setting found");
        }

        public void Set(BaseSettings config)
        {
            if (_settings.ContainsKey(config.GetType()))
                return;

            _settings.Add(config.GetType(), config);
        }
    }
}