using System;
using System.Collections;
using Echosystem.Resonance.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Echosystem.Resonance.Game
{
    public class SceneLoader : MonoBehaviour
    {
        public static SceneLoader Instance;
    
    
        [SerializeField] private TransitionManager _transitionManager;
        [SerializeField] private string _loadingSceneName;
        [SerializeField] private string _orpheusSceneName;
        [SerializeField] private string _storySceneName;
        [SerializeField] private string _playgroundSceneName;
        [SerializeField] private string _caveSceneName;
        [SerializeField] private string _oceanFloorLoadingSceneName;
        [SerializeField] private string _introSceneName;

        [HideInInspector] public bool LoadFirstScene;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
        
        private void Start()
        {
            if (LoadFirstScene)
                LoadScene(Scene.Orpheus, Scene.Loading, 10f);
        }

        public void LoadScene(Scene scene, Scene loaderScene, float loaderSceneDuration)
        {
            string sceneToLoad = null;
            string loadingSceneToLoad = null;

            switch (loaderScene)
            {
                case Scene.OceanFloorLoading:

                    loadingSceneToLoad = _oceanFloorLoadingSceneName;
                    break;

                case Scene.Loading:

                    loadingSceneToLoad = _loadingSceneName;
                    break;
            }

            switch (scene)
            {
                case Scene.Loading:

                    sceneToLoad = _loadingSceneName;
                    break;

                case Scene.Orpheus:

                    sceneToLoad = _orpheusSceneName;
                    break;

                case Scene.Story:

                    sceneToLoad = _storySceneName;
                    break;

                case Scene.Playground:

                    sceneToLoad = _playgroundSceneName;
                    break;

                case Scene.Cave:

                    sceneToLoad = _caveSceneName;
                    break;
                
                case Scene.Intro:

                    sceneToLoad = _introSceneName;
                    break;
            }

            if (sceneToLoad == null)
            {
                return;
            }

            StartCoroutine(WaitForTransition(sceneToLoad, loadingSceneToLoad, loaderSceneDuration));
        }

        private void UnloadScene(string scene)
        {
            SceneManager.UnloadSceneAsync(scene);
        }

        private IEnumerator WaitForTransition(string scene, string loaderScene, float duration)
        {
            _transitionManager.FadeOut(Color.black);
            yield return new WaitForSeconds(_transitionManager.CurrentAnimationLength);

            var loadingScene = SceneManager.LoadSceneAsync(loaderScene);

            _transitionManager.FadeIn(Color.black);

            UnloadScene(SceneManager.GetActiveScene().name);

            yield return new WaitForSeconds(duration);

            _transitionManager.FadeOut(Color.black);

            yield return new WaitForSeconds(_transitionManager.CurrentAnimationLength);

            var targetScene = SceneManager.LoadSceneAsync(scene);

            while (!targetScene.isDone)
            {
                yield return null;
            }

            _transitionManager.FadeIn(Color.black);
        }

        public enum Scene
        {
            Orpheus,
            Story,
            Loading,
            OceanFloorLoading,
            Playground,
            Cave, 
            Intro
        }
    }
}