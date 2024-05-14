using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{
    [Inject]
    ILevelCreator _levelCreator;

    [Inject]
    LevelSettings _levelSetting;

    [Inject]
    IInputService _inputService;

    [Inject]
    BlockWaypointController _BlockWaypointController;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _levelCreator.CreateLevel(Vector2.zero, _levelSetting.levelDatas[DatabaseController.GetCurrentLevelIndex()]);
        _inputService.RegisterActionToPress(StartGame);
        _inputService.Activate();   
    }

    private void StartGame()
    {
        _inputService.UnRegisterActionToPress(StartGame);
        _BlockWaypointController.ConfigureBlock();
        _levelCreator.GetRuntimeLevelData().Player.Activate();
    }

    public void FinishGame(bool isDone)
    {
        var runtimeData = _levelCreator.GetRuntimeLevelData();
        var player = runtimeData.Player;
        player.Deactivate();
    }
}
