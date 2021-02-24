using UnityEngine;
using UnityEngine.Audio;


public class AudioFadeInFrom : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] string exposedParameter;
    [SerializeField] float duration = 10, targetVolume = 1, fromVolume = 0, delay = 0;
    
    void Start()
    {
        audioMixer.SetFloat(exposedParameter,-80f);
        StartCoroutine(FadeMixerGroupFrom.StartFadeFrom(audioMixer, exposedParameter, duration, fromVolume,
            targetVolume, delay));
    }
}