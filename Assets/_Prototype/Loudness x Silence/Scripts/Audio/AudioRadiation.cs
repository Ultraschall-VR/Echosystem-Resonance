using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;

//using Echosystem.Resonance.Prototyping;

namespace Echosystem.Resonance.Prototyping
{
    public class AudioRadiation : MonoBehaviour
    {
        public AudioMixerSnapshot _zero;
        public AudioMixerSnapshot _phase1;
        public AudioMixerSnapshot _phase2;
        public AudioMixerSnapshot _phase3;
        
        [SerializeField] private float transitionZeroIn;
        [SerializeField] private float transitionOneIn;
        [Range(0.0f, 1f)] [SerializeField] private float transitionPointTwo;
        [SerializeField] private float transitionTwoIn;
        [Range(0.0f, 1f)] [SerializeField] private float transitionPointThree;
        [SerializeField] private float transitionThreeIn;

        //   [SerializeField] private float _TransitionOut;
        private float _Loudness = 0f;

        private void Update()
        {
            _Loudness = Observer.LoudnessValue;

            if (Observer.CurrentSilenceSphere != null)
            {
                _zero.TransitionTo(transitionZeroIn);
            }
            /*
            else if (_Loudness == 0f) {
                _zero.TransitionTo(_TransitionIn);
            }
            */
            else if (_Loudness > 0f && _Loudness < transitionPointTwo)
            {
                _phase1.TransitionTo(transitionOneIn);
            }
            else if (_Loudness > transitionPointTwo && _Loudness <= transitionPointThree)
            {
                _phase2.TransitionTo(transitionTwoIn);
            }
            else if (_Loudness > transitionPointThree)
            {
                _phase3.TransitionTo(transitionThreeIn);
            }
        }
    }
}