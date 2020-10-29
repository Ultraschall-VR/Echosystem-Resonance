using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour {
    [SerializeField] AudioSource[] _audiosources;

    private bool audioStarted = false;

    // Plays every AudioSource in Array on Trigger
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player") & !audioStarted) {
                foreach (AudioSource i in _audiosources) {
                    i.Play();
                }
            // Sets audioStarted = true, so Player can't trigger it again
            audioStarted = true;
        }
    }
}
