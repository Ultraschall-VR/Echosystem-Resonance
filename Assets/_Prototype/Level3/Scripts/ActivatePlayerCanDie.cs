using System.Collections;
using System.Collections.Generic;
using Echosystem.Resonance.Prototyping;
using Echosystem.Resonance.UI;
using UnityEngine;

public class ActivatePlayerCanDie : MonoBehaviour
{
    private BeaconSocket _beaconSocket;
    private UiManager _uiManager;

    private bool _activated;

    void Start()
    {
        _beaconSocket = GetComponent<BeaconSocket>();
        SceneSettings.Instance.PlayerCanDie = false;
        _uiManager = FindObjectOfType<UiManager>();

    }

    // Update is called once per frame
    void Update()
    {
        
        if (_beaconSocket.IsOccupied && !_activated)
        {
            _activated = true;
            _uiManager.LoadCanvas(_uiManager.Index+1);
           // Observer.LoudnessValue = 0;
            SceneSettings.Instance.PlayerCanDie = true;
        }
    }
}
