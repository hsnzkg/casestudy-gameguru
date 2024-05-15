using UnityEngine;
using System.Collections;
using DG.Tweening;

namespace Case_1
{
    public class Cell : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _cellSprite;
        [SerializeField] private SpriteRenderer _crossSprite;

        private bool _isFull;
        private bool _isBusy = false;

        private int _posX;
        private int _posY;
        private Vector3 _xScale;

        public bool IsFull => _isFull;

        private void Awake()
        {
            _xScale = _crossSprite.transform.localScale;
        }

        public void SetPosition(int x, int y)
        {
            _posX = x;
            _posY = y;
        }

        public Vector2Int GetPosition()
        {
            return new Vector2Int(_posX, _posY);
        }

        public void Fill()
        {
            if (_isFull || _isBusy)
                return;

            _isFull = true;

            _crossSprite.transform.DOKill();

            _crossSprite.transform.localScale = Vector3.zero;
            _crossSprite.gameObject.SetActive(true);

            _crossSprite.transform.DOScale(_xScale, 0.2f).SetEase(Ease.OutBack);
        }

        public void ResetCell()
        {
            StartCoroutine(CellReset(0.25f));
        }

        private IEnumerator CellReset(float delay)
        {           
            _isBusy = true;
            yield return new WaitForSecondsRealtime(delay);
            _isFull = false;
            _crossSprite.transform.DOKill();
            _crossSprite.transform.DOScale(0, 0.2f).SetEase(Ease.Linear).OnComplete(() =>
            {
                _crossSprite.gameObject.SetActive(false);
                _isBusy = false;
            });
        }
    }

}
