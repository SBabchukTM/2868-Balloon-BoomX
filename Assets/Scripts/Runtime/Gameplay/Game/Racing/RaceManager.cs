using System;
using System.Collections.Generic;
using System.Linq;
using Runtime.Gameplay.Game.Levels;
using Runtime.Gameplay.Game.Systems;
using Zenject;

namespace Runtime.Gameplay.Game.Racing
{
    public class RaceManager : BaseSystem, ITickable
    {
        private readonly GameData _gameData;
        private readonly CheckpointSystem _checkpointSystem;
    
        private readonly Dictionary<BaseBalloonController, float> _progressDictionary = new();
        private readonly List<BaseBalloonController> _finishers = new();
    
        private PlayerBalloonController _player;

        private float _raceLength = 0;

        public event Action OnPlayerFinished;
    
        public RaceManager(SystemsManager manager, GameData gameData, CheckpointSystem checkpointSystem) : base(manager)
        {
            _gameData = gameData;
            _checkpointSystem = checkpointSystem;
        }

        public void RegisterRacer(BaseBalloonController balloonController)
        {
            _progressDictionary.Add(balloonController, 0f);
            if(balloonController is PlayerBalloonController player)
                _player = player;

            balloonController.OnHealthChanged += (health) =>
            {
                if (health > 0)
                    _checkpointSystem.ResetBalloonToCheckpoint(balloonController);
            };
        }

        public void SetRaceLength(float length) => _raceLength = length;

        public override void Reset()
        {
            _progressDictionary.Clear();
            _finishers.Clear();
            _raceLength = 0;
        }

        public void Tick()
        {
            if(!Enabled || _raceLength == 0)
                return;
        
            UpdateRacersProgress();
        }

        private void UpdateRacersProgress()
        {
            var keys = _progressDictionary.Keys.ToList();

            for (int i = keys.Count - 1; i >= 0; i--)
            {
                var racer = keys[i];
            
                if(racer == null)
                    continue;
            
                var racerTransform = racer.transform;
                float progress = racerTransform.position.y / _raceLength;
                _progressDictionary[racer] = progress;

                NotifyPlayerFinished(progress, racer);
            }
        }

        private void NotifyPlayerFinished(float progress, BaseBalloonController racer)
        {
            if (progress >= 1)
            {
                _finishers.Add(racer);
                _progressDictionary.Remove(racer);

                if (racer == _player)
                {
                    _gameData.PlayerFinishPlace = _finishers.Count;
                    OnPlayerFinished?.Invoke();
                }
            }
        }
    }
}
