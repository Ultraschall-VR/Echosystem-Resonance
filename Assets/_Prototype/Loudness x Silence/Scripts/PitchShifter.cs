using Echosystem.Resonance.Prototyping;
using UnityEngine;

public class PitchShifter : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private Transform _tip;
    
    void Update()
    {
        transform.position = Observer.PlayerHead.transform.position;
        transform.eulerAngles = Observer.PlayerHead.transform.eulerAngles;

        if (Observer.FocusedGameObject == null)
        {
            _lineRenderer.enabled = false;
            return;
        }
        
        if (Observer.FocusedGameObject.CompareTag("PitchShifterable"))
        {
            _lineRenderer.enabled = true;
            _lineRenderer.SetPosition(0, _tip.position);
            _lineRenderer.SetPosition(1, Observer.FocusedGameObject.transform.position);
        }
        else
        {
            _lineRenderer.enabled = false;
        }
    }
}
