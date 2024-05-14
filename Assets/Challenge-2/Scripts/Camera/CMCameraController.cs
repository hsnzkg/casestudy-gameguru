using Cinemachine;
using UnityEngine;
using Zenject;

public class CMCameraController : MonoBehaviour, ICameraController
{
    [Inject] private ILevelCreator _levelCreator;

    [SerializeField] private CinemachineVirtualCamera _cm;

    private void Start()
    {
        SetTarget(_levelCreator.GetRuntimeLevelData().Player.transform);
    }

    public void ReleaseTarget()
    {
        _cm.m_LookAt = null;
    }

    public void SetTarget(Transform t)
    {
        _cm.LookAt = t;
        _cm.m_Follow = t;
    }
}