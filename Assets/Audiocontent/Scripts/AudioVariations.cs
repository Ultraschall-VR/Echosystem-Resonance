using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVariations : MonoBehaviour
{
    [SerializeField] AudioSource audioBase;
    [SerializeField] AudioClip[] clipArray;
    private int clipIndex;

    [SerializeField] float pitchMin = 1, pitchMax = 1, volumeMin = 1, volumeMax = 1;


    // Start is called before the first frame update
    void Start()
    {

    }

    void Update() {
        if (Input.GetButtonUp("Fire1")) PlayRoundRobin();
        if (Input.GetButtonUp("Fire2")) PlayRandom();
    }

    void PlayRoundRobin() {
        audioBase.pitch = Random.Range(pitchMin, pitchMax);
        audioBase.volume = Random.Range(volumeMin, volumeMax);

        if (clipIndex < clipArray.Length) {
            audioBase.PlayOneShot(clipArray[clipIndex]);
            clipIndex++;
        }

        else {
            clipIndex = 0;
            audioBase.PlayOneShot(clipArray[clipIndex]);
            clipIndex++;
        }
    }

    /* Random mit eventueller Wiederholung
    void PlayRandom() {
        clipIndex = Random.Range(0, clipArray.Length);
        audioBase.PlayOneShot(clipArray[clipIndex]);
    }
    */

    int RepeatCheck(int previousIndex, int range) {
        int index = Random.Range(0, range);

        while (index == previousIndex) {
            index = Random.Range(0, range);
        }
        return index;
    }

    void PlayRandom() {
        audioBase.pitch = Random.Range(pitchMin, pitchMax);
        audioBase.volume = Random.Range(volumeMin, volumeMax);

        clipIndex = RepeatCheck(clipIndex, clipArray.Length);
        audioBase.PlayOneShot(clipArray[clipIndex]);
    }
}
