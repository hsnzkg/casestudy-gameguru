using UnityEngine;

namespace Case_1
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Camera _camera;

        public void SetCameraForNewGrid(int size)
        {
            _camera.orthographicSize = size + (size / 0.5f);
            var pos = _camera.transform.position;
            pos.x = (float)size / 2;
            _camera.transform.position = pos;
        }
    }
}