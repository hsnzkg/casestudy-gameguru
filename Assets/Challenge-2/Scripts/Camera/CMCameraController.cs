using Cinemachine;
using UnityEngine;
using Zenject;

public class CMCameraController : MonoBehaviour, ICameraController
{
    [SerializeField] private CinemachineVirtualCamera _cm;


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
}