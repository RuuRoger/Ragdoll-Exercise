using System;
using UnityEngine;

namespace Assets.Scripts.Core
{
    public class AnimatorController : MonoBehaviour
    {
        private void OnEnable()
        {
            GameManager.OnPlayerBasicAnimations += UpdateAnimationState;
        }

        private void OnDisable()
        {
            GameManager.OnPlayerBasicAnimations -= UpdateAnimationState;
        }

        private void UpdateAnimationState(bool isMoving)
        {
            if (isMoving)
            {
                Debug.Log("Player is moving");
                // Trigger the walking animation
            }
            else
            {
                Debug.Log("Player is not moving");
                // Trigger the idle animation
            }
        }

    }
}