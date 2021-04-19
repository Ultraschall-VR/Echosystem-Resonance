using UnityEngine;

namespace Echosystem.Resonance.Prototyping
{
    public class AudioRandomPitch : MonoBehaviour
    {
        AudioSource _audioSource;
        [SerializeField] private bool _randomizeVolume;
        [SerializeField] float pitchMin = 1, pitchMax = 1, volumeMin = 1, volumeMax = 1;

        // Start is called before the first frame update
        void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            
            _audioSource.pitch = Random.Range(pitchMin, pitchMax);
            if (_randomizeVolume)
              _audioSource.volume = Random.Range(volumeMin, volumeMax);
        }
    }
}