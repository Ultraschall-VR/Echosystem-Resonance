using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateMachine : MonoBehaviour
{
    [SerializeField] private SceneAsset _orpheusScene;
    [SerializeField] private SceneAsset _storyScene;
    [SerializeField] private SceneAsset _loadingScene;

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
        if (SceneManager.GetActiveScene().name == _orpheusScene.name)
        {
            CurrentGameState = Gamestate.Orpheus;
        }
        else if (SceneManager.GetActiveScene().name == _loadingScene.name)
        {
            CurrentGameState = Gamestate.Loading;
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
        Loading
    }
}
