using UnityEngine;

namespace Echosystem.Resonance.UI
{
    public class ShowToolTipp : MonoBehaviour
    {
        [SerializeField] private ToolTipps.Tooltip _tooltip;
        private ToolTipps _toolTipps;

        private void OnEnable()
        {
            _toolTipps = FindObjectOfType<ToolTipps>();
            _toolTipps.LoadToolTip(_tooltip);
        }
    }
}