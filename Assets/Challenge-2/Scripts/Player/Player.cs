using UnityEngine;
using Zenject;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(PlayerAnimationController))]
public class Player : MonoBehaviour
{
    [Inject] private BlockWaypoingController _blockWaypoingController;
    [Inject] private IAudioService _audioService;
    [Inject] private PlayerSettings _playerSettings;
    
    private PlayerAnimationController _animationController;
    private Rigidbody _rb;
    private CapsuleCollider _collider;
    private Vector3 _targetPosition;
    private bool _isActive = false;
    private float _accelerationRate = 0f;
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
        _currentBlock = _blockWaypoingController.CurrentBlock;
    }

    public void Deactivate()
    {
        if (!_isActive) return;
        _isActive = false;
        _animationController.UpdateAnimation(_isActive);
    }

    private void Init()
    {
        _collider = GetComponent<CapsuleCollider>();
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

    public void UpdateTargetPosition(Vector3 newPos)
    {
        _targetPosition = newPos;
    }

    private Vector3 GetNormalizedDir()
    {
        var value = (_targetPosition - transform.position).normalized;
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
        var currentPosition = _rb.position;
        currentPosition.y = 0;
        var targetPosition = _currentBlock.transform.position;
        targetPosition.y = 0;
        var distance = Mathf.Abs((currentPosition - targetPosition).sqrMagnitude);
        return distance <= 0.1f; 
    }

    private void OnTargetReach()
    {
        _blockWaypoingController.OnTargetReach();
        _currentBlock = _blockWaypoingController.CurrentBlock;
    }

    private void FixedUpdate()
    {
        if (_isActive)
        {
            Accelerate();
            UpdateTargetPosition(_currentBlock.transform.position);
            UpdateVelocity();
            UpdateRotation();
            if(IsFalling()) OnFall();
            if(CheckDistance())OnTargetReach();
        }
        else
        {
            DeAccelerate();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, _targetPosition);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + GetNormalizedDir() * 10f);
    }

    public class Factory : PlaceholderFactory<Player> { }
}
