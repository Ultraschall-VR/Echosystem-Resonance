using System.Collections.Generic;
using System.Linq;
using Echosystem.Resonance.Game;
using UnityEngine;

public class ObjectHighlighter : MonoBehaviour
{
    [SerializeField] private bool _debug;
    [SerializeField] private GrabCaster _grabCaster;
    public GameObject ActiveObject;

    void Update()
    {
        if (_debug)
        {
            DebugMode();
        }
        else
        {
            VRInput();
        }
    }

    private void VRInput()
    {
        RaycastHit hit;

        if (Physics.Raycast(PlayerInput.Instance.ControllerLeft.transform.position,
            PlayerInput.Instance.ControllerLeft.transform.forward, out hit))
        {
            HandleRaycast(hit);
        }
    }

    private void DebugMode()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            HandleRaycast(hit);
        }
    }

    private void HandleRaycast(RaycastHit hit)
    {
        if (hit.collider.GetComponent<VRInteractable>())
        {
            if (hit.collider.gameObject == ActiveObject)
            {
                return;
            }

            ActiveObject = hit.collider.gameObject;
            ActiveObject.GetComponent<VRInteractable>().IsActive = true;
            _grabCaster.ShowCast(transform.position, ActiveObject.transform.position);
        }
        else
        {
            if (ActiveObject != null)
            {
                ActiveObject.GetComponent<VRInteractable>().IsActive = false;
                ActiveObject = null;
                _grabCaster.Hide();
            }
        }
    }
}