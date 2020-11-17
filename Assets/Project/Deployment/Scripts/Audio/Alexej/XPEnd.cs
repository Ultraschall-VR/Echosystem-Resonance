using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class XPEnd : MonoBehaviour {
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] string exposedParameter;
    [SerializeField] float duration = 10, targetVolume = 0, fromVolume = 1, delay = 0;

    [SerializeField] AudioSource[] _audiosources;

    private bool audioStarted = false;

    // Plays every AudioSource in Array on Trigger
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player") & !audioStarted) {
            foreach (AudioSource i in _audiosources) {
                StartCoroutine(FadeMixerGroupFrom.StartFadeFrom(audioMixer, exposedParameter, duration, fromVolume, targetVolume, delay));
                StartCoroutine(PlayAudio(i));
            }
            // Sets audioStarted = true, so Player can't trigger it again
            audioStarted = true;
        }
    }

    private IEnumerator PlayAudio(AudioSource _audio) {
        yield return new WaitForSeconds(duration);
        _audio.Play();
    }
}