using UnityEngine;

namespace Echosystem.Resonance.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class Audiocards : MonoBehaviour
    {
        [SerializeField] private GameObject _playButton;

        AudioSource _audioSource;

        private bool _hasBeenPlayed;

        void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void ShowPlayButton()
        {
            _playButton.SetActive(true);
        }

        public void HidePlayButton()
        {
            _playButton.SetActive(false);
        }

        public void PlayAudioCard()
        {
            if (!_audioSource.isPlaying && !_hasBeenPlayed)
            {
                _audioSource.Play();
                _hasBeenPlayed = true;
                HidePlayButton();
            }
        }
    }
}