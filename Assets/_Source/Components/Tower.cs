using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Components
{
    public class Tower : MonoBehaviour
    {
        public event Action<int> OnSizeChanged;

        [SerializeField]
        private List<GameObject> _activeParts;

#if UNITY_EDITOR
        private void OnValidate()
        {
            _activeParts = GetComponentsInChildren<Transform>().Select(t => t.gameObject).ToList();
            _activeParts.RemoveAt(0);
        }
#endif

        private void OnTriggerEnter(Collider other)
        {
            Destroy(_activeParts[0]);
            _activeParts.RemoveAt(0);
            other.gameObject.SetActive(false);
            OnSizeChanged?.Invoke(_activeParts.Count);
            MoveParts();

            if (_activeParts.Count.Equals(0))
            {
                Destroy(gameObject);
            }
        }

        public void Generate(int count, List<GameObject> partPrefabs)
        {
            foreach (var part in _activeParts)
            {
                Destroy(part);
            }

            _activeParts.Clear();

            for (int i = 0; i < count; i++)
            {
                var nextPart = partPrefabs[i % partPrefabs.Count];
                var moveY =
                    i * nextPart.transform.localScale.y * 2f + nextPart.transform.localScale.y;
                var position = transform.position + new Vector3(0f, moveY, 0f);
                var part = Instantiate(nextPart, position, Quaternion.identity, transform);
                _activeParts.Add(part);
            }

            OnSizeChanged?.Invoke(count);
        }

        private void MoveParts() // TODO: Плавная анимация
        {
            foreach (var part in _activeParts)
            {
                part.transform.Translate(Vector3.down * part.transform.localScale.y * 2f);
            }
        }
    }
}
