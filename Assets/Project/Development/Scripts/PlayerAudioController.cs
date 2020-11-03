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

    [Header("Bow")] 
    [SerializeField] private AudioAsset _bowLoop;
    [SerializeField] private AudioAsset _bowRelease;
    
    [Header("Player hands")] 
    [Header("OTHER")]
    [SerializeField] private AudioAsset _playerHandsLoop;
    [SerializeField] private AudioAsset _playerHandsTriangle;

}

[Serializable]public class AudioAsset
{
    public List<AudioClip> AudioClips;
    public bool SingleClip;
    public float Pitch;
    public float Volume;
    public AudioMixerGroup AudioMixerGroup;
}
