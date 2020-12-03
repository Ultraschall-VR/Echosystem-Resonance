using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Echosystem.Resonance.Game
{
    public class ObjectHighlighter : MonoBehaviour
    {
        [SerializeField] private bool _debug;
        [SerializeField] private GrabCaster _grabCaster;
        public GameObject ActiveObject;

        public bool Locked;

        private List<VRInteractable> _vrInteractables;

        private void Start()
        {
            _vrInteractables = FindObjectsOfType<VRInteractable>().ToList();
        }

        void Update()
        {
            if (_debug)
            {
                DebugMode();
            }

            VRInput();
        }

        private void SafeDisable(VRInteractable Exception)
        {
            if (ActiveObject != null)
            {
                foreach (var vrInteractable in _vrInteractables)
                {
                    if (vrInteractable != Exception)
                        vrInteractable.IsActive = false;
                }

                ActiveObject = null;
            }
        }

        private void VRInput()
        {
            RaycastHit hit;

            if (Physics.Raycast(PlayerInput.Instance.ControllerLeft.transform.position,
                PlayerInput.Instance.ControllerLeft.transform.forward, out hit))
            {
                HandleRaycast(hit, PlayerInput.Instance.ControllerLeft.transform.position);
            }
        }

        private void DebugMode()
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                HandleRaycast(hit, transform.position);
            }
        }

        private void HandleRaycast(RaycastHit hit, Vector3 origin)
        {
            if (hit.collider.GetComponent<VRInteractable>())
            {
                if (Locked)
                {
                    _grabCaster.Hide();
                    return;
                }

                if (hit.collider.gameObject == ActiveObject)
                {
                    return;
                }

                ActiveObject = hit.collider.gameObject;
                ActiveObject.GetComponent<VRInteractable>().IsActive = true;
                _grabCaster.ShowCast(origin, ActiveObject.transform.position);
            }

            else if (ActiveObject != null && !Locked)
            {
                ActiveObject.GetComponent<VRInteractable>().IsActive = false;
                SafeDisable(ActiveObject.GetComponent<VRInteractable>());

                _grabCaster.Hide();
            }
        }
    }
}