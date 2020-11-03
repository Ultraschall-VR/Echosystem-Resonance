﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerAudioController : MonoBehaviour
{
    [Header("Uncovering")]
    [Header("MECHANICS")]
    public AudioAsset UncoveringLoop;
    public AudioAsset UncoveringStart;
    public AudioAsset UncoveringRelease;

    [Header("Teleport")] 
    public AudioAsset TeleportLoop;
    public AudioAsset TeleportRelease;

    [Header("Gravity pull")] 
    public AudioAsset GravityPullSnapping;
    public AudioAsset GravityPullLoop;

    [Header("Sling Shot")] 
    public AudioAsset SlingShotLoop;
}

[Serializable]public class AudioAsset
{
    public List<AudioClip> AudioClips;
    public bool SingleClip;
    public float Volume;
    public bool RandomizePitch;
    public float PitchMin;
    public float PitchMax;
    public float Attack;
    public float Release;
    public AudioMixerGroup AudioMixerGroup;
}
