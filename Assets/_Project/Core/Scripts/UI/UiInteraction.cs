﻿using Echosystem.Resonance.Prototyping;
using UnityEngine;

namespace Echosystem.Resonance.UI
{
    public class UiInteraction : MonoBehaviour
    {
        [SerializeField] private GameObject _cursor;
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private Transform _hand;
        private void Update()
        {
            if (Observer.FocusedGameObject == null)
                return;

            if (Observer.FocusedGameObject.layer == 20)
            {
                float distance = Vector3.Distance(transform.position, Observer.FocusedGameObject.transform.position);

                if (distance > 5)
                    return;
                
                _cursor.SetActive(true);
                _cursor.transform.position = Observer.FocusedPoint;
                _cursor.transform.forward = Observer.FocusedGameObject.transform.forward;

                _lineRenderer.enabled = true;
                _lineRenderer.SetPosition(0, _hand.position);
                _lineRenderer.SetPosition(1, Observer.FocusedPoint);

                InteractibleUI interactibleUi = null;
                
                if (Observer.FocusedGameObject.GetComponent<InteractibleUI>())
                {
                    interactibleUi = Observer.FocusedGameObject.GetComponent<InteractibleUI>();
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