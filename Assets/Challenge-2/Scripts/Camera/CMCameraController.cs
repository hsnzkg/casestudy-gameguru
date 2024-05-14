using Cinemachine;
using UnityEngine;
using Zenject;

public class CMCameraController : MonoBehaviour, ICameraController
{
    [Inject] private ILevelCreator _levelCreator;
    [SerializeField] private CinemachineVirtualCamera _cm;

    private void Awake()
    {
        var player = _levelCreator.GetRuntimeLevelData().Player;
        player.OnFall += ReleaseTarget;
        SetTarget(player.transform);
    }

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