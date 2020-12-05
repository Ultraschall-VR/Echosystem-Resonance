using UnityEngine;

namespace Echosystem.Resonance.UI
{
    public class ToolTipps : MonoBehaviour
    {
        [SerializeField] private GameObject _teleport;
        [SerializeField] private GameObject _uncover;
        [SerializeField] private GameObject _triggerRight;
        [SerializeField] private GameObject _triggerLeft;
        [SerializeField] private GameObject _echoPull;
        [SerializeField] private GameObject _gravityPull;
        [SerializeField] private GameObject _echoBlaster;
        
        
        public enum Tooltip
        {
            Teleport,
            Uncover,
            TriggerRight,
            TriggerLeft,
            EchoPull,
            GravityPull,
            EchoBlaster
        }

        public void ShowToolTipp(Tooltip tooltip)
        {
            
        }
    }
}