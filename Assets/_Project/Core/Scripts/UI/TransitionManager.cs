using UnityEngine;
using UnityEngine.UI;

namespace Echosystem.Resonance.UI
{
    public class TransitionManager : MonoBehaviour
    {
        [SerializeField] private Image _panel;
        [SerializeField] private Animator _animator;

        public float CurrentAnimationLength;

        public void FadeIn(Color color)
        {
            CurrentAnimationLength = _animator.GetCurrentAnimatorStateInfo(0).length;
            _panel.color = color;
            _animator.SetBool("FadeIn", true);
            _animator.SetBool("FadeOut", false);
        }

        public void FadeOut(Color color)
        {
            CurrentAnimationLength = _animator.GetCurrentAnimatorStateInfo(0).length;
            _panel.color = color;
            _animator.SetBool("FadeIn", false);
            _animator.SetBool("FadeOut", true);
        }
    }
}