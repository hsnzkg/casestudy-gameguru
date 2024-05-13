using UnityEngine;
using Zenject;

public class BlockMovementController : MonoBehaviour
{
    [Inject]
    private BlockMovementSetting _setting;

    private bool _isActive = false;
    private bool _facingDirection = false;
    private Vector3 _centerPosition;
    

    private void Start()
    {
        Init();
    }

    [Inject]
    public void Construct(BlockMovementSetting setting)
    {
        _setting = setting; 
    }

    public void Init()
    {
        _centerPosition = transform.position;
    }

    public void MakeStartPositionDecision()
    {
        _facingDirection = Random.Range(0, 2) == 1;
        if (_facingDirection)
        {
            transform.position = new Vector3(_setting.HorizontalOffset,transform.position.y,transform.position.z);
            _facingDirection = false;
        }
        else
        {
            transform.position = new Vector3(_setting.HorizontalOffset * -1f, transform.position.y, transform.position.z);
            _facingDirection = true;
        }
    }

    public void Activate()
    {
        if (_isActive) return;
        _isActive = true;
    }

    public void DeActivate()
    {
        if(!_isActive)return;
        _isActive = false;
    }

    private void Move()
    {
        var currentPosition = transform.position;
        var desiredPosition = _facingDirection ? _centerPosition + Vector3.right * _setting.HorizontalOffset : _centerPosition + Vector3.left * _setting.HorizontalOffset;
        var nextPosition = Vector3.MoveTowards(currentPosition, desiredPosition, _setting.BlockMovementDelta * Time.deltaTime);
        transform.position = nextPosition;
    }

    private void CheckDestination()
    {
        var currentPosition = transform.position;
        var desiredPosition = _facingDirection ? _centerPosition + Vector3.right * _setting.HorizontalOffset : _centerPosition + Vector3.left * _setting.HorizontalOffset;
        var mag = Mathf.Abs(Vector3.Distance(currentPosition, desiredPosition));
        if (mag <= Mathf.Epsilon)
        {
            _facingDirection = !_facingDirection;
            transform.position = desiredPosition;
        }
    }

    private void Update()
    {
        if (_isActive)
        {
            Move();
            CheckDestination();
        }
    }


    public class Factory : PlaceholderFactory<BlockMovementController> { }
}
