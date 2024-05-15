using UnityEngine;

namespace Case_2
{
    public interface ICameraController
    {
        public void SetTarget(Transform t);
        public void ReleaseTarget();
        public void SetWinCamera();
    }
}
