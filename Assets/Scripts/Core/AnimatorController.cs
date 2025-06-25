using System;
using UnityEngine;

namespace Assets.Scripts.Core
{
    [RequireComponent(typeof(Animator))]

    public class AnimatorController : MonoBehaviour
    {
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            GameManager.OnPlayerBasicAnimations += UpdateAnimationState;
        }

        private void OnDisable()
        {
            GameManager.OnPlayerBasicAnimations -= UpdateAnimationState;
        }



        private void UpdateAnimationState(Vector2 inputVector)
        {
            if (_animator != null && _animator.runtimeAnimatorController != null)
            {
                _animator.SetFloat("Axis X", inputVector.x);
                _animator.SetFloat("Axis Y", inputVector.y);
            }
        }

    }
}