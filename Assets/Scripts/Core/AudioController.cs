using UnityEngine;

namespace Assets.Scripts.Core
{
    public class AudioController : MonoBehaviour
    {
        private AudioSource audioSource;

        private void Awake() => audioSource = GetComponent<AudioSource>();

        public void PlaySound() => audioSource.Play();
    }
}