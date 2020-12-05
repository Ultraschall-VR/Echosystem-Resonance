using System;
using UnityEngine;

namespace Echosystem.Resonance.UI
{
    public class UIToolTipTrigger : MonoBehaviour
    {
        [SerializeField] private ToolTipps.Tooltip _tooltip;
        private ToolTipps _toolTipps;

        private bool _isTriggered;

        private void Start()
        {
            Invoke("Initialize", 2f);
        }

        private void Initialize()
        {
            _toolTipps = FindObjectOfType<ToolTipps>();
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && !_isTriggered)
            {
                _isTriggered = true;
                _toolTipps.ShowToolTipp(_tooltip);
            }
        }
    }
}