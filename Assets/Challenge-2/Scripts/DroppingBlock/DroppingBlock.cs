using DG.Tweening;
using UnityEngine;
using Zenject;


namespace Case_2
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(BoxCollider))]
    public class DroppingBlock : MonoBehaviour
    {
        public BlockMaterialChanger ColorController;
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
            var scaleTween = transform.DOScale(Vector3.zero, 2.5f).SetEase(Ease.Linear);
            scaleTween.OnComplete(DeActivate);
            scaleTween.Play();
        }

        public void DeActivate()
        {
            _rb.velocity = Vector3.zero;
            _rb.angularVelocity = Vector3.zero;
            gameObject.SetActive(false);
            if (DOTween.IsTweening(gameObject) || DOTween.IsTweening(transform))
            {
                // There was a bug before, wanted to guarantee
                DOTween.Kill(gameObject.transform);
                DOTween.Kill(gameObject.transform.GetInstanceID());
            }
        }
        public class Factory : PlaceholderFactory<DroppingBlock> { }
    }
}

