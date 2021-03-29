using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Echosystem.Resonance.Prototyping
{
    public class PlayAudio : MonoBehaviour
    {
        public List <AudioSource> _audioSources;

        private void Start()
        {
            _audioSources = GetComponentsInChildren<AudioSource>().ToList();
            foreach (var audio in _audioSources)
            {
                audio.Play();
            }
        }
    }
}