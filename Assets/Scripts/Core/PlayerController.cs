using System;
using UnityEngine;

namespace Assets.Scripts.Core
{
    [RequireComponent(typeof(Rigidbody))]

    public class PlayerController : MonoBehaviour
    {
        #region Events

        public event Action<bool> OnPlayerMovement;

        #endregion

        #region Serialized Fields

        [Header("Movement Settings")]
        [Space(10)]
        [SerializeField] private float _speedMovement;

        #endregion

        #region Private Fields

        private float _verticalInput;
        private Rigidbody _rigidbodyPlayer;
        private bool _currentlyMoving;

        #endregion

        #region  Unity Callbacks

        private void Awake()
        {
            _rigidbodyPlayer = GetComponent<Rigidbody>();
            _currentlyMoving = false;
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

            bool wasMoving = _currentlyMoving;

            if (movement.magnitude > 0.1f)
            {
                _currentlyMoving = true;
            }
            else
            {
                _currentlyMoving = false;
            }

            // Only emit event when movement state changes
            if (wasMoving != _currentlyMoving)
            {
                OnPlayerMovement?.Invoke(_currentlyMoving);
            }
        }

        #endregion
    }
}