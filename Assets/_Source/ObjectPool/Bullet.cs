using UnityEngine;

namespace ObjectPool
{
    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _movementSpeed;
        [SerializeField] private Rigidbody _rigidbody;

        private void OnValidate()
        {
            _rigidbody ??= GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            _rigidbody.AddForce(transform.forward * _movementSpeed, ForceMode.VelocityChange);
        }

        private void OnDisable()
        {
            _rigidbody.linearVelocity = Vector3.zero;
        }
    }
}
