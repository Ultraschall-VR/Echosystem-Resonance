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

        public void ShowToolTipp(Tooltip tooltip)
        {
            DeactivateAll();

            switch (tooltip)
            {
                case Tooltip.Teleport:

                    ShowContent(_teleport);
                    
                    break;

                case Tooltip.Uncover:

                    ShowContent(_uncover);
                    
                    break;

                case Tooltip.TriggerRight:

                    ShowContent(_triggerRight);
                    
                    break;

                case Tooltip.TriggerLeft:

                    ShowContent(_triggerLeft);
                    
                    break;

                case Tooltip.EchoPull:

                    ShowContent(_echoPull);
                    
                    break;

                case Tooltip.GravityPull:

                    ShowContent(_gravityPull);
                    
                    break;

                case Tooltip.EchoBlaster:

                    ShowContent(_echoBlaster);
                    
                    break;
            }
            
            Invoke("DeactivateAll", _lifeTime);
        }

        private void ShowContent(UIToolTipp toolTipp)
        {
            toolTipp.Show();

            _backGround.enabled = true;

            if (toolTipp.Text == null)
                return;

            _textBox.text = toolTipp.Text;
            _textBox.enabled = true;
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