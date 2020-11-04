using UnityEngine;

public class LoadScene : MonoBehaviour
{
    [SerializeField] private SceneLoader.Scene _scene;
    private SceneLoader _sceneLoader;

    private void OnEnable()
    {
        _sceneLoader = FindObjectOfType<SceneLoader>();
        
        _sceneLoader.LoadScene(_scene);
    }
}
