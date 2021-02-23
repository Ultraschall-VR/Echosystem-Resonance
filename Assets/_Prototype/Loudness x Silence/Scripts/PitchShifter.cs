using System.Collections;
using System.Collections.Generic;
using Echosystem.Resonance.Prototyping;
using UnityEngine;

public class PitchShifter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Observer.PlayerHead.transform.position;
        transform.eulerAngles = Observer.PlayerHead.transform.eulerAngles;
    }
}
