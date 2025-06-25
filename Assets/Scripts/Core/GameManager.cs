using System;
using UnityEngine;

namespace Assets.Scripts.Core
{
    public class GameManager : MonoBehaviour
    {
        public static event Action<Vector2> OnPlayerBasicAnimations;

        private PlayerController _playerController;

        private void Awake()
        {
            _playerController = FindFirstObjectByType<PlayerController>();
        }

        private void OnEnable()
        {
            if (_playerController != null)
            {
                _playerController.OnPlayerMovement += CallAnimationController;
            }
        }

        private void OnDisable()
        {
            if (_playerController != null)
            {
                _playerController.OnPlayerMovement -= CallAnimationController;
            }
        }

        private void CallAnimationController(Vector2 inputVector) => OnPlayerBasicAnimations?.Invoke(inputVector);

    }
}