using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Light))  ]
public class LightOnAudio : MonoBehaviour
{
    public AudioPeer _audioPeer;
    public int _band;
    public float _minIntensity, _maxIntesity;
    Light _light;

    // Start is called before the first frame update
    void Start()
    {
        _light = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        _light.intensity = (_audioPeer._audioBandBuffer[_band] * (_maxIntesity - _minIntensity)) + _minIntensity;
    }
}
