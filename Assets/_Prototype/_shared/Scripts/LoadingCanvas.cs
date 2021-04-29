using Echosystem.Resonance.Prototyping;
using UnityEngine;

public class LoadingCanvas : MonoBehaviour
{
    private bool _positionSet;
    
    private void Update()
    {
        if (Observer.Player == null && !_positionSet)
        {
            transform.rotation = Observer.PlayerHead.transform.rotation;
            _positionSet = true;
        }
    }
}
