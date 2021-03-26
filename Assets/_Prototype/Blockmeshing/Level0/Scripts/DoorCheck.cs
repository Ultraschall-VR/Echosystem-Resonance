using UnityEngine;

namespace Echosystem.Resonance.Prototyping
{
    public class DoorCheck : MonoBehaviour
    {
        [SerializeField] private LevelLock _levelUiPad;
        private Animator _animator;
        private bool _animationTriggered;

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
            }
        }
    }
}