using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioDelay : MonoBehaviour
{
    public float DelayTime=0;
    AudioSource audioData;

    void Start() {
        audioData = GetComponent<AudioSource>();
        audioData.PlayDelayed(DelayTime);
    }
}
