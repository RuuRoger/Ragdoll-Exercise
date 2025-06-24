using UnityEngine;
using UnityEngine.XR;

namespace Assets.Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        [Header("🎮 Movement Settings")]
        [Space(10)]
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _moveRotationSpeed;
        [Space(10)]

        [Header("Camera Settings")]
        [Space(10)]
        [SerializeField] private float _cameraSensitivity;
        [SerializeField] private Transform _cameraTransform;

        //Private fields
        private float _horizontalInput;
        private float _verticalInput;
        private Animator _animator;
        private Rigidbody _rigidbody;
        private float _moveCameraX;
        private float _moveCameraY;
        private float _currentVerticalRotation;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _rigidbody = GetComponent<Rigidbody>();
            _currentVerticalRotation = 0f;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        private void Update()
        {
            HandleInput();
            HandleMovementCamera();
        }

        private void FixedUpdate() => HandleMovementPlayer();

        private void HandleInput()
        {
            // Read all input in Update for responsiveness
            _horizontalInput = Input.GetAxis("Horizontal");
            _verticalInput = Input.GetAxis("Vertical");
            _moveCameraX = Input.GetAxis("Mouse X");
            _moveCameraY = Input.GetAxis("Mouse Y");
        }

        private void HandleMovementPlayer()
        {
            // Apply all physics-related movement in FixedUpdate for stability
            Vector3 movement = new Vector3(_horizontalInput, 0, _verticalInput).normalized;
            _rigidbody.linearVelocity = new Vector3(movement.x * _moveSpeed, _rigidbody.linearVelocity.y, movement.z * _moveSpeed);
        }

        private void HandleMovementCamera()
        {
            // Rotation of the player and camera based on input (su)
            transform.Rotate(0, _moveCameraX * _cameraSensitivity, 0);
            _currentVerticalRotation -= _moveCameraY * _cameraSensitivity;
            _currentVerticalRotation = Mathf.Clamp(_currentVerticalRotation, -80f, 80f);
            _cameraTransform.localRotation = Quaternion.Euler(_currentVerticalRotation, 0, 0);
        }
    }
}
