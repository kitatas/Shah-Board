using UnityEngine;

namespace ShahBoard.Common.Presentation.Controller
{
    [RequireComponent(typeof(AudioSource))]
    public abstract class BaseSoundController : MonoBehaviour
    {
        private AudioSource _audioSource;

        protected AudioSource audioSource
        {
            get
            {
                return _audioSource ??= GetComponent<AudioSource>();
            }
        }

        public float GetVolume()
        {
            return audioSource.volume;
        }

        public void SetVolume(float value)
        {
            audioSource.volume = value;
        }
    }
}