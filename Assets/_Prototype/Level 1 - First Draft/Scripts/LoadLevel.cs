using Echosystem.Resonance.Prototyping;
using UnityEngine;

public class LoadLevel : MonoBehaviour
{
    [SerializeField] private int _levelIndex;
    [SerializeField] private int _levelCount;

    private void Start()
    {
        int currentLevel = PlayerPrefs.GetInt("levelUnlocked");

        Debug.Log(currentLevel);

        if (currentLevel == _levelCount)
        {
            Debug.Log("Ja MOIn");
            PlayerPrefs.SetInt("levelUnlocked", currentLevel+1);
        }
        
        FindObjectOfType<LevelLoader>().LoadLevel(_levelIndex);
    }
}