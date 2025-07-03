using System;
using Assets.Scripts.Object;
using UnityEngine;

namespace Assets.Scripts.Core
{
    public class GameManager : MonoBehaviour
    {
        #region Events
        public static event Action<Vector2> OnPlayerBasicAnimations;
        // public static event Action OnBoxExplotion;
        public static event Action OnRespawn;

        #endregion

        #region Private Fields
        private PlayerController _playerController;
        private BoxExplotion _boxExplotion;

        #endregion

        #region Unity Callbacks
        private void Awake()
        {
            _playerController = FindFirstObjectByType<PlayerController>();
            _boxExplotion = FindFirstObjectByType<BoxExplotion>();
        }

        private void OnEnable()
        {
            _playerController.OnPlayerMovement += CallAnimationController;
            _boxExplotion.OnExplotion += RespawnBox;
        }

        #endregion

        #region Private Methods
        private void CallAnimationController(Vector2 inputVector) => OnPlayerBasicAnimations?.Invoke(inputVector);
        private void RespawnBox() => Invoke("ActivateBoxAfterDelay", 8f);


        private void ActivateBoxAfterDelay()
        {
            _boxExplotion.gameObject.SetActive(true);
            OnRespawn?.Invoke();
        }


        #endregion

    }
}