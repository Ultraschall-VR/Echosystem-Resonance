using UnityEngine;

namespace Echosystem.Resonance.Game
{
    public class LoadScene : MonoBehaviour
    {
        [SerializeField] private int _levelIndex;
        
        private void Start()
        {
            if (EchosystemSceneManager.PauseMenu != null)
            {
                if (EchosystemSceneManager.PauseMenu.Active)
                {
                    EchosystemSceneManager.PauseMenu.Toggle(); 
                }
            }
            
            EchosystemSceneManager.LevelLoader.LoadLevel(_levelIndex);
        }
    }
}