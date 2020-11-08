using UnityEngine;

public class LoadScene : MonoBehaviour
{
    [SerializeField] private SceneLoader.Scene _scene;
    [SerializeField] private SceneLoader.Scene _loadingScene;
    [SerializeField] private float _loadDuration;
    private SceneLoader _sceneLoader;

    private void OnEnable()
    {
        _sceneLoader = FindObjectOfType<SceneLoader>();
        
        _sceneLoader.LoadScene(_scene, _loadingScene, _loadDuration);
    }
}
