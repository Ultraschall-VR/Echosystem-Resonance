using Echosystem.Resonance.Prototyping;
using UnityEngine;

namespace Echosystem.Resonance.Game
{
    public class LoadScene : MonoBehaviour
    {
        [SerializeField] private int _levelIndex;
        
        private void Start()
        {
            FindObjectOfType<LevelLoader>().LoadLevel(_levelIndex);
        }
    }
}