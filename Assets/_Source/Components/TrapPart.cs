using UnityEngine;

namespace Components
{
    public class TrapPart : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Bullet")
            {
                Debug.Log("Промазал");
                other.gameObject.SetActive(false);
            }
        }
    }
}
