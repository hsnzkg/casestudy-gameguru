using UnityEngine;
using Zenject;

namespace Case_2
{
    public class PlayerCollisionController : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [Inject] private GameManager _gameManager;
        public LayerMask FinishDetectionLayer;

        private void OnTriggerEnter(Collider other)
        {
            var layer = other.gameObject.layer;
            if (FinishDetectionLayer.Contains(layer))
            {
                _gameManager.FinishGame(true);
            }
        }
    }
}
