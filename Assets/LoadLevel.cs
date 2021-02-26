using Echosystem.Resonance.Prototyping;
using UnityEngine;

public class LoadLevel : MonoBehaviour
{
    private void Start()
    {
        FindObjectOfType<LevelLoader>().LoadLevel(0);
    }
}
