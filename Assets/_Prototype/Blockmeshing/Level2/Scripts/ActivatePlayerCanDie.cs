using System.Collections;
using System.Collections.Generic;
using Echosystem.Resonance.Prototyping;
using UnityEngine;

public class ActivatePlayerCanDie : MonoBehaviour
{
    private BeaconSocket _beaconSocket;

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
           // Observer.LoudnessValue = 0;
            SceneSettings.Instance.PlayerCanDie = true;
        }
    }
}
