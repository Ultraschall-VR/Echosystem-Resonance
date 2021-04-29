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
        [SerializeField] private GameObject _crossfade;

        private float _loadScreenDuration = 10f;

        private void Update()
        {
            if(Observer.Player == null)
                return;

            _crossfade.transform.position = Observer.PlayerHead.transform.position + Observer.PlayerHead.transform.forward;
            _crossfade.transform.rotation = Observer.PlayerHead.transform.rotation;
        }

        private void Start()
        {
            if (SceneManager.GetActiveScene().name == "Loading")
            {
                StartCoroutine("LoadScene");
            }

            EchosystemSceneManager.LevelLoader = this;
        }

        public void LoadLevel(int sceneNumber)
        {
            StartCoroutine(FadeMixerGroupFrom.StartFadeFrom(_audioMixer, _exposedParameter, transitionTime, 1,
                0, 0));
            StartCoroutine(LoadLevelTransition(sceneNumber));
            
            Observer.HudObjectives.Hide();
        }

        IEnumerator LoadLevelTransition(int sceneNumber)
        {
            transition.SetTrigger("Start");
            
            yield return new WaitForSeconds(transitionTime);
            
            var loadingScene = SceneManager.LoadSceneAsync("Loading");

            while (loadingScene.progress < 0.9f)
            {
                yield return null;
            }

            EchosystemSceneManager.SceneToLoad = sceneNumber;
        }

        private IEnumerator LoadScene()
        {
            yield return new WaitForSeconds(_loadScreenDuration);
            
            transition.SetTrigger("Start");
            
            yield return new WaitForSeconds(transitionTime);

            var targetScene = SceneManager.LoadSceneAsync(EchosystemSceneManager.SceneToLoad);
        }
    }
}