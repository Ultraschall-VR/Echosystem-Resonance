using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

[RequireComponent(typeof(AudioSource))]
public class AudioPlayer : MonoBehaviour
{
    private AudioAsset _audioAsset;

    private AudioSource _audioSource;

    private AudioClip _clip;
    private float _pitch;
    private float _volume;
    private float _attack;
    private float _release;
    private int _priority;
    private bool _spatialized;
    private float _spatialzedBlend;
    private AudioMixerGroup _audioMixerGroup;

    private bool _playedOnce;

    private bool _parametersSet = false;

    private bool _audioEnded = true;

    private bool _break;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (_audioEnded)
        {
            _parametersSet = false;
            _audioEnded = false;
        }
    }

    private void SetParameters(AudioAsset audioAsset)
    {
        if (_parametersSet)
            return;

        _audioAsset = audioAsset;

        SetClip();
        SetVolume();
        SetPitch();
        SetEnvelope();
        SetAudioMixerGroup();
        SetPriority();
        SetSpatialization();

        _audioSource.clip = _clip;
        _audioSource.pitch = _pitch;
        _audioSource.volume = _volume;
        _audioSource.priority = _priority;
        _audioSource.spatialize = _spatialized;
        _audioSource.spatialBlend = _spatialzedBlend;

        _parametersSet = true;
    }

    public void PlayAudio(AudioAsset audioAsset)
    {
        _break = false;

        if (!_parametersSet)
        {
            SetParameters(audioAsset);
        }

        if (audioAsset.SingleClip)
        {
            if (!_playedOnce)
            {
                if (!_audioSource.isPlaying)
                    StartCoroutine(AttackEnvelope());
                _playedOnce = true;
            }
        }
        else
        {
            if (!_audioSource.isPlaying)
                StartCoroutine(AttackEnvelope());
        }
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
        _audioEnded = false;

        while (_audioSource.volume <= _volume - 0.1f)
        {
            _audioSource.volume += Time.deltaTime / (_attack / 60);
            yield return null;
        }
    }

    private IEnumerator ReleaseEnvelope()
    {
        while (_audioSource.volume >= 0f)
        {
            _audioSource.volume -= Time.deltaTime / (_release / 60);

            if (_audioSource.volume <= 0.01f)
            {
                _audioSource.Stop();
            }

            yield return _audioEnded = true;
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

    private void SetPriority()
    {
        _priority = _audioAsset.Priority;
    }

    private void SetSpatialization()
    {
        _spatialized = _audioAsset.Spatialized;
        _spatialzedBlend = _audioAsset.SpatializeBlend;
    }
}