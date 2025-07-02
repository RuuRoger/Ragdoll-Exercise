using UnityEngine;
using Cinemachine;

namespace Assets.Scripts.Object
{
    public class CameraRagdollControl : MonoBehaviour
    {
        [Header("Cinemachine Settings")]
        [SerializeField] private CinemachineVirtualCamera _virtualCamera;
        [SerializeField] private Transform _targetPlayer;
        [SerializeField] private Transform _targetInRagdoll;
        [SerializeField] private float _highCamera;

        private Transform _ragdollTarget;

        private void Awake()
        {
            _virtualCamera = FindFirstObjectByType<CinemachineVirtualCamera>();
        }

        public void SwitchToRagdoll()
        {
            _virtualCamera.Follow = _targetInRagdoll;
            _virtualCamera.LookAt = _targetInRagdoll;

            if (_virtualCamera.transform.position.y <= 2)
                _virtualCamera.transform.position = new Vector3(
                    _virtualCamera.transform.position.x,
                    _targetInRagdoll.position.y + _highCamera,
                    _virtualCamera.transform.position.z
                );
        }

        public void SwitchToPlayer()
        {
            _virtualCamera.Follow = _targetPlayer;
            _virtualCamera.LookAt = _targetPlayer;
        }

    }
}