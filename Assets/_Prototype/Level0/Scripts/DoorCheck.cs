using System.Collections;
using UnityEngine;

namespace Echosystem.Resonance.Prototyping
{
    public class DoorCheck : MonoBehaviour
    {
        [SerializeField] private LevelLock _levelUiPad;
        private Animator _animator;
        private bool _animationTriggered;
        private AudioSource  _audioSource;
        private float delay;
        
        [SerializeField] private AudioClip _doorSound;

        private void Start()
        {
            _animator = GetComponentInChildren<Animator>();
            _audioSource = GetComponent<AudioSource>();
            delay = Random.Range(0.1f, 2f);
            _audioSource.clip = _doorSound;
            _audioSource.pitch = Random.Range(.8f, 1.2f);
            _audioSource.volume = 0;

        }

        void Update()
        {
            if (!_animationTriggered && _levelUiPad._unlocked)
            {
               // float delay = Random.Range(0.1f, 2f);
             
             //  _audioSource.PlayDelayed(delay);
                
                Invoke("PlayDelayed", delay);
            }
        }

        private void PlayDelayed()
        {
            _animationTriggered = true;
            _animator.SetBool("Condition", true);
            _audioSource.Play();
            _audioSource.FadeIn(.2f,1);
            

        }
    }
}