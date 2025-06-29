using System;
using UnityEngine;

namespace Assets.Scripts.Core
{
    public class RagdollManager : MonoBehaviour
    {
        #region Serialized Fields

        [Header("Necessary Components")]
        [Space(10)]
        [SerializeField] private Animator _playerAnimator;
        [SerializeField] private Collider _playerCollider;
        [SerializeField] private Rigidbody _playerRigidbody;

        #endregion

        #region Private Fields

        private Rigidbody[] _ragdollRigidbodies;
        private Collider[] _ragdollColliders;
        private Joint[] _ragdollJoints;

        #endregion

        private void Awake()
        {
            //If i use the parameter "true" in GetComponentsInChildren, it will find all components, even if they are disabled!!!😍

            _ragdollRigidbodies = GetComponentsInChildren<Rigidbody>(true);
            _ragdollColliders = GetComponentsInChildren<Collider>(true);
            _ragdollJoints = GetComponentsInChildren<Joint>(true);
        }

        //⚠️⚠️⚠️ HEY!: I must to get use to do xml coments if I want to do public methods

        /// <summary>
        /// Handle the ragdoll state of the player.
        /// </summary>
        /// <param name="enableRagdoll">If true, enables the ragdoll state; otherwise, disables it.</param>
        public void SetRagdollState(bool enableRagdoll)
        {
            _playerAnimator.enabled = !enableRagdoll; //⚠️Remember: if ragdoll is enabled, the animator must be disabled

            if (_playerCollider != null)
                _playerCollider.enabled = !enableRagdoll;

            if (_playerRigidbody != null)
            {
                _playerRigidbody.isKinematic = enableRagdoll;
                _playerRigidbody.linearVelocity = Vector3.zero;
                _playerRigidbody.angularVelocity = Vector3.zero;
            }

            foreach (Rigidbody rb in _ragdollRigidbodies)
            {
                if (rb == _playerRigidbody) continue;

                rb.isKinematic = !enableRagdoll;
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }

            foreach (Collider col in _ragdollColliders)
            {
                if (col == _playerCollider) continue;

                col.enabled = !enableRagdoll;
            }

            foreach (Joint joint in _ragdollJoints)
            {
                if (joint == null) continue;

                joint.enableCollision = !enableRagdoll;
            }

        }

        /// <summary>
        /// Activates the ragdoll state for the player.
        /// </summary>
        public void ActivateRagdoll() => SetRagdollState(true);
    }
}
