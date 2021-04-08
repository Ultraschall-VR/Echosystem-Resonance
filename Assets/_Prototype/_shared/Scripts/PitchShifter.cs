using Echosystem.Resonance.Prototyping;
using UnityEngine;

public class PitchShifter : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private Transform _tip;
    [SerializeField] private Transform _geo;
    private Vector3 _lastPos;

    public bool _lockedTarget;
    public GameObject _focusedObject;

    private void Update()
    {
        if(!SceneSettings.Instance.VREnabled)
            HandleTransformNonVr();
    }

    void FixedUpdate()
    {
        if (!SceneSettings.Instance.PitchShifter)
        {
            gameObject.SetActive(false);
            return;
        }

        if (!SceneSettings.Instance.VREnabled)
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
        if (_focusedObject != null)
        {
            var pillar = _focusedObject.GetComponent<Pillar>();

            _lineRenderer.enabled = true;
            _lineRenderer.SetPosition(0, _tip.position);
            _lineRenderer.SetPosition(1, pillar.GetComponent<PitchShifterable>().Grip.position);

            if (Observer.PlayerInput.RightTriggerPressed.stateUp)
            {
                pillar.CheckPitch();
                _lineRenderer.enabled = false;

                if (Observer.FocusedGameObject != pillar)
                {
                    _focusedObject = null;
                }
            }
        }

        if (Observer.FocusedGameObject == null)
            return;

        if (Observer.FocusedGameObject.GetComponent<PitchShifterable>())
        {
            if (!Observer.FocusedGameObject.GetComponent<PitchShifterable>().Active)
                return;

            var focus = Observer.FocusedGameObject.GetComponent<PitchShifterable>();

            _lineRenderer.enabled = true;
            _lineRenderer.SetPosition(0, _tip.position);
            _lineRenderer.SetPosition(1, focus.Grip.position);

            if (Observer.PlayerInput.RightTriggerPressed.state)
            {
                _focusedObject = Observer.FocusedGameObject;
            }
        }
        else
        {
            _lineRenderer.enabled = false;
        }
    }

    private void NonVrInput()
    {
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
        _geo.position = Observer.PlayerHead.transform.position;
        _geo.eulerAngles = Observer.PlayerHead.transform.eulerAngles;
    }

    private void HandleFocusNonVr()
    {
        if (_focusedObject != null)
        {
            var pillar = _focusedObject.GetComponent<Pillar>();

            _lineRenderer.enabled = true;
            _lineRenderer.SetPosition(0, _tip.position);
            _lineRenderer.SetPosition(1, pillar.GetComponent<PitchShifterable>().Grip.position);

            if (Input.GetMouseButtonUp(0))
            {
                pillar.CheckPitch();
                _lineRenderer.enabled = false;

                if (Observer.FocusedGameObject != pillar)
                {
                    _focusedObject = null;
                }
            }
        }

        if (Observer.FocusedGameObject == null)
            return;

        if (Observer.FocusedGameObject.GetComponent<PitchShifterable>())
        {
            if (!Observer.FocusedGameObject.GetComponent<PitchShifterable>().Active)
                return;

            var focus = Observer.FocusedGameObject.GetComponent<PitchShifterable>();

            _lineRenderer.enabled = true;
            _lineRenderer.SetPosition(0, _tip.position);
            _lineRenderer.SetPosition(1, focus.Grip.position);

            if (Input.GetMouseButton(0))
            {
                _focusedObject = Observer.FocusedGameObject;
            }
        }
        else
        {
            _lineRenderer.enabled = false;
        }
    }
}