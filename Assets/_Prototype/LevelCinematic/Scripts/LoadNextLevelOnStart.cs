using Echosystem.Resonance.Prototyping;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextLevelOnStart : MonoBehaviour
{
    [SerializeField] private bool _startSpecific;
    [SerializeField] private int _levelIndex;

    private void Start()
    {
        if (_startSpecific)
            FindObjectOfType<LevelLoader>().LoadLevel(_levelIndex);
        if (!_startSpecific)
        {
            int currentLevel = SceneManager.GetActiveScene().buildIndex;
            FindObjectOfType<LevelLoader>().LoadLevel(currentLevel + 1);
        }
    }
}