using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class AudioLoopDelay : MonoBehaviour {
    [SerializeField] float delay = 0;
    AudioSource _audio;

    // Start is called before the first frame update
    void Start() {
        _audio = GetComponent<AudioSource>();
    //    SoundLoop();
    }

    
}
