using System;
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

        [SerializeField] private GameObject _textBox;

        [SerializeField] private int _lifeTime;
        
        private void Start()
        {
            DeactivateAll();
        }
        
        public void ShowToolTipp(Tooltip tooltip)
        {
            DeactivateAll();

            switch (tooltip)
            {
                case Tooltip.Teleport:
                    
                    _teleport.SetActive(true);
                    break;
                
                case Tooltip.Uncover:

                    _uncover.SetActive(true);
                    break;
                
                case  Tooltip.TriggerRight:

                    _triggerRight.SetActive(true);
                    break;
                
                case Tooltip.TriggerLeft:

                    _triggerLeft.SetActive(true);
                    break;
                
                case Tooltip.EchoPull:

                    _echoPull.SetActive(true);
                    break;
                
                case Tooltip.GravityPull:

                    _gravityPull.SetActive(true);
                    break;
                
                case Tooltip.EchoBlaster:

                    _echoBlaster.SetActive(true);
                    break;
            }
            
            _textBox.SetActive(true);
            
            Invoke("DeactivateAll", _lifeTime);
        }

        private void DeactivateAll()
        {
            _textBox.SetActive(false);
            _teleport.SetActive(false);
            _uncover.SetActive(false);
            _triggerLeft.SetActive(false);
            _triggerRight.SetActive(false);
            _echoPull.SetActive(false);
            _gravityPull.SetActive(false);
            _echoBlaster.SetActive(false);
        }
        
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
    }
}