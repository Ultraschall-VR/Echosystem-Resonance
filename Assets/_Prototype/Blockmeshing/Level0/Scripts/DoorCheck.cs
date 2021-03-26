using UnityEngine;

namespace Echosystem.Resonance.Prototyping
{
    public class DoorCheck : MonoBehaviour
    {
        [SerializeField] private LevelLock _levelUiPad;
        private Animator _animator;
        private bool _animationTriggered;
        [SerializeField] private AudioClip _doorSound;

        private void Start()
        {
            _animator = GetComponentInChildren<Animator>();
        }

        void Update()
        {
            if (!_animationTriggered && _levelUiPad._unlocked)
            {
                _animationTriggered = true;
                _animator.SetBool("Condition", true);
                AudioSource.PlayClipAtPoint(_doorSound, transform.position);
            }
        }
    }
}