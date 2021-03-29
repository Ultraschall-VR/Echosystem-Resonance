using System;
using Echosystem.Resonance.Prototyping;
using UnityEngine;

public class PitchShifterable : MonoBehaviour
{
    public Transform Grip;
    [HideInInspector] public bool Active;

    private void Start()
    {
        Active = true;
    }

    private void Update()
    {
        if(Observer.Player == null) 
            return;
        
        Grip.LookAt(Observer.PlayerHead.transform);
    }
}
