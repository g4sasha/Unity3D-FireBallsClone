using ObjectPool;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewGun", menuName = "Guns/New")]
    public class GunSO : ScriptableObject
    {
        [field: SerializeField] public Bullet BulletPrefab;
        [field: SerializeField] public int BulletInitPoolSize;
    }
}
