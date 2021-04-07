using Echosystem.Resonance.Game;
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
                origin = Observer.PlayerInput.ControllerRight.transform.position;
                forward = Observer.PlayerInput.ControllerRight.transform.forward;
            }
            else
            {
                origin = Observer.PlayerHead.transform.position;
                forward = Observer.PlayerHead.transform.forward;
            }
            
            if (Physics.Raycast(origin, forward, out hit, 6, _layerMask))
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
            
                
            //if(Observer.FocusedGameObject == null)
            //    return;
            //
            //if (Observer.FocusedGameObject.layer == 20 || Observer.FocusedGameObject.GetComponent<InteractibleUI>())
            //{
            //    float distance = Vector3.Distance(transform.position, Observer.FocusedGameObject.transform.position);
//
            //    if (distance > 6)
            //        return;
            //    
            //    _cursor.SetActive(true);
            //    _cursor.transform.position = Observer.FocusedPoint;
            //    _cursor.transform.forward = Observer.FocusedGameObject.transform.forward;
//
            //    _lineRenderer.enabled = true;
            //    _lineRenderer.SetPosition(0, _hand.position);
            //    _lineRenderer.SetPosition(1, Observer.FocusedPoint);
//
            //    InteractibleUI interactibleUi = null;
            //    
            //    if (Observer.FocusedGameObject.GetComponent<InteractibleUI>())
            //    {
            //        interactibleUi = Observer.FocusedGameObject.GetComponent<InteractibleUI>();
            //    }
//
            //    HandleInput(interactibleUi);
            //}
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