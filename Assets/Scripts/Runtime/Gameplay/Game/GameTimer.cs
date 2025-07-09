using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Runtime.Gameplay.Game
{
    public class GameTimer
    {
        private float _time;
        private int _nextWholeSecond;

        public event Action<int> OnSecondPassed;
        
        public async UniTask StartTime(CancellationToken token)
        {
            _time = 0;
            _nextWholeSecond = 1;
            
            OnSecondPassed?.Invoke(0);
            
            while (!token.IsCancellationRequested)
            {
                _time += Time.deltaTime;

                if (_time > _nextWholeSecond)
                {
                    OnSecondPassed?.Invoke(_nextWholeSecond);
                    _nextWholeSecond++;
                }

                await UniTask.NextFrame(cancellationToken: token);
            }
        }
    }
}