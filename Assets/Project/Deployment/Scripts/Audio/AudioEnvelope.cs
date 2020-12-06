using System.Collections;
using UnityEngine;

namespace Echosystem.Resonance.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioEnvelope : MonoBehaviour
    {
        [HideInInspector] public AudioSource AudioSource;
        private float _targetVol;

        [Header("1 Unit = 1 Millisecond")] [SerializeField]
        private float _attack;

        [SerializeField] private float _release;

        private bool _breakRelease;
        private bool _breakAttack;

        [SerializeField] private bool _debug;
        [SerializeField] private KeyCode _debugKey;

        private void Awake()
        {
            AudioSource = GetComponent<AudioSource>();
            _targetVol = AudioSource.volume;
            AudioSource.volume = 0.0f;
        }

        private void Update()
        {
            if (_debug)
            {
                if (Input.GetKey(_debugKey))
                {
                    Attack();
                }

                else if (Input.GetKeyUp(_debugKey))
                {
                    Release();
                }
            }
        }

        public void Attack()
        {
            if (!AudioSource.loop)
            {
                AudioSource.volume = _targetVol;

                if (!AudioSource.isPlaying)
                {
                    AudioSource.PlayOneShot(AudioSource.clip);
                    StartCoroutine(LerpRelease(_release));
                }
                
                return;
            }

            if (!AudioSource.isPlaying)
            {
                StartCoroutine(LerpAttack(_attack));
            }
        }

        public void Release()
        {
            if (AudioSource.isPlaying)
            {
                StartCoroutine(LerpRelease(_release));
            }
        }

        private IEnumerator LerpAttack(float milliseconds)
        {
            float t = 0.0f;

            float currentVol = AudioSource.volume;
            float randStartPos = Random.Range(0, AudioSource.clip.length);

            AudioSource.time = randStartPos;
            AudioSource.Play();

            milliseconds /= 1000;

            while (t < milliseconds)
            {
                t += Time.deltaTime;

                AudioSource.volume = Mathf.Lerp(currentVol, _targetVol, t / milliseconds);
                yield return null;
            }
        }

        private IEnumerator LerpRelease(float milliseconds)
        {
            float t = 0.0f;

            float currentVol = AudioSource.volume;

            milliseconds /= 1000;

            while (t < milliseconds)
            {
                t += Time.deltaTime;

                if (AudioSource.volume <= 0.05)
                {
                    AudioSource.Stop();
                }

                AudioSource.volume = Mathf.Lerp(currentVol, 0, t / milliseconds);
                yield return null;
            }
        }
    }
}