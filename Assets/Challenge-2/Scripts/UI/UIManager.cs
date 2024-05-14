using UnityEngine;


public class UIManager : MonoBehaviour
{
    [SerializeField] private NextLevelButton _nextLevelButton;


    public void ActivateNextLevelButton() { _nextLevelButton.Activate(); }
}