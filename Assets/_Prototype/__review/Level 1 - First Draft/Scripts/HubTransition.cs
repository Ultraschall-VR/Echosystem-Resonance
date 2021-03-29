using UnityEngine;

namespace Echosystem.Resonance.Prototyping
{
    public class HubTransition : MonoBehaviour
    {
        [SerializeField] private AudioClip _transitionSound;
        private bool _switched = true;
        private AudioSource _audioSource;


        void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        void Update()
        {
            if (Observer.CurrentSilenceSphere != null && !_switched && Observer.SilenceSphereExited)
            {
                PlayTransitionSound(true);
            }

            else if (Observer.CurrentSilenceSphere == null && _switched && Observer.SilenceSphereExited)
            {
                PlayTransitionSound(false);
            }
        }


        private void PlayTransitionSound(bool started)
        {
            _switched = started;
            _audioSource.PlayOneShot(_transitionSound);
        }
    }
}