using System.Collections.Generic;
using Components;
using ObjectPool;
using UnityEngine;

namespace Core
{
    public class Bootstrap : MonoBehaviour
    {
        [Header("Gun")]
        [SerializeField] private Gun _gun;
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private Transform _bulletStorage;
        [SerializeField] private int _bulletPoolSize;

        [Header("Tower")]
        [SerializeField] private Tower _tower;
        [SerializeField] private int _towerSize;
        [SerializeField] private List<GameObject> _towerParts;
        
        private BulletPool _bulletPool;

        private void Start()
        {
            _tower.Generate(_towerSize, _towerParts);
            _bulletPool = new BulletPool(_bulletPrefab, _bulletPoolSize, _bulletStorage);
            _gun.Construct(_bulletPool);
        }
    }
}
