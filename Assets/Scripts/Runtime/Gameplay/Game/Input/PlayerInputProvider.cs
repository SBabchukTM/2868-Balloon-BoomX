using Runtime.Gameplay.Game.Systems;
using UnityEngine;
using Zenject;

namespace Runtime.Gameplay.Game.Input
{
    public class PlayerInputProvider : BaseSystem, ITickable, IInitializable, IInputProvider
    {
        private PlayerInputActions _playerInputActions;

        private Vector2 _input;
    
        public Vector2 Input => _input;

        public PlayerInputProvider(SystemsManager manager) : base(manager)
        {
        }

        public void Initialize()
        {
            _playerInputActions = new();
        }

        public void Tick()
        {
            if(!Enabled)
                return;

            _input = _playerInputActions.Movement.Move.ReadValue<Vector2>();
        }

        public override void Enable(bool enable)
        {
            base.Enable(enable);
        
            if(enable)
                _playerInputActions.Enable();
            else
                _playerInputActions.Disable();
        }

        public override void Reset()
        {
            _input = Vector2.zero;
        }

        public Vector2 GetInput() => _input;
    }
}
