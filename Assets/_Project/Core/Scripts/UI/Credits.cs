using UnityEngine;

namespace Echosystem.Resonance.UI
{
    public class Credits : MonoBehaviour
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private Animator _animator;

        public void Show()
        {
            _transform.gameObject.SetActive(true);
            _animator.enabled = true;
        }
    }
}


