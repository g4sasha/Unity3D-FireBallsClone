using TMPro;
using UnityEngine;

namespace Components
{
    [RequireComponent(typeof(TMP_Text))]
    public class PartsLeftView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _partsLeftField;
        [SerializeField] private string _format;
        [SerializeField] private Tower _tower;

        private void OnValidate() => _partsLeftField ??= GetComponent<TMP_Text>();

        public void Construct(Tower tower) => _tower = tower;

        private void OnEnable() => _tower.OnSizeChanged += OnSizeChanged;

        private void OnDisable() => _tower.OnSizeChanged -= OnSizeChanged;

        private void OnSizeChanged(int value) => _partsLeftField.text = string.Format(_format, value);
    }
}
