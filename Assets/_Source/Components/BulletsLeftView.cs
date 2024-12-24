using UnityEngine;
using UnityEngine.UI;

namespace Components
{
    public class BulletsLeftView : MonoBehaviour
    {
        [SerializeField]
        private Image _image;

        public void SetValue(float value, float max) => _image.fillAmount = value / max;
    }
}
