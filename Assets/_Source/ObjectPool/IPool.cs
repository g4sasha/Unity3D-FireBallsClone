using UnityEngine;

namespace ObjectPool
{
    public interface IPool<T>
    {
        int Count { get; }
        void InitPool(T prefab, int initSize, Transform poolParent);
        T GetFromPool();
        void ReturnToPool(T element);
        T InstantiatePoolObject(T prefab);
    }
}
