using Core.StateMachine;
using Runtime.Gameplay.GameplayStates.Bootstrap.Controllers;
using Runtime.Gameplay.GameplayStates.Game;
using Runtime.Gameplay.GameplayStates.Game.Controllers;
using UnityEngine;
using Zenject;

namespace Runtime.Gameplay.GameplayStates.Bootstrap
{
    [CreateAssetMenu(fileName = "BootstrapInstaller", menuName = "Installers/BootstrapInstaller")]
    public class BootstrapInstaller : ScriptableObjectInstaller<BootstrapInstaller>
    {
        public override void InstallBindings()
        {
            Bind1();

            Bind2();
        }

        private void Bind2()
        {
            Container.Bind<AudioInitializationController>().AsSingle();
            Container.Bind<UserDataStateChangeController>().AsSingle();
        }

        private void Bind1()
        {
            Container.BindInterfacesAndSelfTo<ProjectInitializer>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ProjectInitializationState>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameState>().AsSingle();
            Container.Bind<StateMachine>().AsTransient();
        }
    }
}