using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;



public class AudioFadeIn : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] string exposedParameter;
    [SerializeField] float duration = 10, targetVolume = 1;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeMixerGroup.StartFade(audioMixer, exposedParameter, duration, targetVolume));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
