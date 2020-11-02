using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateMachine : MonoBehaviour
{
    [SerializeField] private SceneAsset _menuScene;
    [SerializeField] private SceneAsset _playgroundScene;
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
        if (SceneManager.GetActiveScene().name == _menuScene.name)
        {
            CurrentGameState = Gamestate.MainMenu;
        } 
        else if (SceneManager.GetActiveScene().name == _playgroundScene.name)
        {
            CurrentGameState = Gamestate.Playground;
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
        MainMenu,
        Playground,
        Story,
        Loading
    }
}
