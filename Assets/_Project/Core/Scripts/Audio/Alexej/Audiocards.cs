using Echosystem.Resonance.ObjectiveManagement;
using UnityEngine;

namespace Echosystem.Resonance.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class Audiocards : MonoBehaviour
    {
        [SerializeField] private GameObject _playButton;

        AudioSource _audioSource;

        private bool _hasBeenPlayed;

        private ObjectiveItem _objectiveItem;

        void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            
            if (GetComponent<ObjectiveItem>() != null)
            {
                _objectiveItem = GetComponent<ObjectiveItem>();
            }
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
                if (_objectiveItem != null)
                {
                    _objectiveItem.UpdateObjective();
                }

                _audioSource.Play();
                _hasBeenPlayed = true;
                HidePlayButton();
            }
        }
    }
}