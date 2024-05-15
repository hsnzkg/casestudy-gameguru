using Cinemachine;
using UnityEngine;

namespace Case_2
{
    public class CMCameraController : MonoBehaviour, ICameraController
    {
        [SerializeField] private CinemachineVirtualCamera _cm;
        [SerializeField] private CinemachineFreeLook _rotatorCm;
        [SerializeField] private float _rotateSpeed;

        public void ReleaseTarget()
        {
            _cm.m_LookAt = null;
            _cm.m_Follow = null;
        }

        public void SetTarget(Transform t)
        {
            _cm.LookAt = t;
            _cm.m_Follow = t;
        }

        public void SetWinCamera()
        {
            _rotatorCm.m_Follow = _cm.m_Follow;
            _rotatorCm.m_LookAt = _cm.m_LookAt;
            _rotatorCm.gameObject.SetActive(true);
        }
        private void Update()
        {
            if (_rotatorCm.gameObject.activeSelf) _rotatorCm.m_XAxis.Value = _rotateSpeed;
        }
    }
}
