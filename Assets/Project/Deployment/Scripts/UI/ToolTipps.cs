using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Echosystem.Resonance.UI
{
    public class ToolTipps : MonoBehaviour
    {
        [SerializeField] private UIToolTipp _teleport;
        [SerializeField] private UIToolTipp _uncover;
        [SerializeField] private UIToolTipp _triggerRight;
        [SerializeField] private UIToolTipp _triggerLeft;
        [SerializeField] private UIToolTipp _echoPull;
        [SerializeField] private UIToolTipp _gravityPull;
        [SerializeField] private UIToolTipp _echoBlaster;
        [SerializeField] private UIToolTipp _audioCards;

        [SerializeField] private TextMeshProUGUI _textBox;

        [SerializeField] private Image _backGround;
        

        [SerializeField] private int _lifeTime;

        private void Start()
        {
            DeactivateAll();
        }

        private void Update()
        {
            FixRotation();
        }

        private void FixRotation()
        {
            var rot = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
            transform.eulerAngles = rot;
        }

        public void ShowToolTipp(Tooltip tooltip, float delay)
        {
            DeactivateAll();

            switch (tooltip)
            {
                case Tooltip.Teleport:

                    ShowContent(_teleport, delay);
                    
                    break;

                case Tooltip.Uncover:

                    ShowContent(_uncover, delay);
                    
                    break;

                case Tooltip.TriggerRight:

                    ShowContent(_triggerRight, delay);
                    
                    break;

                case Tooltip.TriggerLeft:

                    ShowContent(_triggerLeft,delay);
                    
                    break;

                case Tooltip.EchoPull:

                    ShowContent(_echoPull,delay);
                    
                    break;

                case Tooltip.GravityPull:

                    ShowContent(_gravityPull,delay);
                    
                    break;

                case Tooltip.EchoBlaster:

                    ShowContent(_echoBlaster,delay);
                    
                    break;
                
                case Tooltip.AudioCards:
                    
                    ShowContent(_audioCards,delay);

                    break;
            }
            
            
        }

        private void ShowContent(UIToolTipp toolTipp, float delay)
        {
            StopAllCoroutines();
            StartCoroutine(ShowContentDelayed(toolTipp, delay));
        }

        private IEnumerator ShowContentDelayed(UIToolTipp toolTipp, float delay)
        {
            yield return new WaitForSeconds(delay);
            
            toolTipp.Show();

            _backGround.enabled = true;

            if (toolTipp.Text != null)
            {
                _textBox.text = toolTipp.Text;
                _textBox.enabled = true;
            }
            else
            {
                _textBox.text = null;
                _textBox.enabled = false;
            }
            
            Invoke("DeactivateAll", _lifeTime);
            
            yield return null;
        }

        public void HideAll()
        {
            DeactivateAll();
        }

        private void DeactivateAll()
        {
            _backGround.enabled = false;
            _textBox.enabled = false;
            _teleport.Hide();
            _uncover.Hide();
            _triggerLeft.Hide();
            _triggerRight.Hide();
            _echoPull.Hide();
            _gravityPull.Hide();
            _echoBlaster.Hide();
            _audioCards.Hide();
        }

        public enum Tooltip
        {
            Teleport,
            Uncover,
            TriggerRight,
            TriggerLeft,
            EchoPull,
            GravityPull,
            EchoBlaster,
            AudioCards
        }
    }
}