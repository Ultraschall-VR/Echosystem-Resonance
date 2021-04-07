using Echosystem.Resonance.Prototyping;
using UnityEngine;

namespace Echosystem.Resonance.UI
{
    public class UiInteraction : MonoBehaviour
    {
        [SerializeField] private GameObject _cursor;
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private Transform _hand;
        [SerializeField] private LayerMask _layerMask;
        private void Update()
        {
            if(Observer.Player == null)
                return;

            RaycastHit hit;
            Vector3 origin;
            Vector3 forward;

            if (SceneSettings.Instance.VREnabled)
            {
                origin = _hand.position;
                forward = _hand.forward;
            }
            else
            {
                origin = Observer.PlayerHead.transform.position;
                forward = Observer.PlayerHead.transform.forward;
            }
            
            if (Physics.Raycast(origin, forward, out hit, 4, _layerMask))
            {
                _cursor.SetActive(true);
                _cursor.transform.position = hit.point;
                _cursor.transform.forward = hit.transform.gameObject.transform.forward;

                _lineRenderer.enabled = true;
                _lineRenderer.SetPosition(0, _hand.position);
                _lineRenderer.SetPosition(1, hit.point);

                InteractibleUI interactibleUi = null;
                
                if (hit.transform.GetComponent<InteractibleUI>())
                {
                    interactibleUi = hit.transform.GetComponent<InteractibleUI>();
                }

                HandleInput(interactibleUi);
            }
            else
            {
                _lineRenderer.enabled = false;
                _cursor.SetActive(false);
            }
        }

        private static void HandleInput(InteractibleUI interactibleUi)
        {
            if (interactibleUi != null)
            {
                interactibleUi.Hover();
                interactibleUi.DeClick();
                
                if (SceneSettings.Instance.VREnabled)
                {
                    if (Observer.PlayerInput.RightTriggerPressed.stateDown)
                    {
                        interactibleUi.Click();
                    }
                }

                else
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        interactibleUi.Click();
                    }
                }
            }
        }
    }
}