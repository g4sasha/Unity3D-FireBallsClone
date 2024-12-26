using Components;
using GameManagement;
using ObjectPool;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    public class Bootstrap : MonoBehaviour
    {
        [Header("Gun")]
        [SerializeField]
        private Gun _gun;

        [SerializeField]
        private Transform _bulletStorage;

        [SerializeField]
        private GunSO _gunConfiguration;

        [SerializeField]
        private int _mercy;

        [Header("Tower")]
        [SerializeField]
        private Tower _tower;

        [SerializeField]
        private TowerSO _towerConfiguration;

        [Header("UI")]
        [SerializeField]
        private PartsLeftView _partsLeftView;

        [SerializeField]
        private MessageView _messageView;

        [SerializeField]
        private string _winMessage;

        [SerializeField]
        private string _loseMessage;

        private BulletPool _bulletPool;
        private StateMachine<GameState> _gameStateMachine;

        private void Start()
        {
            InitTower();
            InitGun();
            InitUI();
            StartGame();
        }

        private void OnDestroy()
        {
            _tower.OnSizeChanged -= OnTowerSizeChanged;
            _gun.OnLose -= () => _gameStateMachine.ChangeState<LoseState>();
        }

        private void InitTower()
        {
            var towerSize = _towerConfiguration.Size;
            var towerParts = _towerConfiguration.Parts;
            _tower.Generate(towerSize, towerParts);
            _tower.OnSizeChanged += OnTowerSizeChanged;
        }

        private void InitGun()
        {
            var bulletPrefab = _gunConfiguration.BulletPrefab;
            var initBulletPoolSize = _gunConfiguration.BulletInitPoolSize;
            _bulletPool = new BulletPool(bulletPrefab, initBulletPoolSize, _bulletStorage);
            _gun.Construct(_bulletPool, _towerConfiguration.Size, _mercy);
            _gun.OnLose += () =>
            {
                _gameStateMachine.ChangeState<LoseState>();
                _messageView.Show(
                    _loseMessage,
                    () => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex)
                );
            };
        }

        private void InitUI() => _partsLeftView.Construct(_tower);

        private void StartGame()
        {
            var gameStates = new GameState[] { new PlayState(), new WinState(), new LoseState() };
            _gameStateMachine = new StateMachine<GameState>(gameStates);
            _gameStateMachine.ChangeState<PlayState>();
        }

        private void OnTowerSizeChanged(int newSize)
        {
            if (newSize <= 0)
            {
                _gameStateMachine.ChangeState<WinState>();
                _messageView.Show(
                    _winMessage,
                    () => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex)
                );
            }
        }
    }
}
