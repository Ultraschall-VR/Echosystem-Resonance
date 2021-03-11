using System.Collections.Generic;
using UnityEngine;

namespace Echosystem.Resonance.Prototyping
{
    public class PlayAudio : MonoBehaviour
    {
        private List <AudioSource> _audioSources;

        private void Start()
        {
            _audioSources = GetComponentInChildren<List<AudioSource>>();
            foreach (var audio in _audioSources)
            {
                audio.Play();
            }
        }
    }
}