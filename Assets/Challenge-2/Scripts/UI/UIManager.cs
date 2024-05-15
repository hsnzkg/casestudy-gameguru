using UnityEngine;

namespace Case_2
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private NextLevelButton _nextLevelButton;


        public void ActivateNextLevelButton() { _nextLevelButton.Activate(); }
    }
}
