using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Components
{
    public class MessageView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _messageField;

        [SerializeField]
        private Button _nextButton;

        [SerializeField]
        private GameObject _messagePanel;

        // private void Start() => Hide();

        public void Show(string message, Action callback)
        {
            _messageField.text = message;
            _nextButton.onClick.AddListener(() => callback?.Invoke());
            _messagePanel.SetActive(true);
        }

        public void Hide()
        {
            _nextButton.onClick.RemoveAllListeners();
            _messagePanel.SetActive(false);
        }
    }
}
