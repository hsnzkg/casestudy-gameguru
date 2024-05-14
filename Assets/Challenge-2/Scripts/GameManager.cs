using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{
    [Inject]
    private ILevelCreator _levelCreator;

    [Inject]
    private LevelSettings _levelSetting;

    [Inject]
    private IInputService _inputService;

    [Inject]
    private BlockWaypointController _blockWaypointController;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _levelCreator.CreateLevel(Vector2.zero, _levelSetting.levelDatas[DatabaseController.GetCurrentLevelIndex()]);
        _inputService.RegisterActionToPress(StartGame);
        _inputService.Activate();
        _blockWaypointController.Init();
    }

    private void StartGame()
    {
        _inputService.UnRegisterActionToPress(StartGame);
        _blockWaypointController.ConfigureBlock();
        _levelCreator.GetRuntimeLevelData().Player.Activate();
    }

    public void FinishGame(bool isDone)
    {
        if (isDone)
        {
            _levelCreator.GetRuntimeLevelData().Player.Deactivate();
        }
    }
}
