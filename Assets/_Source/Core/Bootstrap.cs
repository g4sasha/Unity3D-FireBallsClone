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

        [Header("UI")]
        [SerializeField] private PartsLeftView _partsLeftView;

        private BulletPool _bulletPool;

        private void Start()
        {
            InitTower();
            InitGun();
            InitUI();
        }

        private void InitTower()
        {
            var towerSize = _towerConfiguration.Size;
            var towerParts = _towerConfiguration.Parts;
            _tower.Generate(towerSize, towerParts);
        }

        private void InitGun()
        {
            var bulletPrefab = _gunConfiguration.BulletPrefab;
            var initBulletPoolSize = _gunConfiguration.BulletInitPoolSize;
            _bulletPool = new BulletPool(bulletPrefab, initBulletPoolSize, _bulletStorage);
            _gun.Construct(_bulletPool);
        }

        private void InitUI() => _partsLeftView.Construct(_tower);
    }
}
