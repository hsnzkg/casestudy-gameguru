using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void UpdateAnimation(bool isRunnning)
    {
        if (isRunnning)
        {
            _animator.SetBool("IsRunning", isRunnning);
        }
    }
}