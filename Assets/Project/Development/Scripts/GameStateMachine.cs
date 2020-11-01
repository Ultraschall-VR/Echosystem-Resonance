using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateMachine : MonoBehaviour
{
    [SerializeField] private SceneAsset _menuScene;
    [SerializeField] private SceneAsset _playgroundScene;
    [SerializeField] private SceneAsset _storyScene;

    public bool MenuOpen;
    
    public static GameStateMachine Instance;

    public Gamestate CurrentGameState;
    
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
        else
        {
            CurrentGameState = Gamestate.Story;
        }
    }
    
    
    public enum Gamestate
    {
        MainMenu,
        Playground,
        Story
    }
}
