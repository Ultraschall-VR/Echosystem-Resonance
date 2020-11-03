using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.Rendering.UI;
using Random = UnityEngine.Random;

public class AudioPlayer : MonoBehaviour
{
    private AudioAsset _audioAsset;

    [SerializeField] private AudioSource _audioSource;

    private AudioClip _clip;
    private float _pitch;
    private float _volume;
    private float _attack;
    private float _release;
    private AudioMixerGroup _audioMixerGroup;

    private bool _parametersSet = false;

    private bool _break;

    private void SetParameters(AudioAsset audioAsset)
    {
        _audioAsset = audioAsset;

        SetClip();
        SetVolume();
        SetPitch();
        SetEnvelope();
        SetAudioMixerGroup();

        _audioSource.clip = _clip;
        _audioSource.pitch = _pitch;
        _audioSource.volume = _volume;

        _parametersSet = true;
    }

    public void PlayAudio(AudioAsset audioAsset)
    {
        _break = false;
        
        if (!_parametersSet)
        {
            SetParameters(audioAsset);
        }

        if (!_audioSource.isPlaying)
            StartCoroutine(AttackEnvelope());
    }

    public void StopAudio()
    {
        _break = true;
        
        if (_audioSource.isPlaying)
            StartCoroutine(ReleaseEnvelope());
    }

    private IEnumerator AttackEnvelope()
    {
        _audioSource.volume = 0.0f;
        _audioSource.Play();

        while (_audioSource.volume <= _volume - 0.1f)
        {
            _audioSource.volume += Time.deltaTime / _attack;
            yield return null;
        }
    }

    private IEnumerator ReleaseEnvelope()
    {
        while (_audioSource.volume >= 0.1f)
        {
            _audioSource.volume -= Time.deltaTime / _release;

            if (_audioSource.volume <= 0.1)
            {
                _audioSource.Stop();
            }
            
            yield return _parametersSet = false;
        }
    }

    private void SetClip()
    {
        if (_audioAsset.SingleClip)
        {
            _clip = _audioAsset.AudioClips[0];
        }
        else
        {
            var rand = Random.Range(0, _audioAsset.AudioClips.Count);
            _clip = _audioAsset.AudioClips[rand];
        }
    }

    private void SetVolume()
    {
        _volume = _audioAsset.Volume;
    }

    private void SetPitch()
    {
        if (_audioAsset.RandomizePitch)
        {
            var rand = Random.Range(_audioAsset.PitchMin, _audioAsset.PitchMax);
            _pitch = rand;
        }
        else
        {
            _pitch = 1.0f;
        }
    }

    private void SetEnvelope()
    {
        _attack = _audioAsset.Attack;
        _release = _audioAsset.Release;
    }

    private void SetAudioMixerGroup()
    {
        _audioMixerGroup = _audioAsset.AudioMixerGroup;
    }
}