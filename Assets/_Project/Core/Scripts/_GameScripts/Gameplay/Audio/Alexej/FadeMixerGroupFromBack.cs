using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public static class FadeMixerGroupFromBack
{

    public static IEnumerator StartFadeFrom(AudioMixer audioMixer, string exposedParam, float duration, float fromVolume, float targetVolume) {


        float currentTime = 0;

        float targetValue = Mathf.Clamp(targetVolume, 0.0001f, 1);

        while (currentTime < duration) {
            audioMixer.SetFloat(exposedParam, fromVolume);
            currentTime += Time.deltaTime;
            float newVol = Mathf.Lerp(fromVolume, targetValue, currentTime / duration);
            audioMixer.SetFloat(exposedParam, Mathf.Log10(newVol) * 20);
            yield return null;
        }



        yield break;
    }
}