using Components;
using ObjectPool;
using ScriptableObjects;
using UnityEngine;

namespace Core
{
    public class Bootstrap : MonoBehaviour
    {
        [Header("Gun")]
        [SerializeField] private Gun _gun;
        [SerializeField] private Transform _bulletStorage;
        [SerializeField] private GunSO _gunConfiguration;

        [Header("Tower")]
        [SerializeField] private Tower _tower;
        [SerializeField] private TowerSO _towerConfiguration;

        private BulletPool _bulletPool;

        private void Start()
        {
            var bulletPrefab = _gunConfiguration.BulletPrefab;
            var initBulletPoolSize = _gunConfiguration.BulletInitPoolSize;

            _tower.Generate(_towerConfiguration.Size, _towerConfiguration.Parts);
            
            _bulletPool = new BulletPool(bulletPrefab, initBulletPoolSize, _bulletStorage);
            _gun.Construct(_bulletPool);
        }
    }
}
