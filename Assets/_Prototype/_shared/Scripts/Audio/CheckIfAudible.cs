using DearVR;
using UnityEngine;


namespace Echosystem.Resonance.Prototyping
{
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(DearVRSource))]
    public class CheckIfAudible : MonoBehaviour
    {
        private AudioListener _audioListener;
        private AudioSource _audioSource;
        private DearVRSource _dearVRSource;
        private float _maxDistance;
        private float _distanceFromPlayer;

        void Start()
        {
            // Finds the Audio Listener and the Audio Source on the object
            _audioListener = Camera.main.GetComponent<AudioListener>();
            _audioSource = gameObject.GetComponent<AudioSource>();
            _dearVRSource = GetComponent<DearVRSource>();

            if (_dearVRSource.UseUnityDistance)
                _maxDistance = _audioSource.maxDistance;
            else
                _maxDistance = 28;

            Debug.Log(_maxDistance);
        }

        void Update()
        {
            _distanceFromPlayer = Vector3.Distance(transform.position, _audioListener.transform.position);

            if (_distanceFromPlayer <= _maxDistance)
            {
                ToggleAudioSource(true);
            }
            else
            {
                ToggleAudioSource(false);
            }
        }

        private void ToggleAudioSource(bool isAudible)
        {
            if (!isAudible && _audioSource.isPlaying)
            {
                _audioSource.Pause();
            }
            else if (isAudible && !_audioSource.isPlaying)
            {
                _audioSource.Play();
            }
        }
    }
}