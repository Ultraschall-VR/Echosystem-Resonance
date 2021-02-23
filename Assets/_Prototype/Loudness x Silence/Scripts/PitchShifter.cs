using Echosystem.Resonance.Prototyping;
using UnityEngine;

public class PitchShifter : MonoBehaviour
{
    void Update()
    {
        transform.position = Observer.PlayerHead.transform.position;
        transform.eulerAngles = Observer.PlayerHead.transform.eulerAngles;
        
        
    }
}
