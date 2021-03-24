using System;
using Echosystem.Resonance.Prototyping;
using UnityEngine;

public class PitchShifter : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private Transform _tip;
    private Vector3 _lastPos;

    public bool _lockedTarget;
    public GameObject _focusedObject;
    void Update()
    {

        if (!SceneSettings.Instance.PitchShifter)
        {
            gameObject.SetActive(false);
            return;
        }
        
        if(!SceneSettings.Instance.VREnabled)
            NonVrInput();
        else
            VrInput();
        
    }

    private void VrInput()
    {
        if (Observer.PlayerInput.RightTriggerPressed.stateDown)
        {
            _lastPos = transform.position;
        }
        
        HandleFocusVr();
        
        
        
        if (_focusedObject != null)
        {
            if (_focusedObject.GetComponent<Pillar>())
            {
                var pillar = _focusedObject.GetComponent<Pillar>();
                
                var delta = transform.position - _lastPos;

                _lastPos = transform.position;
                
                Debug.Log(delta.y);
                
                _lineRenderer.SetPosition(0, _tip.position);
                _lineRenderer.SetPosition(1, pillar.GetComponent<PitchShifterable>().Grip.position);

                pillar.Pitch += delta.y;
            }
        }
    }

    private void HandleFocusVr()
    {
        if (Observer.FocusedGameObject == null)
        {
            _lineRenderer.enabled = false;
            DeactivateLockVr();
            return;
        }
        
        if (Observer.FocusedGameObject.GetComponent<PitchShifterable>())
        {
            if(!Observer.FocusedGameObject.GetComponent<PitchShifterable>().Active)
                return;
            
            if (!_lockedTarget)
            {
                var pitchShifterable = Observer.FocusedGameObject.GetComponent<PitchShifterable>();
                
                _lineRenderer.enabled = true;
                _lineRenderer.SetPosition(0, _tip.position);
                _lineRenderer.SetPosition(1, pitchShifterable.Grip.position);

                if (Observer.PlayerInput.RightTriggerPressed.state)
                {
                    _lockedTarget = true;
                    _focusedObject = Observer.FocusedGameObject;
                }
            }
        }

        if (_focusedObject != null)
        {
            if (Observer.PlayerInput.RightTriggerPressed.stateUp)
            {
                if (_focusedObject.GetComponent<Pillar>())
                {
                    var pillar = _focusedObject.GetComponent<Pillar>();
                    pillar.CheckPitch();
                    DeactivateLockVr();
                }
            }
        }
    }

    private void NonVrInput()
    {
        HandleTransformNonVr();
        HandleFocusNonVr();
        
        if (_focusedObject != null)
        {
            if (_focusedObject.GetComponent<Pillar>())
            {
                var pillar = _focusedObject.GetComponent<Pillar>();

                var scrollWheelDelta = Input.mouseScrollDelta.y;
                
                _lineRenderer.SetPosition(0, _tip.position);
                _lineRenderer.SetPosition(1, pillar.GetComponent<PitchShifterable>().Grip.position);

                if (scrollWheelDelta < 0)
                {
                    pillar.Pitch -= 0.05f;
                }

                if (scrollWheelDelta > 0)
                {
                    pillar.Pitch += 0.05f;
                }
            }
        }
    }

    private void HandleTransformNonVr()
    {
        transform.position = Observer.PlayerHead.transform.position;
        transform.eulerAngles = Observer.PlayerHead.transform.eulerAngles;
    }

    private void HandleFocusNonVr()
    {
        if (Observer.FocusedGameObject == null)
        {
            _lineRenderer.enabled = false;
            DeactivateLockNonVr();
            return;
        }
        
        if (Observer.FocusedGameObject.GetComponent<PitchShifterable>())
        {
            if(!Observer.FocusedGameObject.GetComponent<PitchShifterable>().Active)
                return;
            
            if (!_lockedTarget)
            {
                var pitchShifterable = Observer.FocusedGameObject.GetComponent<PitchShifterable>();
                
                _lineRenderer.enabled = true;
                _lineRenderer.SetPosition(0, _tip.position);
                _lineRenderer.SetPosition(1, pitchShifterable.Grip.position);

                if (Input.GetMouseButton(0))
                {
                    _lockedTarget = true;
                    _focusedObject = Observer.FocusedGameObject;
                }
            }
        }

        if (_focusedObject != null)
        {
            if (Input.GetMouseButtonUp(0))
            {
                if (_focusedObject.GetComponent<Pillar>())
                {
                    var pillar = _focusedObject.GetComponent<Pillar>();
                    pillar.CheckPitch();
                    DeactivateLockNonVr();
                }
            }
        }
    }

    private void DeactivateLockNonVr()
    {
        if (!Input.GetMouseButton(0))
        {
            _lineRenderer.enabled = false;
            _lockedTarget = false;
            _focusedObject = null;
        }
    }

    private void DeactivateLockVr()
    {
        if (!Observer.PlayerInput.RightTriggerPressed.state)
        {
            _lineRenderer.enabled = false;
            _lockedTarget = false;
            _focusedObject = null;
        }
    }
}