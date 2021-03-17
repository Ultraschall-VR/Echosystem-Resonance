using UnityEngine;

namespace Echosystem.Resonance.Prototyping
{
    public class PlayDoorSound : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [Range(1, 4)] [SerializeField] private int _playAfterMelody;

        private bool _activated;

        void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        void Update()
        {
            if (CollectibleManager.Index == _playAfterMelody && !_activated)
            {
                _activated = true;
                _audioSource.Play();
            }
        }
    }
}