using UnityEngine;
using UnityEngine.SceneManagement;

namespace Echosystem.Resonance.Game
{
    public class GameStateMachine : MonoBehaviour
    {
        [SerializeField] private string _orpheusSceneName;
        [SerializeField] private string _storySceneName;
        [SerializeField] private string _loadingSceneName;
        [SerializeField] private string _loadingOceanFloorSceneName;
        [SerializeField] private string _caveSceneName;
        [SerializeField] private string _introSceneName;


        public bool MenuOpen;

        public static GameStateMachine Instance;

        [HideInInspector] public Gamestate CurrentGameState;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

        private void Update()
        {
            if (SceneManager.GetActiveScene().name == _orpheusSceneName)
            {
                CurrentGameState = Gamestate.Orpheus;
            }
            else if (SceneManager.GetActiveScene().name == _loadingSceneName)
            {
                CurrentGameState = Gamestate.Loading;
            }
            else if (SceneManager.GetActiveScene().name == _loadingOceanFloorSceneName)
            {
                CurrentGameState = Gamestate.LoadingOceanFloor;
            }
            else if (SceneManager.GetActiveScene().name == _caveSceneName)
            {
                CurrentGameState = Gamestate.Cave;
            }
            else if (SceneManager.GetActiveScene().name == _introSceneName)
            {
                CurrentGameState = Gamestate.Intro;
            }
            else
            {
                CurrentGameState = Gamestate.Story;
            }
        }


        public enum Gamestate
        {
            Orpheus,
            Story,
            Loading,
            LoadingOceanFloor,
            Cave, 
            Intro
        }
    }
}