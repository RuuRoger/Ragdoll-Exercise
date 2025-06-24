using System;
using UnityEngine;

namespace Assets.Scripts.Core
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Animator))]

    public class PlayerController : MonoBehaviour
    {
        #region Events

        public event Action<float> OnPlayerMovement;

        #endregion

        #region Serialized Fields

        [Header("Movement Settings")]
        [Space(10)]
        [SerializeField] private float _speedMovement;

        #endregion

        #region Private Fields

        private float _verticalInput;
        private Rigidbody _rigidbodyPlayer;

        #endregion

        #region  Unity Callbacks

        private void Awake()
        {
            _rigidbodyPlayer = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            InputsMovement();
        }

        private void FixedUpdate()
        {
            HandleMovement();
        }

        #endregion

        #region Private Methods

        private void InputsMovement()
        {
            _verticalInput = Input.GetAxis("Vertical");
        }

        private void HandleMovement()
        {
            //Remember: I put the direction and the speed in the vector.
            Vector3 movement = transform.forward * _verticalInput * _speedMovement;

            /*Remember: Apply the movement to the player
            Remember: I must to keep the 'y' velocity to avoid falling*/
            _rigidbodyPlayer.linearVelocity = new Vector3(movement.x, _rigidbodyPlayer.linearVelocity.y, movement.z);

            // Send the input value directly to Blend Tree
            OnPlayerMovement?.Invoke(_verticalInput);
        }

        #endregion
    }
}