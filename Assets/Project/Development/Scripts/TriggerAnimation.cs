using UnityEngine;

namespace Echosystem.Resonance.Game
{
    public class TriggerAnimation : MonoBehaviour
    {
        private Animator _animator;

        void Start()
        {
            _animator = gameObject.GetComponent<Animator>();

            _animator.SetBool("triggered", true);
        }
    }
}