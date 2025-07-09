using Runtime.Gameplay.Game.Movement;
using Runtime.Gameplay.Game.Systems;
using UnityEngine;
using Zenject;

namespace Runtime.Gameplay.Game.Visuals
{
    public class GameplayBGLooper : MonoBehaviour, IResettable, IEnableable, IInitializeSystem
    {
        [SerializeField] private Transform _bg1;
        [SerializeField] private Transform _bg2;

        private Transform _targetTransform;
        private float _bgHeight;

        private bool _followPlayer = false;

        [Inject]
        private void Construct(SystemsManager systemsManager, PlayerHealthTracker playerHealthTracker)
        {
            systemsManager.RegisterSystem(this);
            playerHealthTracker.OnPlayerHpChanged += _ => Reset();
        }
    
        private void Awake()
        {
            SpriteRenderer sr = _bg1.GetComponent<SpriteRenderer>();
            _bgHeight = sr.bounds.size.y;
        }

        private void Update()
        {
            if(!_followPlayer || !_targetTransform)
                return;

            if (_targetTransform.position.y > _bg1.position.y + _bgHeight)
            {
                MoveBGUp(ref _bg1, _bg2);
            }

            if (_targetTransform.position.y > _bg2.position.y + _bgHeight)
            {
                MoveBGUp(ref _bg2, _bg1);
            }
        }

        private void MoveBGUp(ref Transform bgToMove, Transform referenceBG)
        {
            bgToMove.position = new Vector3(
                bgToMove.position.x,
                referenceBG.position.y + _bgHeight,
                bgToMove.position.z
            );
        }

        public void Reset()
        {
            _bg1.position = Vector3.zero;
            _bg2.position = Vector3.up * _bgHeight;
        }

        public void Enable(bool enable)
        {
            _followPlayer = enable;
        
            _bg1.gameObject.SetActive(enable);
            _bg2.gameObject.SetActive(enable);
        }

        public void Initialize()
        {
            _targetTransform = FindObjectOfType<PlayerBalloonMovement>(true).transform;
        }
    }
}
