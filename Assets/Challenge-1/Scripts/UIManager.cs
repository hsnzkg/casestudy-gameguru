using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Case_1
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Button _buildButton;
        [SerializeField] private TMP_InputField _inputField;

        public Action<int> OnBuild;

        private void Awake()
        {
            _buildButton.onClick.AddListener(OnPress);
        }

        private void OnPress()
        {
            var input = _inputField.text;
            int size;
            if (string.IsNullOrEmpty(input))
            {
                size = 5;
            }
            else
            {
                size = int.Parse(_inputField.text);
            }
            OnBuild?.Invoke(size);
        }
    }
}
