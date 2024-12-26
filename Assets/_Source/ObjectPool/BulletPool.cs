using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ObjectPool
{
    public class BulletPool : IPool<Bullet>
    {
        public bool HasActive => _pool.Any(b => b != null && b.isActiveAndEnabled);
        public int Count => _pool.Count;
        private readonly Queue<Bullet> _pool = new();
        private Bullet _bulletPrefab;
        private Transform _poolParent;

        public BulletPool(Bullet bulletPrefab, int initSize, Transform poolParent) =>
            InitPool(bulletPrefab, initSize, poolParent);

        public void InitPool(Bullet bulletPrefab, int initSize, Transform poolParent)
        {
            _bulletPrefab = bulletPrefab;
            _poolParent = poolParent;

            for (int i = 0; i < initSize; i++)
            {
                InstantiatePoolObject(bulletPrefab);
            }
        }

        public Bullet GetFromPool()
        {
            if (_pool.TryDequeue(out Bullet bullet))
            {
                if (!bullet.isActiveAndEnabled)
                {
                    bullet.gameObject.SetActive(true);
                    return bullet;
                }
            }

            bullet = InstantiatePoolObject(_bulletPrefab);
            bullet.gameObject.SetActive(true);
            return bullet;
        }

        public void ReturnToPool(Bullet bullet)
        {
            bullet.gameObject.SetActive(false);
            _pool.Enqueue(bullet);
        }

        public Bullet InstantiatePoolObject(Bullet bulletPrefab)
        {
            var bulletInstance = GameObject.Instantiate(bulletPrefab, _poolParent);
            ReturnToPool(bulletInstance);
            return bulletInstance;
        }
    }
}
