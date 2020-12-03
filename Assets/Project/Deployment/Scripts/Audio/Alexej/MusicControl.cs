using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicControl : MonoBehaviour {
    public AudioMixerSnapshot _noMusic;
    public AudioMixerSnapshot _music1;
    public AudioMixerSnapshot _music2;

    // public AudioClip[] stings;
    // public AudioSource stingSource;
    //public float bpm = 128;
    [SerializeField] float _TransitionIn = 2000;
    [SerializeField] float _TransitionOut = 2000;

    //private float m_QuarterNote;

    // Use this for initialization
    void Start() {
       /* m_QuarterNote = 60 / bpm;
        m_TransitionIn = m_QuarterNote;
        m_TransitionOut = m_QuarterNote * 32;
       */

    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("SoundZone1")) {
            _music1.TransitionTo(_TransitionIn);
           // PlaySting();
        }
        if (other.CompareTag("SoundZone2")) {
            _music2.TransitionTo(_TransitionIn);
            // PlaySting();
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.CompareTag("SoundZone1")) {
            _noMusic.TransitionTo(_TransitionOut);
        }
        if (other.CompareTag("SoundZone2")) {
            _noMusic.TransitionTo(_TransitionOut);
        }
    }

    /*
    void PlaySting() {
        int randClip = Random.Range(0, stings.Length);
        stingSource.clip = stings[randClip];
        stingSource.Play();
    }
    */
}