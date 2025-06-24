using System;
using UnityEngine;

namespace Assets.Scripts.Core
{
    public class GameManager : MonoBehaviour
    {
        public static event Action<float> OnPlayerBasicAnimations;

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

        private void CallAnimationController(float speedValue) => OnPlayerBasicAnimations?.Invoke(speedValue);

    }
}