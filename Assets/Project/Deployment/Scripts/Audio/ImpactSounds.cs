using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Echosystem.Resonance.Audio
{
    public class ImpactSounds : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private List<AudioClip> _clips;

        private void OnCollisionEnter(Collision other)
        {
            if (_clips.Count > 1)
            {
                var rand = Random.Range(0, _clips.Count);
                _audioSource.PlayOneShot(_clips[rand]);
            }
            else
            {
                _audioSource.PlayOneShot(_clips[0]);
            }
        }
    }
}