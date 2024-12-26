using System;
using Cysharp.Threading.Tasks;
using InputSystem;
using ObjectPool;
using UnityEngine;

namespace Components
{
    public class Gun : MonoBehaviour
    {
        public event Action OnLose;

        [SerializeField]
        private float _bulletSpawnCooldown;

        [SerializeField]
        private float _bulletLifeTime;

        [SerializeField]
        private Transform _firePoint;

        [SerializeField]
        private InputHandler _input;

        [SerializeField]
        private BulletsLeftView _bulletsLeftView;
        private IPool<Bullet> _bulletPool;
        private UniTask _reloading;
        private int _remainingShots;
        private int _towerSize;
        private int _mercy;

        public void Construct(IPool<Bullet> bulletPool, int towerSize, int mercy)
        {
            _bulletPool = bulletPool;
            _towerSize = towerSize;
            _mercy = mercy;
            _remainingShots = _towerSize + _mercy;
            _bulletsLeftView.SetValue(_remainingShots, _towerSize + _mercy);
            WaitForLose().Forget();
        }

        private void OnEnable() => _input.OnFire += Shoot;

        private void OnDisable() => _input.OnFire -= Shoot;

        public void Shoot()
        {
            if (_reloading.Status != UniTaskStatus.Succeeded || _remainingShots == 0)
            {
                return;
            }

            var bullet = _bulletPool.GetFromPool();
            bullet.transform.position = _firePoint.position;
            HandleBulletLifeTime(bullet).Forget();
            _reloading = Reloading();
            _remainingShots--;
            _bulletsLeftView.SetValue(_remainingShots, _towerSize + _mercy);
        }

        private async UniTask Reloading()
        {
            var cancellationToken = gameObject.GetCancellationTokenOnDestroy();
            var time = TimeSpan.FromSeconds(_bulletSpawnCooldown);
            await UniTask.Delay(time, cancellationToken: cancellationToken);
        }

        private async UniTaskVoid HandleBulletLifeTime(Bullet bullet)
        {
            var cancellationToken = gameObject.GetCancellationTokenOnDestroy();
            var time = TimeSpan.FromSeconds(_bulletLifeTime);
            await UniTask.Delay(time, cancellationToken: cancellationToken);
            _bulletPool.ReturnToPool(bullet);
        }

        private async UniTaskVoid WaitForLose()
        {
            while (_bulletPool.HasActive || _remainingShots > 0)
            {
                await UniTask.Yield();
            }

            OnLose?.Invoke();
        }
    }
}
