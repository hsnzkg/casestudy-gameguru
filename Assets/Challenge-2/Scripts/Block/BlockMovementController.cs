using UnityEngine;
using Zenject;

public class BlockMovementController : MonoBehaviour
{
    [Inject] private FXController _fxController;
    [Inject] private BlockWaypointController _waypoingController;
    [Inject] private BlockMovementSetting _setting;
    [Inject] private ILevelCreator _levelCreator;
    [Inject] private IInputService _inputService;
    [Inject] private IAudioService _audioService;

    [SerializeField] private BlockMaterialChanger _materialChanger;

    private bool _activatedOnce = false;
    private bool _isActive = false;
    private bool _facingDirection = false;
    private Vector3 _centerPosition;
    private Vector3 _initialScale;
    public bool ActivatedOnce => _activatedOnce;

    public void SetInitialScale(Vector3 scale)
    {
        _initialScale = scale;
    }

    public void Prepare(Vector3 centerPosition,Vector3 scale)
    {
        transform.localScale = scale;
        _centerPosition = centerPosition + Vector3.forward * transform.localScale.z;
        _facingDirection = Random.Range(0, 2) == 1;
        if (_facingDirection)
        {
            transform.position = new Vector3(centerPosition.x + scale.x,centerPosition.y,transform.position.z);
            _facingDirection = false;
        }
        else
        {
            transform.position = new Vector3(centerPosition.x + scale.x * -1f, centerPosition.y, transform.position.z);
            _facingDirection = true;
        }
        _inputService.RegisterActionToPress(OnPress);
    }

    private float CalculateThreshold()
    {
        return _centerPosition.x - transform.position.x;
    }

    private void Split(float threshold)
    {
        var direction = threshold > 0 ? -1 : 1; 
        float newXSize = transform.localScale.x - Mathf.Abs(threshold);
        float fallingBlockSize = transform.localScale.x - newXSize;

        float newXPosition = transform.position.x + threshold * 0.5f;
        transform.localScale = new Vector3(newXSize, transform.localScale.y, transform.localScale.z);
        transform.position = new Vector3(newXPosition, transform.position.y, transform.position.z);

        var cubeEdge = transform.position.x + (newXSize / 2f * direction);
        var fallingBlockXPosition = cubeEdge + fallingBlockSize / 2f * direction;

        SpawnDropCube(fallingBlockXPosition,fallingBlockSize);
    }

    private void SpawnDropCube(float xPosition,float fallingBlockSize)
    {
        var block = _levelCreator.GetDroppingBlock();
        if(block != null)
        {

            block.ColorController.ChangeMaterial(_materialChanger.material);

            block.gameObject.transform.localScale = new Vector3(fallingBlockSize, transform.localScale.y, transform.localScale.z);
            block.gameObject.transform.position = new Vector3(xPosition, transform.position.y, transform.position.z);
            block.Activate();
            _audioService.PlaySound("brick");
        }
    }

    private void OnPress()
    {
        var currentScale = transform.localScale.x;
        
        var threshold = CalculateThreshold();
        float scaleFactor = _initialScale.x / currentScale;
        
        float adjustedSplitBadThreshold = currentScale * _setting.SplitBadThreshold * (1/scaleFactor);
        float adjustedSplitGoodThreshold = currentScale / _setting.SplitGoodThreshold;

        if (Mathf.Abs(threshold) >= adjustedSplitBadThreshold)
        {
            DeActivate(true);
        }
        else
        {
            if (Mathf.Abs(threshold) <= adjustedSplitGoodThreshold)
            {
                _fxController.SpawnVFX(transform.localPosition, transform.localScale, Color.white);
                _audioService.PlaySound("note");
                _audioService.GetComboListener().SetCombo(true);
                transform.position = _centerPosition;
            }
            else
            {
                _audioService.GetComboListener().SetCombo(false);
                Split(threshold);
            }
            _waypoingController.OnPress();
            DeActivate();
        }
    }

    public void Activate()
    {
        if (_isActive) return;
        _isActive = true;
        _activatedOnce = true;
        gameObject.SetActive(_isActive);
    }

    public void DeActivate(bool setActive = false)
    {
        if(!_isActive)return;
        _isActive = false;
        _inputService.UnRegisterActionToPress(OnPress);
        if (setActive)gameObject.SetActive(_isActive);
    }

    private void Move()
    {
        var currentPosition = transform.position;
        var currentScale = transform.localScale;    
        var desiredPosition = _facingDirection ? _centerPosition + Vector3.right * currentScale.x : _centerPosition + Vector3.left * currentScale.x;
        var nextPosition = Vector3.MoveTowards(currentPosition, desiredPosition, _setting.BlockMovementDelta * Time.deltaTime);
        transform.position = nextPosition;
    }

    private void CheckDestination()
    {
        var currentPosition = transform.position;
        var currentScale = transform.localScale;
        var desiredPosition = _facingDirection ? _centerPosition + Vector3.right * currentScale.x : _centerPosition + Vector3.left * currentScale.x;
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
