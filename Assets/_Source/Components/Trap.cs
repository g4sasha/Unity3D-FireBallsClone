using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Components
{
    public class PrefabSpawnerRotator : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> prefabs; // Список префабов

        [SerializeField]
        private float spawnRadius = 5f; // Радиус расположения объектов

        [SerializeField]
        private float rotationDuration = 3f; // Длительность одной анимации

        [SerializeField]
        private float height = 0f; // Высота объектов

        private void Start()
        {
            SpawnPrefabs();
            RotateObject();
        }

        private void SpawnPrefabs()
        {
            if (prefabs.Count == 0)
                return;

            float angleStep = 360f / prefabs.Count;
            for (int i = 0; i < prefabs.Count; i++)
            {
                float angle = i * angleStep * Mathf.Deg2Rad;
                Vector3 spawnPosition =
                    new Vector3(
                        Mathf.Cos(angle) * spawnRadius,
                        height,
                        Mathf.Sin(angle) * spawnRadius
                    ) + transform.position;

                GameObject instance = Instantiate(
                    prefabs[i],
                    spawnPosition,
                    Quaternion.identity,
                    transform
                );
                instance.transform.LookAt(transform.position);
                Vector3 rotation = instance.transform.rotation.eulerAngles;
                instance.transform.rotation = Quaternion.Euler(0, rotation.y, 0);
            }
        }

        private void RotateObject()
        {
            DOTween
                .To(
                    () => transform.rotation.eulerAngles.y,
                    y => transform.rotation = Quaternion.Euler(0, y, 0),
                    360f,
                    rotationDuration
                )
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo);
        }
    }
}
