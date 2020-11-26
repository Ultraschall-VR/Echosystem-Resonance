using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent (typeof (AudioSource))]
public class AudioPeer : MonoBehaviour
{
    AudioSource _audioSource;

    // Microphone input
    public AudioClip _audioClip;
    public bool _useMicrophone;
    public string _selectedDevice;
    public AudioMixerGroup _mixerGroupMicrophone, _mixerGroupAudioclip;

    // FFT values
    private float[] _samplesLeft = new float[512];
    private float[] _samplesRight = new float[512];

    private float[] _freqBand = new float[8];
    private float[] _bandBuffer = new float[8];
    private float[] _bufferDecrease = new float[8];
    private float[] _freqBandHighest = new float[8];
    // audio 64
    private float[] _freqBand64 = new float[64];
    private float[] _bandBuffer64 = new float[64];
    private float[] _bufferDecrease64 = new float[64];
    private float[] _freqBandHighest64 = new float[64];

    // Audio band values
    [HideInInspector]
    public float[] _audioBand, _audioBandBuffer;

    [HideInInspector]
    public float[] _audioBand64, _audioBandBuffer64;

    // Amplitude variables
    [HideInInspector]
    public float _Amplitude, _AmplitudeBuffer;
    private float _AmplitudeHighest;

    // Audio profile
    public float _audioProfile;

    // Stereo Channels
    public enum _channel {Stereo, Left, Right};
    public _channel channel = new _channel();


    // Start is called before the first frame update
    void Start()
    {
        _audioBand = new float[8];
        _audioBandBuffer = new float[8];
        _audioBand64 = new float[64];
        _audioBandBuffer64 = new float[64];
        _audioSource = GetComponent<AudioSource>();
        AudioProfile(_audioProfile);

        // Microphone Input

        if (_useMicrophone) {
            if (Microphone.devices.Length > 0) {
                _selectedDevice = Microphone.devices[0].ToString();
                _audioSource.outputAudioMixerGroup = _mixerGroupMicrophone;
                _audioSource.clip = Microphone.Start(_selectedDevice, true, 10, AudioSettings.outputSampleRate);
            }
            else {
                _useMicrophone = false;
            }
        }
        if (!_useMicrophone) {
            _audioSource.outputAudioMixerGroup = _mixerGroupAudioclip;
            _audioSource.clip = _audioClip;
        }

        _audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        GetSpecrumAudioSource();
        MakeFrequencyBands();
        MakeFrequencyBands64();
        BandBuffer();
        BandBuffer64();
        CreateAudioBands();
        CreateAudioBands64();
        GetAmplitude();
    }

    void AudioProfile(float audioProfile) {
        for (int i = 0; i < 8; i++) {
            _freqBandHighest[i] = audioProfile;

        }
    }

    void GetAmplitude() {
        float _CurrentAmplitude = 0;
        float _CurrentAmplitudeBuffer = 0;
        for (int i = 0; i < 8; i++) {
            _CurrentAmplitude += _audioBand[i];
            _CurrentAmplitudeBuffer += _audioBandBuffer[i];
        }
        if (_CurrentAmplitude > _AmplitudeHighest) {
            _AmplitudeHighest = _CurrentAmplitude;
        }
        _Amplitude = _CurrentAmplitude / _AmplitudeHighest;
        _AmplitudeBuffer = _CurrentAmplitudeBuffer / _AmplitudeHighest;
    }

    void CreateAudioBands() {
        for (int i = 0; i < 8; i++) {
            if (_freqBand[i] > _freqBandHighest[i]) {
                _freqBandHighest[i] = _freqBand[i];
            }
            _audioBand[i] = (_freqBand[i] / _freqBandHighest[i]);
            _audioBandBuffer[i] = (_bandBuffer[i] / _freqBandHighest[i]);
        }
    }

    void CreateAudioBands64() {
        for (int i = 0; i < 64; i++) {
            if (_freqBand64[i] > _freqBandHighest64[i]) {
                _freqBandHighest64[i] = _freqBand64[i];
            }
            _audioBand64[i] = (_freqBand64[i] / _freqBandHighest64[i]);
            _audioBandBuffer64[i] = (_bandBuffer64[i] / _freqBandHighest64[i]);
        }
    }

    void GetSpecrumAudioSource() {
        _audioSource.GetSpectrumData(_samplesLeft, 0, FFTWindow.Blackman);
        _audioSource.GetSpectrumData(_samplesRight, 1, FFTWindow.Blackman);
    }

    void BandBuffer() {
        for (int g = 0; g < 8; ++g) {
            if (_freqBand[g] > _bandBuffer[g]) {
                _bandBuffer[g] = _freqBand[g];
                _bufferDecrease[g] = 0.005f;
            }
            if (_freqBand[g] < _bandBuffer[g]) {
                _bandBuffer [g] -= _bufferDecrease[g];
                _bufferDecrease[g] *= 1.2f;

            }
        }
    }

    void BandBuffer64() {
        for (int g = 0; g < 64; ++g) {
            if (_freqBand64[g] > _bandBuffer64[g]) {
                _bandBuffer64[g] = _freqBand64[g];
                _bufferDecrease64[g] = 0.005f;
            }
            if (_freqBand64[g] < _bandBuffer64[g]) {
                _bandBuffer64[g] -= _bufferDecrease64[g];
                _bufferDecrease64[g] *= 1.2f;

            }
        }
    }

    void MakeFrequencyBands() {
        /*
         * 22050 / 512 = 43hertz per sample
         * 
         * 20 - 60 hertz
         * 60 - 250 hertz
         * 250 - 500 hertz
         * 500 - 2000 hertz
         * 2000 - 4000 hertz
         * 6000 - 20000 hertz
         * 
         * 0 - 2 = 86 hertz
         * 1 - 4 = 172 hertz - 87-258
         * 2 - 8 = 344 hertz - 259-602
         * 3 - 16 = 688 hertz - 603-1290
         * 4 - 32 = 1376 hertz - 1291-2666
         * 5 - 64 = 2752 hertz - 2667-5418
         * 6 - 128 = 5504 hertz - 5419-10922
         * 7 - 256 = 11008 hertz - 10923-21930
         * 510
         */

        int count = 0;

        for (int i = 0; i < 8; i++) {

            float average = 0;
            int sampleCount = (int)Mathf.Pow(2, i) * 2;

            if (i == 7) {
                sampleCount += 2;
            }
            for (int j = 0; j < sampleCount; j++) {
                if (channel == _channel.Stereo) { 
                average += (_samplesLeft[count] + _samplesRight[count]) * (count + 1);
                }
                if (channel == _channel.Left) {
                    average += _samplesLeft[count]* (count + 1);
                }
                if (channel == _channel.Right) {
                    average += _samplesRight[count] * (count + 1);
                }
                count++;
            } 

            average /= count;

            _freqBand[i] = average * 10;
        }

    }

    void MakeFrequencyBands64() {
        /*
         * 22050 / 512 = 43hertz per sample
         * 
         * 20 - 60 hertz
         * 60 - 250 hertz
         * 250 - 500 hertz
         * 500 - 2000 hertz
         * 2000 - 4000 hertz
         * 6000 - 20000 hertz
         * 
         * 0 - 2 = 86 hertz
         * 1 - 4 = 172 hertz - 87-258
         * 2 - 8 = 344 hertz - 259-602
         * 3 - 16 = 688 hertz - 603-1290
         * 4 - 32 = 1376 hertz - 1291-2666
         * 5 - 64 = 2752 hertz - 2667-5418
         * 6 - 128 = 5504 hertz - 5419-10922
         * 7 - 256 = 11008 hertz - 10923-21930
         * 510
         */

        int count = 0;
        int sampleCount = 1;
        int power = 0;

        for (int i = 0; i < 64; i++) {

            float average = 0;

            if (i == 16 || i == 32 || i == 40 || i == 48 || i == 56) {
                power++;
                sampleCount = (int)Mathf.Pow (2, power);
                if ( power ==3) {
                    sampleCount -= 2;
                }
            }
            for (int j = 0; j < sampleCount; j++) {
                if (channel == _channel.Stereo) {
                    average += (_samplesLeft[count] + _samplesRight[count]) * (count + 1);
                }
                if (channel == _channel.Left) {
                    average += _samplesLeft[count] * (count + 1);
                }
                if (channel == _channel.Right) {
                    average += _samplesRight[count] * (count + 1);
                }
                count++;
            }

            average /= count;

            _freqBand64[i] = average * 80;
        }

    }

}


