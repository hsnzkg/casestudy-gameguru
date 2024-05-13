using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(PlayerAnimationController))]
public class Player : MonoBehaviour
{
    [SerializeField] private Vector3 _targetPosition;
    
    private PlayerAnimationController _animationController;
    private Rigidbody _rb;
    private CapsuleCollider _collider;
    private bool _isActive = false;

    private void Awake()
    {
        Init();
        _isActive = true;
    }

    private void Init()
    {
        _collider = GetComponent<CapsuleCollider>();
        _rb = GetComponent<Rigidbody>();
        _animationController = GetComponent<PlayerAnimationController>();
    }

    public void UpdateTargetPosition(Vector3 newPos)
    {
        _targetPosition = newPos;
    }

    private Vector3 GetNormalizedDir()
    {
        var value = (_targetPosition - transform.position).normalized;
        value.y = 0f;

        if (Mathf.Abs(value.magnitude) <= Mathf.Epsilon) return Vector3.zero;
        return value;
    }
    
    private void UpdateVelocity()
    {
        var currentVel = _rb.velocity;
        var desiredVel = GetNormalizedDir();
        var finalVel = new Vector3(desiredVel.x, currentVel.y, desiredVel.z);
        _rb.velocity = finalVel * 50 * Time.fixedDeltaTime;
    }

    private void FixedUpdate()
    {
        if (_isActive)
        {
            UpdateVelocity();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, _targetPosition);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + GetNormalizedDir() * 10f);
    }
}
