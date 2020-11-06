using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private TransitionManager _transitionManager;
    [SerializeField] private string _loadingSceneName;
    [SerializeField] private string _orpheusSceneName;
    [SerializeField] private string _storySceneName;
    [SerializeField] private string _playgroundSceneName;
    [SerializeField] private string _caveSceneName;

    [HideInInspector] public bool LoadFirstScene;
    
    private void Start()
    {
        if(LoadFirstScene)
            LoadScene(Scene.Orpheus);
    }

    public void LoadScene(Scene scene)
    {
        string sceneToLoad = null;
        
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
        }

        if (sceneToLoad == null)
        {
            return; 
        }
        
        StartCoroutine(WaitForTransition(sceneToLoad));
    }
    
    private void UnloadScene(string scene)
    {
        SceneManager.UnloadSceneAsync(scene);
    }

    private IEnumerator WaitForTransition(string scene)
    {
        _transitionManager.FadeOut(Color.black);
        yield return new WaitForSeconds(_transitionManager.CurrentAnimationLength);

        var loadingScene = SceneManager.LoadSceneAsync(_loadingSceneName);
        
        _transitionManager.FadeIn(Color.black);

        UnloadScene(SceneManager.GetActiveScene().name);

        yield return new WaitForSeconds(10f);
        
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
        Playground,
        Cave
    }
}