using System.Collections.Generic;

namespace Runtime.Gameplay.Game.Systems
{
    public class SystemsManager
    {
        private readonly List<IResettable> _resettables = new List<IResettable>();
        private readonly List<IEnableable> _enableables = new List<IEnableable>();
        private readonly List<IInitializeSystem> _initializeSystems = new List<IInitializeSystem>();
        private readonly List<ICleanupSystem> _cleanupSystems = new List<ICleanupSystem>();

        public void RegisterSystem(object system)
        {
            if(system is IResettable resettable)
                _resettables.Add(resettable);
        
            if(system is IEnableable enableable)
                _enableables.Add(enableable);
        
            if(system is IInitializeSystem initializeSystem)
                _initializeSystems.Add(initializeSystem);
        
            if(system is ICleanupSystem cleanupSystem)
                _cleanupSystems.Add(cleanupSystem);
        }

        public void EnableAllSystem(bool enable) => _enableables.ForEach(x => x.Enable(enable));
        public void ResetAllSystems() => _resettables.ForEach(x => x.Reset());
    
        public void InitializeAllSystems() => _initializeSystems.ForEach(x => x.Initialize());
    
        public void CleanupAllSystems() => _cleanupSystems.ForEach(x => x.Cleanup());
    }
}
