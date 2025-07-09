using Core.StateMachine;
using Cysharp.Threading.Tasks;
using Runtime.Gameplay.GameplayStates.Game;
using UnityEngine;
using Zenject;

namespace Runtime.Gameplay.GameplayStates.Bootstrap
{
    public class ProjectInitializer : IInitializable
    {
        private readonly StateMachine _stateMachine;
        private readonly ProjectInitializationState _projectInitializationState;
        private readonly GameState _gameState;

        public ProjectInitializer(StateMachine stateMachine, ProjectInitializationState projectInitializationState, GameState gameState)
        {
            _stateMachine = stateMachine;
            _projectInitializationState = projectInitializationState;
            _gameState = gameState;
        }

        public void Initialize()
        {
            Application.targetFrameRate = 60;
            _stateMachine.Initialize(_projectInitializationState, _gameState);
            _stateMachine.GoTo<ProjectInitializationState>().Forget();
        }
    }
}