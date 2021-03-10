using System;
using UnityEngine;

namespace Echosystem.Resonance.Prototyping
{
    public class PlayAnimation : MonoBehaviour
    {
        private Animator _animator;
        private void Start()
        {
            _animator = GetComponent<Animator>();
            _animator.SetBool("Condition", true);
        }
    }
}