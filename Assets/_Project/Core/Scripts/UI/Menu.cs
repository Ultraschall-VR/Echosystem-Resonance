using System.Collections.Generic;
using UnityEngine;

namespace Echosystem.Resonance.UI
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] private List<Collider> _colliders;
        [SerializeField] private CanvasGroup _canvasGroup;

        private void Start()
        {
            Hide();
        }

        public void Hide()
        {
            _canvasGroup.alpha = 0;
        }

        public void Show()
        {
            _canvasGroup.alpha = 1;
        }

        public void EnableColliders(bool enable)
        {
            if (_colliders.Count != 0)
            {
                foreach (var collider in _colliders)
                {
                    collider.enabled = enable;
                }
            }
        }
    }
}