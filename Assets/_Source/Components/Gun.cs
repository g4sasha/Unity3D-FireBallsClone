using System;
using Cysharp.Threading.Tasks;
using InputSystem;
using ObjectPool;
using UnityEngine;

namespace Components
{
    public class Gun : MonoBehaviour
    {
        [SerializeField] private float _bulletSpawnCooldown;
        [SerializeField] private float _bulletLifeTime;
        [SerializeField] private Transform _firePoint;
        [SerializeField] private InputHandler _input;
        private IPool<Bullet> _bulletPool;
        private UniTask _reloading;

        public void Construct(IPool<Bullet> bulletPool) => _bulletPool = bulletPool;

        private void OnEnable() => _input.OnFire += Shoot;

        private void OnDisable() => _input.OnFire -= Shoot;

        public void Shoot()
        {
            if (_reloading.Status != UniTaskStatus.Succeeded)
            {
                return;
            }

            var bullet = _bulletPool.GetFromPool();
            bullet.transform.position = _firePoint.position;
            HandleBulletLifeTime(bullet).Forget();
            _reloading = Reloading();
        }

        private async UniTask Reloading()
        {
            var cancellationToken = this.GetCancellationTokenOnDestroy();
            var time = TimeSpan.FromSeconds(_bulletSpawnCooldown);
            await UniTask.Delay(time, cancellationToken: cancellationToken);
        }

        private async UniTaskVoid HandleBulletLifeTime(Bullet bullet)
        {
            var cancellationToken = this.GetCancellationTokenOnDestroy();
            var time = TimeSpan.FromSeconds(_bulletLifeTime);
            await UniTask.Delay(time, cancellationToken: cancellationToken);
            _bulletPool.ReturnToPool(bullet);
        }
    }
}
