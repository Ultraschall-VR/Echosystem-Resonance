using Echosystem.Resonance.Game;
using TMPro;
using UnityEngine;
using Slider = UnityEngine.UI.Slider;

namespace Echosystem.Resonance.UI
{
    public class ObjectInteraction : MonoBehaviour
    {
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private Slider _slider;
        [SerializeField] private TextMeshProUGUI _text;

        public VRInteraction _selectedInteraction;

        private void CalculateRaycast()
        {
            if (_selectedInteraction != null)
            {
                _selectedInteraction.DecreasePower();
            }

            RaycastHit hit;
            if (Physics.Raycast(_playerInput.ControllerRight.transform.position,
                _playerInput.ControllerRight.transform.forward, out hit,
                5f))
            {
                if (hit.transform.gameObject.GetComponent<VRInteraction>())
                {
                    _rectTransform.gameObject.SetActive(true);

                    if (_selectedInteraction == null)
                    {
                        _selectedInteraction = hit.transform.gameObject.GetComponent<VRInteraction>();
                    }

                    _text.text = _selectedInteraction.gameObject.name;

                    if (_playerInput.RightTriggerPressed.state)
                    {
                        _selectedInteraction.IncreasePower();
                    }
                    else
                    {
                        _selectedInteraction.DecreasePower();
                    }
                }
                else
                {
                    _rectTransform.gameObject.SetActive(false);
                    _selectedInteraction = null;
                }
            }
        }

        private void Update()
        {
            CalculateRaycast();
            _rectTransform.position = _playerInput.ControllerRight.transform.position;
            _rectTransform.LookAt(_playerInput.Head.transform.position);

            if (_selectedInteraction != null)
            {
                _slider.value = _selectedInteraction.Power;
            }
        }

        private void Start()
        {
            _rectTransform.gameObject.SetActive(false);
        }
    }
}