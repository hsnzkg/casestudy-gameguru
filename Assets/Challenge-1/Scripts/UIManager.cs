using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Case_1
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GridController _gridController;
        [SerializeField] private Button _buildButton;
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private TextMeshProUGUI _matchCountText;

        public Action<int,float> OnBuild;
        private int _matchCount = 0;

        private void Awake()
        {
            UpdateMatchText();
            _buildButton.onClick.AddListener(OnPress);
        }

        public void OnMatch()
        {
            _matchCount++;
            UpdateMatchText();
        }

        private void UpdateMatchText()
        {
            _matchCountText.text = "Match Count: " + _matchCount.ToString();
        }

        private void OnPress()
        {
            _matchCount = 0;
            UpdateMatchText();

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
            OnBuild?.Invoke(size, _gridController.CellSize);
        }
    }
}
