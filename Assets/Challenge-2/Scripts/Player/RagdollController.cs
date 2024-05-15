using System.Collections.Generic;
using UnityEngine;

namespace Case_2
{
    public class RagdollController : MonoBehaviour
    {
        [SerializeField] private List<Rigidbody> _ragdollRBList;
        [SerializeField] private List<Collider> _ragdollColliderList;
        [SerializeField] private Rigidbody _characterRB;
        [SerializeField] private Collider _characterCollider;
        [SerializeField] private Animator _chracterAnimator;

        private void Awake()
        {
            DeactivateRagdoll();
        }

        public void ActivateRagdoll()
        {
            _characterCollider.enabled = false;
            _chracterAnimator.enabled = false;
            _characterRB.useGravity = false;

            foreach (var c in _ragdollColliderList)
            {
                c.enabled = true;
            }

            foreach (var r in _ragdollRBList)
            {
                r.useGravity = true;
            }
        }

        public void DeactivateRagdoll()
        {
            _characterCollider.enabled = true;
            _chracterAnimator.enabled = true;
            _characterRB.useGravity = true;

            foreach (var c in _ragdollColliderList)
            {
                c.enabled = false;
            }

            foreach (var r in _ragdollRBList)
            {
                r.useGravity = false;
            }
        }
    }
}
