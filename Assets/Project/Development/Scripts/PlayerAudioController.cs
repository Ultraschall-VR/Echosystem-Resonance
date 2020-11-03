using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerAudioController : MonoBehaviour
{
    [Header("Uncovering")]
    [Header("MECHANICS")]
    [SerializeField] private AudioAsset _uncoveringLoop;
    [SerializeField] private AudioAsset _uncoveringStart;
    [SerializeField] private AudioAsset _uncoveringRelease;

    [Header("Teleport")] 
    [SerializeField] private AudioAsset _teleportLoop;
    [SerializeField] private AudioAsset _teleportRelease;

    [Header("Gravity pull")] 
    [SerializeField] private AudioAsset _gravityPullSnapping;
    [SerializeField] private AudioAsset _gravityPullLoop;

    [Header("Sling Shot")] 
    [SerializeField] private AudioAsset _slingShotLoop;
}

[Serializable]public class AudioAsset
{
    public List<AudioClip> AudioClips;
    public bool SingleClip;
    public bool RandomizePitch;
    public float Attack;
    public float Release;
    public AudioMixerGroup AudioMixerGroup;
}
