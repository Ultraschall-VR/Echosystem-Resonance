using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioRandomClip : MonoBehaviour
{
    AudioSource randomSound;
    [SerializeField] private bool _randomDelay;
    [SerializeField] AudioClip[] audioSources;

    // Start is called before the first frame update
    void Start()
    {
        randomSound= GetComponent<AudioSource>();
        randomSound.clip = audioSources[Random.Range(0, audioSources.Length)];
        randomSound.Play();

        // CallAudio();
    }
/*
    void CallAudio() {
        Invoke("RandomSoundness", 0);
    }

    void RandomSoundness() {
        randomSound.clip = audioSources[Random.Range(0, audioSources.Length)];
        randomSound.Play();
        CallAudio();
    }
*/
}
