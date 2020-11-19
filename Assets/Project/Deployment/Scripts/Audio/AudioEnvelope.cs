using System.Collections;
using Echosystem.Resonance.Game;
using UnityEngine;

namespace Echosystem.Resonance.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioEnvelope : MonoBehaviour
    {
        private AudioSource _audioSource;
        private float _targetVol;
        
        [Header("1 Unit = 1 Millisecond")]
        [SerializeField] private float _attack;
        [SerializeField] private float _release;

        private bool _breakRelease;
        private bool _breakAttack;

        [SerializeField] private bool _debug;
        [SerializeField] private KeyCode _debugKey;
        
        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _targetVol = _audioSource.volume;
            _audioSource.volume = 0.0f;
        }

        private void Update()
        {
            if (_debug)
            {
                if (Input.GetKey(_debugKey))
                {
                    Attack();
                }

                else if(Input.GetKeyUp(_debugKey))
                {
                    Release();
                }
            }
        }

        public void Attack()
        {
            if (!_audioSource.isPlaying)
            {
                StartCoroutine(LerpAttack(_attack));
            }
        }

        public void Release()
        {
            if (_audioSource.isPlaying)
            {
                StartCoroutine(LerpRelease(_release));
            }
        }

        private IEnumerator LerpAttack(float milliseconds)
        {
            float t = 0.0f;
            
            float currentVol = _audioSource.volume;
            float randStartPos = Random.Range(0, _audioSource.clip.length);

            _audioSource.time = randStartPos;
            _audioSource.Play();

            milliseconds /= 1000;
            
            while (t < milliseconds)
            {
                t += Time.deltaTime;
                
                _audioSource.volume = Mathf.Lerp(currentVol, _targetVol, t/milliseconds);
                yield return null;
            }
        }

        private IEnumerator LerpRelease(float milliseconds)
        {
            float t = 0.0f;
            
            float currentVol = _audioSource.volume;
            
            milliseconds /= 1000;
            
            while (t < milliseconds)
            {
                t += Time.deltaTime;
                
                if (_audioSource.volume <= 0.05)
                {
                    _audioSource.Stop();
                }

                _audioSource.volume = Mathf.Lerp(currentVol, 0, t/milliseconds);
                yield return null;
            }
        }
    }
}