using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

namespace Echosystem.Resonance.Prototyping
{
    public class LevelLoader : MonoBehaviour
    {
        public Animator transition;
        public float transitionTime;
        [SerializeField] private GameObject _pauseManager;
        
        // AUDIO
        [SerializeField] private AudioMixer _audioMixer;
        [SerializeField] private string _exposedParameter;

        public void LoadLevel(int sceneNumber)
        {
            Debug.Log("sceneBuildIndex to load:" + sceneNumber);
            StartCoroutine(FadeMixerGroupFrom.StartFadeFrom(_audioMixer, _exposedParameter, transitionTime, 1,
                0, 0));
            StartCoroutine(LoadLevelTransition(sceneNumber));
        }

        IEnumerator LoadLevelTransition(int sceneNumber)
        {
            if (PauseMenuEchosystem.GameIsPaused == true)
            {
                _pauseManager.GetComponent<PauseMenuEchosystem>().ResumeToGame();
            }

            transition.SetTrigger("Start");

            yield return new WaitForSeconds(transitionTime);

            SceneManager.LoadScene(sceneNumber);
        }
    }
}