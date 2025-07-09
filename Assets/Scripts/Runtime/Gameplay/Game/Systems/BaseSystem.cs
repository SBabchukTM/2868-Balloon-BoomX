namespace Runtime.Gameplay.Game.Systems
{
    public abstract class BaseSystem : IEnableable, IResettable
    {
        protected bool Enabled;

        public BaseSystem(SystemsManager manager) => manager.RegisterSystem(this);
    
        public virtual void Enable(bool enable)
        {
            Enabled = enable;
        }

        public abstract void Reset();
    }
}
