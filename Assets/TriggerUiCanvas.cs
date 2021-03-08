using System;
using Echosystem.Resonance.Prototyping;
using Echosystem.Resonance.UI;
using UnityEngine;

public class TriggerUiCanvas : MonoBehaviour
{
    private UiManager _uiManager;

    private void Start()
    {
        _uiManager = FindObjectOfType<UiManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Observer.Player)
        {
            _uiManager.LoadCanvas(_uiManager.Index+1);
        }
    }
}
