using System;
using UnityEngine;

namespace Assets.Scripts.Core
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Animator))]

    public class PlayerController : MonoBehaviour
    {
        #region Events

        public event Action<Vector2> OnPlayerMovement;

        #endregion

        #region Serialized Fields

        [Header("Movement Settings")]
        [Space(10)]
        [SerializeField] private float _speedMovement;
        [SerializeField] private float _rotationSpeedX;

        #endregion

        #region Private Fields

        private float _verticalInput;
        private float _horizontalInput;
        private float _rotationInputX;
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
            _horizontalInput = Input.GetAxis("Horizontal");
            _rotationInputX = Input.GetAxis("Mouse X");
        }

        private void HandleMovement()
        {
            //Value directly to Blend Tree
            Vector2 inputVector = new Vector2(_horizontalInput, _verticalInput);

            //Normalized the input
            if (inputVector.magnitude > 1f)
                inputVector.Normalize();

            //Remember: I put the direction and the speed in the vector.
            Vector3 movement = (transform.forward * inputVector.y + transform.right * inputVector.x) * _speedMovement;

            /*Remember: Apply the movement to the player
            Remember: I must to keep the 'y' velocity to avoid falling*/
            _rigidbodyPlayer.linearVelocity = new Vector3(movement.x, _rigidbodyPlayer.linearVelocity.y, movement.z);

            if (_rotationInputX != 0)
                transform.Rotate(0, _rotationInputX * _rotationSpeedX, 0);

            OnPlayerMovement?.Invoke(inputVector);
        }

        #endregion
    }
}