using Echosystem.Resonance.Prototyping;
using UnityEngine;

public class LoadLevel : MonoBehaviour
{
    [SerializeField] private int _levelIndex;

    private void Start()
    {
        FindObjectOfType<LevelLoader>().LoadLevel(_levelIndex);
    }
}
