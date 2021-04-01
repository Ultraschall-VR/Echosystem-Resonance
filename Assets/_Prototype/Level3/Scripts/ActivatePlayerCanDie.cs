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
    }

    // Update is called once per frame
    void Update()
    {
        if (_beaconSocket.IsOccupied && !_activated)
        {
            _activated = true;
            SceneSettings.Instance.PlayerCanDie = true;
        }
    }
}
