using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private TransitionManager _transitionManager;
    
    [SerializeField] private SceneAsset _loadingScene;
    [SerializeField] private SceneAsset _orpheusScene;
    [SerializeField] private SceneAsset _storyScene;
    [SerializeField] private SceneAsset _playgroundScene;
    
    public void LoadScene(Scene scene)
    {
        string sceneToLoad = null;
        
        switch (scene)
        {
            case Scene.Loading:

                sceneToLoad = _loadingScene.name;
                break;
            
            case Scene.Orpheus:

                sceneToLoad = _orpheusScene.name;
                break;
            
            case Scene.Story:

                sceneToLoad = _storyScene.name;
                break;
            
            case Scene.Playground:

                sceneToLoad = _playgroundScene.name;
                break;
        }

        if (sceneToLoad == null)
        {
            return; 
        }
        
        StartCoroutine(WaitForTransition(sceneToLoad));
    }

    private void Start()
    {
        LoadScene(Scene.Playground);
    }
    
    private void UnloadScene(string scene)
    {
        SceneManager.UnloadSceneAsync(scene);
    }

    private IEnumerator WaitForTransition(string scene)
    {
        _transitionManager.FadeOut(Color.black);
        yield return new WaitForSeconds(_transitionManager.CurrentAnimationLength);

        var loadingScene = SceneManager.LoadSceneAsync(_loadingScene.name);

        UnloadScene(SceneManager.GetActiveScene().name);
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
        Playground
    }
}