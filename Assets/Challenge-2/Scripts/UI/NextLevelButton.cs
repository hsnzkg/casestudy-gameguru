using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class NextLevelButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [Inject] private GameManager _gameManager;

    public void Activate()
    {
        gameObject.SetActive(true);
        _button.onClick.AddListener(OnPressed);
    }

    private void OnPressed()
    {
        _button.onClick.RemoveAllListeners();
        _gameManager.ReloadGame();
    }
}
