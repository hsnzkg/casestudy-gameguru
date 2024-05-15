using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Case_2
{
    public class GameManager : MonoBehaviour
    {
        [Inject] private UIManager _uiManager;
        [Inject] private ILevelCreator _levelCreator;
        [Inject] private LevelSettings _levelSetting;
        [Inject] private IInputService _inputService;
        [Inject] private BlockWaypointController _blockWaypointController;
        [Inject] private ICameraController _cameraController;

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
            _cameraController.SetTarget(_levelCreator.GetRuntimeLevelData().Player.transform);
        }

        private void StartGame()
        {
            _inputService.UnRegisterActionToPress(StartGame);
            _blockWaypointController.ConfigureBlock();

            var player = _levelCreator.GetRuntimeLevelData().Player;
            player.OnFall += () => { FinishGame(false); };
            player.Activate();
            player.OnFall += _cameraController.ReleaseTarget;
        }

        public void ReloadGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void FinishGame(bool isDone)
        {
            if (isDone)
            {
                _uiManager.ActivateNextLevelButton();
                _levelCreator.GetRuntimeLevelData().Player.Deactivate();
                DatabaseController.SetLevelIndex(Mathf.Clamp(DatabaseController.GetCurrentLevelIndex() + 1, 0, _levelSetting.levelDatas.Count - 1));
                _cameraController.SetWinCamera();
            }
            else
            {
                foreach (var item in _levelCreator.GetRuntimeLevelData().DroppingBlocks)
                {
                    item.DeActivate();
                };
                DOVirtual.DelayedCall(1f, ReloadGame).Play();
            }
        }
    }

}
