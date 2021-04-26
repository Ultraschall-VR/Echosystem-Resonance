using UnityEngine;
using UnityEngine.Audio;

namespace Echosystem.Resonance.Prototyping
{
    public class AudioRadiation : MonoBehaviour
    {
        #region VARIABLES

        [SerializeField] private AudioMixerSnapshot _zero;
        [SerializeField] private AudioMixerSnapshot _phase1;
        [SerializeField] private AudioMixerSnapshot _phase2;
        [SerializeField] private AudioMixer _audioMixer;
        [SerializeField] private AudioClip _deathSound;
        private float _transitionZeroIn = 2;
        private float _transitionOneIn = 3;
        private float _transitionPointTwo = .5f;
        private float _transitionPointThree = 1;
        private float _transitionTwoIn;
        private float _loudness = 0f;
        private bool _dead;

        #endregion

        private void Update()
        {
            _loudness = Observer.LoudnessValue;

            if (Observer.IsRespawning && !_dead)
            {
                _dead = true;
                AudioSourceExtensions.PlayClip2D(_deathSound, .5f);
            }

            if (!Observer.IsRespawning)
            {
                _dead = false;
            }

            if (Observer.CurrentSilenceSphere != null)
            {
                _zero.TransitionTo(_transitionZeroIn);
            }

            else if (_loudness > 0f && _loudness < _transitionPointTwo)
            {
                _phase1.TransitionTo(_transitionOneIn);
            }


            if (SceneSettings.Instance.PlayerCanDie)
            {
                _audioMixer.SetFloat("phase2Vol", Mathf.Log10((_loudness - .5f) / (1f - .5f)) * 20);
            }
        }
    }
}