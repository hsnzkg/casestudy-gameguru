using UnityEngine;

namespace Case_1
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private UIManager _uiManager;
        [SerializeField] private Camera _mainCam;
        [SerializeField] private GridController _gridController;
        [SerializeField] private CameraController _cameraController;
        private bool _isActive = true;

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            _gridController.CreateGrid();
            _uiManager.OnBuild += _gridController.RebuildGrid;

            _cameraController.SetCameraForNewGrid(_gridController.NSize,_gridController.CellSize);
            _uiManager.OnBuild += _cameraController.SetCameraForNewGrid;
        }

        private void Update()
        {
            var flag = Input.GetMouseButtonDown(0);
            if (_isActive)
            {
                if (flag && !UIExtensions.IsPointerOverUIObject())
                {
                    var worldPos = _mainCam.ScreenToWorldPoint(Input.mousePosition);
                    var gridPos = worldPos.GetGridPosFromWorld(_gridController.CellSize);
                    if (_gridController.IsOverlap(gridPos))
                    {
                        _gridController.ActivateCell(gridPos);
                    }
                }
            }
        }
    }

}
