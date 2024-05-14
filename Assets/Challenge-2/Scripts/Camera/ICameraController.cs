using UnityEngine;

public interface ICameraController
{
    public void SetTarget(Transform t);
    public void ReleaseTarget();
    public void SetWinCamera();
}