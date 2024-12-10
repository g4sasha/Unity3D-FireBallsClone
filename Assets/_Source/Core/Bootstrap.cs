using Components;
using ObjectPool;
using UnityEngine;

namespace Core
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private Gun _gun;
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private Transform _bulletStorage;
        [SerializeField] private int _bulletPoolSize;
        private BulletPool _bulletPool;

        private void Start()
        {
            _bulletPool = new BulletPool(_bulletPrefab, _bulletPoolSize, _bulletStorage);
            _gun.Construct(_bulletPool);
        }
    }
}
