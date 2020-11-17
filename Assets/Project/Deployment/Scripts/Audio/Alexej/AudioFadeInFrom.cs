using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;



public class AudioFadeInFrom : MonoBehaviour {
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] string exposedParameter;
    [SerializeField] float duration = 10, targetVolume = 1, fromVolume = 0, delay = 0;

    // Start is called before the first frame update
    void Start() {
        StartCoroutine(FadeMixerGroupFrom.StartFadeFrom(audioMixer, exposedParameter, duration, fromVolume, targetVolume, delay));
    }

    // Update is called once per frame
    void Update() {

    }
}
