using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Echosystem.Resonance.Game
{
    public class UncoveringShock : MonoBehaviour
    {
        private List<AudioReactive> _audioReactives;
        private List<AudioReactiveEnvironment> _audioReactiveEnvironments;

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            _audioReactives = FindObjectsOfType<AudioReactive>().ToList();
            _audioReactiveEnvironments = FindObjectsOfType<AudioReactiveEnvironment>().ToList();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                foreach (var audioReactive in _audioReactiveEnvironments)
                {
                    audioReactive.Uncover();
                }
            }
            
            if (Input.GetKeyUp(KeyCode.K))
            {
                foreach (var audioReactive in _audioReactiveEnvironments)
                {
                    audioReactive.Cover();
                }
            }
        }
    } 
}


