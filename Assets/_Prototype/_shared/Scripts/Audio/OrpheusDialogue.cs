﻿using UnityEngine;

public class OrpheusDialogue : MonoBehaviour
{
    public AudioSource OrpheusAudioSource; 
    [SerializeField] private AudioClip[] _audioClips;
    

    private int _counter;

    void Start()
    {
        OrpheusAudioSource = GetComponent<AudioSource>();
        
    }

  
    /*
    void ContinueContinue()
    {
        _audioSource.PlayOneShot(_audioClips[_counter]);
        _counter++;
    }
    */
    
    public void PlayOrpheusIndex(int index, float delay)
    {
     //   OrpheusAudioSource.PlayOneShot(_audioClips[index]);
     OrpheusAudioSource.clip = _audioClips[index];
     OrpheusAudioSource.PlayDelayed(delay);
    }
}
