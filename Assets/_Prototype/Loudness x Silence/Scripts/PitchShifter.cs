using Echosystem.Resonance.Prototyping;
using UnityEngine;

public class PitchShifter : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private Transform _tip;

    public bool _lockedTarget;
    public GameObject _focusedObject;

    void Update()
    {
        if (!SceneSettings.Instance.PitchShifter)
        {
            gameObject.SetActive(false);
            return;
        }
        
        if(SceneSettings.Instance.NonVr)
            NonVrInput();
        else
            VrInput();
        

        if (_focusedObject != null)
        {
            if (_focusedObject.GetComponent<Pillar>())
            {
                var pillar = _focusedObject.GetComponent<Pillar>();

                pillar.Pitch = Observer.Player.transform.eulerAngles.y/90;

            }
        }
    }

    private void VrInput()
    {
        // Implement
    }

    private void NonVrInput()
    {
        HandleTransformNonVr();
        HandleFocusNonVr();
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
            if (!_lockedTarget)
            {
                var pitchShifterable = Observer.FocusedGameObject.GetComponent<PitchShifterable>();
                
                _lineRenderer.enabled = true;
                _lineRenderer.SetPosition(0, _tip.position);
                _lineRenderer.SetPosition(1, pitchShifterable.Grip.position);
                
                if (Input.GetMouseButton(0))
                {
                    _lineRenderer.enabled = false;
                    _lockedTarget = true;
                    _focusedObject = Observer.FocusedGameObject;
                }
            }
        }
    }

    private void DeactivateLockNonVr()
    {
        if (!Input.GetMouseButton(0))
        {
            _lockedTarget = false;
            _focusedObject = null;
        }
    }
}