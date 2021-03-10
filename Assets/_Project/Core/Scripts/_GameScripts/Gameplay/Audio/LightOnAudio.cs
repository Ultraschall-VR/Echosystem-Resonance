using UnityEngine;

[RequireComponent (typeof (Light))  ]
public class LightOnAudio : MonoBehaviour
{
    public AudioPeer _audioPeer;
    [Range(0,8)]
    public int _band;
    public float _minIntensity, _maxIntesity;
    Light _light;

    // Start is called before the first frame update
    void Start()
    {
        _audioPeer = GetComponent<AudioPeer>();
        _light = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        _light.intensity = (_audioPeer._audioBandBuffer[_band] * (_maxIntesity - _minIntensity)) + _minIntensity;
    }
}
