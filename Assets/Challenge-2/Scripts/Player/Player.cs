using UnityEngine;
using Zenject;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(PlayerAnimationController))]
public class Player : MonoBehaviour
{
    [Inject] private BlockWaypointController _blockWaypointController;
    [Inject] private IAudioService _audioService;
    [Inject] private PlayerSettings _playerSettings;
    
    private PlayerAnimationController _animationController;
    private Rigidbody _rb;

    private bool _isActive = false;
    private float _accelerationRate = 0f;
    private int _curveIndex = 0;
    private Transform _currentBlock;

    private void Awake()
    {
        Init();
    }

    public void Activate()
    {
        if (_isActive) return;
        _isActive = true;
        _animationController.UpdateAnimation(_isActive);
        _currentBlock = _blockWaypointController.CurrentBlock;
    }

    public void Deactivate()
    {
        if (!_isActive) return;
        _isActive = false;
        _animationController.UpdateAnimation(_isActive);
    }

    private void Init()
    {
        _rb = GetComponent<Rigidbody>();
        _animationController = GetComponent<PlayerAnimationController>();
    }

    private void Accelerate()
    {
        _accelerationRate = Mathf.Clamp(_accelerationRate + _playerSettings.AccelerationRate * Time.deltaTime, 0f, 1f);
    }

    private void DeAccelerate()
    {
        _accelerationRate = Mathf.Clamp(_accelerationRate - _playerSettings.DeAccelerationRate * Time.deltaTime, 0f, 1f);
    }

    private Vector3 GetNormalizedDir()
    {
        var pointA = _rb.position;
        var pointB = _currentBlock.position + Vector3.back * _currentBlock.localScale.z * 0.5f;
        var pointC = _currentBlock.position;
        var pointD = _currentBlock.position + Vector3.forward * _currentBlock.localScale.z * 0.5f;

        pointA.y = 0;
        pointB.y = 0;
        pointC.y = 0;
        pointD.y = 0;

        Vector3 value;
        if (_curveIndex == 0)
        {
            value = (pointB - pointA).normalized;
        }
        else if(_curveIndex == 1)
        {
            value = (pointC - pointA).normalized;
        }
        else 
        {
            value = (pointD - pointA).normalized;
        }
        value.y = 0f;
        return value;
    }
    
    private void UpdateVelocity()
    {
        var currentVel = _rb.velocity;
        var desiredVel = _rb.transform.forward.normalized;
        var iterator = _playerSettings.MovementDelta * Time.fixedDeltaTime;
        var finalVel = new Vector3(desiredVel.x * iterator * _accelerationRate, currentVel.y, desiredVel.z * iterator * _accelerationRate);
        _rb.velocity = finalVel;
    }

    private void UpdateRotation()
    {
        var lookRotation = Quaternion.LookRotation(GetNormalizedDir());
        var currentRotation = _rb.rotation;
        var desiredRotation = Quaternion.Lerp(currentRotation, lookRotation, _playerSettings.RotationDelta * Time.deltaTime);
        var dif = Quaternion.Angle(currentRotation, desiredRotation);
        _rb.rotation = desiredRotation;
        if (dif <= 0.1f)
        {
            _rb.rotation = lookRotation;
        }
    }

    private bool IsFalling()
    {
        var vel = _rb.velocity;
        var flag = vel.y < -1;
        return flag;
    }

    private void OnFall()
    {
        if (_isActive)
        {
            Deactivate();
            _audioService.PlaySound("fall");
        }
    }

    private bool CheckDistance()
    {
        var pointA = _rb.position;
        var pointB = _currentBlock.position + Vector3.back * _currentBlock.localScale.z * 0.5f;
        var pointC = _currentBlock.position;
        var pointD = _currentBlock.position + Vector3.forward * _currentBlock.localScale.z * 0.5f;

        pointA.y = 0;
        pointB.y = 0;
        pointC.y = 0;
        pointD.y = 0;

        float distance;

        if (_curveIndex == 0)
        {
            distance = Mathf.Abs((pointA - pointB).sqrMagnitude);
        }
        else if (_curveIndex == 1)
        {
            distance = Mathf.Abs((pointA - pointC).sqrMagnitude);
        }
        else
        {
            distance = Mathf.Abs((pointA - pointD).sqrMagnitude);
        }
        return distance <= 0.05f; 
    }

    private void OnTargetReach()
    {
        _blockWaypointController.OnTargetReach();
        _currentBlock = _blockWaypointController.CurrentBlock;
    }

    private void Update()
    {
        if (_isActive)
        {
            Accelerate();
            if (IsFalling()) OnFall();
            if (CheckDistance())
            {
                _curveIndex++;
                if(_curveIndex >= 3)
                {
                    _curveIndex = 0;
                    OnTargetReach();
                }
            }
        }
        else
        {
            DeAccelerate();
        }
    }

    private void FixedUpdate()
    {
        if (_isActive)
        {
            UpdateVelocity();
            UpdateRotation(); 
        }
    }

    private void OnDrawGizmos()
    {
        if (!_currentBlock) return;
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + GetNormalizedDir() * 10f);
    }

    public class Factory : PlaceholderFactory<Player> { }
}
