using Echosystem.Resonance.Prototyping;
using Echosystem.Resonance.UI;
using UnityEngine;

public class TriggerUiCanvas : MonoBehaviour
{
    private UiManager _uiManager;

    private bool _triggered = false;

    private void Start()
    {
        _uiManager = FindObjectOfType<UiManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Observer.Player && !_triggered)
        {
            _triggered = true;
            _uiManager.LoadCanvas(_uiManager.Index+1);
        }
    }
}
