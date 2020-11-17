using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLight : MonoBehaviour
{
    // [SerializeField] private AudioSource _audiosource;
    // [SerializeField] private Light _light;

    AudioSource _audioSource;
    Light _light;

    // AUDIO--------------
    public float updateStep = 0.1f;
    public int sampleDataLength = 1024;

    private float currentUpdateTime = 0f;

    public float clipLoudness;
    private float[] clipSampleData;

    public float sizeFactor = 500;
    //----------------

    // LIGHT---------------
    private float _newIntensity;
    public float smooth = 2.0f; 

   // public float minSize = 0;
   // public float maxSize = 500;

    // Use this for initialization
    private void Awake() {
        clipSampleData = new float[sampleDataLength];
        _newIntensity = _light.intensity;
    }

    void Start() {
        _light = GetComponent<Light>();
        _audioSource = GetComponent<AudioSource>();
    }

    //private float _volume;
    //private float _intensity;

    // Update is called once per frame
    private void Update() {
        currentUpdateTime += Time.deltaTime;
        if (currentUpdateTime >= updateStep) {
            currentUpdateTime = 0f;
            _audioSource.clip.GetData(clipSampleData, _audioSource.timeSamples); //I read 1024 samples, which is about 80 ms on a 44khz stereo clip, beginning at the current sample position of the clip.
            clipLoudness = 0f;
            foreach (var sample in clipSampleData) {
                clipLoudness += Mathf.Abs(sample);
            }
            clipLoudness /= sampleDataLength; //clipLoudness is what you are looking for

            clipLoudness *= sizeFactor;
            //  clipLoudness = Mathf.Clamp(clipLoudness, minSize, maxSize);
            clipLoudness = _newIntensity;
            //cube.transform.localScale = new Vector3(clipLoudness, clipLoudness, clipLoudness);
            _light.intensity = Mathf.Lerp(_light.intensity, _newIntensity, Time.deltaTime * smooth);
        }
    }
}
