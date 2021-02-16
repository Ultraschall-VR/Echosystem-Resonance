using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime;
    public AudioMixer audioMixer;

    public void LoadLevel(int sceneNumber)
    {
        Debug.Log("sceneBuildIndex to load:" + sceneNumber);
        StartCoroutine(LoadLevelTransition(sceneNumber));
      //  StartCoroutine(FadeMixerGroupFromBack.StartFadeFrom(audioMixer, "masterVol", transitionTime, 1, 0));
      //  StartCoroutine(FadeMixerGroupFrom.StartFadeFrom(audioMixer, "masterVol", transitionTime, 0, 1, 0));
    }

    IEnumerator LoadLevelTransition(int sceneNumber)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(sceneNumber);
    }
}
