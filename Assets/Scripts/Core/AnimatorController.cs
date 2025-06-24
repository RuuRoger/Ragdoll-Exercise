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



        private void UpdateAnimationState(float speedValue)
        {
            if (_animator != null && _animator.runtimeAnimatorController != null)
            {
                _animator.SetFloat("Axis Y", speedValue);
            }
            else
            {
                Debug.LogWarning("Animator Controller not assigned to " + gameObject.name);
            }
        }

    }
}