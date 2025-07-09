using System;

namespace Runtime.Gameplay.Game.Levels
{
    public class GameData
    {
        public event Action<int> OnCoinCollected;

        private int _coins;

        public int Coins
        {
            get { return _coins; }
            set
            {
                _coins = value;
                OnCoinCollected?.Invoke(value);
            }
        }

        public int LevelID { get; set; } = 0;

        public GameplayMode GameplayMode { get; set; }

        public int PlayerFinishPlace { get; set; } = 1;
        public int TimeAttackTime { get; set; } = 0;
    }

    public enum GameplayMode
    {
        Race,
        TimeAttack
    }
}