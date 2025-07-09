using Runtime.Gameplay.BalloonSkinsShop;
using Runtime.Gameplay.Game;
using Runtime.Gameplay.Game.Abilities;
using Runtime.Gameplay.Game.Abilities.Boost;
using Runtime.Gameplay.Game.Abilities.Rocket;
using Runtime.Gameplay.Game.Abilities.Spikers;
using Runtime.Gameplay.Game.Achievements;
using Runtime.Gameplay.Game.Factories;
using Runtime.Gameplay.Game.Input;
using Runtime.Gameplay.Game.Levels;
using Runtime.Gameplay.Game.Movement;
using Runtime.Gameplay.Game.Racing;
using Runtime.Gameplay.Game.Systems;
using Runtime.Gameplay.GameplayStates.Game.Controllers;
using Runtime.Gameplay.GameplayStates.Game.Menu;
using Runtime.Gameplay.HelperServices.Shop;
using Runtime.Gameplay.Inventory;
using Runtime.Gameplay.Roulette;
using Runtime.Gameplay.ScreensPopups;
using UnityEngine;
using Zenject;

namespace Runtime.Gameplay.GameplayStates.Game
{
    [CreateAssetMenu(fileName = "GameInstaller", menuName = "Installers/GameInstaller")]
    public class GameInstaller : ScriptableObjectInstaller<GameInstaller>
    {
        public override void InstallBindings()
        {
            BindController();
            BindServices();
            BindData();
            BindSystems();
            BindRocketAbility();
            BindBoostAbility();
            BindSpikesAbility();
            
            Container.BindFactory<BotInputProvider, BotInputProviderFactory>();
            Container.Bind<IInputProvider>().To<BotInputProvider>().WhenInjectedInto<BotBalloonMovement>();
        }

        private void BindData()
        {
            Container.Bind<IShopItemsStorage>().To<ShopItemsStorage>().AsSingle();
            Container.Bind<ShopItemDisplayModel>().AsTransient();
            Container.BindInterfacesAndSelfTo<CameraSizeProvider>().AsSingle().NonLazy();
        }

        private void BindServices()
        {
            Container.Bind<IProcessPurchaseService>().To<ProcessPurchaseService>().AsSingle();
            Container.Bind<ISelectPurchaseItemService>().To<SelectPurchaseItemService>().AsSingle();
            Container.Bind<IPurchaseEffectsService>().To<PurchaseEffectsService>().AsSingle();
            Container.Bind<IShopItemsDisplayService>().To<ShopItemsDisplayService>().AsSingle();
        }

        private void BindController()
        {
            Container.Bind<MenuStateController>().AsSingle();
            Container.Bind<InitShopState>().AsSingle();
            Container.Bind<ShopStateController>().AsSingle();
            Container.Bind<AccountScreenStateController>().AsSingle();
            Container.Bind<StartSettingsController>().AsSingle();
            Container.Bind<ShopItemDisplayController>().AsSingle();
            Container.Bind<LevelSelectionStateController>().AsSingle();
            Container.Bind<LosePopupStateController>().AsSingle();
            Container.Bind<WinPopupStateController>().AsSingle();
            Container.Bind<HPDepletePopupController>().AsSingle();
            Container.Bind<PausePopupStateController>().AsSingle();
            Container.Bind<UserProgressService>().AsSingle();
            Container.Bind<PlayerHealthTracker>().AsSingle();
            Container.Bind<GameData>().AsSingle();
            Container.Bind<RouletteSpinner>().AsSingle();
            Container.Bind<UserLoginService>().AsSingle();
            Container.BindInterfacesAndSelfTo<UserRecordsFactory>().AsSingle();

            Container.Bind<GameplayStateController>().AsSingle();
            Container.Bind<HowToPlayStateController>().AsSingle();
            Container.Bind<InventoryStateController>().AsSingle();
            Container.Bind<LeaderboardStateController>().AsSingle();
            Container.Bind<MissionsStateController>().AsSingle();
            Container.Bind<ModeSelectionStateController>().AsSingle();
            Container.Bind<PrivacyPolicyStateController>().AsSingle();
            Container.Bind<RouletteStateController>().AsSingle();
            Container.Bind<SettingsStateController>().AsSingle();
            Container.Bind<TermsOfUseStateController>().AsSingle();
            Container.Bind<MainScreenStateController>().AsSingle();
            Container.Bind<RouletteItemSelector>().AsSingle();
            Container.Bind<GameTimer>().AsSingle();
            Container.Bind<SkinProvider>().AsSingle();
            Container.Bind<AchievementsManager>().AsSingle().NonLazy();
            
            Container.Bind<GameSetupController>().AsSingle().NonLazy();
            Container.Bind<BalloonPositionClamper>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<RocketFactory>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<RaceSpawner>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<RaceManager>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<RaceObjectsSpawner>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<CheckpointSystem>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<InventoryItemsFactory>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<AchievementsFactory>().AsSingle().NonLazy();
        }
        
        private void BindSystems()
        {
            Container.Bind<PlayerInputProvider>().AsSingle();
            Container.Bind<IInputProvider>().To<PlayerInputProvider>().FromResolve().WhenInjectedInto<PlayerBalloonMovement>();
            Container.Bind<IInitializable>().To<PlayerInputProvider>().FromResolve();
            Container.Bind<ITickable>().To<PlayerInputProvider>().FromResolve();
            
            Container.Bind<SystemsManager>().AsSingle().NonLazy();
        }

        private void BindRocketAbility()
        {
            Container.Bind<PlayerRockerAbilityTriggerCondition>().AsSingle();
            Container.Bind<BotRocketAbilityTriggerCondition>().AsTransient();
            
            Container.Bind<RocketAbilityTriggerCondition>().To<PlayerRockerAbilityTriggerCondition>().FromResolve()
                .WhenInjectedInto<PlayerBalloonController>();
            Container.Bind<BaseAbilityTriggerCondition>().To<PlayerRockerAbilityTriggerCondition>().FromResolve()
                .WhenInjectedInto<PlayerRocketAbilityTrigger>();

            Container.Bind<RocketAbilityTriggerCondition>().To<BotRocketAbilityTriggerCondition>().FromResolve()
                .WhenInjectedInto<BotBalloonController>();
            
            Container.Bind<RocketAbility>().AsTransient();
            
            Container.Bind<PlayerRocketAbility>().AsSingle();
            Container.Bind<RocketAbility>().To<PlayerRocketAbility>().FromResolve().WhenInjectedInto<PlayerBalloonController>();
            Container.Bind<BaseAbility>().To<PlayerRocketAbility>().FromResolve().WhenInjectedInto<PlayerRocketAbilityTrigger>();
        }
        
        private void BindBoostAbility()
        {
            Container.Bind<PlayerBoostAbilityTriggerCondition>().AsSingle();
            Container.Bind<BotBoostAbilityTriggerCondition>().AsTransient();

            Container.Bind<BoostAbilityTriggerCondition>().To<PlayerBoostAbilityTriggerCondition>().FromResolve()
                .WhenInjectedInto<PlayerBalloonController>();
            Container.Bind<BaseAbilityTriggerCondition>().To<PlayerBoostAbilityTriggerCondition>().FromResolve()
                .WhenInjectedInto<PlayerBoostAbilityTrigger>();

            Container.Bind<BoostAbilityTriggerCondition>().To<BotBoostAbilityTriggerCondition>().FromResolve()
                .WhenInjectedInto<BotBalloonController>();
            
            Container.Bind<BoostAbility>().AsTransient();
            
            Container.Bind<PlayerBoostAbility>().AsSingle();
            Container.Bind<BoostAbility>().To<PlayerBoostAbility>().FromResolve().WhenInjectedInto<PlayerBalloonController>();
            Container.Bind<BaseAbility>().To<PlayerBoostAbility>().FromResolve().WhenInjectedInto<PlayerBoostAbilityTrigger>();
        }
        
        private void BindSpikesAbility()
        {
            Container.Bind<PlayerSpikesAbilityTriggerCondition>().AsSingle();
            Container.Bind<BotSpikesAbilityTriggerCondition>().AsTransient();

            Container.Bind<SpikesAbilityTriggerCondition>().To<PlayerSpikesAbilityTriggerCondition>().FromResolve()
                .WhenInjectedInto<PlayerBalloonController>();
            Container.Bind<BaseAbilityTriggerCondition>().To<PlayerSpikesAbilityTriggerCondition>().FromResolve()
                .WhenInjectedInto<PlayerSpikesAbilityTrigger>();

            Container.Bind<SpikesAbilityTriggerCondition>().To<BotSpikesAbilityTriggerCondition>().FromResolve()
                .WhenInjectedInto<BotBalloonController>();
            
            Container.Bind<SpikesAbility>().AsTransient();
            
            Container.Bind<PlayerSpikesAbility>().AsSingle();
            Container.Bind<SpikesAbility>().To<PlayerSpikesAbility>().FromResolve().WhenInjectedInto<PlayerBalloonController>();
            Container.Bind<BaseAbility>().To<PlayerSpikesAbility>().FromResolve().WhenInjectedInto<PlayerSpikesAbilityTrigger>();
        }

        public class BotInputProviderFactory : Zenject.PlaceholderFactory<BotInputProvider> { }
    }
}