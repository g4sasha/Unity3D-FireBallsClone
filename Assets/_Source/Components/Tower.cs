using System.Collections.Generic;
using UnityEngine;

namespace Components

{
    public class Tower : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _parts;
        [SerializeField] private List<GameObject> _partsTypes;

        private void Start()
        {
            Generate(100, _partsTypes);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Bullet")
            {
                Destroy(_parts[0]);
                _parts.RemoveAt(0);
                other.gameObject.SetActive(false);
                MoveParts();
            }

            if (_parts.Count.Equals(0))
            {
                Destroy(gameObject);
            }
        }

        public void Generate(int count, List<GameObject> parts)
        {
            foreach (var part in _parts)
            {
                Destroy(part);
            }

            _parts = new List<GameObject>();

            for (int i = 0; i < count; i++)
            {
                var nextPart = parts[i % parts.Count];
                Debug.Log("NextPart: " + nextPart);
                var position = transform.position + new Vector3(0f, i * nextPart.transform.localScale.y * 2f + nextPart.transform.localScale.y, 0f);
                var part = Instantiate(nextPart, position, Quaternion.identity, transform);
                _parts.Add(part);
            }
        }

        private void MoveParts()
        {
            foreach (var part in _parts)
            {
                part.transform.Translate(Vector3.down * part.transform.localScale.y * 2f);
            }
        }
    }
}