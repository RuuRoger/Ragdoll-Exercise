using System;
using UnityEngine;

namespace Assets.Scripts.Core
{
    public class GameManager : MonoBehaviour
    {
        #region Events
        public static event Action<Vector2> OnPlayerBasicAnimations;

        #endregion

        #region Private Fields
        private PlayerController _playerController;

        #endregion

        #region Unity Callbacks
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

        #endregion

        #region Private Methods
        private void CallAnimationController(Vector2 inputVector) => OnPlayerBasicAnimations?.Invoke(inputVector);

        #endregion

    }
}