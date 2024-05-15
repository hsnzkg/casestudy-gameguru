using UnityEngine;

namespace Case_2
{
    public class PlayerAnimationController : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        public void UpdateAnimation(bool isRunnning)
        {
            _animator.SetBool("IsRunning", isRunnning);
        }
    }
}
