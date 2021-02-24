using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Echosystem.Resonance.Prototyping
{
    public class LevelLoader : MonoBehaviour
    {
        public Animator transition;
        public float transitionTime;
        [SerializeField] private GameObject _pauseManager;

        public void LoadLevel(int sceneNumber)
        {
            Debug.Log("sceneBuildIndex to load:" + sceneNumber);
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