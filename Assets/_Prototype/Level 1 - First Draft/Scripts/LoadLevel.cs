using Echosystem.Resonance.Prototyping;
using UnityEngine;

public class LoadLevel : MonoBehaviour
{
    [SerializeField] private int _levelIndex;
    [SerializeField] private int _currentLevel;

    private void Start()
    {
        if (_currentLevel != null)
        {
            if (PlayerPrefs.GetInt("levelUnlocked") == _currentLevel)
            {
                PlayerPrefs.SetInt("levelUnlocked", _currentLevel++);
            }  
        }
       

        FindObjectOfType<LevelLoader>().LoadLevel(_levelIndex);
    }
}