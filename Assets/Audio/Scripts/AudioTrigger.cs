using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour {
    public AudioSource[] _audiosources;

    // Plays every AudioSource in Array on Trigger
    void OnTriggerEnter(Collider other) {
        foreach (AudioSource i in _audiosources) {
            i.Play();
        } 
    }
}
