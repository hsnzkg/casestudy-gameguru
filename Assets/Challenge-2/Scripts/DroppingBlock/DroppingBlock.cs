using DG.Tweening;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class DroppingBlock : MonoBehaviour
{
    public bool IsActive => gameObject.activeSelf;
    private Rigidbody _rb;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void Activate()
    {
        gameObject.SetActive(true);
        var delayedCall = DOVirtual.DelayedCall(1f,DeActivate);
        delayedCall.Play();
    }

    public void DeActivate()
    {
        gameObject.SetActive(false);
        if (DOTween.IsTweening(gameObject))
        {
            // There was a bug before, wanted to guarantee
            DOTween.Kill(gameObject);
            DOTween.Kill(gameObject.GetInstanceID());
        }
    }

    public class Factory : PlaceholderFactory<DroppingBlock> { }
}
