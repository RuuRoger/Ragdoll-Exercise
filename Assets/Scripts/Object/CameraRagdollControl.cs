using UnityEngine;
using Cinemachine;
using Assets.Scripts.Core;

namespace Assets.Scripts.Object
{
    public class CameraRagdollControl : MonoBehaviour
    {
        [Header("Cinemachine Settings")]
        [SerializeField] private CinemachineVirtualCamera _virtualCamera;
        [SerializeField] private Transform _targetPlayer;
        [SerializeField] private Transform _targetInRagdoll;
        [SerializeField] private float _highCamera;

        private void Awake()
        {
            _virtualCamera = FindFirstObjectByType<CinemachineVirtualCamera>();
        }

        public void SwitchToRagdoll()
        {
            _virtualCamera.Follow = _targetInRagdoll;
            _virtualCamera.LookAt = _targetInRagdoll;

            Invoke("ResetCameraPositon", 4f);
            Invoke("SwitchToPlayer", 5f);
        }

        public void SwitchToPlayer()
        {
            PlayerController player = FindFirstObjectByType<PlayerController>();

            player.transform.position = _targetInRagdoll.position;
            player.transform.rotation = Quaternion.identity;



            player.GetComponent<Animator>().enabled = true;

            _virtualCamera.Follow = _targetPlayer;
            _virtualCamera.LookAt = _targetPlayer;

        }

        private void ResetCameraPositon()
        {
            //The idea is move in axis Y the camera, because sometiees the camera is down of ground position looking the player
            if (_virtualCamera.transform.position.y <= 2)
                _virtualCamera.transform.position = new Vector3(
                    _virtualCamera.transform.position.x,
                    _targetInRagdoll.position.y + _highCamera,
                    _virtualCamera.transform.position.z
                );

        }
    }

}
