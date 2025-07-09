using Core;
using Core.Compressor;
using Core.Factory;
using Core.Services.Audio;
using Runtime.Gameplay.BalloonSkinsShop;
using Runtime.Gameplay.HelperServices.ApplicationState;
using Runtime.Gameplay.HelperServices.Audio;
using Runtime.Gameplay.HelperServices.NetworkConnection;
using Runtime.Gameplay.HelperServices.ScreenOrientation;
using Runtime.Gameplay.HelperServices.UI;
using Runtime.Gameplay.HelperServices.UserData;
using Runtime.Gameplay.HelperServices.UserData.Data;
using Runtime.Gameplay.UserProfile;
using UnityEngine;
using Zenject;
using ILogger = Core.ILogger;

namespace Runtime.Gameplay.HelperServices
{
    [CreateAssetMenu(fileName = "ServicesInstaller", menuName = "Installers/ServicesInstaller")]
    public class ServicesInstaller : ScriptableObjectInstaller<ServicesInstaller>
    {
        public override void InstallBindings()
        {
            Bind1();
            Bind2();
            Bind3();
        }

        private void Bind3()
        {
            Container.Bind<AvatarSelectionService>().AsSingle();
            Container.Bind<UserDataValidationService>().AsSingle();
            Container.Bind<ImageProcessingService>().AsSingle();
            Container.BindInterfacesAndSelfTo<ScreenOrientationAlertController>().AsSingle();
            Container.BindInterfacesAndSelfTo<ShopService>().AsSingle();
        }

        private void Bind2()
        {
            Container.Bind<ISerializationProvider>().To<JsonSerializationProvider>().AsSingle();
            Container.Bind<IAudioService>().To<AudioService>().AsSingle();
            Container.Bind<INetworkConnectionService>().To<NetworkConnectionService>().AsSingle();
            Container.Bind<BaseCompressor>().To<ZipCompressor>().AsSingle();
            Container.Bind<GameObjectFactory>().AsSingle();
            Container.Bind<ApplicationStateService>().AsSingle();
            Container.Bind<UserDataService>().AsSingle();
            Container.Bind<ServerProvider>().AsSingle();
            Container.Bind<IUserInventoryService>().To<UserInventoryService>().AsSingle();
            Container.Bind<UserProfileManager>().AsSingle();
        }

        private void Bind1()
        {
            Container.Bind<IUiService>().To<UiService>().AsSingle();
            Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
            Container.Bind<IPersistentDataProvider>().To<PersistantDataProvider>().AsSingle();
            Container.Bind<ISettingProvider>().To<SettingsProvider.SettingsProvider>().AsSingle();
            Container.Bind<ILogger>().To<SimpleLogger>().AsSingle();
            Container.Bind<IFileStorageService>().To<PersistentFileStorageService>().AsSingle();
            Container.Bind<IFileCleaner>().To<FileCleaner>().AsSingle();
        }
    }
}